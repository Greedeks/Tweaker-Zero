using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
