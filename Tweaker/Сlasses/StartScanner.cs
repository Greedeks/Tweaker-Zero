using System.Threading.Tasks;

namespace Tweaker.Сlasses
{
    internal sealed class StartScanner
    {
        private readonly CheckApplicationCopy _checkApplicationCopy = new CheckApplicationCopy();
        private readonly CheckWindowsVersion _checkWindowsVersion = new CheckWindowsVersion();
        internal readonly ToastNotification _toastNotification = new ToastNotification();

        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private readonly GetSystemInformation _getsystemInformation = new GetSystemInformation();
        private readonly ApplicationsSystem _applicationsSystem = new ApplicationsSystem();


        internal void BeforeLoadingCheck()
        {
            Parallel.Invoke(() => { _checkApplicationCopy.CheckAC(); });
            Parallel.Invoke(() => { _checkWindowsVersion.CheckVersion(); });
            Parallel.Invoke(() => { _toastNotification.Load(); });
        }

        internal void ScantheSystem()
        {
            Parallel.Invoke(
            () => { GetSystemInformation._urlImage = _getsystemInformation.SetImageUser(); },
            () => { _getsystemInformation.GetInormationPC(); },
            () => { _settingsWindows.TaskCheckStateConfidentiality(); },
            () => { _settingsWindows.TaskCheckStateSystem(); },
            () => { _settingsWindows.ProtocolCheckStateSystem(); },
            () => { _settingsWindows.VerificationWindows(); },
            () => { _applicationsSystem.CheckInstalledApps(); }
            );
        }
    }
}
