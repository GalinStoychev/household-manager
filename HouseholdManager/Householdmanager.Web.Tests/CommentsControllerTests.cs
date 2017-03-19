using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Comments.Controllers;
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
        public void CommentController_ShouldReturnDefaultView_WhenIndexIsCalled()
        {
            // Arrange
            var commentController = new CommentsController(mappingServiceMock.Object, webHelperMock.Object, commentServiceMock.Object);
            // Act
            // Assert
            commentController.WithCallTo(x => x.Index(new Guid())).ShouldRenderDefaultView();
        }
    }
}
