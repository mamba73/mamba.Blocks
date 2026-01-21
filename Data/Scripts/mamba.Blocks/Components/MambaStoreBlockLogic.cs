// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Components/MambaStoreBlockLogic.cs
using System;
using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Utils;

namespace mamba.Blocks.Components
{
    [MyEntityComponentDescriptor(typeof(VRage.Game.ModAPI.IMyCubeBlock), false, "StoreBlockAdmin")]
    public class MambaStoreBlockLogic : MyGameLogicComponent
    {
        private IMyTerminalBlock m_block;
        private IMyCubeGrid m_grid;
        private float m_timer = 0f;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);

            m_block = Entity as IMyTerminalBlock;
            if (m_block == null) return;

            m_grid = m_block.CubeGrid;

            NeedsUpdate |= MyEntityUpdateEnum.EACH_100TH_FRAME; // mod-friendly

            ModCommunication.Log("[DEBUG mamba] Logic attached to block");
        }

        public override void UpdateBeforeSimulation()
        {
            base.UpdateBeforeSimulation();
            m_timer += 1f;
            if (m_timer < 600f) return; // ~10 sekundi
            m_timer = 0f;
            UpdateOffers();
        }

        private void UpdateOffers()
        {
            var blocks = new List<IMyTerminalBlock>();
            var system = MyAPIGateway.TerminalActionsHelper.GetTerminalSystemForGrid(m_grid);
            if (system == null) return;

            system.GetBlocks(blocks);
            foreach (var b in blocks)
            {
                if (b.CustomName != null && b.CustomName.Contains("Cargo4Store"))
                    ModCommunication.Log("[DEBUG mamba] Found Cargo4Store: " + b.CustomName);
            }
        }
    }
}
