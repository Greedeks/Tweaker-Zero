using System;
using System.Management;

namespace Tweaker.Сlasses
{
    internal class CheckWindowsVersion
    {
        private string _version = default;
        private string GetVerison()
        {
            foreach (var managementObj in new ManagementObjectSearcher("root\\cimv2", "select Caption from Win32_OperatingSystem").Get())
                _version = Convert.ToString(managementObj["Caption"]).Substring(Convert.ToString(managementObj["Caption"]).IndexOf('W') + 8);
            return _version;
        }

        internal void CheckVersion()
        {
            GetVerison();
            if (_version.Substring(0, _version.LastIndexOf(' ')) != "10" || _version.Substring(0, _version.LastIndexOf(' ')) != "11")
            {
                new MessageForUser("К сожалению ваша версия Windows, не поддерживается. Запуск будет отменен!").ShowDialog();
            }
        }
    }
}
