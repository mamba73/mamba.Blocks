// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Components/MambaStoreBlockLogic.cs

using System;
using System.Collections.Generic;
using System.Reflection;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.Utils;
using VRageMath;
using mamba.Blocks;

namespace mamba.Blocks.Components
{
    // Custom logic za StoreBlockAdmin - auto-scan Cargo4Store i add offers
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
            ParseCustomData(); // Parse PriceMul iz CustomData

            // NeedsUpdate |= MyEntityUpdateEnum.EACH_60; // Update svakih 60 tickova (~1s)
            NeedsUpdate |= MyEntityUpdateEnum.EACH_10; // Update svakih 10 tickova (~1s)

            ModCommunication.Log("[DEBUG mamba] Cargo4Store logic attached to " + m_store.CustomName);
        }

        public override void UpdateBeforeSimulation()
        {
            base.UpdateBeforeSimulation();

            m_scanTimer += 1f;
            if (m_scanTimer < 3600f) return; // Scan svakih 60s (3600 tickova)

            m_scanTimer = 0f;
            ParseCustomData(); // Re-parse CustomData ako admin promijeni
            ScanAndUpdateOffers();
        }

        private void ParseCustomData()
        {
            string data = m_store.CustomData;
            if (string.IsNullOrEmpty(data)) return;

            // Jednostavan format: PriceMul:1.2
            if (data.Contains("PriceMul:"))
            {
                string mulStr = data.Substring(data.IndexOf("PriceMul:") + 8).Trim();
                float.TryParse(mulStr, out m_priceMultiplier);
                ModCommunication.Log("[DEBUG mamba] Parsed PriceMul: " + m_priceMultiplier + " from CustomData");
            }
        }

        private void ScanAndUpdateOffers()
        {
            List<IMySlimBlock> blocks = new List<IMySlimBlock>();
            m_grid.GetBlocks(blocks, b => b.FatBlock is IMyCargoContainer);

            Dictionary<MyDefinitionId, MyFixedPoint> totalItems = new Dictionary<MyDefinitionId, MyFixedPoint>();

            foreach (var slim in blocks)
            {
                var cargo = slim.FatBlock as IMyCargoContainer;
                if (cargo == null || !cargo.CustomName.Contains("Cargo4Store")) continue;

                var inventory = cargo.GetInventory(0);
                var items = new List<MyInventoryItem>();
                inventory.GetItems(items);

                foreach (var item in items)
                {
                    MyDefinitionId itemId = item.Type;
                    if (!totalItems.ContainsKey(itemId))
                        totalItems[itemId] = 0;
                    totalItems[itemId] += item.Amount;
                }
            }

            if (totalItems.Count == 0)
            {
                ModCommunication.Log("[DEBUG mamba] No items in Cargo4Store - clearing offers");
                ClearAllOffers();
                return;
            }

            UpdateStoreOffers(totalItems);
            ModCommunication.Log("[DEBUG mamba] Updated " + totalItems.Count + " offers from Cargo4Store");
        }

        private void UpdateStoreOffers(Dictionary<MyDefinitionId, MyFixedPoint> items)
        {
            // Reflection na internal StoreItems za add/update offers
            var storeItemsField = m_store.GetType().GetField("m_storeItems", BindingFlags.NonPublic | BindingFlags.Instance);
            if (storeItemsField == null) return;

            var storeItems = (List<MyStoreItem>)storeItemsField.GetValue(m_store);
            if (storeItems == null) return;

            // Clear old Cargo4Store offers (oznaka "Cargo4Store")
            storeItems.RemoveAll(item => item.Name.Contains("Cargo4Store"));

            foreach (var kvp in items)
            {
                var itemId = kvp.Key;
                var amount = kvp.Value;

                // Dobij vanilla cijenu ili default
                float basePrice = GetVanillaPrice(itemId);
                float sellPrice = basePrice * m_priceMultiplier;

                // Kreiraj novi offer
                var newOffer = new MyStoreItem
                {
                    ItemId = itemId,
                    AvailableAmount = amount,
                    BuyPrice = 0, // Ne kupuj, samo prodaja
                    SellPrice = (MyFixedPoint)sellPrice,
                    Name = "Cargo4Store: " + itemId.SubtypeName // Oznaka za lakše brisanje
                };

                storeItems.Add(newOffer);
            }

            // Refresh GUI
            m_store.RequestNewItemList();
        }

        private void ClearAllOffers()
        {
            var storeItemsField = m_store.GetType().GetField("m_storeItems", BindingFlags.NonPublic | BindingFlags.Instance);
            if (storeItemsField == null) return;

            var storeItems = (List<MyStoreItem>)storeItemsField.GetValue(m_store);
            if (storeItems == null) return;

            storeItems.Clear();
            m_store.RequestNewItemList();
        }

        private float GetVanillaPrice(MyDefinitionId itemId)
        {
            // Placeholder - vanilla cijene (možeš proširiti dictionary ili koristiti API)
            return 10f; // Default 10 Space Money po item
        }

        protected override void UnloadData()
        {
            base.UnloadData();
        }
    }
}