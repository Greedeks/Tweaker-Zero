using System.Threading.Tasks;
using System.Windows;
using Tweaker.Windows;

namespace Tweaker.Сlasses
{
    internal class ToastNotification
    {
        private NotificationWindow notificationWindow = new NotificationWindow();

        internal void Show(string _Tittle, string _Text, byte _Action)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Parallel.Invoke(() =>
                {
                    if (notificationWindow.IsLoaded == false)
                    {
                        notificationWindow = new NotificationWindow
                        {
                            AddTitle = _Tittle,
                            AddText = _Text,
                            ActionChoice = _Action,
                        };
                        notificationWindow.Show();
                    }
                });
            });
        }
    }
}
