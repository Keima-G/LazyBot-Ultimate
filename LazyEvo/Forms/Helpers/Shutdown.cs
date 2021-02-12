namespace LazyEvo.Forms.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Management;

    internal class Shutdown
    {
        private static bool FindAndKillProcess(string name)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith(name))
                {
                    process.Kill();
                    return true;
                }
            }
            return false;
        }

        internal static void ShutDownComputer()
        {
            FindAndKillProcess("Wow");
            ManagementClass class2 = new ManagementClass("Win32_OperatingSystem");
            class2.Get();
            class2.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject methodParameters = class2.GetMethodParameters("Win32Shutdown");
            methodParameters["Flags"] = "1";
            methodParameters["Reserved"] = "0";
            foreach (ManagementObject obj3 in class2.GetInstances())
            {
                obj3.InvokeMethod("Win32Shutdown", methodParameters, null);
            }
            Environment.Exit(0);
        }
    }
}

