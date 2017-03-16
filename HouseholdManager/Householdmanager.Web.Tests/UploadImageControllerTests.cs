using System;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using HouseholdManager.Web.Areas.Household.Controllers;
using System.Web;
using System.Web.Mvc;
using HouseholdManager.Common.Constants;
using System.IO;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class UploadImageControllerTests
    {
        [Test]
        public void UploadImageController_ShouldRedirectToRouteNamedHouseholdCreate_WhenUploadIsCalled()
        {
            // Arrange
            var uploadImageController = new UploadImageController();
            var file = new Mock<HttpPostedFileBase>();

            // Act
            // Assert
            uploadImageController.WithCallTo(x => x.Upload(file.Object))
                .ShouldRedirectToRoute("Household_create");
        }

        [Test]
        public void UploadImageController_ShouldHasAuthorizedAttribute()
        {
            // Arrange
            var uploadImageController = new UploadImageController();

            // Act
            var result = uploadImageController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void Upload_ShouldHaveHttpPostAttribute()
        {
            // Arrange
            var uploadImageController = new UploadImageController();

            // Act
            var result = uploadImageController.GetType().GetMethod("Upload").GetCustomAttributes(typeof(HttpPostAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void TempData_ShouldHaveImageNotSelectedErrorMessage_WhenPassedFileHasEmptyContent()
        {
            // Arrange
            var uploadImageController = new UploadImageController();
            var file = new Mock<HttpPostedFileBase>();

            // Act
            uploadImageController.Upload(file.Object);

            // Assert
            Assert.That(uploadImageController.TempData[CommonConstants.UploadMessage].ToString() == "Please, choose a file.");
        }

        [Test]
        public void TempData_ShouldHaveImageNotSelectedErrorMessage_WhenPassedFileIsNull()
        {
            // Arrange
            var uploadImageController = new UploadImageController();

            // Act
            uploadImageController.Upload(null);

            // Assert
            Assert.That(uploadImageController.TempData[CommonConstants.UploadMessage].ToString() == "Please, choose a file.");
        }

        [Test]
        public void TempData_ShouldHaveImageExtentionErrorMessage_WhenPassedFileIsNotTheCorrectFormat()
        {
            // Arrange
            var uploadImageController = new UploadImageController();
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.ContentLength).Returns(1);

            // Act
            uploadImageController.Upload(file.Object);

            // Assert
            Assert.That(uploadImageController.TempData[CommonConstants.UploadMessage].ToString() == "Only JPEG and PNG files are allowed.");
        }

        [Test]
        public void TempData_ShouldHaveImageSizeErrorMessage_WhenPassedFileHasContentLengthBiggerThanFourMB()
        {
            // Arrange
            var uploadImageController = new UploadImageController();
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.ContentLength).Returns(4 * 1000 * 1024 + 1);
            file.Setup(x => x.ContentType).Returns("image/jpeg");

            // Act
            uploadImageController.Upload(file.Object);

            // Assert
            Assert.That(uploadImageController.TempData[CommonConstants.UploadMessage].ToString() == "The image must be up to 4MB.");
        }

        [Test]
        public void TempData_ShouldHaveImageUploadGeneralErrorMessage_WhenPassedFileNoData()
        {
            // Arrange
            var uploadImageController = new UploadImageController();
            var file = new Mock<HttpPostedFileBase>();
            file.Setup(x => x.ContentLength).Returns(1);
            file.Setup(x => x.ContentType).Returns("image/jpeg");

            // Act
            uploadImageController.Upload(file.Object);

            // Assert
            Assert.That(uploadImageController.TempData[CommonConstants.UploadMessage].ToString() == "There was error when uplaoding. Please, try again.");
        }
    }
}
