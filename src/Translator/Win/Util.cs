using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Gekka.Language.Translator.Win
{
    class Util
    {
        //[System.Runtime.InteropServices.DllImport("User32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall, SetLastError = true)]
        //[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        //private static extern bool ShowWindow([System.Runtime.InteropServices.In] IntPtr hWnd, [System.Runtime.InteropServices.In] int nCmdShow);

        [System.Runtime.InteropServices.DllImport("shell32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern IntPtr CommandLineToArgvW([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)] string lpCmdLine, out uint pNumArgs);

        public static string[] SplitCommandLineArgs(string? commandLine)
        {
            if (commandLine == null) return new string[0];

            var argv = CommandLineToArgvW(commandLine, out var argc);
            if (argv == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception();
            }

            try
            {
                List<string> list = new List<string>();
                for (var i = 0; i < argc; i++)
                {
                    var p = System.Runtime.InteropServices.Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    var u = System.Runtime.InteropServices.Marshal.PtrToStringUni(p);
                    if (u != null)
                    {
                        list.Add(u);
                    }
                }

                return list.ToArray();
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.FreeHGlobal(argv);
            }
        }



        public static IEnumerable<(System.Diagnostics.Process process, IList<string> args)> GetProcessAndCommandLineArgs(string processName)
        {
            using (var mc = new System.Management.ManagementClass("Win32_Process"))
            {
                foreach (var mo in mc.GetInstances())
                {
                    try
                    {
                        if (mo["Name"] is string name && string.Equals(name.ToLower(), processName, StringComparison.OrdinalIgnoreCase))
                        {
                            var pid = (uint)mo["ProcessId"];
                            var process = System.Diagnostics.Process.GetProcessById((int)pid);
                            var arg = mo["CommandLine"] as string;

                            string[] parts = Win.Util.SplitCommandLineArgs(arg);

                            yield return (process, parts);
                        }
                    }
                    finally
                    {
                        mo.Dispose();
                    }
                }
            }
        }

        /// <summary>既存のドライバプロセスを終了させる</summary>
        public static void CloseExistingDriver(string processName)
        {
            var cp = System.Diagnostics.Process.GetCurrentProcess();

            Windows.Win32.PInvoke.ShowWindow(new Windows.Win32.Foundation.HWND(cp.MainWindowHandle), Windows.Win32.UI.WindowsAndMessaging.SHOW_WINDOW_CMD.SW_MINIMIZE);

            foreach (var p in System.Diagnostics.Process.GetProcessesByName(processName).OrderBy(_ => _.ProcessName))//"chromedriver"
            {
                if (p.Id != cp.Id)
                {
                    if (p.MainWindowHandle != IntPtr.Zero)
                    {
                        p.CloseMainWindow();
                    }
                    else
                    {
                        try
                        {
                            using var searcher = new System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT ParentProcessId FROM Win32_Process WHERE ProcessId=" + p.Id);
                            foreach (System.Management.ManagementObject mo in searcher.Get())
                            {
                                using (mo)
                                {
                                    var s = mo["ParentProcessId"]?.ToString();
                                    if (int.TryParse(s, out var parentPid))
                                    {
                                        var parentProcess = System.Diagnostics.Process.GetProcessById(parentPid);
                                        if (parentProcess.MainWindowHandle != IntPtr.Zero)
                                        {
                                            p.CloseMainWindow();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
    }
}
