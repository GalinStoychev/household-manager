using HouseholdManager.Logic.Contracts;
using System.Drawing;
using System.IO;

namespace HouseholdManager.Logic.Utils
{
    public class ImageLoader : IImageLoader
    {
        public byte[] LoadImage(string path)
        {
            var img = Image.FromFile(path);
            byte[] arr;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                arr = ms.ToArray();
            }

            return arr;
        }
    }
}
