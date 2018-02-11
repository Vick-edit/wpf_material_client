using System.Windows;
using System.Windows.Threading;
using WPF_client.Properties;
using WPF_client.Utilities;

namespace WPF_client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemSettingsHandler.RestoreSettings();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Settings.Default.Save();
            base.OnExit(e);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var mainException = e.Exception?.InnerException ?? e.Exception;

            MessageBox.Show("An unhandled exception just occurred:\r\n" + mainException?.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            ExceptionLogger.Log(e.Exception);
        }

    }
}
