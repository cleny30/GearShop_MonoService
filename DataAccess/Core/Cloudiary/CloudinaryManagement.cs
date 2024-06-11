using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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

        public async Task<bool> Delete(string url)
        {
            var myAccount = new Account(
              _cloudinarySettings.CloudName, // Your Cloudinary cloud name
              _cloudinarySettings.ApiKey,    // Your Cloudinary API key
              _cloudinarySettings.ApiSecret  // Your Cloudinary API secret
          );

          var cloudinary = new Cloudinary(myAccount);
          string publicID = ExtractPublicIdFromUrl(url);
          // Set up the deletion parameters
          var deletionParams = new DeletionParams(publicID);

          // Delete the image from Cloudinary
          var deletionResult = await cloudinary.DestroyAsync(deletionParams);

          // Check if deletion was successful
            if (deletionResult.Result == "ok")
            {
                return true;
            }
            else
            {
                // Log or handle the failure scenario appropriately
                return false;
            }
        }

        public string ExtractPublicIdFromUrl(string url)
        {
            var uri = new Uri(url);
            var segments = uri.Segments;

            // Find the index of 'upload/' segment
            int uploadIndex = Array.IndexOf(segments, "upload/");

            if (uploadIndex != -1 && uploadIndex + 1 < segments.Length)
            {
                // Construct the path starting from 'upload/' segment
                var pathSegments = segments.Skip(uploadIndex + 1).TakeWhile(s => s.TrimEnd('/') != "");

                // Join the path segments to form the desired path
                string publicId = string.Join("", pathSegments);

                // Remove version number (v1717908943/) if present
                if (publicId.StartsWith("v") && publicId.Contains("/"))
                {
                    int versionIndex = publicId.IndexOf('/');
                    publicId = publicId.Substring(versionIndex + 1);
                }

                // Remove extension if present
                if (publicId.Contains("."))
                {
                    publicId = publicId.Substring(0, publicId.LastIndexOf('.'));
                }

                return publicId;
            }

            return null; // or throw exception if 'upload/' segment not found
        }
    }
}
