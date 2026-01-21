// mamba.Blocks/Data/Scripts/mamba.Blocks/Gui/StoreBlockAdminGui.cs
using System;
using System.Collections.Generic;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using VRage.Game;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRage.Utils;
using VRageMath;
using mamba.Blocks;
using mamba.Blocks.Components;

namespace mamba.Blocks.Gui
{
    public static class StoreBlockAdminGui
    {
        private static bool m_initialized = false;
        private static IMyStoreBlock m_currentBlock;

        // Tab identifiers
        private const int TAB_BUY = 0;
        private const int TAB_SELL = 1;
        private const int TAB_SELL_GRIDS = 2;
        private const int TAB_BUY_GRIDS = 3;
        private const int TAB_ADMIN = 4;

        public static void Init()
        {
            if (m_initialized) return;
            m_initialized = true;
            ModCommunication.Log("[DEBUG mamba] StoreBlockAdmin GUI initialized.");
        }

        // Poziva se iz MyGameLogicComponent kada igrač otvori F-screen
        public static void Open(IMyStoreBlock storeBlock)
        {
            if (storeBlock == null) return;
            m_currentBlock = storeBlock;

            // Kreiraj GUI
            CreateGui();
        }

        private static void CreateGui()
        {
            if (m_currentBlock == null) return;

            // Ovo je placeholder – VRage F-screen GUI kod
            // Vanilla GUI koristi MyGuiScreenTerminalBlock, mi ne diramo vanilla, samo dodajemo tabove
            try
            {
                ModCommunication.Log("[DEBUG mamba] Opening StoreBlockAdmin GUI for: " + m_currentBlock.CustomName);

                // Tabovi
                OpenTab(TAB_BUY);
                OpenTab(TAB_SELL);
                OpenTab(TAB_SELL_GRIDS);
                OpenTab(TAB_BUY_GRIDS);
                OpenTab(TAB_ADMIN);
            }
            catch (Exception e)
            {
                ModCommunication.Log("[ERROR mamba] GUI Open failed: " + e.Message);
            }
        }

        private static void OpenTab(int tabId)
        {
            switch (tabId)
            {
                case TAB_BUY:
                    ShowBuyTab();
                    break;
                case TAB_SELL:
                    ShowSellTab();
                    break;
                case TAB_SELL_GRIDS:
                    ShowSellGridsTab();
                    break;
                case TAB_BUY_GRIDS:
                    ShowBuyGridsTab();
                    break;
                case TAB_ADMIN:
                    ShowAdminTab();
                    break;
            }
        }

        private static void ShowBuyTab()
        {
            // Lista svih itema za kupnju
            // Vanilla + Cargo4Store update
            ModCommunication.Log("[DEBUG mamba] Buy Tab Opened - updating from Cargo4Store");

            MambaStoreBlockLogic logic = m_currentBlock.GameLogic as MambaStoreBlockLogic;
            if (logic != null)
            {
                // Ovdje bi pozvao ScanAndUpdateOffers da update-a Cargo4Store
                logic.UpdateCargo4StoreOffers(); // bez reflection
                // logic.GetType().GetMethod("ScanAndUpdateOffers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(logic, null);
            }

            // Prikaz liste – placeholder
            MyAPIGateway.Utilities.ShowMessage("StoreBlockAdmin", "Buy tab: Items updated from Cargo4Store");
        }

        private static void ShowSellTab()
        {
            // Vanilla ponude
            MyAPIGateway.Utilities.ShowMessage("StoreBlockAdmin", "Sell tab: vanilla offers");
        }

        private static void ShowSellGridsTab()
        {
            // Lista gridova koje admin može prodati
            MyAPIGateway.Utilities.ShowMessage("StoreBlockAdmin", "Sell Grids tab: TODO - implement grid listing");
        }

        private static void ShowBuyGridsTab()
        {
            // Placeholder
            MyAPIGateway.Utilities.ShowMessage("StoreBlockAdmin", "Buy Grids tab: in development");
        }

        private static void ShowAdminTab()
        {
            // Vanilla admin UI + textbox za default cijenu
            MyAPIGateway.Utilities.ShowMessage("StoreBlockAdmin", "Administration tab: add new offer (default price editable)");
        }
    }
}
