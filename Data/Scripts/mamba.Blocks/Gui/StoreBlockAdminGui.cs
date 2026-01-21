// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/StoreBlockAdminGui.cs
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces.Terminal;
using VRage.Game.ModAPI;
using VRage.Utils;
using System;

namespace mamba.Blocks.Gui
{
    public static class StoreBlockAdminGui
    {
        private static bool m_initialized = false;

        public static void Init()
        {
            if (m_initialized) return;

            try
            {
                AddTerminalButton("Buy Items (Cargo4Store)", "Mamba_BuyTab", b => ModCommunication.Log("[DEBUG mamba] Buy tab clicked"));
                AddTerminalButton("Sell Items", "Mamba_SellTab", b => ModCommunication.Log("[DEBUG mamba] Sell tab clicked"));
                AddTerminalButton("Sell Grids", "Mamba_SellGridsTab", b => ModCommunication.Log("[DEBUG mamba] Sell Grids clicked"));
                AddTerminalButton("Buy Grids (in dev)", "Mamba_BuyGridsTab", b =>
                {
                    MyAPIGateway.Utilities?.ShowMessage("mamba.Blocks", "Buy Grids is under development.");
                });
                AddTerminalButton("Administration", "Mamba_AdminTab", b => ModCommunication.Log("[DEBUG mamba] Admin tab clicked"));

                m_initialized = true;
                ModCommunication.Log("[DEBUG mamba] Terminal GUI initialized.");
            }
            catch (Exception e)
            {
                ModCommunication.Log("[ERROR mamba] GUI init failed: " + e.Message);
            }
        }

        private static void AddTerminalButton(string title, string id, Action<IMyTerminalBlock> action)
        {
            var button = MyAPIGateway.TerminalControls.CreateControl<IMyTerminalControlButton, IMyTerminalBlock>(id);
            button.Title = MyStringId.GetOrCompute(title);
            button.Tooltip = MyStringId.GetOrCompute(title);
            button.Enabled = b => b != null && b.IsFunctional;
            button.Visible = b => b != null && b.BlockDefinition.ToString().Contains("StoreBlockAdmin");
            button.Action = action;
            MyAPIGateway.TerminalControls.AddControl<IMyTerminalBlock>(button);
        }
    }
}
