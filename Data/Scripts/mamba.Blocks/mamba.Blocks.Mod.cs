// Full path: mamba.Blocks/Data/Scripts/mamba.Blocks/mamba.Blocks.Mod.cs
using System;
using Sandbox.ModAPI;
using VRage.Game.Components;
using mamba.Blocks.Gui;

namespace mamba.Blocks
{
    public static class ModCommunication
    {
        public const string MOD_NAME = "mamba.Blocks";
        public static void Log(string message, string prefix = "INFO")
        {
            VRage.Utils.MyLog.Default.WriteLineAndConsole($"{MOD_NAME} [{prefix}] {message}");
        }
    }

    [MySessionComponentDescriptor(MyUpdateOrder.BeforeSimulation)]
    public class mambaBlocksSession : MySessionComponentBase
    {
        private bool m_isInitialized = false;

        public override void BeforeStart()
        {
            if (m_isInitialized || MyAPIGateway.Session == null) return;
            
            try 
            {
                SimpleGuiTest.Init(); // Register terminal controls
                ModCommunication.Log("Session initialized.");
                m_isInitialized = true;
            }
            catch (Exception e)
            {
                ModCommunication.Log($"Initialization failed: {e.Message}", "ERROR");
            }
        }

        protected override void UnloadData()
        {
            SimpleGuiTest.Unload();
            m_isInitialized = false;
        }
    }
}
