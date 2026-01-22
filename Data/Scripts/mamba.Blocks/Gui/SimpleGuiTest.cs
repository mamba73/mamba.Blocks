// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/SimpleGuiTest.cs
using System;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces.Terminal;
using VRage.Utils;
using VRage.Game;

namespace mamba.Blocks.Gui
{
    public static class SimpleGuiTest
    {
        private static bool m_initialized = false;

        public static void Init()
        {
            if (m_initialized) return;

            var adminBtn = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyTerminalBlock>("Mamba_AdminOpen");
            adminBtn.Title = MyStringId.GetOrCompute("Open Admin Store");
            adminBtn.Tooltip = MyStringId.GetOrCompute("Opens the Mamba Store Interface");
            adminBtn.Action = block => OpenScreen(block.CustomName ?? block.DisplayNameText);
            
            adminBtn.Visible = block => block.BlockDefinition.SubtypeId == "StoreBlockAdmin";
            
            MyAPIGateway.TerminalControls.AddControl<IMyTerminalBlock>(adminBtn);

            m_initialized = true;
        }

        public static void OpenScreen(string blockName)
        {
            // Fixed call without named parameters to avoid API mismatch
            MyAPIGateway.Utilities.ShowMissionScreen(
                "MAMBA ADMIN STORE",
                blockName,
                "INTERFACE LOG:",
                "Access granted.\nAdmin mode active.\n\nUse Terminal [K] for specific Buy/Sell actions.",
                null,
                "CLOSE"
            );
        }

        public static void Unload() => m_initialized = false;
    }
}
