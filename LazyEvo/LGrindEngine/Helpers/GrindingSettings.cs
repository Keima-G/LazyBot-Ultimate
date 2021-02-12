namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyLib.Helpers;
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal class GrindingSettings
    {
        public const string SettingsName = @"\Settings\lazy_grinding.ini";
        public static string OurDirectory;
        public static bool Skin;
        public static bool WaitForLoot;
        public static bool StopLootOnFull;
        public static bool Loot;
        public static bool Mount;
        public static bool Jump;
        public static string Profile;
        public static int ApproachRange;
        public static bool SkipMobsWithAdds;
        public static int SkipAddsDistance;
        public static int SkipAddsCount;
        public static bool ShouldTrain;

        public static void LoadSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_grinding.ini");
            Skin = manager.GetBoolean("Grinding", "Skin", false);
            WaitForLoot = manager.GetBoolean("Grinding", "WaitForLoot", false);
            StopLootOnFull = manager.GetBoolean("Grinding", "StopLootOnFull", false);
            Loot = manager.GetBoolean("Grinding", "Loot", true);
            Mount = manager.GetBoolean("Grinding", "Mount", true);
            ApproachRange = manager.GetInt("Grinding", "ApproachRange", 40);
            Profile = manager.GetString("Grinding", "Profile", string.Empty);
            Jump = manager.GetBoolean("Grinding", "Jump", false);
            SkipMobsWithAdds = manager.GetBoolean("Grinding", "SkipMobsWithAdds", false);
            ShouldTrain = manager.GetBoolean("Grinding", "ShouldTrain", false);
            ShouldTrain = false;
            SkipAddsDistance = manager.GetInt("Grinding", "SkipAddsDistance", 20);
            SkipAddsCount = manager.GetInt("Grinding", "SkipAddsCount", 2);
        }

        public static void SaveSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_grinding.ini");
            manager.IniWriteValue("Grinding", "Profile", Profile);
            manager.IniWriteValue("Grinding", "Skin", Skin.ToString());
            manager.IniWriteValue("Grinding", "WaitForLoot", WaitForLoot.ToString());
            manager.IniWriteValue("Grinding", "StopLootOnFull", StopLootOnFull.ToString());
            manager.IniWriteValue("Grinding", "Loot", Loot.ToString());
            manager.IniWriteValue("Grinding", "Mount", Mount.ToString());
            manager.IniWriteValue("Grinding", "Jump", Jump.ToString());
            manager.IniWriteValue("Grinding", "ApproachRange", ApproachRange.ToString());
            manager.IniWriteValue("Grinding", "SkipMobsWithAdds", SkipMobsWithAdds.ToString());
            manager.IniWriteValue("Grinding", "SkipAddsDistance", SkipAddsDistance.ToString());
            manager.IniWriteValue("Grinding", "SkipAddsCount", SkipAddsCount.ToString());
            manager.IniWriteValue("Grinding", "ShouldTrain", ShouldTrain.ToString());
        }
    }
}

