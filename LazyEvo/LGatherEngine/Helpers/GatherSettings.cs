namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib.Helpers;
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal class GatherSettings
    {
        internal const string SettingsName = @"\Settings\lazy_flying.ini";
        internal static string OurDirectory;
        internal static bool Herb;
        internal static bool Mine;
        internal static float ApproachModifier;
        internal static string MaxUnits;
        internal static bool StopOnDeath;
        internal static bool StopHarvestWithPlayerAround;
        internal static bool AvoidPlayers;
        internal static bool AutoBlacklist;
        internal static bool AvoidElites;
        internal static bool StopOnFullBags;
        internal static bool WaitForLoot;
        internal static bool WaitForRessSick;
        internal static string FlyingMountBar;
        internal static string FlyingMountKey;
        internal static string Profile;
        internal static bool FindCorpse;
        internal static bool Fish;
        internal static bool Lure;
        internal static double MaxTimeAtSchool;
        internal static double FishApproach;
        internal static string LureBar;
        internal static string LureKey;
        internal static string WaterwalkBar;
        internal static string WaterwalkKey;
        internal static string ExtraBar;
        internal static string ExtraKey;
        public static bool DruidAvoidCombat;
        public static bool SendKeyOnStartCombat;

        public static void LoadSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_flying.ini");
            Herb = manager.GetBoolean("Flying", "Herb", false);
            Mine = manager.GetBoolean("Flying", "Mine", false);
            ApproachModifier = (float) Convert.ToDouble(manager.GetString("Flying", "ApproachModifier", "0"));
            MaxUnits = manager.GetString("Flying", "MaxUnits", "3");
            StopOnDeath = manager.GetBoolean("Flying", "StopOnDeath", false);
            StopHarvestWithPlayerAround = manager.GetBoolean("Flying", "StopHarvest", true);
            AvoidPlayers = manager.GetBoolean("Flying", "AvoidPlayers", true);
            AutoBlacklist = manager.GetBoolean("Flying", "AutoBlacklist", false);
            StopOnFullBags = manager.GetBoolean("Flying", "StopOnFullBags", false);
            AvoidElites = manager.GetBoolean("Flying", "AvoidElites", true);
            FindCorpse = manager.GetBoolean("Flying", "FindCorpse", true);
            WaitForLoot = manager.GetBoolean("Flying", "WaitForLoot", true);
            WaitForRessSick = manager.GetBoolean("Flying", "WaitForRessSick", false);
            FlyingMountBar = manager.GetString("Flying", "FlyingMountBar", "0");
            FlyingMountKey = manager.GetString("Flying", "FlyingMountKey", "0");
            Profile = manager.GetString("Flying", "Profile", string.Empty);
            DruidAvoidCombat = manager.GetBoolean("Flying", "DruidAvoidCombat", false);
            Fish = manager.GetBoolean("Flying", "Fish", false);
            Lure = manager.GetBoolean("Flying", "Lure", false);
            SendKeyOnStartCombat = manager.GetBoolean("Flying", "SendKeyOnStartCombat", false);
            MaxTimeAtSchool = Convert.ToDouble(manager.GetString("Flying", "MaxTimeAtSchool", "4"));
            FishApproach = Convert.ToDouble(manager.GetString("Flying", "FishApproach", "30"));
            LureBar = manager.GetString("Flying", "LureBar", "1");
            LureKey = manager.GetString("Flying", "LureKey", "1");
            WaterwalkBar = manager.GetString("Flying", "WaterwalkBar", "1");
            WaterwalkKey = manager.GetString("Flying", "WaterwalkKey", "1");
            ExtraBar = manager.GetString("Flying", "ExtraBar", "1");
            ExtraKey = manager.GetString("Flying", "ExtraKey", "1");
        }

        public static void SaveSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_flying.ini");
            manager.IniWriteValue("Flying", "Herb", Herb.ToString());
            manager.IniWriteValue("Flying", "Mine", Mine.ToString());
            manager.IniWriteValue("Flying", "ApproachModifier", ApproachModifier.ToString());
            manager.IniWriteValue("Flying", "MaxUnits", MaxUnits);
            manager.IniWriteValue("Flying", "StopOnDeath", StopOnDeath.ToString());
            manager.IniWriteValue("Flying", "StopHarvest", StopHarvestWithPlayerAround.ToString());
            manager.IniWriteValue("Flying", "StopOnFullBags", StopOnFullBags.ToString());
            manager.IniWriteValue("Flying", "AvoidPlayers", AvoidPlayers.ToString());
            manager.IniWriteValue("Flying", "AutoBlacklist", AutoBlacklist.ToString());
            manager.IniWriteValue("Flying", "AvoidElites", AvoidElites.ToString());
            manager.IniWriteValue("Flying", "FindCorpse", FindCorpse.ToString());
            manager.IniWriteValue("Flying", "WaitForLoot", WaitForLoot.ToString());
            manager.IniWriteValue("Flying", "WaitForRessSick", WaitForRessSick.ToString());
            manager.IniWriteValue("Flying", "FlyingMountBar", FlyingMountBar);
            manager.IniWriteValue("Flying", "FlyingMountKey", FlyingMountKey);
            manager.IniWriteValue("Flying", "Profile", Profile);
            manager.IniWriteValue("Flying", "DruidAvoidCombat", DruidAvoidCombat);
            manager.IniWriteValue("Flying", "Fish", Fish);
            manager.IniWriteValue("Flying", "Lure", Lure);
            manager.IniWriteValue("Flying", "MaxTimeAtSchool", MaxTimeAtSchool);
            manager.IniWriteValue("Flying", "FishApproach", FishApproach);
            manager.IniWriteValue("Flying", "LureBar", LureBar);
            manager.IniWriteValue("Flying", "LureKey", LureKey);
            manager.IniWriteValue("Flying", "WaterwalkBar", WaterwalkBar);
            manager.IniWriteValue("Flying", "WaterwalkKey", WaterwalkKey);
            manager.IniWriteValue("Flying", "ExtraBar", ExtraBar);
            manager.IniWriteValue("Flying", "ExtraKey", ExtraKey);
            manager.IniWriteValue("Flying", "SendKeyOnStartCombat", SendKeyOnStartCombat);
        }
    }
}

