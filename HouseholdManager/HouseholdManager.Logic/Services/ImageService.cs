using HouseholdManager.Common.Constants;
using HouseholdManager.Logic.Contracts;
using System;

namespace HouseholdManager.Logic.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageLoader imageLoader;
        private readonly IImagePathResolver imagePathResolver;

        public ImageService(IImageLoader imageLoader, IImagePathResolver imagePathResolver)
        {
            if (imageLoader == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "imageloader"));
            }

            if (imagePathResolver == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "imagePathResolver"));
            }

            this.imageLoader = imageLoader;
            this.imagePathResolver = imagePathResolver;
        }

        public byte[] LoadHouseholdDefaultImage()
        {
            var path = this.imagePathResolver.ResolveTripsImageFilePath();
            var image = this.imageLoader.LoadImage(path);

            return image;
        }
    }
}
