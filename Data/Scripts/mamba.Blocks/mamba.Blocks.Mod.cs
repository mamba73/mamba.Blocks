// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/mamba.Blocks.Mod.cs
using System;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Utils;

namespace mamba.Blocks
{
    public static class ModCommunication
    {
        public const string MOD_NAME = "mamba.Blocks";

        public static void Log(string message, string prefix = "INFO")
        {
            string fullMsg = MOD_NAME + " [" + prefix + "] " + message;
            VRage.Utils.MyLog.Default.WriteLineAndConsole(fullMsg);
        }
    }

    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation)]
    public class mambaBlocksSession : MySessionComponentBase
    {
        private bool m_isInitialized = false;

        public override void LoadData()
        {
            base.LoadData();

            if (m_isInitialized || MyAPIGateway.Session == null)
                return;

            try
            {
                if (MyAPIGateway.Utilities != null)
                    MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "Mod is starting to load...");

                ModCommunication.Log("Mod is starting to load...");

                // Gui.StoreBlockAdminGui.Init();
                Gui.SimpleGuiTest.Init();

                if (MyAPIGateway.Utilities != null)
                    MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "Mod loaded SUCCESSFULLY!");

                ModCommunication.Log("Mod loaded SUCCESSFULLY!");
                m_isInitialized = true;
            }
            catch (Exception e)
            {
                ModCommunication.Log("Mod load FAILED: " + e.Message + " | Stack: " + e.StackTrace, "ERROR");
                if (MyAPIGateway.Utilities != null)
                    MyAPIGateway.Utilities.ShowMessage("mamba.Blocks", "ERROR: " + e.Message);
            }
        }
    }
}
