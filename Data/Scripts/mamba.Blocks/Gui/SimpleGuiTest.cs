// File: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/SimpleGuiTest.cs
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces.Terminal;
using VRage.Game.ModAPI;
using VRage.Game;
using VRage.Utils;
using System;

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
                // Gumb: Sell Your Grid
                var sellButton = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyStoreBlock>(SELL_BUTTON_ID);
                sellButton.Title = MyStringId.GetOrCompute("Sell Your Grid");
                sellButton.Tooltip = MyStringId.GetOrCompute("Sell your grids - test");
                sellButton.Visible = block =>
                {
                    if (block == null) return false;
                    return block.BlockDefinition.TypeId.ToString() == "MyObjectBuilder_StoreBlock" &&
                           block.BlockDefinition.SubtypeId.ToString() == "StoreBlockAdmin";
                };
                sellButton.Action = block =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Sell Your Grid clicked!");
                    ModCommunication.Log("[DEBUG mamba] Sell clicked on: " + (block.CustomName ?? block.DisplayNameText));
                };
                sellButton.Enabled = block => block != null && block.IsFunctional;
                MyAPIGateway.TerminalControls.AddControl<IMyStoreBlock>(sellButton);

                // Gumb: Buy Grid
                var buyButton = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyStoreBlock>(BUY_BUTTON_ID);
                buyButton.Title = MyStringId.GetOrCompute("Buy Grid");
                buyButton.Tooltip = MyStringId.GetOrCompute("Buy grids - test");
                buyButton.Visible = block =>
                {
                    if (block == null) return false;
                    return block.BlockDefinition.TypeId.ToString() == "MyObjectBuilder_StoreBlock" &&
                           block.BlockDefinition.SubtypeId.ToString() == "StoreBlockAdmin";
                };
                buyButton.Action = block =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Buy Grid clicked!");
                    ModCommunication.Log("[DEBUG mamba] Buy clicked on: " + (block.CustomName ?? block.DisplayNameText));
                };
                buyButton.Enabled = block => block != null && block.IsFunctional;
                MyAPIGateway.TerminalControls.AddControl<IMyStoreBlock>(buyButton);

                m_initialized = true;
                ModCommunication.Log("[DEBUG mamba] SimpleGuiTest initialized - buttons added to K tab");
            }
            catch (Exception e)
            {
                ModCommunication.Log("[DEBUG mamba] GUI init failed: " + e.Message, "ERROR");
                MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "GUI ERROR: " + e.Message);
            }
        }
    }
}