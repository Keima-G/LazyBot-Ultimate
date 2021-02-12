namespace LazyEvo.Plugins.RotationPlugin
{
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    internal class RotationSettings
    {
        public const string SettingsName = @"\Settings\rotation.ini";
        public static string LoadedRotationManager;
        public static List<KeysData> KeysList = new List<KeysData>();

        private static void LoadKeys()
        {
            KeysList.Add(new KeysData("Back", 8));
            KeysList.Add(new KeysData("Tab", 9));
            KeysList.Add(new KeysData("Enter", 13));
            KeysList.Add(new KeysData("ESC", 0x1b));
            KeysList.Add(new KeysData("Space", 0x20));
            KeysList.Add(new KeysData("PAGE UP", 0x21));
            KeysList.Add(new KeysData("Page Down", 0x22));
            KeysList.Add(new KeysData("END", 0x23));
            KeysList.Add(new KeysData("Home", 0x24));
            KeysList.Add(new KeysData("Left", 0x25));
            KeysList.Add(new KeysData("Up", 0x26));
            KeysList.Add(new KeysData("Right", 0x27));
            KeysList.Add(new KeysData("Down", 40));
            KeysList.Add(new KeysData("PrintScreen", 0x2c));
            KeysList.Add(new KeysData("Insert", 0x2d));
            KeysList.Add(new KeysData("D 0", 0x30));
            KeysList.Add(new KeysData("D 1", 0x31));
            KeysList.Add(new KeysData("D 2", 50));
            KeysList.Add(new KeysData("D 3", 0x33));
            KeysList.Add(new KeysData("D 4", 0x34));
            KeysList.Add(new KeysData("D 5", 0x35));
            KeysList.Add(new KeysData("D 6", 0x36));
            KeysList.Add(new KeysData("D 7", 0x37));
            KeysList.Add(new KeysData("D 8", 0x38));
            KeysList.Add(new KeysData("D 9", 0x39));
            KeysList.Add(new KeysData("A", 0x41));
            KeysList.Add(new KeysData("B", 0x42));
            KeysList.Add(new KeysData("C", 0x43));
            KeysList.Add(new KeysData("D", 0x44));
            KeysList.Add(new KeysData("E", 0x45));
            KeysList.Add(new KeysData("F", 70));
            KeysList.Add(new KeysData("G", 0x47));
            KeysList.Add(new KeysData("H", 0x48));
            KeysList.Add(new KeysData("I", 0x49));
            KeysList.Add(new KeysData("J", 0x4a));
            KeysList.Add(new KeysData("K", 0x4b));
            KeysList.Add(new KeysData("L", 0x4c));
            KeysList.Add(new KeysData("M", 0x4d));
            KeysList.Add(new KeysData("N", 0x4e));
            KeysList.Add(new KeysData("O", 0x4f));
            KeysList.Add(new KeysData("P", 80));
            KeysList.Add(new KeysData("Q", 0x51));
            KeysList.Add(new KeysData("R", 0x52));
            KeysList.Add(new KeysData("S", 0x53));
            KeysList.Add(new KeysData("T", 0x54));
            KeysList.Add(new KeysData("U", 0x55));
            KeysList.Add(new KeysData("V", 0x56));
            KeysList.Add(new KeysData("W", 0x57));
            KeysList.Add(new KeysData("X", 0x58));
            KeysList.Add(new KeysData("Y", 0x59));
            KeysList.Add(new KeysData("Z", 90));
            KeysList.Add(new KeysData("NumPad 0", 0x60));
            KeysList.Add(new KeysData("NumPad 1", 0x61));
            KeysList.Add(new KeysData("NumPad 2", 0x62));
            KeysList.Add(new KeysData("NumPad 3", 0x63));
            KeysList.Add(new KeysData("NumPad 4", 100));
            KeysList.Add(new KeysData("NumPad 5", 0x65));
            KeysList.Add(new KeysData("NumPad 6", 0x66));
            KeysList.Add(new KeysData("NumPad 7", 0x67));
            KeysList.Add(new KeysData("NumPad 8", 0x68));
            KeysList.Add(new KeysData("NumPad 9", 0x69));
            KeysList.Add(new KeysData("*", 0x6a));
            KeysList.Add(new KeysData("+", 0x6b));
            KeysList.Add(new KeysData("-", 0x6d));
            KeysList.Add(new KeysData("Decimal", 110));
            KeysList.Add(new KeysData("/", 0x6f));
            KeysList.Add(new KeysData("F1", 0x70));
            KeysList.Add(new KeysData("F2", 0x71));
            KeysList.Add(new KeysData("F3", 0x72));
            KeysList.Add(new KeysData("F4", 0x73));
            KeysList.Add(new KeysData("F5", 0x74));
            KeysList.Add(new KeysData("F6", 0x75));
            KeysList.Add(new KeysData("F7", 0x76));
            KeysList.Add(new KeysData("F8", 0x77));
            KeysList.Add(new KeysData("F9", 120));
            KeysList.Add(new KeysData("F10", 0x79));
            KeysList.Add(new KeysData("F11", 0x7a));
            KeysList.Add(new KeysData("F12", 0x7b));
        }

        public static void LoadSettings()
        {
            LoadedRotationManager = new IniManager(new FileInfo(Application.ExecutablePath).DirectoryName + @"\Settings\rotation.ini").GetString("Config", "LoadedRotationManager", string.Empty);
            LoadKeys();
        }

        public static void SaveSettings()
        {
            new IniManager(new FileInfo(Application.ExecutablePath).DirectoryName + @"\Settings\rotation.ini").IniWriteValue("Config", "LoadedRotationManager", LoadedRotationManager);
        }
    }
}

