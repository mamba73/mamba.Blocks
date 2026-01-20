using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.Utils;

namespace mamba.Blocks.Components
{
    [MyEntityComponentDescriptor(typeof(VRage.Game.MyObjectBuilder_CubeBlock), false)]
    public class MambaDebugAllBlocks : MyGameLogicComponent
    {
        private IMyCubeBlock _block;
        private bool _done;

        public override void Init(VRage.ObjectBuilders.MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);

            _block = Entity as IMyCubeBlock;
            if (_block == null) return;

            NeedsUpdate = MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
        }

        public override void UpdateOnceBeforeFrame()
        {
            if (_done) return;
            _done = true;

            MyAPIGateway.Utilities.ShowMessage(
                "MAMBA",
                "Attached to: " + _block.BlockDefinition.SubtypeName
            );
        }
    }
}
