using HouseholdManager.Common.Contracts;
using HouseholdManager.Web.Controllers;
using HouseholdManager.Web.WebHelpers.Contracts;
using Moq;
using NUnit.Framework;
using System;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class BaseControllerTests
    {
        [Test]
        public void BaseController_ShouldThrowArgumentNullException_WhenMappingServiceIsNull()
        {
            // Arrange
            var webHelperMock = new Mock<IWebHelper>();

            // Assert
            Assert.Throws<ArgumentNullException>(() => new BaseController(null, webHelperMock.Object));
        }

        [Test]
        public void BaseController_ShouldThrowArgumentNullException_WhenWebHelperIsNull()
        {
            // Arrange
            var mappingServiceMock = new Mock<IMapingService>();

            // Assert
            Assert.Throws<ArgumentNullException>(() => new BaseController(mappingServiceMock.Object, null));
        }
    }
}
