// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/SimpleGuiTest.cs

using System;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces.Terminal;
using VRage.ModAPI;
using VRage.Utils;

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
                CreateTestButton();

                m_initialized = true;
                ModCommunication.Log("Simple GUI test initialized - test button registered.");
                if (MyAPIGateway.Utilities != null)
                {
                    MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "GUI test: button registered");
                }
            }
            catch (Exception e)
            {
                ModCommunication.Log("GUI test FAILED: " + e.Message, "ERROR");
                if (MyAPIGateway.Utilities != null)
                {
                    MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "GUI ERROR: " + e.Message);
                }
            }
        }

        private static void CreateTestButton()
        {
            var button = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyStoreBlock>(TEST_BUTTON_ID);

            button.Title = MyStringId.GetOrCompute("Sell Your Grid TEST");
            button.Tooltip = MyStringId.GetOrCompute("Test button - should appear only on admin store");

            // Najjednostavnija vidljivost: pojavljuje se na SVIM Store blockovima
            // (da vidimo radi li uopće; kasnije ćemo filtrirati)
            button.Visible = (IMyTerminalBlock block) => true;  // privremeno da testiramo

            // Alternativa ako želiš pokušati filtrirati već sad (možeš isprobati obje)
            // button.Visible = (block) => block != null && block.BlockDefinition != null && block.BlockDefinition.Id.SubtypeName == "StoreBlockAdmin";

            button.Action = (IMyTerminalBlock block) =>
            {
                if (MyAPIGateway.Session != null && MyAPIGateway.Session.LocalHumanPlayer != null)
                {
                    MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "TEST BUTTON CLICKED! - Sell Your Grid");
                    ModCommunication.Log("Test button clicked on block: " + (block.CustomName ?? block.DisplayNameText));
                }
            };

            button.Enabled = (block) => block.IsFunctional;

            // Dodajemo kontrolu
            MyAPIGateway.TerminalControls.AddControl<IMyStoreBlock>(button);

            ModCommunication.Log("Test button created and added.");
        }
    }
}