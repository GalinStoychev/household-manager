using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Models;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Logic.Services;
using System.Linq.Expressions;

namespace HouseholdManager.Logic.Tests
{
    [TestFixture]
    public class HouseholdServiceTests
    {
        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(null, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object));
        }

        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenHouseholdRepositoryIsNull()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(unitOfWorkMock.Object, null, userRepoMock.Object, householdFactoryMock.Object));
        }

        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenUserRepositoryIsNull()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, null, householdFactoryMock.Object));
        }

        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenHouseholdFactoryIsNull()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, null));
        }

        [Test]
        public void HouseholdFactory_ShouldCallCreateHouseholdOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            //Assert
            householdFactoryMock.Verify(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            //Assert
            userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void HouseholdRepository_ShouldCallAddOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            //Assert
            householdRepoMock.Verify(x => x.Add(It.IsAny<Household>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            //Assert
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void HouseholdUsersCount_ShouldBeIncreasedByOne_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);
            var expected = householdStub.Users.Count + 1;

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            //Assert
            Assert.That(expected == householdStub.Users.Count);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetFirstOnce_WhenGetHouseholdIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            householdService.GetHousehold(new Guid());

            //Assert
            householdRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>()), Times.Once);
        }

        [Test]
        public void GetHousehold_ShouldReturnInstanceOfHousehold_WhenCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            var result = householdService.GetHousehold(new Guid());

            //Assert
            Assert.IsInstanceOf<Household>(result);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetFirstOnce_WhenGetHouseholdUsersIsCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            householdService.GetHouseholdUsers(new Guid());

            //Assert
            householdRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>()), Times.Once);
        }

        [Test]
        public void GetHouseholdUsers_ShouldReturnIEnumerableOfUsers_WhenCalled()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var householdRepoMock = new Mock<IRepository<Household>>();
            var userRepoMock = new Mock<IRepository<User>>();
            var householdFactoryMock = new Mock<IHouseholdFactory>();
            var householdStub = new Household("_", "_", new byte[0]);
            householdRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            //Act
            var result = householdService.GetHouseholdUsers(new Guid());

            //Assert
            Assert.IsInstanceOf<IEnumerable<User>>(result);
        }
    }
}
