// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Components/MambaStoreBlockLogic.cs
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using Sandbox.Game.Entities;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.Utils;

namespace mamba.Blocks.Components
{
    public class MambaStoreBlockLogic : MyGameLogicComponent
    {
        private IMyTerminalBlock _block;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);
            _block = Entity as IMyTerminalBlock;
            if (_block == null) return;

            NeedsUpdate |= MyEntityUpdateEnum.EACH_10TH_FRAME;
        }

        public override void UpdateBeforeSimulation()
        {
            base.UpdateBeforeSimulation();
            if (_block == null) return;

            // Check if any player is interacting (Use / F)
            var slim = _block.CubeGrid.GetCubeBlock(_block.Position);
            if (slim == null) return;

            // Example: trigger GUI open manually via server call or other detection
            // Here placeholder, in practice use terminal action / custom use
            // if (player presses F and is in range and block subtype = StoreBlockAdmin)
            //     mamba.Blocks.Gui.StoreBlockAdminFGui.Open(_block);
        }
    }
}
