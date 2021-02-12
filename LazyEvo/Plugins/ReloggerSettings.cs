namespace LazyEvo.Plugins
{
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class ReloggerSettings
    {
        public const string SettingsName = @"\Settings\lazy_relog.ini";
        public static string OurDirectory;
        public static string CharacterName;

        public static void LoadSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_relog.ini");
            try
            {
                AccountName = string.IsNullOrEmpty(manager.GetString("Relog", "AccountName", string.Empty)) ? string.Empty : Encryptor.Decrypt(manager.GetString("Relog", "AccountName", string.Empty));
                AccountPw = string.IsNullOrEmpty(manager.GetString("Relog", "AccountPW", string.Empty)) ? string.Empty : Encryptor.Decrypt(manager.GetString("Relog", "AccountPW", string.Empty));
                if (!string.IsNullOrEmpty(manager.GetString("Relog", "CharacterName", string.Empty)))
                {
                    CharacterName = manager.GetString("Relog", "CharacterName", string.Empty);
                }
                ReloggingEnabled = manager.GetBoolean("Relog", "EnableRelogging", false);
                PeriodicReloggingEnabled = manager.GetBoolean("Relog", "EnablePeriodicRelogging", false);
                PeriodicLogOut = manager.GetInt("Relog", "PeriodicLogOut", 60);
                PeriodicLogIn = manager.GetInt("Relog", "PeriodicLogIn", 30);
                AccountAccount = manager.GetInt("Relog", "AccountAccount", 1);
                CharacterName = manager.GetString("Relog", "CharacterName", string.Empty);
            }
            catch (Exception)
            {
                Logging.Debug("Could not load relogger settings. All relogger values have been reset.", new object[0]);
                AccountName = "";
                AccountPw = "";
                CharacterName = "";
                ReloggingEnabled = false;
                PeriodicReloggingEnabled = false;
                PeriodicLogIn = 30;
                PeriodicLogOut = 60;
                AccountAccount = 1;
            }
        }

        public static void SaveSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_relog.ini");
            manager.IniWriteValue("Relog", "AccountName", Encryptor.Encrypt(AccountName));
            manager.IniWriteValue("Relog", "AccountPW", Encryptor.Encrypt(AccountPw));
            manager.IniWriteValue("Relog", "EnableRelogging", ReloggingEnabled.ToString());
            manager.IniWriteValue("Relog", "EnablePeriodicRelogging", PeriodicReloggingEnabled.ToString());
            manager.IniWriteValue("Relog", "PeriodicLogOut", PeriodicLogOut.ToString());
            manager.IniWriteValue("Relog", "PeriodicLogIn", PeriodicLogIn.ToString());
            manager.IniWriteValue("Relog", "AccountAccount", AccountAccount.ToString());
            manager.IniWriteValue("Relog", "CharacterName", CharacterName);
        }

        public static string AccountName { get; set; }

        public static string AccountPw { get; set; }

        public static string _CharacterName { get; set; }

        public static bool ReloggingEnabled { get; set; }

        public static bool PeriodicReloggingEnabled { get; set; }

        public static int PeriodicLogOut { get; set; }

        public static int PeriodicLogIn { get; set; }

        public static int AccountAccount { get; set; }
    }
}

