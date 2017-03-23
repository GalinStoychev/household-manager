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
    public class HouseholdsControllerTests
    {
        private Mock<IHouseholdService> householdServiceMock;
        private Mock<IMapingService> mappingServiceMock;
        private Mock<IWebHelper> webHelperMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.householdServiceMock = new Mock<IHouseholdService>();
            this.mappingServiceMock = new Mock<IMapingService>();
            this.webHelperMock = new Mock<IWebHelper>();
        }

        [Test]
        public void HouseholdsController_ShouldThrowArgumentNullException_WhenHouseholdServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdsController(null, mappingServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void HouseholdsControllerr_ShouldHaveOneAuthorizeAttribute()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            var result = householdController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void Index_SholdReturnUsersGridView_WhenCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Index()).ShouldRenderView("HouseholdsGrid");
        }

        [Test]
        public void Index_SholdReturnUsersGridViewWithListOfUsersViewModel_WhenCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Index())
                .ShouldRenderView("HouseholdsGrid")
                .WithModel<List<HouseholdsViewModel>>();
        }

        [Test]
        public void HouseholdService_ShouldCallGetAllOnce_WhenIndexGetIsCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Index();

            // Assert
            this.householdServiceMock.Verify(x => x.GetAll(), Times.Once);
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(10)]
        public void MappingService_ShouldCallMapAsManyTimesAsHouseholdsAreReturnedFromHouseholdService_WhenIndexGetIsCalled(int households)
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var householdsFromDb = new List<Household>();
            for (int i = 0; i < households; i++)
            {
                householdsFromDb.Add(new Household("_", "_", new byte[0]));
            }

            this.householdServiceMock.Setup(x => x.GetAll()).Returns(householdsFromDb);

            // Act
            householdController.Index();

            // Assert
            this.mappingServiceMock.Verify(x => x.Map<HouseholdsViewModel>(It.IsAny<object>()), Times.Exactly(households));
        }

        [Test]
        public void UpdatePost_SholdReturnUsersGridView_WhenCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            householdController.WithCallTo(c => c.Update(new HouseholdsViewModel())).ShouldRedirectToRoute("");
        }

        [Test]
        public void HouseholdService_SholdCallDeleteOnce_WhenUpdatePostIsCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Update(new HouseholdsViewModel());

            // Assert
            this.householdServiceMock.Verify(x => x.Delete(It.IsAny<Guid>(), It.IsAny<bool>()));
        }

        [Test]
        public void HouseholdService_SholdCallUpdateHouseholdInfoOnce_WhenUpdatePostIsCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            householdController.Update(new HouseholdsViewModel());

            // Assert
            this.householdServiceMock.Verify(x => x.UpdateHouseholdInfo(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public void ShowUsers_SholdReturnUserssGridView_WhenCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            this.householdServiceMock.Setup(x => x.GetHouseholdUsers(It.IsAny<Guid>())).Returns(new List<User>());

            // Act
            // Assert
            householdController.WithCallTo(c => c.ShowUsers(new HouseholdsViewModel())).ShouldRenderView("UsersGrid");
        }

        [Test]
        public void ShowUsers_SholdReturnUserssGridViewWithListOfUserssViewModel_WhenCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            this.householdServiceMock.Setup(x => x.GetHouseholdUsers(It.IsAny<Guid>())).Returns(new List<User>());

            // Act
            // Assert
            householdController.WithCallTo(c => c.ShowUsers(new HouseholdsViewModel()))
                .ShouldRenderView("UsersGrid")
                .WithModel<List<UsersViewModel>>();
        }

        [Test]
        public void HouseholdService_SholdCallGetHouseholdUsersOnce_WhenShowUsersIsCalled()
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            this.householdServiceMock.Setup(x => x.GetHouseholdUsers(It.IsAny<Guid>())).Returns(new List<User>());

            // Act
            householdController.ShowUsers(new HouseholdsViewModel());

            // Assert
            this.householdServiceMock.Verify(x => x.GetHouseholdUsers(It.IsAny<Guid>()), Times.Once);
        }

        [TestCase(0)]
        [TestCase(4)]
        [TestCase(10)]
        public void MappingService_ShouldCallMapAsManyTimesAsAreUsersInHousehold_WhenShowUsersIsCalled(int users)
        {
            // Arrange
            var householdController = new HouseholdsController(householdServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var usersFromDb = new List<User>();
            for (int i = 0; i < users; i++)
            {
                usersFromDb.Add(new User());
            }

            this.householdServiceMock.Setup(x => x.GetHouseholdUsers(It.IsAny<Guid>())).Returns(usersFromDb);


            // Act
            householdController.ShowUsers(new HouseholdsViewModel());

            // Assert
            this.mappingServiceMock.Verify(x => x.Map<UsersViewModel>(It.IsAny<object>()), Times.Exactly(users));
        }
    }
}
