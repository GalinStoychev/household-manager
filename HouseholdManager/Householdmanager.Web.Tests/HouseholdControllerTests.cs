using System;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Web.WebHelpers.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Controllers;
using HouseholdManager.Web.Areas.Household.Models;
using HouseholdManager.Models;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class HouseholdControllerTests
    {
        private Mock<IUserService> userServiceMock;
        private Mock<IHouseholdService> householdServiceMock;
        private Mock<IImageService> imageServiceMock;
        private Mock<IMapingService> mappingServiceMock;
        private Mock<IWebHelper> webHelperMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.userServiceMock = new Mock<IUserService>();
            this.householdServiceMock = new Mock<IHouseholdService>();
            this.imageServiceMock = new Mock<IImageService>();
            this.mappingServiceMock = new Mock<IMapingService>();
            this.webHelperMock = new Mock<IWebHelper>();
        }

        [Test]
        public void HouseholdController_ShouldThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdController(null, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void HouseholdController_ShouldThrowArgumentNullException_WhenHouseholdServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdController(userServiceMock.Object, null, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void HouseholdController_ShouldThrowArgumentNullException_WhenImageServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdController(userServiceMock.Object, householdServiceMock.Object, null, mappingServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void HouseholdController_ShouldThrowArgumentNullException_WhenMappingServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, null, webHelperMock.Object));
        }

        [Test]
        public void HouseholdController_ShouldThrowArgumentNullException_WhenWebHelperIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, null));
        }

        [Test]
        public void HouseholdController_ShouldHaveOneAuthorizeAttribute()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            var result = householdController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void HouseholdController_SholdReturnDefaultView_WhenIndexIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void HouseholdController_SholdReturnDefaultViewWithHouseholdViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<HouseholdViewModel>();
        }

        [Test]
        public void WebHelper_ShouldCallGetHouseholdIdFromCookieOnce_WhenIndexIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Index();

            // Assert
            this.webHelperMock.Verify(x => x.GetHouseholdIdFromCookie(), Times.Once);
        }

        [Test]
        public void HouseholdService_ShouldCallGetHouseholdOnce_WhenIndexIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Index();

            // Assert
            this.householdServiceMock.Verify(x => x.GetHousehold(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void MappingService_ShouldCallMapOnce_WhenIndexIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Index();

            // Assert
            this.mappingServiceMock.Verify(x => x.Map<Household, HouseholdViewModel>(It.IsAny<Household>(), It.IsAny<HouseholdViewModel>()), Times.Once);
        }

        [Test]
        public void HouseholdController_SholdReturnDefaultView_WhenCreateGetIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Create()).ShouldRenderDefaultView();
        }

        [Test]
        public void HouseholdController_SholdReturnDefaultViewWithHouseholdViewModel_WhenCreateGetIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Create())
                .ShouldRenderDefaultView()
                .WithModel<HouseholdViewModel>();
        }

        [Test]
        public void ImageService_SholdCallLoadHouseholdDefaultImage_WhenCreateGetIsCalledAndTempDataIsEmpty()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Create();

            // Assert
            this.imageServiceMock.Verify(x => x.LoadHouseholdDefaultImage(), Times.Once);
        }

        [Test]
        public void HouseholdController_SholdRedirectToAction_WhenCreatePostIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Create(new HouseholdViewModel()))
                .ShouldRedirectTo(x => x.SetCurrentHousehold("_"));
        }

        [Test]
        public void householdService_ShouldCallCreateHousehold_WhenCreatePostIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Create(new HouseholdViewModel());

            // Assert
            householdServiceMock.Verify(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CreatePost_ShouldHaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            var result = householdController.GetType().GetMethod("Create", new Type[] { typeof(HouseholdViewModel) })
                .GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void HouseholdController_SholdRedirectToRout_WhenSetCurrentHouseholdIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.SetCurrentHousehold("_"))
                .ShouldRedirectToRoute("Household_single");
        }

        [Test]
        public void UserService_SholdCallSetCurrentHouseholdOnce_WhenSetCurrentHouseholdIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.SetCurrentHousehold("_");

            // Assert
            userServiceMock.Verify(x => x.SetCurrentHousehold(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }


        [Test]
        public void UserService_SholdCallGetCurrentHouseholdOnce_WhenSetCurrentHouseholdIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.SetCurrentHousehold("_");

            // Assert
            userServiceMock.Verify(x => x.GetCurrentHousehold(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void WebHelper_SholdCallGetUserIdOnce_WhenSetCurrentHouseholdIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.SetCurrentHousehold("_");

            // Assert
            webHelperMock.Verify(x => x.GetUserId(), Times.Once);
        }

        [Test]
        public void WebHelper_SholdCallGetUserNameOnce_WhenSetCurrentHouseholdIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.SetCurrentHousehold("_");

            // Assert
            webHelperMock.Verify(x => x.GetUserName(), Times.Once);
        }

        [Test]
        public void WebHelper_SholdCallSetHouseholdCookieOnce_WhenSetCurrentHouseholdIsCalled()
        {
            // Arrange
            var householdController = new HouseholdController(userServiceMock.Object, householdServiceMock.Object, imageServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.SetCurrentHousehold("_");

            // Assert
            webHelperMock.Verify(x => x.SetHouseholdCookie(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
