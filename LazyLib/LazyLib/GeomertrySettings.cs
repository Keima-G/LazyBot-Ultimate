namespace LazyLib
{
    using LazyLib.Helpers;
    using System;
    using System.IO;
    using System.Windows.Forms;

    public class GeomertrySettings
    {
        public const string SettingsName = @"\Settings\geometry.ini";
        public static string OurDirectory;
        public static string MainGeometry;
        public static string RotatorStatus;
        public static string RotationForm;
        public static string ProcessSelector;
        public static string RotatorForm;
        public static string RuleEditor;
        public static string ScriptEditor;

        public static void LoadSettings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            IniManager manager = new IniManager(OurDirectory + @"\Settings\geometry.ini");
            MainGeometry = manager.GetString("Geometry", "MainGeometry", string.Empty);
            RotatorStatus = manager.GetString("Geometry", "RotatorStatus", string.Empty);
            RotationForm = manager.GetString("Geometry", "RotationForm", string.Empty);
            ProcessSelector = manager.GetString("Geometry", "ProcessSelector", string.Empty);
            RotatorForm = manager.GetString("Geometry", "RotatorForm", string.Empty);
            RuleEditor = manager.GetString("Geometry", "RuleEditor", string.Empty);
            ScriptEditor = manager.GetString("Geometry", "ScriptEditor", string.Empty);
        }

        public static void Save()
        {
            IniManager manager = new IniManager(OurDirectory + @"\Settings\geometry.ini");
            manager.IniWriteValue("Geometry", "MainGeometry", MainGeometry);
            manager.IniWriteValue("Geometry", "RotatorStatus", RotatorStatus);
            manager.IniWriteValue("Geometry", "RotationForm", RotationForm);
            manager.IniWriteValue("Geometry", "ProcessSelector", ProcessSelector);
            manager.IniWriteValue("Geometry", "RotatorForm", RotatorForm);
            manager.IniWriteValue("Geometry", "RuleEditor", RuleEditor);
            manager.IniWriteValue("Geometry", "ScriptEditor", ScriptEditor);
        }
    }
}

