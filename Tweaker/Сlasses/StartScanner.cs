using System.Threading.Tasks;

namespace Tweaker.Сlasses
{
    internal sealed class StartScanner
    {
        private readonly SettingsWindows _settingsWindows = new SettingsWindows();
        private readonly GetSystemInformation _getsystemInformation = new GetSystemInformation();
        private readonly ApplicationsSystem _applicationsSystem = new ApplicationsSystem();

        internal void ScantheSystem()
        {
            Parallel.Invoke(
            () => { GetSystemInformation._urlImage = _getsystemInformation.SetImageUser(); }, 
            () => { _getsystemInformation.GetInormationPC(); }, 
            () => { _settingsWindows.TaskCheckStateConfidentiality(); }, 
            () => { _applicationsSystem.CheckInstalledApps(); } 
            );
        }
    }
}
