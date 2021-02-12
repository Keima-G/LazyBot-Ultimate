namespace LazyEvo
{
    using LazyEvo.Forms;
    using LazyEvo.Forms.Helpers;
    using LazyEvo.Plugins;
    using LazyLib;
    using LazyLib.Dialogs.UnhandledExceptionDlg;
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal static class Program
    {
        public static int AttachTo;

        private static void CheckDotNet()
        {
            try
            {
                DoCheckDotNet();
            }
            catch
            {
                MessageBox.Show("You do not have .Net 3.5 installed, LazyBot cannot work without it. Please install .Net 3.5 from microsoft");
                Environment.Exit(0);
            }
        }

        private static void DoCheckDotNet()
        {
            AppDomain.CurrentDomain.Load("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
        }

        [STAThread]
        private static void Main()
        {
            LazyLib.Dialogs.UnhandledExceptionDlg.UnhandledExceptionDlg dlg1 = new LazyLib.Dialogs.UnhandledExceptionDlg.UnhandledExceptionDlg {
                RestartApp = false
            };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CheckDotNet();
            string directoryName = new FileInfo(Application.ExecutablePath).DirectoryName;
            if (!Directory.Exists(directoryName + @"\Logs"))
            {
                Directory.CreateDirectory(directoryName + @"\Logs");
            }
            if (File.Exists(directoryName + @"\Logs\OldLogFile.txt"))
            {
                File.Delete(directoryName + @"\Logs\OldLogFile.txt");
            }
            if (File.Exists(directoryName + @"\Logs\LogFile.txt"))
            {
                File.Move(directoryName + @"\Logs\LogFile.txt", directoryName + @"\Logs\OldLogFile.txt");
            }
            LazyForms.Load();
            LazySettings.LoadSettings();
            ReloggerSettings.LoadSettings();
            if (LazySettings.FirstRun)
            {
                new Wizard().ShowDialog();
            }
            new Selector().ShowDialog();
            if (AttachTo == 0)
            {
                Environment.Exit(0);
            }
            Application.Run(LazyForms.MainForm);
        }

        private static void YouAreNotAllowed()
        {
            LazyEvo.Forms.Main.OneInstance = true;
        }
    }
}

