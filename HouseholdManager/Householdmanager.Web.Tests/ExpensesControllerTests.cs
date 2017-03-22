using System;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Web.WebHelpers.Contracts;
using HouseholdManager.Web.Areas.Household.Controllers;
using HouseholdManager.Web.Areas.Household.Models;
using System.Collections.Generic;
using HouseholdManager.Models;

namespace Householdmanager.Web.Tests
{
    [TestFixture]
    public class ExpensesControllerTests
    {
        private Mock<IExpenseService> expenseServiceMock;
        private Mock<IMapingService> mappingServiceMock;
        private Mock<IWebHelper> webHelperMock;

        [SetUp]
        public void SetUpMocks()
        {
            this.expenseServiceMock = new Mock<IExpenseService>();
            this.mappingServiceMock = new Mock<IMapingService>();
            this.webHelperMock = new Mock<IWebHelper>();
        }

        [Test]
        public void ExpensesController_ShouldThrowArgumentNullException_WhenExpenseServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ExpensesController(null, mappingServiceMock.Object, webHelperMock.Object));
        }

        [Test]
        public void ExpensesController_ShouldHaveOneAuthorizeAttribute()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            var result = expensesController.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void ExpenseController_SholdReturnDefaultView_WhenIndexIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            expensesController.WithCallTo(c => c.Index("_", "_", 1)).ShouldRenderDefaultView();
        }

        [Test]
        public void ExpenseController_SholdReturnDefaultViewWithShowExpenseViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            expensesController.WithCallTo(c => c.Index("_", "_", 1))
                .ShouldRenderDefaultView()
                 .WithModel<ShowExpensesViewModel>();
        }

        [Test]
        public void ExpenseService_SholdCallGetExpensesCountOnce_WhenIndexIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            expensesController.Index("_", "_", 1);

            // Assert
            expenseServiceMock.Verify(x => x.GetExpensesCount(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void WebHelper_SholdCallGetHouseholdIdFromCookieOnce_WhenIndexIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            expensesController.Index("_", "_", 1);

            // Assert
            webHelperMock.Verify(x => x.GetHouseholdIdFromCookie(), Times.Once);
        }

        [Test]
        public void ExpenseService_SholdCallGetExpensesOnce_WhenIndexIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            expensesController.Index("_", "_", 1);

            // Assert
            expenseServiceMock.Verify(x => x.GetExpenses(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()), Times.Once);
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void MappingService_SholdCallMapAsManyTimesAsExpensesAreReturnedFromExpenseService_WhenIndexIsCalled(int numberOfExpenses)
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var expenses = new List<Expense>();
            for (int i = 0; i < numberOfExpenses; i++)
            {
                expenses.Add(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            }

            expenseServiceMock.Setup(x => x.GetExpenses(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(expenses);

            // Act
            expensesController.Index("_", "_", 1);

            // Assert
            mappingServiceMock.Verify(x => x.Map<ExpenseViewModel>(It.IsAny<Expense>()), Times.Exactly(numberOfExpenses));
        }

        [Test]
        public void Pay_ShouldHaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);

            // Act
            var result = expensesController.GetType().GetMethod("Pay", new Type[] { typeof(ExpenseViewModel) })
                .GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void ExpenseController_SholdRedirectToRoutNamed_HouseholdExpenses_WhenPayIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel() { Cost = 1M };

            // Act
            // Assert
            expensesController.WithCallTo(c => c.Pay(model)).ShouldRedirectToRoute("Household_expenses");
        }

        [Test]
        public void ExpenseService_SholdCallPayOnce_WhenPayIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel() { Cost = 1M };

            // Act
            expensesController.Pay(model);

            // Assert
            expenseServiceMock.Verify(x => x.Pay(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()), Times.Once);
        }

        [Test]
        public void WebHelper_SholdCallGetUserByIdOnce_WhenPayIsCalled()
        {
            // Arrange
            var expensesController = new ExpensesController(expenseServiceMock.Object, mappingServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel() { Cost = 1M };

            // Act
            expensesController.Pay(model);

            // Assert
            webHelperMock.Verify(x => x.GetUserId(), Times.Once);
        }
    }
}
