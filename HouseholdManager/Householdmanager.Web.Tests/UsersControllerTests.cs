using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Models;
using HouseholdManager.Web.Areas.Admin.Controllers;
using HouseholdManager.Web.Areas.Admin.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUserService> userServiceMock;
        private Mock<IMapingService> mappingServiceMock;
        private Mock<IWebHelper> webHelperMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.userServiceMock = new Mock<IUserService>();
            this.mappingServiceMock = new Mock<IMapingService>();
            this.webHelperMock = new Mock<IWebHelper>();
        }

        [Test]
        public void UsersController_ShouldThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(null, mappingServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void UsersController_ShouldHaveOneAuthorizeAttribute()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            var result = usersController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void Index_SholdReturnUsersGridView_WhenCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            usersController.WithCallTo(c => c.Index()).ShouldRenderView("UsersGrid");
        }

        [Test]
        public void Index_SholdReturnUsersGridViewWithListOfUsersViewModel_WhenCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            usersController.WithCallTo(c => c.Index())
                .ShouldRenderView("UsersGrid")
                .WithModel<List<UsersViewModel>>();
        }

        [Test]
        public void UserService_ShouldCallGetAllOnce_WhenIndexGetIsCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            usersController.Index();

            // Assert
            this.userServiceMock.Verify(x => x.GetAll(), Times.Once);
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(10)]
        public void MappingService_ShouldCallMapAsManyTimesAsUsersAreReturnedFromUserService_WhenIndexGetIsCalled(int users)
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var usersFromDb = new List<User>();
            for (int i = 0; i < users; i++)
            {
                usersFromDb.Add(new User());
            }

            this.userServiceMock.Setup(x => x.GetAll()).Returns(usersFromDb);

            // Act
            usersController.Index();

            // Assert
            this.mappingServiceMock.Verify(x => x.Map<UsersViewModel>(It.IsAny<object>()), Times.Exactly(users));
        }

        [Test]
        public void UpdatePost_SholdReturnUsersGridView_WhenCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            usersController.WithCallTo(c => c.Update(new UsersViewModel())).ShouldRedirectToRoute("");
        }

        [Test]
        public void WebHelper_SholdCallChangeAdminRoleOnce_WhenUpdatePostIsCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            usersController.Update(new UsersViewModel());

            // Assert
            this.webHelperMock.Verify(x => x.ChangeAdminRole(It.IsAny<string>(), It.IsAny<bool>()));
        }

        [Test]
        public void UserService_SholdCallDeleteOnce_WhenUpdatePostIsCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            usersController.Update(new UsersViewModel());

            // Assert
            this.userServiceMock.Verify(x => x.Delete(It.IsAny<string>(), It.IsAny<bool>()));
        }

        [Test]
        public void UserService_SholdCallUpdateUserInfoOnce_WhenUpdatePostIsCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            usersController.Update(new UsersViewModel());

            // Assert
            this.userServiceMock.Verify(x => x.UpdateUserInfo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public void ShowHouseholds_SholdReturnHouseholdsGridView_WhenCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object); ;
            this.userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());

            // Act
            // Assert
            usersController.WithCallTo(c => c.ShowHouseholds(new UsersViewModel())).ShouldRenderView("HouseholdsGrid");
        }

        [Test]
        public void ShowHouseholds_SholdReturnHouseholdsGridViewWithListOfHouseholdsViewModel_WhenCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object); ;
            this.userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());

            // Act
            // Assert
            usersController.WithCallTo(c => c.ShowHouseholds(new UsersViewModel()))
                .ShouldRenderView("HouseholdsGrid")
                .WithModel<List< HouseholdsViewModel>>();
        }

        [Test]
        public void UserService_ShouldCallGetUserInfoOnce_WhenShowHouseholdsIsCalled()
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object); ;
            this.userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(new User());

            // Act
            usersController.ShowHouseholds(new UsersViewModel());

            // Assert
            this.userServiceMock.Verify(x => x.GetUserInfo(It.IsAny<string>()), Times.Once);
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(10)]
        public void MappingService_ShouldCallMapAsManyTimesAsAreUserHouseholds_WhenShowHouseholdsIsCalled(int households)
        {
            // Arrange
            var usersController = new UsersController(userServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var user = new User();
            for (int i = 0; i < households; i++)
            {
                user.Households.Add(new Household("_", "_", new byte[0]));
            }

            this.userServiceMock.Setup(x => x.GetUserInfo(It.IsAny<string>())).Returns(user);


            // Act
            usersController.ShowHouseholds(new UsersViewModel());

            // Assert
            this.mappingServiceMock.Verify(x => x.Map<HouseholdsViewModel>(It.IsAny<object>()), Times.Exactly(households));
        }
    }
}
