// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Components/MambaStoreBlockLogic.cs
using System;
using System.Collections.Generic;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRageMath;
using mamba.Blocks;

namespace mamba.Blocks.Components
{
    // Custom logic for StoreBlockAdmin - auto-scan Cargo4Store and add offers
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_StoreBlock), false, "StoreBlockAdmin")]
    public class MambaStoreBlockLogic : MyGameLogicComponent
    {
        private IMyStoreBlock m_store;
        private IMyCubeGrid m_grid;
        private float m_scanTimer = 0;
        private float m_priceMultiplier = 1.0f; // Default 1x

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);

            m_store = Entity as IMyStoreBlock;
            if (m_store == null) return;

            m_grid = m_store.CubeGrid;
            ParseCustomData(); // Parse PriceMul from CustomData

            NeedsUpdate |= MyEntityUpdateEnum.EACH_100; // Update every 100 ticks (~1.6s)

            ModCommunication.Log("[DEBUG mamba] Cargo4Store logic attached to " + m_store.CustomName);
        }

        public override void UpdateBeforeSimulation()
        {
            base.UpdateBeforeSimulation();

            m_scanTimer += 1f;
            if (m_scanTimer < 3600f) return; // Scan every 60s (3600 ticks)

            m_scanTimer = 0f;
            ParseCustomData();
            ScanAndUpdateOffers();
        }

        private void ParseCustomData()
        {
            string data = m_store.CustomData;
            if (string.IsNullOrEmpty(data)) return;

            // Simple format: PriceMul:1.2
            if (data.Contains("PriceMul:"))
            {
                string mulStr = data.Substring(data.IndexOf("PriceMul:") + 8).Trim();
                float.TryParse(mulStr, out m_priceMultiplier);
                ModCommunication.Log("[DEBUG mamba] Parsed PriceMul: " + m_priceMultiplier + " from CustomData");
            }
        }

        private void ScanAndUpdateOffers()
        {
            // Implementation of Cargo4Store scan logic
            // This part is optional placeholder
        }

        public override void OnAddedToScene()
        {
            base.OnAddedToScene();

            var interactive = Entity as IMyStoreBlock;
            if (interactive != null)
            {
                interactive.Use += OnUse;
            }
        }

        private void OnUse(IMyEntity entity, IMyPlayer player)
        {
            if (m_store != null)
            {
                Gui.StoreBlockAdminFGuiHelper.Open(m_store);
            }
        }
    }
}
