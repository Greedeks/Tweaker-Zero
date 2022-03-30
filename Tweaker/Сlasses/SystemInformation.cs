using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tweaker.Сlasses
{
    internal sealed class SystemInformation
    {
        internal static ImageSource _urlImage = default;
        private string _UserName = default;

        [DllImport("shell32.dll", EntryPoint = "#261", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void GetUserTilePath(string username, UInt32 whatever, StringBuilder picpath, int maxLength);

        private  string GetUserTilePath(string username)
        {
            var _sb = new StringBuilder(1000);
            GetUserTilePath(username, 0x80000000, _sb, _sb.Capacity);
            _UserName = username;
            return _sb.ToString();
        }

        internal  ImageSource SetImageUser()
        {
            ImageSource _imgSource = new BitmapImage(new Uri(GetUserTilePath(_UserName)));
            return _imgSource;
        }
    }
}
