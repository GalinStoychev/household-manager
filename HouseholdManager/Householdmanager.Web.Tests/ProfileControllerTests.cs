using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Models;
using HouseholdManager.Web.Controllers;
using HouseholdManager.Web.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class ProfileControllerTests
    {
        [Test]
        public void ProfileController_ShouldThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Arrange
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(null, mappingServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void ProfileController_ShouldThrowArgumentNullException_WhenMappingServiceIsNull()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var webHelperMock = new Mock<IWebHelper>();

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(userServiceMock.Object, null, webHelperMock.Object));
        }

        [Test]
        public void ProfileController_ShouldThrowArgumentNullException_WhenWebHelperServiceIsNull()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ProfileController(userServiceMock.Object, mappingServiceMock.Object, null));
        }

        [Test]
        public void ProfileController_SholdReturnInstanceOfProfileController_WhenInitialized()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();

            // Act
            var profileController = new ProfileController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Assert
            Assert.IsInstanceOf<ProfileController>(profileController);
        }

        [Test]
        public void ProfileController_SholdReturnDefaultView_WhenIndexIsCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());
            mappingServiceMock.Setup(x => x.Map<ProfileViewModel>(It.IsAny<object>())).Returns(new ProfileViewModel());

            var profileController = new ProfileController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            profileController.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void ProfileController_SholdReturnDefaultViewWithProfileViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());
            mappingServiceMock.Setup(x => x.Map<ProfileViewModel>(It.IsAny<object>())).Returns(new ProfileViewModel());

            var profileController = new ProfileController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            profileController.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<ProfileViewModel>();
        }

        [Test]
        public void ProfileController_SholdReturnDefaultViewWithTheCorrectProfileViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());
            var expected = new ProfileViewModel();
            mappingServiceMock.Setup(x => x.Map<ProfileViewModel>(It.IsAny<object>())).Returns(expected);

            var profileController = new ProfileController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            profileController.WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel(expected);
        }

        [Test]
        public void UserService_ShouldCallGetUserInfoOnce_WhenIndexIsCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());
            mappingServiceMock.Setup(x => x.Map<ProfileViewModel>(It.IsAny<object>())).Returns(new ProfileViewModel());

            var profileController = new ProfileController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            profileController.Index();

            // Assert
            userServiceMock.Verify(x => x.GetUserInfo(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void MappingService_ShouldCallMapOnce_WhenIndexIsCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();
            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());
            mappingServiceMock.Setup(x => x.Map<ProfileViewModel>(It.IsAny<object>())).Returns(new ProfileViewModel());

            var profileController = new ProfileController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            profileController.Index();

            // Assert
            mappingServiceMock.Verify(x => x.Map<ProfileViewModel>(It.IsAny<User>()), Times.Once);
        }

        [TestCase(3)]
        [TestCase(10)]
        [TestCase(0)]
        public void ProfileUser_ShouldHaveExactAmountOfHouseholds_WhenIndexIsCalled(int households)
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mappingServiceMock = new Mock<IMapingService>();
            var webHelperMock = new Mock<IWebHelper>();
            var user = new User();
            for (int i = 0; i < households; i++)
            {
                user.Households.Add(new Household(null, null, null));
            }

            userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(user);
            var profileUser = new ProfileViewModel();
            mappingServiceMock.Setup(x => x.Map<ProfileViewModel>(It.IsAny<object>())).Returns(profileUser);

            var profileController = new ProfileController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            profileController.Index();

            // Assert
            Assert.That(profileUser.Households.Count == households);
        }
    }
}
