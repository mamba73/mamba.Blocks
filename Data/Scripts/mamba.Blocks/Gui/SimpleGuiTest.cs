// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/SimpleGuiTest.cs

using System;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces.Terminal;
using VRage.ModAPI;
using VRage.Utils;
using mamba.Blocks;

namespace mamba.Blocks.Gui
{
    public static class SimpleGuiTest
    {
        private const string SELL_BUTTON_ID = "Mamba_TestSellGrid";
        private const string BUY_BUTTON_ID = "Mamba_TestBuyGrid";

        private static bool m_initialized = false;

        public static void Init()
        {
            if (m_initialized) return;

            try
            {
                // Prvi gumb - Sell Your Grid TEST
                var sellButton = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyStoreBlock>(SELL_BUTTON_ID);

                sellButton.Title = MyStringId.GetOrCompute("Sell Your Grid TEST");
                sellButton.Tooltip = MyStringId.GetOrCompute("Prodaja gridova - test");

                sellButton.Visible = delegate (IMyTerminalBlock block)
                {
                    if (block == null) return false;

                    string defString = block.BlockDefinition.ToString();

                    bool isAdmin = defString.Contains("StoreBlockAdmin");

                    ModCommunication.Log("[DEBUG mamba] Sell Visible - Block: " + (block.CustomName ?? "No name") +
                                         " | BlockDefinition.ToString(): '" + defString + "' | IsAdmin: " + isAdmin);

                    return isAdmin;
                };

                sellButton.Action = delegate (IMyTerminalBlock block)
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Sell Your Grid - UNDER DEVELOPMENT");
                    ModCommunication.Log("[DEBUG mamba] Sell clicked on: " + (block.CustomName ?? block.DisplayNameText));
                };

                sellButton.Enabled = delegate (IMyTerminalBlock block) { return block.IsFunctional; };

                MyAPIGateway.TerminalControls.AddControl<IMyStoreBlock>(sellButton);

                // Drugi gumb - Grid Purchase TEST
                var buyButton = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyStoreBlock>(BUY_BUTTON_ID);

                buyButton.Title = MyStringId.GetOrCompute("Grid Purchase TEST");
                buyButton.Tooltip = MyStringId.GetOrCompute("Kupnja gridova - test");

                buyButton.Visible = delegate (IMyTerminalBlock block)
                {
                    if (block == null) return false;

                    string defString = block.BlockDefinition.ToString();

                    bool isAdmin = defString.Contains("StoreBlockAdmin");

                    ModCommunication.Log("[DEBUG mamba] Buy Visible - Block: " + (block.CustomName ?? "No name") +
                                         " | BlockDefinition.ToString(): '" + defString + "' | IsAdmin: " + isAdmin);

                    return isAdmin;
                };

                buyButton.Action = delegate (IMyTerminalBlock block)
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Grid Purchase - IN DEVELOPMENT");
                    ModCommunication.Log("[DEBUG mamba] Buy clicked on: " + (block.CustomName ?? block.DisplayNameText));
                };

                buyButton.Enabled = delegate (IMyTerminalBlock block) { return block.IsFunctional; };

                MyAPIGateway.TerminalControls.AddControl<IMyStoreBlock>(buyButton);

                m_initialized = true;
                ModCommunication.Log("[DEBUG mamba] Oba gumba dodana u K tab.");
                MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Oba gumba dodana - provjeri K tab na Admin bloku");
            }
            catch (Exception e)
            {
                ModCommunication.Log("[DEBUG mamba] GUI FAILED: " + e.Message, "ERROR");
                MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "GUI ERROR: " + e.Message);
            }
        }
    }
}