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
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IRepository<Household>> householdRepoMock;
        private Mock<IRepository<User>> userRepoMock;
        private Mock<IHouseholdFactory> householdFactoryMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.householdRepoMock = new Mock<IRepository<Household>>();
            this.userRepoMock = new Mock<IRepository<User>>();
            this.householdFactoryMock = new Mock<IHouseholdFactory>();
        }

        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(null, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object));
        }

        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenHouseholdRepositoryIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(unitOfWorkMock.Object, null, userRepoMock.Object, householdFactoryMock.Object));
        }

        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenUserRepositoryIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, null, householdFactoryMock.Object));
        }

        [Test]
        public void HouseholdService_ShouldThrowArgumentNullException_WhenHouseholdFactoryIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, null));
        }

        [Test]
        public void HouseholdFactory_ShouldCallCreateHouseholdOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            // Assert
            this.householdFactoryMock.Verify(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallGetByIdOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            // Assert
            this.userRepoMock.Verify(x => x.GetById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void HouseholdRepository_ShouldCallAddOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            // Assert
            this.householdRepoMock.Verify(x => x.Add(It.IsAny<Household>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void HouseholdUsersCount_ShouldBeIncreasedByOne_WhenCreateHouseholdIsCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdFactoryMock.Setup(x => x.CreateHousehold(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(householdStub);
            var expected = householdStub.Users.Count + 1;

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.CreateHousehold("_", "_", new byte[0], "_");

            // Assert
            Assert.That(expected == householdStub.Users.Count);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetFirstOnce_WhenGetHouseholdIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.GetHousehold(new Guid());

            // Assert
            this.householdRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>()), Times.Once);
        }

        [Test]
        public void GetHousehold_ShouldReturnInstanceOfHousehold_WhenCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            var result = householdService.GetHousehold(new Guid());

            // Assert
            Assert.IsInstanceOf<Household>(result);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetFirstOnce_WhenGetHouseholdUsersIsCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.GetHouseholdUsers(new Guid());

            // Assert
            this.householdRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>()), Times.Once);
        }

        [Test]
        public void GetHouseholdUsers_ShouldReturnIEnumerableOfUsers_WhenCalled()
        {
            // Arrange
            var householdStub = new Household("_", "_", new byte[0]);
            householdRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Household, bool>>>(), It.IsAny<Expression<Func<Household, object>>>())).Returns(householdStub);

            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            var result = householdService.GetHouseholdUsers(new Guid());

            // Assert
            Assert.IsInstanceOf<IEnumerable<User>>(result);
        }

        [Test]
        public void GetAll_ShouldReturnIEnumerableOfHouseholds_WhenCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            var result = householdService.GetAll();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Household>>(result);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetAllOnce_WhenGetAllIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.GetAll();

            // Assert
            this.householdRepoMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetByIdOnce_WhenUpdateHouseholdInfoIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);
            var householdStub = new Household("_", "_", new byte[0]);
            this.householdRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(householdStub);

            // Act
            householdService.UpdateHouseholdInfo(new Guid(), "_", "_");

            // Assert
            this.householdRepoMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void HouseholdRepository_ShouldCallUpdateOnce_WhenUpdateHouseholdInfoIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);
            var householdStub = new Household("_", "_", new byte[0]);
            this.householdRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(householdStub);

            // Act
            householdService.UpdateHouseholdInfo(new Guid(), "_", "_");

            // Assert
            this.householdRepoMock.Verify(x => x.Update(It.IsAny<Household>()), Times.Once);
        }

        [Test]
        public void UnotOfWork_ShouldCallCommitOnce_WhenUpdateHouseholdInfoIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);
            var householdStub = new Household("_", "_", new byte[0]);
            this.householdRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(householdStub);

            // Act
            householdService.UpdateHouseholdInfo(new Guid(), "_", "_");

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetByIdOnce_WhenDeleteIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);
            var householdStub = new Household("_", "_", new byte[0]);
            this.householdRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(householdStub);

            // Act
            householdService.Delete(new Guid(), true);

            // Assert
            this.householdRepoMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void HouseholdRepository_ShouldCallUpdateOnce_WhenDeleteIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);
            var householdStub = new Household("_", "_", new byte[0]);
            this.householdRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(householdStub);

            // Act
            householdService.Delete(new Guid(), true);

            // Assert
            this.householdRepoMock.Verify(x => x.Update(It.IsAny<Household>()), Times.Once);
        }

        [Test]
        public void UnotOfWork_ShouldCallCommitOnce_WhenDeleteIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);
            var householdStub = new Household("_", "_", new byte[0]);
            this.householdRepoMock.Setup(x => x.GetById(It.IsAny<object>())).Returns(householdStub);

            // Act
            householdService.Delete(new Guid(), true);

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void HouseholdRepository_ShouldCallGetAllOnce_WhenGetHouseholdsCountIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            householdService.GetHouseholdsCount();

            // Assert
            this.householdRepoMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetHouseholdsCount_ShouldReturnInt_WhenItIsCalled()
        {
            // Arrange
            var householdService = new HouseholdService(unitOfWorkMock.Object, householdRepoMock.Object, userRepoMock.Object, householdFactoryMock.Object);

            // Act
            var result = householdService.GetHouseholdsCount();

            // Assert
            Assert.IsInstanceOf<int>(result);
        }
    }
}
