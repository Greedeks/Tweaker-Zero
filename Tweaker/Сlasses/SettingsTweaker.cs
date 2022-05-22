using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Tweaker.Сlasses
{
    internal sealed class SettingsTweaker
    {
        private static bool _notification = default;
        private static bool _notificationSound = default;
        private static byte _notificationVolume = default;
        private static bool _topMost = default;

        internal static bool Notification { get => _notification; set => _notification = value; }
        internal static bool NotificationSound { get => _notificationSound; set => _notificationSound = value; }
        internal static byte NotificationVolume { get => _notificationVolume; set => _notificationVolume = value; }
        internal static bool TopMost { get => _topMost; set => _topMost = value; }

        internal void CheckStettingsTweaker()
        {
            RegistryKey _registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Tweaker Zero");
            if (_registryKey == null)
            {
                _registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Tweaker Zero");
                _registryKey.SetValue("Notification", "True", RegistryValueKind.String);
                _registryKey.SetValue("NotificationSound", "True", RegistryValueKind.String);
                _registryKey.SetValue("NotificationVolume", "100", RegistryValueKind.String);
                _registryKey.SetValue("TopMost", "False", RegistryValueKind.String);
            }
            else
                Update();
        }

        internal void Update()
        {
            RegistryKey _registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Tweaker Zero");
            Parallel.Invoke(() =>
            {
                _registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Tweaker Zero");
                Notification = bool.Parse(_registryKey.GetValue("Notification").ToString());
                NotificationSound = bool.Parse(_registryKey.GetValue("NotificationSound").ToString());
                NotificationVolume = byte.Parse(_registryKey.GetValue("NotificationVolume").ToString());
                TopMost = bool.Parse(_registryKey.GetValue("TopMost").ToString());
            });
        }

        internal void ChangeNotificationState(in bool _state)
        {
            RegistryKey _registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Tweaker Zero");
            _registryKey.SetValue("Notification", _state.ToString(), RegistryValueKind.String);
            Notification = bool.Parse(_registryKey.GetValue("Notification").ToString());
        }

        internal void ChangeNotificationSoundState(in bool _state)
        {
            RegistryKey _registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Tweaker Zero");
            _registryKey.SetValue("NotificationSound", _state.ToString(), RegistryValueKind.String);
            NotificationSound = bool.Parse(_registryKey.GetValue("NotificationSound").ToString());
        }

        internal void ChangeNotificationVolume(in byte _value)
        {
            RegistryKey _registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Tweaker Zero");
            _registryKey.SetValue("NotificationVolume", _value.ToString(), RegistryValueKind.String);
            NotificationVolume = byte.Parse(_registryKey.GetValue("NotificationVolume").ToString());
        }

        internal void ChangeTopMostState(in bool _state)
        {
            RegistryKey _registryKey = Registry.CurrentUser.CreateSubKey(@"Software\Tweaker Zero");
            _registryKey.SetValue("TopMost", _state.ToString(), RegistryValueKind.String);
            TopMost = bool.Parse(_registryKey.GetValue("TopMost").ToString());
        }

        internal void SelfRemoval()
        {
            Registry.CurrentUser.DeleteSubKey(@"Software\Tweaker Zero", true);
            Application.Current.Shutdown();
            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + (new FileInfo((new Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath)).Name + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }
    }
}
