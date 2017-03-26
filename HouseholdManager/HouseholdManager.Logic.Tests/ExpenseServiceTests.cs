using System;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Models;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Logic.Services;
using System.Linq.Expressions;

namespace HouseholdManager.Logic.Tests
{
    [TestFixture]
    public class ExpenseServiceTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IRepository<Expense>> expenseRepoMock;
        private Mock<IRepository<ExpenseCategory>> expenseCategoryRepoMock;
        private Mock<IExpenseFactory> expenseFactoryMock;
        private Mock<ICommentFactory> commentFactoryMock;


        [SetUp]
        public void SetUpMocks()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.expenseRepoMock = new Mock<IRepository<Expense>>();
            this.expenseCategoryRepoMock = new Mock<IRepository<ExpenseCategory>>();
            this.expenseFactoryMock = new Mock<IExpenseFactory>();
            this.commentFactoryMock = new Mock<ICommentFactory>();
        }

        [Test]
        public void ExpenseServiceService_ShouldThrowNewArgumentException_WhenUnitOfWorkIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseService(null, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object));
        }


        [Test]
        public void ExpenseServiceService_ShouldThrowNewArgumentException_WhenExpenseRepositoryIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseService(unitOfWorkMock.Object, null, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object));
        }


        [Test]
        public void ExpenseServiceService_ShouldThrowNewArgumentException_WhenExpenseCategoryRepositoryIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, null, expenseFactoryMock.Object, commentFactoryMock.Object));
        }


        [Test]
        public void ExpenseServiceService_ShouldThrowNewArgumentException_WhenExpenseFactoryIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, null, commentFactoryMock.Object));
        }


        [Test]
        public void ExpenseServiceService_ShouldThrowNewArgumentException_WhenCommentFacotryIsNull()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, null));
        }

        [Test]
        public void ExpenseRepository_ShouldCallUpdateOnce_WhenAssignUserToExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetFirst(
                It.IsAny<Expression<Func<Expense, bool>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>()))
                .Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.AssignUserToExpense("_", new Guid());

            // Assert
            this.expenseRepoMock.Verify(x => x.Update(It.IsAny<Expense>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenAssignUserToExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetFirst(
                It.IsAny<Expression<Func<Expense, bool>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>()))
                .Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.AssignUserToExpense("_", new Guid());

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ExpenseRepository_ShouldCallGetFirstOnce_WhenAssignUserToExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetFirst(
                It.IsAny<Expression<Func<Expense, bool>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>()))
                .Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.GetExpense(new Guid());

            // Assert
            this.expenseRepoMock.Verify(x => x.GetFirst(
                It.IsAny<Expression<Func<Expense, bool>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>(),
                It.IsAny<Expression<Func<Expense, object>>>()),
                Times.Once);
        }

        [Test]
        public void ExpenseFactory_ShouldCallCreateExpenseOnce_WhenCreateExpenseIsCalled()
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.CreateExpense("_", "_", new Guid(), new Guid(), 1M, DateTime.Now, null, null);

            // Assert
            this.expenseFactoryMock.Verify(x => x.CreateExpense(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<decimal>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                Times.Once);
        }

        [Test]
        public void Expenserepository_ShouldCallAddOnce_WhenCreateExpenseIsCalled()
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.CreateExpense("_", "_", new Guid(), new Guid(), 1M, DateTime.Now, null, null);

            // Assert
            this.expenseRepoMock.Verify(x => x.Add(It.IsAny<Expense>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenCreateExpenseIsCalled()
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.CreateExpense("_", "_", new Guid(), new Guid(), 1M, DateTime.Now, null, null);

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }


        [TestCase(null)]
        [TestCase("")]
        public void CommentFactory_ShouldNotCallCreateComment_WhenProvidedCommentIsNullOrEmptyAndCreateExpenseIsCalled(string comment)
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.CreateExpense("_", "_", new Guid(), new Guid(), 1M, DateTime.Now, comment, null);

            // Assert
            this.commentFactoryMock.Verify(x => x.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public void CommentFactory_ShouldCallCreateCommentOnce_WhenCommentIsProvidedAndCreateExpenseIsCalled()
        {
            // Arrange
            var expense = new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now);
            this.expenseFactoryMock.Setup(x => x.CreateExpense(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<decimal>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(expense);
            this.commentFactoryMock.Setup(x => x.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Guid>()))
                .Returns(new Comment("_", "_", DateTime.Now, new Guid()));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.CreateExpense("_", "_", new Guid(), new Guid(), 1M, DateTime.Now, "_", null);

            // Assert
            this.commentFactoryMock.Verify(x => x.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ExpenseCategoryRepo_ShouldCallGetAllOnce_WhenGetExpenseCategoriesIsCalled()
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.GetExpenseCategories();

            // Assert
            this.expenseCategoryRepoMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetExpenseCategories_ShouldReturnIEnumerableOfExpenseCategory_WhenItIsCalled()
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            var result = expenseService.GetExpenseCategories();

            // Assert
            Assert.IsInstanceOf<IEnumerable<ExpenseCategory>>(result);
        }

        [Test]
        public void ExpenseRepository_ShouldCallUpdateOnce_WhenPayIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetFirst(
              It.IsAny<Expression<Func<Expense, bool>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>()))
              .Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.Pay(new Guid(), "_", null, 1M);

            // Assert
            this.expenseRepoMock.Verify(x => x.Update(It.IsAny<Expense>()), Times.Once);
        }

        [Test]
        public void UnitOfWOrk_ShouldCallCommitOnce_WhenPayIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetFirst(
              It.IsAny<Expression<Func<Expense, bool>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>()))
              .Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.Pay(new Guid(), "_", null, 1M);

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void CommentFactory_ShouldCallCreateCommentOnce_WhenThePassedCommentTextIsNotNullOrEmptyPayIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetFirst(
              It.IsAny<Expression<Func<Expense, bool>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>()))
              .Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            this.commentFactoryMock.Setup(x => x.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Guid>()))
               .Returns(new Comment("_", "_", DateTime.Now, new Guid()));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.Pay(new Guid(), "_", "_", 1M);

            // Assert
            this.commentFactoryMock.Verify(x => x.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Guid>()),
                Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CommentFactory_ShouldNotCallCreateComment_WhenThePassedCommentTextIsNullOrEmptyPayIsCalled(string comment)
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetFirst(
              It.IsAny<Expression<Func<Expense, bool>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>(),
              It.IsAny<Expression<Func<Expense, object>>>()))
              .Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));

            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.Pay(new Guid(), "_", comment, 1M);

            // Assert
            this.commentFactoryMock.Verify(x => x.CreateComment(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Guid>()),
               Times.Never);
        }

        [Test]
        public void ExpenseRepository_ShouldCallGetById_WhenUpdateExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.UpdateExpense(new Guid(), "_", new Guid(), 1M, DateTime.Now, "'_");

            // Assert
            this.expenseRepoMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ExpenseRepository_ShouldCallUpdateOnce_WhenUpdateExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.UpdateExpense(new Guid(), "_", new Guid(), 1M, DateTime.Now, "'_");

            // Assert
            this.expenseRepoMock.Verify(x => x.Update(It.IsAny<Expense>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenUpdateExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.UpdateExpense(new Guid(), "_", new Guid(), 1M, DateTime.Now, "'_");

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ExpenseRepository_ShouldCallGetById_WhenDeleteExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.DeleteExpense(new Guid(), false);

            // Assert
            this.expenseRepoMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ExpenseRepository_ShouldCallUpdateOnce_WhenDeleteExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.DeleteExpense(new Guid(), false);

            // Assert
            this.expenseRepoMock.Verify(x => x.Update(It.IsAny<Expense>()), Times.Once);
        }

        [Test]
        public void UnitOfWork_ShouldCallCommitOnce_WhenDeleteExpenseIsCalled()
        {
            // Arrange
            this.expenseRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Expense("_", new Guid(), "_", new Guid(), 1M, DateTime.Now, DateTime.Now));
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.DeleteExpense(new Guid(), false);

            // Assert
            this.unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void ExpenseRepository_ShouldCallGetAllOnce_WhenGetExpensesCountIsCalled()
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            expenseService.GetExpensesCount();

            // Assert
            this.expenseRepoMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void GetExpensesCount_ShouldReturnInt_WhenItIsCalled()
        {
            // Arrange
            var expenseService = new ExpenseService(unitOfWorkMock.Object, expenseRepoMock.Object, expenseCategoryRepoMock.Object, expenseFactoryMock.Object, commentFactoryMock.Object);

            // Act
            var result = expenseService.GetExpensesCount();

            // Assert
            Assert.IsInstanceOf<int>(result);
        }
    }
}
