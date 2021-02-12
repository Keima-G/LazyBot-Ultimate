namespace LazyEvo.Plugins
{
    using LazyEvo.Forms.Helpers;
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGrindEngine;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Net.Sockets;
    using System.Threading;

    internal class Relogger
    {
        private const string Logout = "/logout";
        private const string Gluedialogbackground = "GlueDialogBackground";
        private const string Accountloginloginbutton = "AccountLoginLoginButton";
        private const string Accountloginaccountedit = "AccountLoginAccountEdit";
        private const string Accountloginpasswordedit = "AccountLoginPasswordEdit";
        private const string Tokenenterdialog = "TokenEnterDialog";
        private const string RealmListDialog = "RealmList";
        private const string RealmListOk = "RealmListOkButton";
        private const string Wowaccountselectdialogbackgroundacceptbutton = "WoWAccountSelectDialogBackgroundAcceptButton";
        private const string Charselectenterworldbutton = "CharSelectEnterWorldButton";
        private const string WwwGoogleCom = "www.google.com";
        private const string WoWAccountSelectDialogBackgroundContainerButton = "WoWAccountSelectDialogBackgroundContainerButton{0}";
        private const string Wowaccountselectdialogbackground = "WoWAccountSelectDialogBackground";
        private static Thread _relogger;
        public static bool PeriodicLogoutActive;

        private static bool CheckConnection()
        {
            try
            {
                new TcpClient("www.google.com", 80).Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void CheckForDis()
        {
            if (!LazyLib.Wow.ObjectManager.InGame && (LazyForms.MainForm.ShouldRelog && ReloggerSettings.ReloggingEnabled))
            {
                try
                {
                    Frame frameByName = InterfaceHelper.GetFrameByName("GlueDialogBackground");
                    if (((frameByName != null) && frameByName.IsVisible) && ((_relogger == null) || !_relogger.IsAlive))
                    {
                        Logging.Write("Oooh no disconnect", new object[0]);
                        Thread thread = new Thread(new ThreadStart(Relogger.DoReconnect)) {
                            IsBackground = true
                        };
                        _relogger = thread;
                        _relogger.Start();
                    }
                }
                catch (Exception exception)
                {
                    Logging.Debug("Error relogging: " + exception, new object[0]);
                }
            }
        }

        private static void CheckForRealmList()
        {
            if (InterfaceHelper.GetFrameByName("RealmList").IsVisible && !LazyLib.Wow.ObjectManager.InGame)
            {
                Logging.Write(LogType.Warning, "Realm Listing Detected!", new object[0]);
                Logging.Write(LogType.Warning, "This usually means the server has crashed", new object[0]);
                Logging.Write(LogType.Warning, "Waiting 5 minutes before attempting relog", new object[0]);
                Thread.Sleep(0x493e0);
                InterfaceHelper.GetFrameByName("RealmListOkButton").LeftClick();
            }
        }

        private static void DoReconnect()
        {
            if (Relog())
            {
                Thread.Sleep(0x7d0);
                MoveHelper.Stop();
                GatherEngine.Navigator.Stop();
                GrindingEngine.Navigator.Stop();
                Logging.Write(LogType.Good, "Relogging worked :)", new object[0]);
                LazyForms.MainForm.StartBotting();
            }
            else if (!LazyLib.Wow.ObjectManager.InGame)
            {
                Logging.Write("Could not relog :(", new object[0]);
            }
            else
            {
                Logging.Debug(LogType.Error, "Odd? It appears relog failed but we are ingame, starting bot anyway", new object[0]);
                LazyForms.MainForm.StartBotting();
            }
        }

        public static bool LogOutFor(int mins)
        {
            if (LazyLib.Wow.ObjectManager.InGame)
            {
                PeriodicLogoutActive = true;
                KeyHelper.ChatboxSendText("/logout");
            }
            Ticker ticker = new Ticker((double) (mins * 0xea60));
            while (!ticker.IsReady)
            {
                Thread.Sleep(0x2710);
            }
            return Relog();
        }

        public static bool Relog()
        {
            bool flag;
            try
            {
                PeriodicLogoutActive = false;
                Thread.Sleep(0x9c4);
                while (true)
                {
                    if (CheckConnection())
                    {
                        if (InterfaceHelper.GetFrameByName("GlueDialogBackground").IsVisible)
                        {
                            KeyHelper.SendEnter();
                        }
                        Thread.Sleep(500);
                        if (InterfaceHelper.GetFrameByName("AccountLoginLoginButton").IsVisible)
                        {
                            InterfaceHelper.GetFrameByName("AccountLoginAccountEdit").SetEditBoxText(ReloggerSettings.AccountName);
                            Thread.Sleep(0xbb8);
                            InterfaceHelper.GetFrameByName("AccountLoginPasswordEdit").LeftClick();
                            Thread.Sleep(0x5dc);
                            InterfaceHelper.GetFrameByName("AccountLoginPasswordEdit").SetEditBoxText(ReloggerSettings.AccountPw);
                            Thread.Sleep(0x3e8);
                            InterfaceHelper.GetFrameByName("AccountLoginLoginButton").LeftClick();
                        }
                        Thread.Sleep(0x1388);
                        try
                        {
                            if (InterfaceHelper.GetFrameByName("WoWAccountSelectDialogBackground").IsVisible)
                            {
                                InterfaceHelper.GetFrameByName($"WoWAccountSelectDialogBackgroundContainerButton{ReloggerSettings.AccountAccount}").LeftClick();
                                Thread.Sleep(0x7d0);
                                InterfaceHelper.GetFrameByName("WoWAccountSelectDialogBackgroundAcceptButton").LeftClick();
                            }
                        }
                        catch
                        {
                        }
                        if (InterfaceHelper.GetFrameByName("TokenEnterDialog").IsVisible)
                        {
                            Logging.Write("Can't log in with authenticator attached", new object[0]);
                            flag = false;
                        }
                        else
                        {
                            while (true)
                            {
                                if (InterfaceHelper.GetFrameByName("CharSelectEnterWorldButton").IsVisible)
                                {
                                    InterfaceHelper.GetFrameByName("CharSelectEnterWorldButton").LeftClick();
                                    Thread.Sleep(0x2710);
                                    while (true)
                                    {
                                        if (!LazyLib.Wow.ObjectManager.InGame)
                                        {
                                            Logging.Debug("Not InGame", new object[0]);
                                            Thread.Sleep(0x3e8);
                                            if (!InterfaceHelper.GetFrameByName("AccountLoginLoginButton").IsVisible)
                                            {
                                                continue;
                                            }
                                            flag = false;
                                        }
                                        else
                                        {
                                            LazyForms.MainForm.StopBotting(true);
                                            Logging.Write(LogType.Good, "Relogging worked :)", new object[0]);
                                            Latency.Sleep(0x1388);
                                            LazyForms.MainForm.StartBotting();
                                            Thread.Sleep(0x1388);
                                            Ticker ticker = new Ticker(8000.0);
                                            while (true)
                                            {
                                                if (ticker.IsReady || LazyLib.Wow.ObjectManager.MyPlayer.IsMoving)
                                                {
                                                    if (ticker.IsReady)
                                                    {
                                                        Logging.Write(LogType.Warning, "We did not start moving, restarting", new object[0]);
                                                        LazyForms.MainForm.StopBotting(true);
                                                        Thread.Sleep(0x7d0);
                                                        LazyForms.MainForm.StartBotting();
                                                    }
                                                    flag = true;
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                    break;
                                }
                                CheckForRealmList();
                                Thread.Sleep(0xbb8);
                            }
                        }
                        break;
                    }
                    Thread.Sleep(0x1388);
                }
            }
            catch (Exception exception)
            {
                Logging.Debug("Error when relogging: " + exception, new object[0]);
                return false;
            }
            return flag;
        }
    }
}

