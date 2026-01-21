// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Components/MambaStoreBlockLogic.cs

using System;
using System.Collections.Generic;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRageMath;
using VRage.Utils;
using mamba.Blocks;

namespace mamba.Blocks.Components
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_StoreBlock), false, "StoreBlockAdmin")]
    public class MambaStoreBlockLogic : MyGameLogicComponent
    {
        private IMyStoreBlock m_store;
        private IMyCubeGrid m_grid;
        private float m_scanTimer = 0;
        private float m_priceMultiplier = 1.0f;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            base.Init(objectBuilder);

            m_store = Entity as IMyStoreBlock;
            if (m_store == null) return;

            m_grid = m_store.CubeGrid;
            ParseCustomData();

            // Starija API verzija: EACH_10TH_FRAME
            NeedsUpdate |= MyEntityUpdateEnum.EACH_10TH_FRAME;
            ModCommunication.Log("[DEBUG mamba] StoreBlockAdmin logic attached to " + m_store.CustomName);
        }

        public override void UpdateBeforeSimulation()
        {
            base.UpdateBeforeSimulation();

            m_scanTimer += 1f;
            if (m_scanTimer < 3600f) return; // Scan svakih 60s

            m_scanTimer = 0f;
            ParseCustomData();
            ScanAndUpdateOffers();
        }

        private void ParseCustomData()
        {
            string data = m_store.CustomData;
            if (string.IsNullOrEmpty(data)) return;

            if (data.Contains("PriceMul:"))
            {
                string mulStr = data.Substring(data.IndexOf("PriceMul:") + 8).Trim();
                float.TryParse(mulStr, out m_priceMultiplier);
                ModCommunication.Log("[DEBUG mamba] Parsed PriceMul: " + m_priceMultiplier);
            }
        }

        private void ScanAndUpdateOffers()
        {
            // Ovdje samo placeholder logika
            ModCommunication.Log("[DEBUG mamba] ScanAndUpdateOffers executed");
        }

        /// <summary>
        /// Javna metoda koju GUI može pozvati direktno, bez reflectiona.
        /// </summary>
        public void UpdateCargo4StoreOffers()
        {
            ScanAndUpdateOffers();
        }
    }
}
