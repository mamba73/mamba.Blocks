// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Components/MambaStoreBlockLogic.cs
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.Utils;
using VRage.ObjectBuilders;
using mamba.Blocks.Gui;
using VRage.Game;
using Sandbox.Game; // Added this for MyControlsSpace

namespace mamba.Blocks.Components
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_TerminalBlock), false, "StoreBlockAdmin")]
    public class MambaStoreBlockLogic : MyGameLogicComponent
    {
        private IMyTerminalBlock _block;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);
            _block = Entity as IMyTerminalBlock;
            
            // Interaction requires frequent updates
            NeedsUpdate |= MyEntityUpdateEnum.EACH_FRAME;
        }

        public override void UpdateBeforeSimulation()
        {
            if (_block == null || MyAPIGateway.Utilities.IsDedicated) return;

            // MyControlsSpace.USE now recognized via Sandbox.Game
            if (MyAPIGateway.Input.IsNewGameControlPressed(MyControlsSpace.USE))
            {
                if (IsPlayerLookingAtBlock())
                {
                    var player = MyAPIGateway.Session.Player;
                    var relation = _block.GetUserRelationToOwner(player.IdentityId);
                    
                    if (relation == MyRelationsBetweenPlayerAndBlock.Owner || MyAPIGateway.Session.IsUserAdmin(player.SteamUserId))
                    {
                        SimpleGuiTest.OpenScreen(_block.CustomName ?? _block.DisplayNameText);
                    }
                    else
                    {
                        MyAPIGateway.Utilities.ShowNotification("Access Denied: Not the owner!", 2000, "Red");
                    }
                }
            }
        }

        private bool IsPlayerLookingAtBlock()
        {
            var player = MyAPIGateway.Session?.Player;
            if (player?.Character == null) return false;

            IHitInfo hit;
            var headMatrix = player.Character.GetHeadMatrix(true);
            var start = headMatrix.Translation;
            var end = start + headMatrix.Forward * 2.5f;

            if (MyAPIGateway.Physics.CastRay(start, end, out hit))
            {
                var hitEntity = hit.HitEntity as IMyTerminalBlock;
                return hitEntity != null && hitEntity.EntityId == _block.EntityId;
            }
            return false;
        }
    }
}
