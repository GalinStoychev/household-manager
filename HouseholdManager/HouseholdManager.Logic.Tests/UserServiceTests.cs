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
        [Test]
        public void UserService_ShouldThrowNewArgumentException_WhenUnitOfWorkIsNull()
        {
            //Arrange
            var userRepoMock = new Mock<IRepository<User>>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null, userRepoMock.Object));
        }

        [Test]
        public void UserService_ShouldThrowNewArgumentException_WhenUserRepositoryIsNull()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(unitOfWorkMock.Object, null));
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenAddHouseholdIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var user = new User();
            userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(user);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.AddHousehold(null, null);

            //Assert
            userRepoMock.Verify(x => x.GetById(It.IsAny<object>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenAddHouseholdIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userMock = new Mock<User>();
            userMock.Setup(x => x.Households).Returns(new List<Household>());
            userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userMock.Object);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.AddHousehold(null, null);

            //Assert
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void UserHousehold_ShouldBeIncreasedByOne_WhenAddHouseholdIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userMock = new Mock<User>();
            userMock.Setup(x => x.Households).Returns(new List<Household>());
            var expeceted = userMock.Object.Households.Count + 1;
            userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userMock.Object);
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
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            var householdStub = new Household("_", " _", new byte[0]);
            userRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(userStub);
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
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.GetCurrentHousehold("_");

            //Assert
            userRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void GetCurrentHousehold_ShouldReturnInstanseOfHousehold_WhenCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            userStub.SetCurrentHousehold(new Household("_", "_", new byte[0]));
            userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(userStub);
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
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.GetUserInfo("_");

            //Assert
            userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetUserInfo_ShouldReturnInstanceOfUser_WhenCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
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
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold(new Guid(), "_");

            //Assert
            userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void unitOfWork_ShouldCallCommitOnce_WhenFirstOverloadOfSetCurrentHouseholdIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            var householdStub = new Household("_", "_", new byte[0]);
            userStub.Households.Add(householdStub);
            userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold(householdStub.Id, "_");

            //Assert
            userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenSecondOverloadOfSetCurrentHouseholdIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold("_", "_");

            //Assert
            userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void unitOfWork_ShouldCallCommitOnce_WhenSecondOverloadOfSetCurrentHouseholdIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var userStub = new User();
            var householdStub = new Household("_", "_", new byte[0]);
            userStub.Households.Add(householdStub);
            userRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(userStub);
            var userService = new UserService(unitOfWorkMock.Object, userRepoMock.Object);

            //Act
            userService.SetCurrentHousehold(householdStub.Name, "_");

            //Assert
            userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }
    }
}
