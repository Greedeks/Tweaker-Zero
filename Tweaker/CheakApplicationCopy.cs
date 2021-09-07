using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Tweaker
{
    public static class CheakApplicationCopy
    {
        [DllImport("user32.dll")]
        private static extern bool _ShowWindow(IntPtr handle, int cmdShow);
        [DllImport("user32.dll")]
        private static extern int _SetForegroundWindow(IntPtr handle);

        private static Mutex mutex = new Mutex(false, "Tweaker");

        public static void CheakAC()
        {
            #region Проверка запущенного приложения
            if (!mutex.WaitOne(150, false))
            {
                new MessageForUser().ShowDialog();
                string processName = Process.GetCurrentProcess().ProcessName;
                Process process = Process.GetProcesses().Where(p => p.ProcessName == processName).FirstOrDefault();
                if (process != null)
                {
                    IntPtr handle = process.MainWindowHandle;
                    _ShowWindow(handle, 1);
                    _SetForegroundWindow(handle);
                }
            }
            #endregion
        }
    }
}
