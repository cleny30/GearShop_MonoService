using CloudinaryDotNet;
using DataAccess.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Core.Cloudiary
{
    public class CloudinaryManagement
    {
        private readonly CloudinarySettings _cloudinarySettings;

        public CloudinaryManagement()
        {
            var configService = new ConfigurationService();
            _cloudinarySettings = configService.CloudinarySettings;
        }
        public async Task<string> Upload(string filePath, string folder)
        {

            var myAccount = new Account(
               _cloudinarySettings.CloudName, // Your Cloudinary cloud name
               _cloudinarySettings.ApiKey,    // Your Cloudinary API key
               _cloudinarySettings.ApiSecret  // Your Cloudinary API secret
           );

            var cloudinary = new Cloudinary(myAccount);

            cloudinary.Api.Secure = true;

            // Set up the image upload parameters
            var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(filePath),
                Folder = folder // The folder path where you want to upload the image
            };

            // Upload the image to Cloudinary
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            // Set the ImageUrl property to the secure URL
            return uploadResult.SecureUrl.ToString();
        }
    }
}
