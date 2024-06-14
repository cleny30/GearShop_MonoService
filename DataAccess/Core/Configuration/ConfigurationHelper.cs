using Microsoft.Extensions.Configuration;

namespace DataAccess.Core.Configuration
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
