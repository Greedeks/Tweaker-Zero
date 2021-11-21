using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Tweaker
{
    internal sealed class CheackApplicationCopy
    {
        [DllImport("user32.dll")]
        private static extern bool _ShowWindow(IntPtr handle, int cmdShow);
        [DllImport("user32.dll")]
        private static extern int _SetForegroundWindow(IntPtr handle);

        private readonly static Mutex mutex = new Mutex(false, "Tweaker");

        internal void CheackAC()
        {
            if (!mutex.WaitOne(150, false))
            {
                using (Mutex mutex = new Mutex(false, @"Global\" + "Tweaker"))
                {
                    if (mutex.WaitOne(0, false))
                    {
                        new MessageForUser().ShowDialog();
                    }
                }
                string _processName = Process.GetCurrentProcess().ProcessName;
                Process process = Process.GetProcesses().Where(p => p.ProcessName == _processName).FirstOrDefault();
                if (process != null)
                {
                    IntPtr handle = process.MainWindowHandle;
                    _ShowWindow(handle, 1);
                    _SetForegroundWindow(handle);
                }
            }
        }
    }
}
