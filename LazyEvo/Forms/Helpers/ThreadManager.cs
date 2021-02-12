namespace LazyEvo.Forms.Helpers
{
    using LazyEvo.Plugins;
    using LazyLib.Helpers;
    using System;
    using System.Threading;

    internal class ThreadManager
    {
        private static Chat _chat;
        private static Thread _monitor;

        private static void DoWork()
        {
            while (true)
            {
                try
                {
                    while (true)
                    {
                        if (!Relogger.PeriodicLogoutActive)
                        {
                            Relogger.CheckForDis();
                        }
                        RefreshGui.Refresh();
                        _chat.ReadChat();
                        Followers.CheckFollow();
                        PeriodicRelogger.Monitor();
                        StopAfter.Monitor();
                        Thread.Sleep(0x5dc);
                        break;
                    }
                }
                catch
                {
                }
            }
        }

        public static void Start()
        {
            _chat = new Chat();
            _chat.PrepareReading();
            Thread thread = new Thread(new ThreadStart(ThreadManager.DoWork)) {
                IsBackground = true
            };
            _monitor = thread;
            _monitor.Name = "Monitor";
            _monitor.Start();
        }
    }
}

