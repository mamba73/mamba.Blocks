// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/SimpleGuiTest.cs

using System;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces.Terminal;
using VRage.ModAPI;
using VRage.Utils;
using mamba.Blocks; // za ModCommunication

namespace mamba.Blocks.Gui
{
    public static class SimpleGuiTest
    {
        private const string TEST_BUTTON_ID = "Mamba_TestSellGrid";

        private static bool m_initialized = false;

        public static void Init()
        {
            if (m_initialized)
                return;

            try
            {
                var button = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyStoreBlock>(TEST_BUTTON_ID);

                button.Title = MyStringId.GetOrCompute("Sell Your Grid TEST");
                button.Tooltip = MyStringId.GetOrCompute("Test button - samo na Admin Store");

                // Vidljivost - ispravljena provjera (direktno BlockDefinition.SubtypeName)
                button.Visible = delegate (IMyTerminalBlock block)
                {
                    if (block == null)
                    {
                        ModCommunication.Log("[DEBUG mamba] Visible: block is null");
                        return false;
                    }

                    string subtype = block.BlockDefinition.SubtypeName;

                    bool isAdmin = !string.IsNullOrEmpty(subtype) && subtype == "StoreBlockAdmin";

                    ModCommunication.Log("[DEBUG mamba] Visible check - Block: " + (block.CustomName ?? "No name") +
                                         " | SubtypeName: '" + (subtype ?? "null") + "' | IsAdmin: " + isAdmin);

                    return isAdmin;
                };

                button.Action = delegate (IMyTerminalBlock block)
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "TEST BUTTON CLICKED! - Sell Your Grid");
                    ModCommunication.Log("[DEBUG mamba] Test button clicked on: " + (block.CustomName ?? block.DisplayNameText));
                };

                button.Enabled = delegate (IMyTerminalBlock block)
                {
                    return block.IsFunctional;
                };

                MyAPIGateway.TerminalControls.AddControl<IMyStoreBlock>(button);

                m_initialized = true;
                ModCommunication.Log("[DEBUG mamba] Test button added to Control Panel (K tab).");
                MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Test button registered - vidi u K tab-u");
            }
            catch (Exception e)
            {
                ModCommunication.Log("[DEBUG mamba] GUI test FAILED: " + e.Message, "ERROR");
                MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "GUI ERROR: " + e.Message);
            }
        }
    }
}