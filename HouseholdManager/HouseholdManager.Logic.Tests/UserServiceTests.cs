using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Services;
using HouseholdManager.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HouseholdManager.Logic.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IRepository<User>> userRepoMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.userRepoMock = new Mock<IRepository<User>>();
        }

        [Test]
        public void UserService_ShouldThrowNewArgumentException_WhenUnitOfWorkIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null, userRepoMock.Object));
        }

        [Test]
        public void UserService_ShouldThrowNewArgumentException_WhenUserRepositoryIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(unitOfWorkMock.Object, null));
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenAddHouseholdIsCalled()
        {
            //Arrange
            var user = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(user);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.AddHousehold(null, null);

            //Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenAddHouseholdIsCalled()
        {
            //Arrange
            var userMock = new Mock<User>();
            userMock.Setup(x => x.Households).Returns(new List<Household>());
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userMock.Object);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.AddHousehold(null, null);

            //Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void UserHousehold_ShouldBeIncreasedByOne_WhenAddHouseholdIsCalled()
        {
            //Arrange
            var userMock = new Mock<User>();
            userMock.Setup(x => x.Households).Returns(new List<Household>());
            var expeceted = userMock.Object.Households.Count + 1;
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userMock.Object);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.AddHousehold(null, null);

            //Assert
            Assert.That(expeceted == userMock.Object.Households.Count);
        }

        [Test]
        public void UserCurrentHousehold_ShouldBeChangesWithTheOncePassed_WhenAddHouseholdIsCalled()
        {
            //Arrange
            var userStub = new User();
            var householdStub = new Household("_", " _", new byte[0]);
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.AddHousehold(householdStub, null);

            //Assert
            Assert.That(userStub.CurrentHousehold.Equals(householdStub));
        }

        [Test]
        public void UserRepository_ShouldCallGetFirstOnce_WhenGetCurrentHouseholdIsCalled()
        {
            //Arrange
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.GetCurrentHousehold("_");

            //Assert
            this.userRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void GetCurrentHousehold_ShouldReturnInstanseOfHousehold_WhenCalled()
        {
            //Arrange
            var userStub = new User();
            userStub.SetCurrentHousehold(new Household("_", "_", new byte[0]));
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            var result = userService.GetCurrentHousehold("_");

            //Assert
            Assert.IsInstanceOf<Household>(result);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenGetUserInfoIsCalled()
        {
            //Arrange
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.GetUserInfo("_");

            //Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetUserInfo_ShouldReturnInstanceOfUser_WhenCalled()
        {
            //Arrange
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            var result = userService.GetUserInfo("_");

            //Assert
            Assert.IsInstanceOf<User>(result);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenFirstOverloadOfSetCurrentHouseholdIsCalled()
        {
            //Arrange
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold(new Guid(), "_");

            //Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void unitOfWork_ShouldCallCommitOnce_WhenFirstOverloadOfSetCurrentHouseholdIsCalled()
        {
            //Arrange
            var userStub = new User();
            var householdStub = new Household("_", "_", new byte[0]);
            userStub.Households.Add(householdStub);
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold(householdStub.Id, "_");

            //Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenSecondOverloadOfSetCurrentHouseholdIsCalled()
        {
            //Arrange
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold("_", "_");

            //Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenSecondOverloadOfSetCurrentHouseholdIsCalled()
        {
            //Arrange
            var userStub = new User();
            var householdStub = new Household("_", "_", new byte[0]);
            userStub.Households.Add(householdStub);
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold(householdStub.Name, "_");

            //Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAll_ShouldReturnIEnumerableOfUsers_WhenCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            // Act
            var result = userService.GetAll();

            // Assert
            Assert.IsInstanceOf<IEnumerable<User>>(result);
        }

        [Test]
        public void UserRepository_ShouldCallGetAllOnce_WhenGetAllIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            // Act
            var result = userService.GetAll();

            // Assert
            this.userRepoMock.Verify(x => x.GetAll(
                It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Expression<Func<User, User>>>(),
                It.IsAny<Expression<Func<User, object>>>()),
                Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenGetAllIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);

            // Act
            userService.UpdateUserInfo("_", "_", "_", "_");

            // Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallUpdateOnce_WhenGetAllIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);

            // Act
            userService.UpdateUserInfo("_", "_", "_", "_");

            // Assert
            this.userRepoMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenGetAllIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);

            // Act
            userService.UpdateUserInfo("_", "_", "_", "_");

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenDeleteIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);

            // Act
            userService.Delete("_", true);

            // Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallUpdateOnce_WhenDeleteIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);

            // Act
            userService.Delete("_", true);

            // Assert
            this.userRepoMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenDeleteIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);
            var userStub = new User();
            this.userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);

            // Act
            userService.Delete("_", true);

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallGetAllOnce_WhenGetUsersCountIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            // Act
            userService.GetUsersCount();

            // Assert
            this.userRepoMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetUsersCount_ShouldReturnInt_WhenItIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            // Act
            var result = userService.GetUsersCount();

            // Assert
            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        public void UserRepository_ShouldCallGetFirstOnce_WhenGetByUsernameIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            // Act
            userService.GetByUsername("_");

            // Assert
            this.userRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>(), null), Times.Once);
        }

        [Test]
        public void GetByUsername_ShouldReturnUser_WhenItIsCalled()
        {
            // Arrange
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>(), null)).Returns(new User());

            // Act
            var result = userService.GetByUsername("_");

            // Assert
            Assert.IsInstanceOf<User>(result);
        }
    }
}
