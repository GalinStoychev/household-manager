using System;
using Moq;
using NUnit.Framework;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Models;
using HouseholdManager.Logic.Services;
using System.Linq.Expressions;
using HouseholdManager.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdManager.Logic.Tests
{
    [TestFixture]
    public class InvitationServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IRepository<Invitation>> invitationRepoMock;
        private Mock<IRepository<User>> userRepoMock;
        private Mock<IInvitationFactory> invitationFactoryMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.invitationRepoMock = new Mock<IRepository<Invitation>>();
            this.userRepoMock = new Mock<IRepository<User>>();
            this.invitationFactoryMock = new Mock<IInvitationFactory>();
        }

        [Test]
        public void InvitationService_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new InvitationService(null, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object));
        }

        [Test]
        public void InvitationService_ShouldThrowArgumentNullException_WhenInvitationServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new InvitationService(unitOfWorkMock.Object, null, userRepoMock.Object, invitationFactoryMock.Object));
        }

        [Test]
        public void InvitationService_ShouldThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, null, invitationFactoryMock.Object));
        }

        [Test]
        public void InvitationService_ShouldThrowArgumentNullException_WhenInvitationFactoryIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, null));
        }

        [Test]
        public void InvitationRepository_ShouldCallGetFirstOnce_WhenAcceptInvitationIsCalled()
        {
            // Arrange
            var invitation = new Invitation(new Guid(), "_", Status.Accepted);
            this.invitationRepoMock.Setup(x => x.GetFirst(
                It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, object>>>(), It.IsAny<Expression<Func<Invitation, object>>>()))
                .Returns(invitation);
            invitation.User = new User();

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AcceptInvitation(new Guid());

            // Assert
            invitationRepoMock.Verify(x => x.GetFirst(
                It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, object>>>(), It.IsAny<Expression<Func<Invitation, object>>>()),
                Times.Once);
        }

        [Test]
        public void InvitationRepository_ShouldCallUpdateOnce_WhenAcceptInvitationIsCalled()
        {
            // Arrange
            var invitation = new Invitation(new Guid(), "_", Status.Accepted);
            this.invitationRepoMock.Setup(x => x.GetFirst(
                It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, object>>>(), It.IsAny<Expression<Func<Invitation, object>>>()))
                .Returns(invitation);
            invitation.User = new User();

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AcceptInvitation(new Guid());

            // Assert
            invitationRepoMock.Verify(x => x.Update(It.IsAny<Invitation>()), Times.Once);
        }

        [Test]
        public void UserRepository_ShouldCallUpdateOnce_WhenAcceptInvitationIsCalled()
        {
            // Arrange
            var invitation = new Invitation(new Guid(), "_", Status.Accepted);
            this.invitationRepoMock.Setup(x => x.GetFirst(
                It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, object>>>(), It.IsAny<Expression<Func<Invitation, object>>>()))
                .Returns(invitation);
            invitation.User = new User();

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AcceptInvitation(new Guid());

            // Assert
            userRepoMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenAcceptInvitationIsCalled()
        {
            // Arrange
            var invitation = new Invitation(new Guid(), "_", Status.Accepted);
            this.invitationRepoMock.Setup(x => x.GetFirst(
                It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, object>>>(), It.IsAny<Expression<Func<Invitation, object>>>()))
                .Returns(invitation);
            invitation.User = new User();

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AcceptInvitation(new Guid());

            // Assert
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void AddInvitation_ShouldReturnTrue_WhenCalled()
        {
            // Arrange
            var user = new User();
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>())).Returns(new List<Invitation>());

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            var result = invitationService.AddInvitation("_", new Guid());

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void AddInvitation_ShouldReturnFalse_WhenUserIsAlreadyInThisHousehold()
        {
            // Arrange
            var user = new User();
            user.Households.Add(new Household("_", "_", new byte[0]));
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>())).Returns(new List<Invitation>());

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            var result = invitationService.AddInvitation("_", user.Households.First().Id);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void AddInvitation_ShouldReturnFalse_WhenSuchInvitationAlreadyExists()
        {
            // Arrange
            var user = new User();
            var household = new Household("_", "_", new byte[0]);
            user.Households.Add(household);
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>()))
                .Returns(new List<Invitation>() { new Invitation(household.Id, user.Id, Status.Accepted) });

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            var result = invitationService.AddInvitation(user.Id, household.Id);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void UserRepository_ShouldCallGetFirstOnce_WhenAddInvitationIsCalled()
        {
            // Arrange
            var user = new User();
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>())).Returns(new List<Invitation>());

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AddInvitation("_", new Guid());

            // Assert
            userRepoMock.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void InvitationRepository_ShouldCallGetAllOnce_WhenAddInvitationIsCalled()
        {
            // Arrange
            var user = new User();
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>())).Returns(new List<Invitation>());

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AddInvitation("_", new Guid());

            // Assert
            invitationRepoMock.Verify(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>()), Times.Once);
        }


        [Test]
        public void InvitationFactory_ShouldCallCreateInvitationOnce_WhenAddInvitationIsCalled()
        {
            // Arrange
            var user = new User();
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>())).Returns(new List<Invitation>());

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AddInvitation("_", new Guid());

            // Assert
            this.invitationFactoryMock.Verify(x => x.CreateInvitation(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Status>()), Times.Once);
        }

        [Test]
        public void InvitationRepository_ShouldCallAddOnce_WhenAddInvitationIsCalled()
        {
            // Arrange
            var user = new User();
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>())).Returns(new List<Invitation>());

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AddInvitation("_", new Guid());

            // Assert
            this.invitationRepoMock.Verify(x => x.Add(It.IsAny<Invitation>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenAddInvitationIsCalled()
        {
            // Arrange
            var user = new User();
            this.userRepoMock.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(user);
            this.invitationRepoMock.Setup(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>())).Returns(new List<Invitation>());

            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.AddInvitation("_", new Guid());

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void InvitationRepository_ShouldCallGetAllOnce_WhenGetUserInvitationsIsCalled()
        {
            // Arrange
            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            invitationService.GetUserInvitations("_");

            // Assert
            invitationRepoMock.Verify(x => x.GetAll<Invitation>(It.IsAny<Expression<Func<Invitation, bool>>>(), It.IsAny<Expression<Func<Invitation, Invitation>>>(), It.IsAny<Expression<Func<Invitation, object>>>()),
                Times.Once);
        }

        [Test]
        public void GetUserInvitations_ShouldReturnIEnumerableOfInvitations_WhenItIsCalled()
        {
            // Arrange
            var invitationService = new InvitationService(unitOfWorkMock.Object, invitationRepoMock.Object, userRepoMock.Object, invitationFactoryMock.Object);

            // Act
            var result = invitationService.GetUserInvitations("_");

            // Assert
            Assert.IsInstanceOf<IEnumerable<Invitation>>(result);
        }
    }
}
