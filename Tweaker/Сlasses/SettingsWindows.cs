using Microsoft.Win32;
using System.Windows;
using Tweaker.Pages;

namespace Tweaker.Сlasses
{
    internal class SettingsWindows
    {
        private readonly RegistryKey classesRootKey = Registry.ClassesRoot, currentUserKey = Registry.CurrentUser,
            localMachineKey = Registry.LocalMachine, usersKey = Registry.Users,
            currentConfigKey = Registry.CurrentConfig;  
        private readonly RegistryKey[] _key = new RegistryKey[100];

        internal void GetSettingConfidentiality(Confidentiality confidentiality)
        {
            #region Get
            //#1
            _key[0] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            //#2
            _key[1] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings");
            _key[2] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials");
            _key[3] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language");
            _key[4] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization");
            _key[5] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows");
            //#3
            _key[6] = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\WMI\Autologger\Diagtrack-Listener");
            _key[7] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Attachments");
            //#4

            #endregion


            #region Cheack/Set
            if (_key[0].GetValue("Enabled").ToString() != "0")
            {
                confidentiality.TButton1.State = true;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton1.State = false;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
            if (_key[1].GetValue("Enabled").ToString() != "0" || _key[2].GetValue("Enabled").ToString() != "0" ||
                _key[3].GetValue("Enabled").ToString() != "0" || _key[4].GetValue("Enabled").ToString() != "0" ||
                _key[5].GetValue("Enabled").ToString() != "0")
            {
                confidentiality. TButton2.State = true;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton2.State = false;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
            if (_key[6].GetValue("Start").ToString() != "0" || _key[7].GetValue("SaveZoneInformation").ToString() != "1")
            {
                confidentiality.TButton3.State = true;
                confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton3.State = false;
                confidentiality.Tweak3.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }
            #endregion
        }
    }
}
