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
        private const string BUY_BUTTON_ID = "Mamba_BuyTab";
        private const string SELL_BUTTON_ID = "Mamba_SellTab";
        private const string SELL_GRIDS_BUTTON_ID = "Mamba_SellGridsTab";
        private const string BUY_GRIDS_BUTTON_ID = "Mamba_BuyGridsTab";
        private const string ADMIN_BUTTON_ID = "Mamba_AdminTab";

        private static bool m_initialized = false;

        public static void Init()
        {
            if (m_initialized) return;

            try
            {
                AddTerminalButton("Buy", BUY_BUTTON_ID, block =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Buy Tab Selected!");
                });

                AddTerminalButton("Sell", SELL_BUTTON_ID, block =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Sell Tab Selected!");
                });

                AddTerminalButton("Sell Grids", SELL_GRIDS_BUTTON_ID, block =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Sell Grids Tab Selected!");
                });

                AddTerminalButton("Buy Grids (in dev)", BUY_GRIDS_BUTTON_ID, block =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Buy Grids is under development.");
                });

                AddTerminalButton("Administration", ADMIN_BUTTON_ID, block =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Administration Tab Selected!");
                });

                m_initialized = true;
                ModCommunication.Log("[DEBUG mamba] SimpleGuiTest initialized - 5 buttons added to K tab");
            }
            catch (Exception e)
            {
                ModCommunication.Log("[DEBUG mamba] GUI init failed: " + e.Message, "ERROR");
                MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "GUI ERROR: " + e.Message);
            }
        }

        private static void AddTerminalButton(string title, string id, Action<IMyTerminalBlock> action)
        {
            var button = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyTerminalBlock>(id);
            button.Title = MyStringId.GetOrCompute(title);
            button.Tooltip = MyStringId.GetOrCompute(title);
            button.Visible = block =>
            {
                if (block == null) return false;
                return block.BlockDefinition.TypeId.ToString() == "MyObjectBuilder_StoreBlock" &&
                       block.BlockDefinition.SubtypeId.ToString() == "StoreBlockAdmin";
            };
            button.Action = action;
            button.Enabled = block => block != null && block.IsFunctional;
            MyAPIGateway.TerminalControls.AddControl<IMyTerminalBlock>(button);
        }
    }
}