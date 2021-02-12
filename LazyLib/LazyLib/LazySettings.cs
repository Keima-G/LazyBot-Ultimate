namespace LazyLib
{
    using LazyLib.Helpers;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class LazySettings
    {
        public const string SettingsName = @"\Settings\lazy_evo.ini";
        public static string OurDirectory;
        public static int Latency;
        public static bool DebugLog;
        public static bool DebugMode;
        public static bool ShouldMail;
        public static string MailTo;
        public static string Password;
        public static string UserName;
        public static string CharacterName;
        public static bool BackgroundMode;
        public static bool HookMouse;
        public static bool SetupUseHotkeys;
        public static bool StopAfterBool;
        public static string StopAfter;
        public static string LogOutOnFollowTime;
        public static bool SoundFollow;
        public static bool SoundWhisper;
        public static bool SoundStop;
        public static bool Shutdown;
        public static bool LogoutOnFollow;
        public static bool CombatBoolEat;
        public static bool CombatBoolDrink;
        public static string CombatEatAt;
        public static string CombatDrinkAt;
        public static bool UseCtm;
        public static string KeysGroundMountBar;
        public static string KeysGroundMountKey;
        public static string KeysAttack1Bar;
        public static string KeysAttack1Key;
        public static string KeysEatBar;
        public static string KeysEatKey;
        public static string KeysDrinkBar;
        public static string KeysDrinkKey;
        public static string KeysMoteExtractorBar;
        public static string KeysMoteExtractorKey;
        public static string KeysStafeLeftKeyText;
        public static string KeysStafeRightKeyText;
        public static string KeysInteractKeyText;
        public static string KeysInteractTargetText;
        public static string KeysTargetLastTargetText;
        public static bool ShouldVendor;
        public static bool ShouldRepair;
        public static bool SellCommon;
        public static bool SellUncommon;
        public static bool SellPoor;
        public static bool SellRare;
        public static bool SellEpic;
        public static bool SellLegendary;
        public static string FreeBackspace;
        public static string SelectedEngine;
        public static string SelectedCombat;
        public static bool FirstRun;
        public static bool MacroForMail;
        public static string KeysMailMacroBar;
        public static string KeysMailMacroKey;
        public static LazyLanguage Language;
        public static string TelnetPassword;
        public static int TelnetPort;
        public static int PullControlDistance;
        public static bool GroundGather;
        public static bool JustFishing;
        public static int GroundGatherDistance;

        public static void LoadSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_evo.ini");
            SelectedEngine = manager.GetString("Engine", "Selected", string.Empty);
            SelectedCombat = manager.GetString("Combat", "Selected", string.Empty);
            FirstRun = manager.GetBoolean("Config", "FirstRun", true);
            DebugMode = manager.GetBoolean("Config", "DebugMode", false);
            Password = manager.GetString("Config", "UserName", string.Empty);
            UserName = manager.GetString("Config", "Password", string.Empty);
            CharacterName = manager.GetString("Config", "CharcterName", string.Empty);
            BackgroundMode = manager.GetBoolean("Config", "BackgroundMode", false);
            HookMouse = manager.GetBoolean("Config", "HookMouse", false);
            SetupUseHotkeys = manager.GetBoolean("Config", "UseHotkeys", false);
            StopAfterBool = manager.GetBoolean("Config", "StopAfter", false);
            StopAfter = manager.GetString("Config", "StopAfterTime", "120");
            LogOutOnFollowTime = manager.GetString("Config", "LogoutOnFollowTime", "2");
            SoundFollow = manager.GetBoolean("Config", "FollowSound", true);
            SoundWhisper = manager.GetBoolean("Config", "WhisperSound", true);
            SoundStop = manager.GetBoolean("Config", "SoundStop", true);
            Shutdown = manager.GetBoolean("Config", "ShutdownComputer", false);
            LogoutOnFollow = manager.GetBoolean("Config", "LogoutOnFollow", false);
            UseCtm = manager.GetBoolean("Config", "UseCtm", false);
            DebugLog = manager.GetBoolean("Config", "DebugLog", false);
            Latency = manager.GetInt("Config", "Latency", 0);
            Language = (LazyLanguage) manager.GetInt("Config", "Language", 0);
            CombatBoolEat = manager.GetBoolean("Combat", "CBEat", true);
            CombatBoolDrink = manager.GetBoolean("Combat", "CBDrink", true);
            CombatEatAt = manager.GetString("Combat", "COEat", "0");
            CombatDrinkAt = manager.GetString("Combat", "CODrink", "0");
            PullControlDistance = manager.GetInt("PullController", "DistanceControl", 0);
            GroundGather = manager.GetBoolean("SpecialGather", "UseGroundMount", true);
            GroundGatherDistance = manager.GetInt("SpecialGather", "GatherDistance", 0);
            JustFishing = manager.GetBoolean("Fishing", "ImJustFishing", false);
            KeysGroundMountBar = manager.GetString("Keys", "GroundMountBar", "1");
            KeysGroundMountKey = manager.GetString("Keys", "GroundMountKey", "1");
            KeysAttack1Bar = manager.GetString("Keys", "Attack1Bar", "1");
            KeysAttack1Key = manager.GetString("Keys", "Attack1Key", "1");
            KeysEatBar = manager.GetString("Keys", "EatBar", "1");
            KeysEatKey = manager.GetString("Keys", "EatKey", "1");
            KeysDrinkBar = manager.GetString("Keys", "DrinkBar", "1");
            KeysDrinkKey = manager.GetString("Keys", "DrinkKey", "1");
            KeysMoteExtractorBar = manager.GetString("Keys", "MoteBar", "1");
            KeysMoteExtractorKey = manager.GetString("Keys", "MoteKey", "1");
            KeysStafeLeftKeyText = manager.GetString("Keys", "StafeLeftKeyText", "Q");
            KeysStafeRightKeyText = manager.GetString("Keys", "StafeRightKeyText", "E");
            KeysInteractKeyText = manager.GetString("Keys", "InteractText", "U");
            KeysInteractTargetText = manager.GetString("Keys", "InteractTargetText", "P");
            KeysTargetLastTargetText = manager.GetString("Keys", "KeysTargetLastTargetText", "G");
            ShouldMail = manager.GetBoolean("Mail", "ShouldMail", false);
            MailTo = manager.GetString("Mail", "MailTo", string.Empty);
            MacroForMail = false;
            KeysMailMacroBar = manager.GetString("Mail", "KeysMailMacroBar", "1");
            KeysMailMacroKey = manager.GetString("Mail", "KeysMailMacroKey", "1");
            ShouldVendor = manager.GetBoolean("Vendor", "ShouldVendor", false);
            ShouldRepair = manager.GetBoolean("Vendor", "ShouldRepair", false);
            SellCommon = manager.GetBoolean("Vendor", "SellCommon", false);
            SellUncommon = manager.GetBoolean("Vendor", "SellUncommon", false);
            SellPoor = manager.GetBoolean("Vendor", "SellPoor", false);
            FreeBackspace = manager.GetString("Vendor", "FreeBackspace", "2");
        }

        public static void SaveSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\lazy_evo.ini");
            manager.IniWriteValue("PullController", "DistanceControl", PullControlDistance);
            manager.IniWriteValue("Engine", "Selected", SelectedEngine);
            manager.IniWriteValue("Combat", "Selected", SelectedCombat);
            manager.IniWriteValue("Config", "FirstRun", FirstRun);
            manager.IniWriteValue("Config", "UserName", Password);
            manager.IniWriteValue("Config", "Password", UserName);
            manager.IniWriteValue("Config", "BackgroundMode", BackgroundMode);
            manager.IniWriteValue("Config", "HookMouse", HookMouse);
            manager.IniWriteValue("Config", "UseHotkeys", SetupUseHotkeys);
            manager.IniWriteValue("Config", "StopAfter", StopAfterBool);
            manager.IniWriteValue("Config", "StopAfterTime", StopAfter);
            manager.IniWriteValue("Config", "FollowSound", SoundFollow);
            manager.IniWriteValue("Config", "WhisperSound", SoundWhisper);
            manager.IniWriteValue("Config", "SoundStop", SoundStop);
            manager.IniWriteValue("Config", "ShutdownComputer", Shutdown);
            manager.IniWriteValue("Config", "LogoutOnFollow", LogoutOnFollow);
            manager.IniWriteValue("Config", "LogoutOnFollowTime", LogOutOnFollowTime);
            manager.IniWriteValue("Config", "UseCtm", UseCtm);
            manager.IniWriteValue("Config", "DebugLog", DebugLog);
            manager.IniWriteValue("Config", "Latency", Latency);
            manager.IniWriteValue("Config", "Language", Convert.ToInt32(Language));
            manager.IniWriteValue("Combat", "CBEat", CombatBoolEat);
            manager.IniWriteValue("Combat", "CBDrink", CombatBoolDrink);
            manager.IniWriteValue("Combat", "COEat", CombatEatAt);
            manager.IniWriteValue("Combat", "CODrink", CombatDrinkAt);
            manager.IniWriteValue("Keys", "GroundMountBar", KeysGroundMountBar);
            manager.IniWriteValue("Keys", "GroundMountKey", KeysGroundMountKey);
            manager.IniWriteValue("Keys", "Attack1Bar", KeysAttack1Bar);
            manager.IniWriteValue("Keys", "Attack1Key", KeysAttack1Key);
            manager.IniWriteValue("Keys", "EatBar", KeysEatBar);
            manager.IniWriteValue("Keys", "EatKey", KeysEatKey);
            manager.IniWriteValue("Keys", "DrinkBar", KeysDrinkBar);
            manager.IniWriteValue("Keys", "DrinkKey", KeysDrinkKey);
            manager.IniWriteValue("Keys", "MoteBar", KeysMoteExtractorBar);
            manager.IniWriteValue("Keys", "MoteKey", KeysMoteExtractorKey);
            manager.IniWriteValue("Keys", "InteractText", KeysInteractKeyText);
            manager.IniWriteValue("Keys", "InteractTargetText", KeysInteractTargetText);
            manager.IniWriteValue("Keys", "StafeLeftKeyText", KeysStafeLeftKeyText);
            manager.IniWriteValue("Keys", "StafeRightKeyText", KeysStafeRightKeyText);
            manager.IniWriteValue("Keys", "KeysTargetLastTargetText", KeysTargetLastTargetText);
            manager.IniWriteValue("Mail", "ShouldMail", ShouldMail);
            manager.IniWriteValue("Mail", "MailTo", MailTo);
            manager.IniWriteValue("Mail", "MacroForMail", MacroForMail);
            manager.IniWriteValue("Mail", "KeysMailMacroBar", KeysMailMacroBar);
            manager.IniWriteValue("Mail", "KeysMailMacroKey", KeysMailMacroKey);
            manager.IniWriteValue("Vendor", "ShouldVendor", ShouldVendor);
            manager.IniWriteValue("Vendor", "ShouldRepair", ShouldRepair);
            manager.IniWriteValue("Vendor", "SellCommon", SellCommon);
            manager.IniWriteValue("Vendor", "SellUncommon", SellUncommon);
            manager.IniWriteValue("Vendor", "SellPoor", SellPoor);
            manager.IniWriteValue("Vendor", "FreeBackspace", FreeBackspace);
        }

        public enum LazyLanguage
        {
            Unknown,
            English,
            Russian,
            German,
            French,
            Spanish
        }
    }
}

