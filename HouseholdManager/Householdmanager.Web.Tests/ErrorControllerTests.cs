using HouseholdManager.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class ErrorControllerTests
    {
        [Test]
        public void Index_SholdReturnDefaultView_WhenCalled()
        {
            // Arrange
            var errorController = new ErrorController();

            // Act
            // Assert
            errorController.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void Unauthorized_SholdReturnDefaultView_WhenCalled()
        {
            // Arrange
            var errorController = new ErrorController();

            // Act
            // Assert
            errorController.WithCallTo(c => c.Unauthorized()).ShouldRenderDefaultView();
        }

        [Test]
        public void NotFound_SholdReturnDefaultView_WhenCalled()
        {
            // Arrange
            var errorController = new ErrorController();

            // Act
            // Assert
            errorController.WithCallTo(c => c.NotFound()).ShouldRenderDefaultView();
        }

        [Test]
        public void BadRequest_SholdReturnDefaultView_WhenCalled()
        {
            // Arrange
            var errorController = new ErrorController();

            // Act
            // Assert
            errorController.WithCallTo(c => c.BadRequest()).ShouldRenderDefaultView();
        }

        [Test]
        public void ServerError_SholdReturnDefaultView_WhenCalled()
        {
            // Arrange
            var errorController = new ErrorController();

            // Act
            // Assert
            errorController.WithCallTo(c => c.ServerError()).ShouldRenderDefaultView();
        }
    }
}
