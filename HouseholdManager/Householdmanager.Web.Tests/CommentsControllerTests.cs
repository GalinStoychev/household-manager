using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Comments.Controllers;
using HouseholdManager.Web.Areas.Comments.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Web;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class CommentsControllerTests
    {
        private Mock<ICommentService> commentServiceMock;
        private Mock<IMapingService> mappingServiceMock;
        private Mock<IWebHelper> webHelperMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.commentServiceMock = new Mock<ICommentService>();
            this.mappingServiceMock = new Mock<IMapingService>();
            this.webHelperMock = new Mock<IWebHelper>();
        }

        [Test]
        public void CommentController_ShouldThrowArgumentNullException_WhenCommentServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new CommentsController(mappingServiceMock.Object, webHelperMock.Object, null));
        }

        [Test]
        public void CommentController_ShouldHaveOneAuthorizeAttribute()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            var result = commentController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void CommentController_ShouldReturnDefaultView_WhenIndexIsCalledAndIsAjaxCall()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);
            this.webHelperMock.Setup(x => x.CheckIfAjaxCall(It.IsAny<HttpContextBase>())).Returns(true);

            // Act
            // Assert
            commentController.WithCallTo(x => x.Index(new Guid())).ShouldRenderDefaultView();
        }

        [Test]
        public void CommentController_ShouldReturnDefaultViewWithCommentsViewModel_WhenIndexIsCalledAndIsAjaxCall()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);
            this.webHelperMock.Setup(x => x.CheckIfAjaxCall(It.IsAny<HttpContextBase>())).Returns(true);

            // Act
            // Assert
            commentController.WithCallTo(x => x.Index(new Guid()))
                .ShouldRenderDefaultView()
                .WithModel<CommentsViewModel>();
        }

        [Test]
        public void CommentController_ShouldReturnDefaultView_WhenIndexIsCalledAndIsNotAjaxCall()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);
            this.webHelperMock.Setup(x => x.CheckIfAjaxCall(It.IsAny<HttpContextBase>())).Returns(false);

            // Act
            // Assert
            commentController.WithCallTo(x => x.Index(new Guid())).ShouldRedirectTo("/");
        }

        [Test]
        public void WebHelper_ShouldCallCheckIfAjaxCall_WhenIndexIsCalled()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            commentController.Index(new Guid());

            // Assert
            this.webHelperMock.Verify(x => x.CheckIfAjaxCall(It.IsAny<HttpContextBase>()), Times.Once);
        }

        [Test]
        public void CommentService_ShouldCallGetExpenseCommentsOnce_WhenIndexIsCalled()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);
            this.webHelperMock.Setup(x => x.CheckIfAjaxCall(It.IsAny<HttpContextBase>())).Returns(true);

            // Act
            commentController.Index(new Guid());

            // Assert
            this.commentServiceMock.Verify(x => x.GetExpenseComments(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CreateGet_ShouldHaveChildActionOnlyAttribute()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            var result = commentController.GetType().GetMethod("Create", new Type[] { typeof(Guid) })
                .GetCustomAttributes(typeof(ChildActionOnlyAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void CommentController_ShouldReturn_CreateCommentPartial_WhenCreateGetIsCall()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            // Assert
            commentController.WithCallTo(c => c.Create(It.IsAny<Guid>())).ShouldRenderPartialView("_CreateCommentPartial");
        }

        [Test]
        public void CreatePost_ShouldHaveValidateAntiForgeryToken()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            var result = commentController.GetType().GetMethod("Create", new Type[] { typeof(CommentViewModel) })
                .GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void CommentService_ShouldCallAddCommentOnce_WhenCreatePostIsCalled()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            commentController.Create(new CommentViewModel());

            // Assert
            commentServiceMock.Verify(x => x.AddComment(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void WebHelper_ShouldCallGetUserIdOnce_WhenCreatePostIsCalled()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            commentController.Create(new CommentViewModel());

            // Assert
            webHelperMock.Verify(x => x.GetUserId(), Times.Once);
        }

        [Test]
        public void WebHelper_ShouldCallGetHouseholdNameFromCookieOnce_WhenCreatePostIsCalled()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            commentController.Create(new CommentViewModel());

            // Assert
            webHelperMock.Verify(x => x.GetHouseholdNameFromCookie(), Times.Once);
        }

        [Test]
        public void CommentController_ShouldRedirectToRoute_WhenCreatePostIsCall()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);

            // Act
            // Assert
            commentController.WithCallTo(c => c.Create(new CommentViewModel()))
                .ShouldRedirectToRoute("Household_single_expense");
        }
    }
}
