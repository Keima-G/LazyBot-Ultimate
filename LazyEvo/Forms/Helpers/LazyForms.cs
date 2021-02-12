namespace LazyEvo.Forms.Helpers
{
    using LazyEvo.Forms;
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal class LazyForms
    {
        internal static Selector ProcessSelForm;
        internal static Main MainForm;
        internal static Setup SetupForm;
        internal static string OurDirectory;

        internal static void Load()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            ProcessSelForm = new Selector();
            MainForm = new Main();
            SetupForm = new Setup();
        }
    }
}

