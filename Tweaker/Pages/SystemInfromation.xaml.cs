using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class SystemInfromation : Page
    {
        public SystemInfromation()
        {
            InitializeComponent();
            UserAvatar.ImageSource = SystemInformation._urlImage;

        }

    }
}
