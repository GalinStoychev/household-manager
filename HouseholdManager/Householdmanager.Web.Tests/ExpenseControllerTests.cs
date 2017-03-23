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
        public void ExpenseController_ShouldThrowArgumentNullException_WhenHouseholdServiceIsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, null, webHelperMock.Object));
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
            expenseController.WithCallTo(x => x.Index(new Guid())).ShouldRenderDefaultView();
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultViewWithShowExpenseViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Index(new Guid()))
                .ShouldRenderDefaultView()
                .WithModel<ExpenseViewModel>();
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultViewWithParticularShowExpenseViewModel_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var viewModelToReturn = new ExpenseViewModel();
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(viewModelToReturn);

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Index(new Guid()))
                .ShouldRenderDefaultView()
                .WithModel(viewModelToReturn);
        }

        [Test]
        public void ExpenseController_ShouldRedirectToErrorUnauthorizedPage_WhenHouseholdIdIsNotEqualToExpenseHouseholdId()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.webHelperMock.Setup(x => x.GetHouseholdIdFromCookie()).Returns(Guid.NewGuid());

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Index(new Guid())).ShouldRedirectTo("/Error/Unauthorized");
        }

        [Test]
        public void ExpenseService_ShouldCallGetExpenseOnce_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Index(new Guid());

            // Assert
            expenseServiceMock.Verify(x => x.GetExpense(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void MappingService_ShouldCallMapOnce_WhenIndexIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Index(new Guid());

            // Assert
            mappingServiceMock.Verify(x => x.Map<ExpenseViewModel>(It.IsAny<Expense>()), Times.Once);
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultView_WhenCraeteGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Create()).ShouldRenderDefaultView();
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultViewWithShowExpenseViewModel_WhenCreateGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Create())
                .ShouldRenderDefaultView()
                .WithModel<ExpenseViewModel>();
        }

        [Test]
        public void ExpenseService_ShouldCallGetExpenseCategoriesOnce_WhenCreateGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Create();

            // Assert
            expenseServiceMock.Verify(x => x.GetExpenseCategories(), Times.Once);
        }

        [Test]
        public void WebHelper_ShouldCallGetHouseholdIdFromCookieOnce_WhenCreateGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Create();

            // Assert
            webHelperMock.Verify(x => x.GetHouseholdIdFromCookie(), Times.Once);
        }

        [Test]
        public void HouseholdService_ShouldCallGetHouseholdUsersOnce_WhenCreateGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Create();

            // Assert
            householdServiceMock.Verify(x => x.GetHouseholdUsers(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CreatePost_ShouldHaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            var result = expenseController.GetType().GetMethod("Create", new Type[] { typeof(ExpenseViewModel) })
                .GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void CreatePost_ShouldRedirectErrorBadRequest_WhenModelStateIsNotValid()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            expenseController.ModelState.AddModelError("key", "error message");

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Create(new ExpenseViewModel()))
                .ShouldRedirectTo("/Error/BadRequest");
        }

        [Test]
        public void ExpenseController_ShouldReturnDefaultView_WhenCraetePostIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel()
            {
                Name = "_",
                AssignedUser = "_",
                Category = new Guid().ToString(),
                ExpectedCost = 1M,
                Comment = "_"
            };

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Create(model))
                .ShouldRedirectTo<ExpensesController>(x => x.Index("_", "_", 1));
        }

        [Test]
        public void WebHelper_ShouldGetHouseholdIdFromCookieOnce_WhenCraetePostIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel()
            {
                Name = "_",
                AssignedUser = "_",
                Category = new Guid().ToString(),
                ExpectedCost = 1M,
                Comment = "_"
            };

            // Act
            expenseController.Create(model);

            // Assert
            this.webHelperMock.Verify(x => x.GetHouseholdIdFromCookie(), Times.Once);
        }

        [Test]
        public void ExpenseService_ShouldCreateExpenseOnce_WhenCraetePostIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel()
            {
                Name = "_",
                AssignedUser = "_",
                Category = new Guid().ToString(),
                ExpectedCost = 1M,
                Comment = "_"
            };

            // Act
            expenseController.Create(model);

            // Assert
            this.expenseServiceMock.Verify(x => x.CreateExpense(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<decimal>(),
                It.IsAny<DateTime>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void WebHelper_ShouldGetHouseholdNameFromCookieOnce_WhenCraetePostIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel()
            {
                Name = "_",
                AssignedUser = "_",
                Category = new Guid().ToString(),
                ExpectedCost = 1M,
                Comment = "_"
            };

            // Act
            expenseController.Create(model);

            // Assert
            this.webHelperMock.Verify(x => x.GetHouseholdNameFromCookie(), Times.Once);
        }

        [Test]
        public void WebHelper_ShouldGetUserIdOnce_WhenCraetePostIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var model = new ExpenseViewModel()
            {
                Name = "_",
                AssignedUser = "_",
                Category = new Guid().ToString(),
                ExpectedCost = 1M,
                Comment = "_"
            };

            // Act
            expenseController.Create(model);

            // Assert
            this.webHelperMock.Verify(x => x.GetUserId(), Times.Once);
        }

        [Test]
        public void ExpenseService_ShouldCallGetExpenseCategoriesOnce_WhenEditGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());
            // Act
            expenseController.Edit(new Guid().ToString());

            // Assert
            expenseServiceMock.Verify(x => x.GetExpenseCategories(), Times.Once);
        }

        [Test]
        public void WebHelper_ShouldCallGetHouseholdIdFromCookieTwice_WhenEditGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());

            // Act
            expenseController.Edit(new Guid().ToString());

            // Assert
            webHelperMock.Verify(x => x.GetHouseholdIdFromCookie(), Times.Exactly(2));
        }

        [Test]
        public void HouseholdService_ShouldCallGetHouseholdUsersOnce_WhenEditGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());

            // Act
            expenseController.Edit(new Guid().ToString());

            // Assert
            householdServiceMock.Verify(x => x.GetHouseholdUsers(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ExpenseService_ShouldCallGetExpenseOnce_WhenEditGetIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());

            // Act
            expenseController.Edit(new Guid().ToString());

            // Assert
            expenseServiceMock.Verify(x => x.GetExpense(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void EditGet_ShouldReturnDefaultView_WhenCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Edit(new Guid().ToString())).ShouldRenderDefaultView();
        }


        [Test]
        public void EditGet_ShouldReturnDefaultViewWithExpenseViewModel_WhenCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Edit(new Guid().ToString()))
                .ShouldRenderDefaultView()
                .WithModel<ExpenseViewModel>();
        }

        [Test]
        public void EditGet_ShouldRedirectToErrorUnauthorizedPage_WhenHouseholdIdIsNotEqualToExpenseHouseholdId()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseServiceMock.Setup(x => x.GetExpense(It.IsAny<Guid>())).Returns(expense);
            this.mappingServiceMock.Setup(x => x.Map<ExpenseViewModel>(It.IsAny<object>())).Returns(new ExpenseViewModel());
            this.webHelperMock.Setup(x => x.GetHouseholdIdFromCookie()).Returns(Guid.NewGuid());

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Edit(new Guid().ToString())).ShouldRedirectTo("/Error/Unauthorized");
        }

        [Test]
        public void ExpenseService_ShouldCallUpdateExpenseOnce_WhenEditPostIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Edit(new ExpenseViewModel() { Category = new Guid().ToString() });

            // Assert
            expenseServiceMock.Verify(x => x.UpdateExpense(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<decimal>(), It.IsAny<DateTime>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void EditPost_ShouldRedirectToAction_WhenIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Edit(new ExpenseViewModel() { Category = new Guid().ToString() }))
                .ShouldRedirectTo<ExpenseController>(x => x.Index(new Guid()));
        }

        [Test]
        public void EditPost_ShouldRedirectErrorBadRequest_WhenModelStateIsNotValid()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);
            expenseController.ModelState.AddModelError("key", "error message");

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Edit(new ExpenseViewModel() { Category = new Guid().ToString() }))
                .ShouldRedirectTo("/Error/BadRequest");
        }

        [Test]
        public void EditPost_ShouldHaveValidateAntiForgeryTokenAttribute()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            var result = expenseController.GetType().GetMethod("Edit", new Type[] { typeof(ExpenseViewModel) })
                .GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false).Length;

            // Assert
            Assert.That(result == 1);
        }

        [Test]
        public void DeletePost_ShouldRedirectToAction_WhenIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            // Assert
            expenseController.WithCallTo(x => x.Delete(new ExpenseViewModel()))
                .ShouldRedirectTo<ExpenseController>(x => x.Index(new Guid()));
        }

        [Test]
        public void ExpenseService_ShouldCallDeleteOnce_WhenDeletePostIsCalled()
        {
            // Arrange
            var expenseController = new ExpenseController(expenseServiceMock.Object, mappingServiceMock.Object, householdServiceMock.Object, webHelperMock.Object);

            // Act
            expenseController.Delete(new ExpenseViewModel());

            // Assert
            this.expenseServiceMock.Verify(x => x.Delete(It.IsAny<Guid>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
