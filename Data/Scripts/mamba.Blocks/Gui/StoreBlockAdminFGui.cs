// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/StoreBlockAdminFGui.cs
using System;
using System.Collections.Generic;
using Sandbox.Game.Gui;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using VRage.Game;
using VRage.Game.GUI;
using VRage.Game.ModAPI;
using VRage.Game.Components;
using VRage.Utils;
using VRageMath;
using VRage.Game.GUI.TextPanel;

namespace mamba.Blocks.Gui
{
    public class StoreBlockAdminFGui : MyGuiScreenBase
    {
        private IMyStoreBlock m_store;
        private Vector2 m_size = new Vector2(0.8f, 0.8f);
        private Vector2 m_tabSize = new Vector2(0.75f, 0.7f);
        private List<string> m_tabs = new List<string> { "Buy", "Sell", "Sell Grids", "Buy Grids", "Administration" };
        private int m_currentTab = 0;

        private MyGuiControlPanel m_panel;

        public StoreBlockAdminFGui(IMyStoreBlock store)
            : base(new Vector2(0.5f, 0.5f), MyGuiConstants.SCREEN_BACKGROUND_COLOR, null, true, null)
        {
            m_store = store;
            RecreateControls(true);
        }

        public override string GetFriendlyName()
        {
            return "StoreBlockAdminFGui";
        }

        public override void RecreateControls(bool constructor)
        {
            base.RecreateControls(constructor);

            m_panel = new MyGuiControlPanel();
            m_panel.Position = Vector2.Zero;
            m_panel.Size = m_tabSize;
            Controls.Add(m_panel);

            AddTabButtons();
            DrawCurrentTab();
        }

        private void AddTabButtons()
        {
            Vector2 pos = new Vector2(-0.35f, -0.32f);
            float width = 0.14f;

            for (int i = 0; i < m_tabs.Count; i++)
            {
                int tabIndex = i;
                var btn = new MyGuiControlButton
                {
                    Position = pos + new Vector2(i * width, 0),
                    Size = new Vector2(width, 0.05f),
                    Text = m_tabs[i],
                    Action = b => { m_currentTab = tabIndex; DrawCurrentTab(); }
                };
                Controls.Add(btn);
            }
        }

        private void DrawCurrentTab()
        {
            // Clear old controls (osim tab buttona)
            for (int i = Controls.Count - 1; i >= m_tabs.Count; i--)
                Controls.RemoveAt(i);

            switch (m_currentTab)
            {
                case 0: DrawBuyTab(); break;
                case 1: DrawSellTab(); break;
                case 2: DrawSellGridsTab(); break;
                case 3: DrawBuyGridsTab(); break;
                case 4: DrawAdministrationTab(); break;
            }
        }

        private void DrawBuyTab()
        {
            var label = new MyGuiControlLabel
            {
                Position = new Vector2(0, -0.28f),
                Text = "Buy Tab - Cargo4Store Items",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };
            Controls.Add(label);

            // TODO: ovdje pozvati logiku iz MambaStoreBlockLogic za cargo4store
            if (m_store != null)
            {
                // Primjer placeholder teksta
                var info = new MyGuiControlLabel
                {
                    Position = new Vector2(0, -0.22f),
                    Text = "Buy tab content goes here...",
                    OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
                };
                Controls.Add(info);
            }
        }

        private void DrawSellTab()
        {
            var label = new MyGuiControlLabel
            {
                Position = new Vector2(0, -0.28f),
                Text = "Sell Tab - Vanilla Items",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };
            Controls.Add(label);
        }

        private void DrawSellGridsTab()
        {
            var label = new MyGuiControlLabel
            {
                Position = new Vector2(0, -0.28f),
                Text = "Sell Grids Tab - Custom Logic",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };
            Controls.Add(label);
        }

        private void DrawBuyGridsTab()
        {
            var label = new MyGuiControlLabel
            {
                Position = new Vector2(0, -0.28f),
                Text = "Buy Grids Tab - In Development",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };
            Controls.Add(label);
        }

        private void DrawAdministrationTab()
        {
            var label = new MyGuiControlLabel
            {
                Position = new Vector2(0, -0.28f),
                Text = "Administration Tab - Default GUI with Price Override",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };
            Controls.Add(label);
        }
    }

    public static class StoreBlockAdminFGuiHelper
    {
        public static void Open(IMyStoreBlock store)
        {
            var gui = new StoreBlockAdminFGui(store);
            MyGuiSandbox.AddScreen(gui);
        }
    }
}
