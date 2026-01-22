using System;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRage.Game.ModAPI;
using VRageMath;
using Sandbox.Game.Entities;
using Sandbox.Graphics.GUI;
using VRage.Utils;
using Sandbox.Game; // Dodano za MyVisualScriptLogicProvider
using Sandbox.Game.EntityComponents;

namespace Mamba.Blocks.Components
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_TerminalBlock), false, "StoreBlockAdmin")]
    public class MambaStoreBlockLogic : MyGameLogicComponent
    {
        private IMyTerminalBlock _block;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);
            _block = Entity as IMyTerminalBlock;

            NeedsUpdate |= MyEntityUpdateEnum.BEFORE_NEXT_FRAME | MyEntityUpdateEnum.EACH_10TH_FRAME;
        }

        public override void UpdateOnceBeforeFrame()
        {
            if (_block == null) return;

            // Popravljeno: SetEmissiveParts je najpouzdanija metoda
            _block.SetEmissiveParts("StoreScreen_01", Color.Cyan, 1.0f);
            _block.SetEmissiveParts("StoreScreen_02", Color.White, 1.0f);
        }

        public override void UpdateBeforeSimulation10()
        {
            if (MyAPIGateway.Gui.GetCurrentScreen == MyTerminalPageEnum.None)
            {
                if (IsPlayerLookingAtBlock())
                {
                    MyAPIGateway.Utilities.ShowNotification("Press [F] to open Admin Menu", 160, MyFontEnum.White);

                    if (MyAPIGateway.Input.IsNewGameControlPressed(MyStringId.GetOrCompute("USE")))
                    {
                        OpenMyCustomPopup();
                    }
                }
            }
        }

        private bool IsPlayerLookingAtBlock()
        {
            var player = MyAPIGateway.Session.Player;
            if (player?.Character == null) return false;

            double distSq = Vector3D.DistanceSquared(player.GetPosition(), _block.WorldMatrix.Translation);
            if (distSq > 16) return false; 

            // Popravljeno: GetHeadMatrix zahtijeva 4 bool parametra u ModAPI-ju
            // (includeY, includeX, forceHeadAnim, dummy)
            MatrixD headMatrix = player.Character.GetHeadMatrix(true, true, false, false);
            Vector3D headPos = headMatrix.Translation;
            Vector3D forward = headMatrix.Forward;
            
            LineD ray = new LineD(headPos, headPos + forward * 4);
            
            IHitInfo hit;
            if (MyAPIGateway.Physics.CastRay(ray.From, ray.To, out hit))
            {
                // Provjeravamo je li pogođen entitet naš blok ili netko od njegovih subpartova
                return hit.HitEntity == _block || hit.HitEntity.Parent == _block;
            }

            return false;
        }

        private void OpenMyCustomPopup()
        {
            // Ovdje ide tvoja klasa ekrana
            MyAPIGateway.Utilities.ShowNotification("Otvaram Admin Sučelje...", 2000, MyFontEnum.Green);
        }

        public override void Close()
        {
            _block = null;
        }
    }
}
