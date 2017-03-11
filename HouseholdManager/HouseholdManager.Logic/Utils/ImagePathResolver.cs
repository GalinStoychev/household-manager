using HouseholdManager.Logic.Contracts;
using System;
using System.IO;

namespace HouseholdManager.Logic.Utils
{
    public class ImagePathResolver : IImagePathResolver
    {
        private const string ImagesDirectory = @"Content\Images\";
        private const string HouseholdDefaultImage = "defaultHousehold.png";

        public object ConfigurationManager { get; private set; }

        public string ResolveTripsImageFilePath()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImagesDirectory, HouseholdDefaultImage);
            return filePath;
        }
    }
}
