using Dashboard_Admin.SignalRManager;
using DataAccess.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using WPFStylingTest;

namespace Dashboard_Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string FilePath = "RememberMe.json";
        private static ServiceProvider serviceProvider;
        public static SignalRConnectionManager SignalRConnection { get; private set; }

        public App()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependencyInjection();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static async Task InitializeSignalRConnectionAsync(string url, string username = null)
        {
            SignalRConnection = new SignalRConnectionManager(url, username);
            await SignalRConnection.StartConnectionAsync();
        }

        public static T GetService<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window windowToOpen;

            if (File.Exists(FilePath))
            {
                string jsonContent = File.ReadAllText(FilePath);
                var settings = JsonSerializer.Deserialize<Settings>(jsonContent);

                if (settings != null && settings.RememberMe)
                {
                    // Open the main dashboard window if RememberMe is true
                    windowToOpen = new MainWindow();
                }
                else
                {
                    // Open the login page if RememberMe is false
                    windowToOpen = new Loginpage();
                }
            }
            else
            {
                // Open the login page if the file does not exist
                windowToOpen = new Loginpage();
            }

            // Set the window to open as the main window
            MainWindow = windowToOpen;
            windowToOpen.Show();
        }

    }

    public class Settings
    {
        public bool RememberMe { get; set; }
    }

}


