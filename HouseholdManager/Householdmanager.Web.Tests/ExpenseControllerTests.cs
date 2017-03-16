using HouseholdManager.Common.Contracts;
using HouseholdManager.Models;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Controllers;
using HouseholdManager.Web.Areas.Household.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class ExpenseControllerTests
    {
        private Mock<IExpenseService> expenseServiceMock;
        private Mock<IMapingService> mappingServiceMock;
        private Mock<IHouseholdService> householdServiceMock;
        private Mock<IWebHelper> webHelperMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.expenseServiceMock = new Mock<IExpenseService>();
            this.mappingServiceMock = new Mock<IMapingService>();
            this.householdServiceMock = new Mock<IHouseholdService>();
            this.webHelperMock = new Mock<IWebHelper>();
        }

        [Test]
        public void ExpenseController_ShouldThrowArgumentNullException_WhenExpenseServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseController(null, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void ExpenseController_ShouldThrowArgumentNullException_WhenMappingServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseController(expenseServiceMock.Object, null, householdServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void ExpenseController_ShouldThrowArgumentNullException_WhenHouseholdServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, null, webHelperMock.Object));
        }

        [Test]
        public void ExpenseController_ShouldThrowArgumentNullException_WhenWebServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, null));
        }

        [Test]
        public void ExpenseController_ShouldHaveOneAuthorizeAttribute()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            var result = expenseController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultView_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Index(new Guid().ToString())).ShouldRenderDefaultView();
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultViewWithShowExpenseViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            this.mappingServiceMock.Setup(x => x.Map<ShowExpenseViewModel>(It.IsAny<object>())).Returns(new ShowExpenseViewModel());

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Index(new Guid().ToString()))
                .ShouldRenderDefaultView()
                .WithModel<ShowExpenseViewModel>();
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultViewWithParticularShowExpenseViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var viewModelToReturn = new ShowExpenseViewModel();
            this.mappingServiceMock.Setup(x => x.Map<ShowExpenseViewModel>(It.IsAny<object>())).Returns(viewModelToReturn);

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Index(new Guid().ToString()))
                .ShouldRenderDefaultView()
                .WithModel(viewModelToReturn);
        }

        [Test]
        public void ExpenseService_ShouldCallGetExpenseOnce_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Index(new Guid().ToString());

            // Assert
            expenseServiceMock.Verify(x => x.GetExpense(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void MappingService_ShouldCallMapOnce_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Index(new Guid().ToString());

            // Assert
            mappingServiceMock.Verify(x => x.Map<ShowExpenseViewModel>(It.IsAny<Expense>()), Times.Once);
        }
    }
}
