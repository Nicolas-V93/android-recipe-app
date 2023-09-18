using CloudinaryDotNet;
using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;

namespace Imi.Project.Mobile.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        public Cloudinary GetCloudinaryInstance()
        {
            var cloudinaryUrl = UserSecretsHelper.Instance["CloudinaryUrl"];

            Cloudinary cloudinary = new Cloudinary(cloudinaryUrl);
            cloudinary.Api.Secure = true;
            return cloudinary;
        }
    }
}
