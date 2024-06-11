using DataAccess.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WPFStylingTest;

namespace Dashboard_Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ServiceProvider serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureDependencyInjection();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }

    }

}
