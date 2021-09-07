using System;
using System.Windows;

namespace Tweaker
{
    public partial class App : Application
    {
        public App() => AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Exception);

        static void Exception(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show(e.ToString());
        }
    }
}
