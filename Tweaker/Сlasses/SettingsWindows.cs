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
        internal readonly RegistryKey[] key = new RegistryKey[100];

        public void GetSettingConfidentiality(Confidentiality confidentiality)
        {
            //#1
            key[0] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\AdvertisingInfo");
            //#2
            key[1] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings");
            key[2] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials");
            key[3] = currentUserKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language");
            key[4] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization");
            key[5] = currentUserKey.OpenSubKey(@"SOFTWARE\\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows");
            //#3





            if (key[0].GetValue("Enabled").ToString() != "0")
            {
                confidentiality.TButton1.State = true;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton1.State = false;
                confidentiality.Tweak1.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

            if (key[1].GetValue("Enabled").ToString() != "0" || key[2].GetValue("Enabled").ToString() != "0" ||
                key[3].GetValue("Enabled").ToString() != "0" || key[4].GetValue("Enabled").ToString() != "0" ||
                key[5].GetValue("Enabled").ToString() != "0")
            {
                confidentiality. TButton2.State = true;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_ON"];
            }
            else
            {
                confidentiality.TButton2.State = false;
                confidentiality.Tweak2.Style = (Style)Application.Current.Resources["Tweaks_OFF"];
            }

        }
    }
}
