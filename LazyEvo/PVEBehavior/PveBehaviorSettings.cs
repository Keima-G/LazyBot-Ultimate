namespace LazyEvo.PVEBehavior
{
    using LazyLib.Helpers;
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal class PveBehaviorSettings
    {
        public const string SettingsName = @"\Settings\pve_behavior.ini";
        public static bool AllowScripts;
        public static string LoadedBeharvior;
        public static bool AvoidAddsCombat;
        public static int SkipAddsDis;

        public static void LoadSettings()
        {
            IniManager manager = new IniManager(new FileInfo(Application.ExecutablePath).DirectoryName + @"\Settings\pve_behavior.ini");
            LoadedBeharvior = manager.GetString("Config", "LoadedBeharvior", string.Empty);
            AvoidAddsCombat = manager.GetBoolean("Config", "AvoidAddsCombat", false);
            SkipAddsDis = manager.GetInt("Config", "SkipAddsDis", 0);
            AllowScripts = manager.GetBoolean("Config", "AllowScripts", false);
        }

        public static void SaveSettings()
        {
            IniManager manager = new IniManager(new FileInfo(Application.ExecutablePath).DirectoryName + @"\Settings\pve_behavior.ini");
            manager.IniWriteValue("Config", "LoadedBeharvior", LoadedBeharvior);
            manager.IniWriteValue("Config", "AvoidAddsCombat", AvoidAddsCombat.ToString());
            manager.IniWriteValue("Config", "SkipAddsDis", SkipAddsDis.ToString());
            manager.IniWriteValue("Config", "AllowScripts", AllowScripts);
        }
    }
}

