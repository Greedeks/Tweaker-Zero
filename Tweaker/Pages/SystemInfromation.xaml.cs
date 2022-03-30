using System.Windows.Controls;
using Tweaker.Сlasses;

namespace Tweaker.Pages
{
    public partial class SystemInfromation : Page
    {
        GetSystemInformation getSystemInformation = new GetSystemInformation();
        public SystemInfromation()
        {
            InitializeComponent();
            UserAvatar.ImageSource = GetSystemInformation._urlImage;
            UserName.Text = getSystemInformation.NameUser();
        }

    }
}
