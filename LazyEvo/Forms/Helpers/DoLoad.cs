namespace LazyEvo.Forms.Helpers
{
    using LazyEvo;
    using LazyEvo.Other;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Helpers.Vendor;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class DoLoad
    {
        public static void Close()
        {
            LazyLib.Wow.ObjectManager.Close();
            ItemDatabase.Close();
            Hook.ReleaseMouse();
            Logging.Debug("Done closing", new object[0]);
            Environment.Exit(0);
        }

        public static void Load()
        {
            Thread thread = new Thread(new ThreadStart(DoLoad.LoadTheShit)) {
                IsBackground = true
            };
            thread.Name = "LoadTheShit";
            thread.Start();
        }

        private static void LoadNow()
        {
            if (Program.AttachTo != -1)
            {
                LazyLib.Wow.ObjectManager.Initialize(Program.AttachTo);
                try
                {
                    if (LazySettings.HookMouse)
                    {
                        Hook.DoHook();
                    }
                }
                catch
                {
                }
                LazyLib.Helpers.Chat.NewChatMessage += new EventHandler<GChatEventArgs>(LazyForms.MainForm.ChatNewChatMessage);
            }
            Langs.Load();
            ThreadManager.Start();
            ItemDatabase.Open();
            LazyForms.MainForm.LicenseOk();
        }

        private static void LoadTheShit()
        {
            LazyLib.Wow.ObjectManager.MakeReady();
            Logging.Write("Lazybot De-Evolution By Greaver", new object[0]);
            Logging.Write("Original Lazybot by Arutha, thank you for making it open source!", new object[0]);
            Logging.Write("LazyBot is free and open source software!", new object[0]);
            Logging.Write("http://www.assembla.com/spaces/lazybot/wiki", new object[0]);
            Logging.Write("Keys should be placed on bar 1-6 and position 1-9!", new object[0]);
            Logging.Write("**********", new object[0]);
            Logging.Write(LogType.Warning, "Read the readme file before using this bot!", new object[0]);
            Logging.Write("**********", new object[0]);
            if (LazySettings.GroundGather)
            {
                Logging.Write(LogType.Error, "Gather Mode set to Ground Mount!", new object[0]);
                Logging.Write("Gather Distance set to: " + LazySettings.GroundGatherDistance + " Yards", new object[0]);
            }
            if (LazySettings.JustFishing)
            {
                Logging.Write(LogType.Error, "ImJustFishing is ENABLED", new object[0]);
            }
            LoadNow();
        }
    }
}

