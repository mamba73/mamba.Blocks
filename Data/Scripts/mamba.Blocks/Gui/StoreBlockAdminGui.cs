// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/StoreBlockAdminFGui.cs
using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRage.Utils;
using VRage.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ObjectBuilders.Definitions;
using System.Collections.Generic;
using VRageMath;
using VRage.Game.GUI.TextBox;
using Sandbox.Graphics.GUI;

namespace mamba.Blocks.Gui
{
    public static class StoreBlockAdminFGui
    {
        public static void Open(IMyTerminalBlock block)
        {
            if (block == null) return;

            // Check if block is our custom StoreBlockAdmin
            if (block.BlockDefinition.SubtypeId != "StoreBlockAdmin") return;

            // Open a custom GUI window
            var screen = new StoreBlockAdminFGUIScreen(block);
            MyAPIGateway.Gui.ShowScreen(screen);
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

    // Custom GUI Screen
    internal class StoreBlockAdminFGUIScreen : MyGuiScreenBase
    {
        private IMyTerminalBlock m_block;

        public StoreBlockAdminFGUIScreen(IMyTerminalBlock block) : base()
        {
            m_block = block;
            CanBeHidden = true;
            CanHaveFocus = true;
            Enabled = true;
            UseOpacity = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            // Create simple label
            var caption = new MyGuiControlLabel(new Vector2(0, -0.3f), null, "Store Block Admin Interface");
            Controls.Add(caption);

            var infoLabel = new MyGuiControlLabel(new Vector2(0, -0.1f), null, $"Block: {m_block.CustomName}");
            Controls.Add(infoLabel);

            var buyButton = new MyGuiControlButton(new Vector2(0, 0.1f), MyGuiControlButtonStyleEnum.Default, text: new StringBuilder("Buy Tab"));
            buyButton.ButtonClicked += (b) => MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "Buy Tab Selected!");
            Controls.Add(buyButton);

            var sellButton = new MyGuiControlButton(new Vector2(0, 0.3f), MyGuiControlButtonStyleEnum.Default, text: new StringBuilder("Sell Tab"));
            sellButton.ButtonClicked += (b) => MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "Sell Tab Selected!");
            Controls.Add(sellButton);
        }

        public override string GetFriendlyName()
        {
            return "StoreBlockAdminFGUIScreen";
        }
    }
}