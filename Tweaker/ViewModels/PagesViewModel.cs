using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Tweaker.Pages;

namespace Tweaker.ViewModels
{
    internal class PagesViewModel : MainWindow
    {
        private Page Page_Confidentiality = new Confidentiality();

        public ICommand PageConfidentiality
        {
            get {
              return (ICommand)Page_Confidentiality;
            } 
        }

    }
}
