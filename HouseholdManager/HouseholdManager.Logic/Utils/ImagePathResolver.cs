using HouseholdManager.Logic.Contracts;
using System;
using System.IO;

namespace HouseholdManager.Logic.Utils
{
    public class ImagePathResolver : IImagePathResolver
    {
        private const string ImagesDirectory = @"Content\Images\";
        private const string HouseholdDefaultImageAppSetting = "HouseholdDefaultImage";

        public object ConfigurationManager { get; private set; }

        public string ResolveTripsImageFilePath()
        {
            var filePath = System.Configuration.ConfigurationManager.AppSettings[HouseholdDefaultImageAppSetting];
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImagesDirectory, "defaultHousehold.png");

            return filePath;
        }
    }
}
