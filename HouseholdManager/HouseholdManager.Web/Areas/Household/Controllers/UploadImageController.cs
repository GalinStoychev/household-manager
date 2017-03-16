using HouseholdManager.Common.Constants;
using ImageResizer;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    [Authorize]
    public class UploadImageController : Controller
    {
        private const int FourMb = 4 * 1000 * 1024;
        private const string JpegContentType = "image/jpeg";
        private const string ImageContentType = "image/png";
        private const string ImageNotSelectedError = "Please, choose a file.";
        private const string ImageExtentionError = "Only JPEG and PNG files are allowed.";
        private const string ImageSizeError = "The image must be up to 4MB.";
        private const string ImageUploadGeneralError = "There was error when uplaoding. Please, try again.";
        private const string ImageUplaodedSuccessfully = "Image uploaded.";
        private const string InstructionsQueryString = "width=500;format=jpg;mode=max";

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                if (file == null || file.ContentLength == 0)
                {
                    this.TempData.Add(CommonConstants.UploadMessage, ImageNotSelectedError);
                }
                else if (file.ContentType != JpegContentType && file.ContentType != ImageContentType)
                {
                    this.TempData.Add(CommonConstants.UploadMessage, ImageExtentionError);
                }
                else if (file.ContentLength > FourMb)
                {
                    this.TempData.Add(CommonConstants.UploadMessage, ImageSizeError);
                }
                else
                {
                    byte[] imageAsByteArray = this.ResizeImage(file);
                    this.TempData.Add(CommonConstants.UploadMessage, ImageUplaodedSuccessfully);
                    this.TempData.Add("image", imageAsByteArray);
                }
            }
            catch (Exception ex)
            {
                var es = ex.Message;
                this.TempData.Add(CommonConstants.UploadMessage, ImageUploadGeneralError);
            }

            return RedirectToRoute("Household_create");
        }

        private byte[] ResizeImage(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            var fileStream = file.InputStream;
            byte[] imageAsByteArray = null;

            using (var ms = new MemoryStream())
            {
                var instructions = new Instructions(InstructionsQueryString);
                var imageJob = new ImageJob(fileStream, ms, instructions);
                imageJob.Build();
                imageAsByteArray = ms.ToArray();
            }

            return imageAsByteArray;
        }
    }
}