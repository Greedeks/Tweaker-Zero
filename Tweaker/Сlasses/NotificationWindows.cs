using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.IO;
namespace Tweaker.Сlasses
{
    internal sealed class NotificationWindows
    {
        internal void ShowNotification(in string _textNotification)
        {
            ToastNotificationManagerCompat.History.Clear();

            string _imageUri = Path.GetFullPath(@"C:\All My\Tweaker\Tweaker\Images\Tweaker.png");
            new ToastContentBuilder()
                .AddText("Внимание")
                .AddText(_textNotification)
                .AddAppLogoOverride(new Uri(_imageUri))
                .Show();
        }
    }
}
