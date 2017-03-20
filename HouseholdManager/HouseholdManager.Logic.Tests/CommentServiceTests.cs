using System;
using Moq;
using NUnit.Framework;
using HouseholdManager.Models;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Logic.Services;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Tests
{
    [TestFixture]
    public class CommentServiceTests
    {
        [Test]
        public void CommentService_ShouldThrowArgumentNullException_WhenUnitOfWorkIsNull()
        {
            // Arrange
            var commentRepoMock = new Mock<IRepository<Comment>>();
            var commentFactoryMock = new Mock<ICommentFactory>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(null, commentRepoMock.Object, commentFactoryMock.Object));
        }

        [Test]
        public void CommentService_ShouldThrowArgumentNullException_WhenCommentRepoIsNull()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var commentFactoryMock = new Mock<ICommentFactory>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(unitOfWorkMock.Object, null, commentFactoryMock.Object));
        }

        [Test]
        public void CommentService_ShouldThrowArgumentNullException_WhenCommentFactoryIsNull()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var commentRepoMock = new Mock<IRepository<Comment>>();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => new CommentService(unitOfWorkMock.Object, commentRepoMock.Object, null));
        }

        [Test]
        public void CommentFactory_ShouldCallCreateCommentOnce_WhenAddCommentIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var commentRepoMock = new Mock<IRepository<Comment>>();
            var commentFactoryMock = new Mock<ICommentFactory>();

            var commentService = new CommentService(unitOfWorkMock.Object, commentRepoMock.Object, commentFactoryMock.Object);
            //Act
            commentService.AddComment(new Guid(), "_", "_");

            //Assert
            commentFactoryMock.Verify(x => x.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CommentRepo_ShouldCallAddOnce_WhenAddCommentIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var commentRepoMock = new Mock<IRepository<Comment>>();
            var commentFactoryMock = new Mock<ICommentFactory>();

            var commentService = new CommentService(unitOfWorkMock.Object, commentRepoMock.Object, commentFactoryMock.Object);
            //Act
            commentService.AddComment(new Guid(), "_", "_");

            //Assert
            commentRepoMock.Verify(x => x.Add(It.IsAny<Comment>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenAddCommentIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var commentRepoMock = new Mock<IRepository<Comment>>();
            var commentFactoryMock = new Mock<ICommentFactory>();

            var commentService = new CommentService(unitOfWorkMock.Object, commentRepoMock.Object, commentFactoryMock.Object);
            //Act
            commentService.AddComment(new Guid(), "_", "_");

            //Assert
            unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void CommentRepo_ShouldCallGetAllOnce_WhenGetExpenseCommentsIsCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var commentRepoMock = new Mock<IRepository<Comment>>();
            var commentFactoryMock = new Mock<ICommentFactory>();

            var commentService = new CommentService(unitOfWorkMock.Object, commentRepoMock.Object, commentFactoryMock.Object);
            //Act
            commentService.GetExpenseComments(new Guid());
            
            //Assert
            commentRepoMock.Verify(x => x.GetAll(
                It.IsAny<Expression<Func<Comment, bool>>>(), 
                It.IsAny<Expression<Func<Comment, Comment>>>(),
                It.IsAny<Expression<Func<Comment, object>>>()),
                Times.Once);
        }

        [Test]
        public void GetExpenseComments_ShouldReturnIEnumerableOfComments_WhenCalled()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var commentRepoMock = new Mock<IRepository<Comment>>();
            var commentFactoryMock = new Mock<ICommentFactory>();

            var commentService = new CommentService(unitOfWorkMock.Object, commentRepoMock.Object, commentFactoryMock.Object);
            //Act
            var result = commentService.GetExpenseComments(new Guid());

            //Assert
            Assert.IsInstanceOf<IEnumerable<Comment>>(result);
        }
    }
}
