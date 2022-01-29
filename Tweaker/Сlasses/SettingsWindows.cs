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
        Confidentiality confidentiality = new Confidentiality();

        internal void GetSettingConfidentiality()
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
            
        }
    }
}
