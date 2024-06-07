using DataAccess.Core.Cloudiary;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Core.Configuration
{
    public class ConfigurationService
    {
        public CloudinarySettings CloudinarySettings { get; }

        public ConfigurationService()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            CloudinarySettings = new CloudinarySettings();
            configuration.GetSection("Cloudinary").Bind(CloudinarySettings);
        }
    }
}
