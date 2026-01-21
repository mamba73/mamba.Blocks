// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/StoreBlockAdminFGui.cs
using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRage.Utils;
using VRage.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ObjectBuilders.Definitions;
using System.Collections.Generic;

namespace mamba.Blocks.Gui
{
    public static class StoreBlockAdminFGui
    {
        public static void Open(IMyTerminalBlock block)
        {
            if (block == null) return;

            // Check if block is our custom StoreBlockAdmin
            if (block.BlockDefinition.SubtypeId != "StoreBlockAdmin") return;

            // Vanilla logic: anyone can use = true
            bool canUse = true;

            // Only show GUI if player has access or AnyoneCanUse
            if (!canUse) return;

            // Tabs
            List<string> tabs = new List<string> { "Buy", "Sell", "Sell Grids", "Buy Grids", "Administration" };

            foreach (string tab in tabs)
            {
                MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", $"Tab: {tab}");
            }

            // Example: call method to populate Buy tab (Cargo4Store sync)
            UpdateBuyTab(block);

            // Example: call Administration tab
            ShowAdminTab(block);
        }

        private static void UpdateBuyTab(IMyTerminalBlock block)
        {
            ModCommunication.Log("[DEBUG mamba] Buy tab updated (Cargo4Store) for " + (block.CustomName ?? "Unnamed"));
        }

        private static void ShowAdminTab(IMyTerminalBlock block)
        {
            ModCommunication.Log("[DEBUG mamba] Administration tab with default prices for " + (block.CustomName ?? "Unnamed"));
        }
    }
}
