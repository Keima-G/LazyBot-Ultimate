namespace LazyEvo.Forms.Helpers
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class StopAfter
    {
        private static Ticker _stopAfter;

        public static void BotStarted()
        {
            if (LazySettings.StopAfterBool)
            {
                _stopAfter = new Ticker((Convert.ToDouble(LazySettings.StopAfter) * 60.0) * 1000.0);
                _stopAfter.Reset();
                Logging.Write(LogType.Info, "Stop after enabled, will stop in " + Convert.ToDouble(LazySettings.StopAfter) + " minuttes", new object[0]);
            }
        }

        public static void BotStopped()
        {
            _stopAfter = null;
        }

        public static void Monitor()
        {
            if ((_stopAfter != null) && (LazySettings.StopAfterBool && (_stopAfter.IsReady && !LazyLib.Wow.ObjectManager.ShouldDefend)))
            {
                LazyForms.MainForm.StopBotting(true);
                LazyForms.MainForm.ShouldRelog = false;
                Thread.Sleep(0xbb8);
                Logging.Write(LogType.Info, "[Engine]Stop after " + Convert.ToDouble(LazySettings.StopAfter) + " minuttes done", new object[0]);
                KeyHelper.ChatboxSendText("/logout");
                if (LazySettings.Shutdown)
                {
                    Shutdown.ShutDownComputer();
                }
            }
        }
    }
}

