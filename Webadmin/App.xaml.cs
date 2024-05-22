using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Webadmin.Managers;
using Webadmin.View;
using Webadmin.ViewModel;

namespace Webadmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            WindowManager.ShowWindow(WindowType.Login);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
        }
    }

}
