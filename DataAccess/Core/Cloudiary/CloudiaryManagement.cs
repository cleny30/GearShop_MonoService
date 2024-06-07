using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Core.Cloudiary
{
    public class CloudiaryManagement
    {
        public async Task<string> Upload(string filePath, string folder)
        {

            var myAccount = new Account(
               "dklkzeill", // Your Cloudinary cloud name
               "827655834549677",    // Your Cloudinary API key
               "f9BGy89C7Wp_HVrnuitK8POPjlE"  // Your Cloudinary API secret
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
