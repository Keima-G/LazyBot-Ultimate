namespace LazyEvo.Plugins
{
    using LazyEvo.Forms.Helpers;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class PeriodicRelogger
    {
        private static Ticker _logOutAfter;

        public static void BotStarted()
        {
            if (ReloggerSettings.PeriodicReloggingEnabled && ReloggerSettings.ReloggingEnabled)
            {
                _logOutAfter = new Ticker((Convert.ToDouble(ReloggerSettings.PeriodicLogOut) * 60.0) * 1000.0);
                _logOutAfter.Reset();
                Logging.Write(LogType.Info, "Periodic relog enabled. Next logout in: " + Convert.ToDouble(ReloggerSettings.PeriodicLogOut) + " minutes", new object[0]);
            }
        }

        public static void BotStopped()
        {
            _logOutAfter = null;
        }

        public static void Monitor()
        {
            if ((_logOutAfter != null) && (ReloggerSettings.PeriodicReloggingEnabled && (_logOutAfter.IsReady && !LazyLib.Wow.ObjectManager.ShouldDefend)))
            {
                LazyForms.MainForm.StopBotting(true);
                Thread.Sleep(0xbb8);
                Logging.Write(LogType.Info, "[Engine] Periodic logout as " + Convert.ToDouble(ReloggerSettings.PeriodicLogOut) + " minutes have passed", new object[0]);
                Relogger.LogOutFor(ReloggerSettings.PeriodicLogIn);
            }
        }
    }
}

