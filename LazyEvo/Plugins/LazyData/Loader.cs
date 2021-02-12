namespace LazyEvo.Plugins.LazyData
{
    using LazyEvo.Plugins.ExtraLazy;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.IPlugin;
    using System;
    using System.Threading;

    public class Loader : ILazyPlugin
    {
        private static Professions professions;
        private static frmProfessions settingsForm;
        private static bool professionsLoaded;
        private static Thread workerThread;
        private static string previousBlueChat;

        public void BotStart()
        {
        }

        public void BotStop()
        {
        }

        private void ChatNewChatMessage(object sender, GChatEventArgs e)
        {
        }

        public string GetName() => 
            "LazyData Demo";

        public static void getProfessions()
        {
            CS$<>9__CachedAnonymousMethodDelegate1 ??= p => professionsReady(p);
            Professions professions1 = new Professions(CS$<>9__CachedAnonymousMethodDelegate1);
        }

        public void PluginLoad()
        {
            settingsForm = new frmProfessions();
            professionsLoaded = false;
            Logging.Write("LazyData demo started", new object[0]);
            Chat.NewChatMessage += new EventHandler<GChatEventArgs>(this.ChatNewChatMessage);
        }

        public void PluginUnload()
        {
            Logging.Write("LazyData Demo stopped", new object[0]);
            Chat.NewChatMessage -= new EventHandler<GChatEventArgs>(this.ChatNewChatMessage);
        }

        public static void professionsReady(Professions p)
        {
            settingsForm.Professions = p;
            settingsForm.createDisplay();
            professions = p;
            professionsLoaded = true;
            if (!workerThread.IsAlive)
            {
                workerThread.Start();
            }
        }

        public void Pulse()
        {
        }

        public void Settings()
        {
            Logging.Write("Viewing professions", new object[0]);
            settingsForm.Show();
            workerThread = new Thread(new ThreadStart(Loader.updateSkills));
        }

        public static void stopUpdating()
        {
            workerThread.Abort();
        }

        private static void updateSkills()
        {
            while (true)
            {
                string msg = EventMessage.readChat();
                if (msg != previousBlueChat)
                {
                    settingsForm.BlueChat = msg;
                    settingsForm.setBlueChat();
                    if (professions.MsgUpdate(msg))
                    {
                        professionsReady(professions);
                    }
                    previousBlueChat = msg;
                }
                Thread.Sleep(500);
            }
        }
    }
}

