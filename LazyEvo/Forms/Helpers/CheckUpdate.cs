namespace LazyEvo.Forms.Helpers
{
    using LazyLib;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    internal class CheckUpdate
    {
        private static string _ourDirectory;
        private static readonly string ExecutableName = Application.ExecutablePath;
        private static string _executableDirectoryName;
        private static FileInfo _executableFileInfo;

        public static void CheckForUpdate()
        {
            _executableFileInfo = new FileInfo(ExecutableName);
            _executableDirectoryName = _executableFileInfo.DirectoryName;
            _ourDirectory = _executableDirectoryName;
            try
            {
                Process process = new Process {
                    StartInfo = new ProcessStartInfo(_ourDirectory + @"\wyUpdate.exe", "-quickcheck -justcheck -noerr")
                };
                process.Start();
                process.WaitForExit();
                int exitCode = process.ExitCode;
                process.Close();
                if (exitCode == 2)
                {
                    MessageBox.Show("New update ready. Closing to update");
                    Process.Start(_ourDirectory + @"\wyUpdate.exe");
                    Environment.Exit(0);
                }
            }
            catch
            {
                Logging.Write("Could not start the updating program, cannot auto update: ", new object[0]);
            }
        }
    }
}

