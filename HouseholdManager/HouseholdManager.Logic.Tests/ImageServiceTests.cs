using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Logic.Services;

namespace HouseholdManager.Logic.Tests
{
    [TestFixture]
    public class ImageServiceTests
    {
        [Test]
        public void ImageService_ShouldThrowArgumentNullException_WhenImageLoaderIsNull()
        {
            // Arrange
            var imagePathResolverMock = new Mock<IImagePathResolver>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ImageService(null, imagePathResolverMock.Object));
        }

        [Test]
        public void ImageService_ShouldThrowArgumentNullException_WhenImagePathResolverIsNull()
        {
            // Arrange
            var imageLoaderMock = new Mock<IImageLoader>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ImageService(imageLoaderMock.Object, null));
        }

        [Test]
        public void ImagePathResolver_ShouldCallResolveTripsImageFilePathOnce_WhenLoadHouseholdDefaultImageIsCalled()
        {
            // Arrange
            var imageLoaderMock = new Mock<IImageLoader>();
            var imagePathResolverMock = new Mock<IImagePathResolver>();

            var imageService = new ImageService(imageLoaderMock.Object, imagePathResolverMock.Object);

            //Act
            var result = imageService.LoadHouseholdDefaultImage();

            //Assert
            imagePathResolverMock.Verify(x => x.ResolveTripsImageFilePath(), Times.Once);
        }

        [Test]
        public void ImageLoader_ShouldCallLoadImageOnce_WhenLoadHouseholdDefaultImageIsCalled()
        {
            // Arrange
            var imageLoaderMock = new Mock<IImageLoader>();
            var imagePathResolverMock = new Mock<IImagePathResolver>();

            var imageService = new ImageService(imageLoaderMock.Object, imagePathResolverMock.Object);

            //Act
            var result = imageService.LoadHouseholdDefaultImage();

            //Assert
            imageLoaderMock.Verify(x => x.LoadImage(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void LoadHouseholdDefaultImage_ShouldReturnByteArray_WhenLoadHouseholdDefaultImageIsCalled()
        {
            // Arrange
            var imageLoaderMock = new Mock<IImageLoader>();
            var imagePathResolverMock = new Mock<IImagePathResolver>();

            var imageService = new ImageService(imageLoaderMock.Object, imagePathResolverMock.Object);

            //Act
            var result = imageService.LoadHouseholdDefaultImage();

            //Assert
            Assert.IsInstanceOf<byte[]>(result);
        }
    }
}
