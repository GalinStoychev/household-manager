using HouseholdManager.Logic.Contracts;
using System;
using System.Collections.Generic;
using HouseholdManager.Models;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Common.Constants;
using HouseholdManager.Logic.Contracts.Factories;

namespace HouseholdManager.Logic.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Expense> expenseRepositoryEF;
        private readonly IRepository<ExpenseCategory> expenseCategoryRepositoryEF;
        private readonly IExpenseFactory expenseFactory;
        private readonly ICommentFactory commentFactory;

        public ExpenseService(
            IUnitOfWork unitOfWork,
            IRepository<Expense> expenseRepositoryEF,
            IRepository<ExpenseCategory> expenseCategoryRepositoryEF,
            IExpenseFactory expenseFactory,
            ICommentFactory commentFactory)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "unitOfWork"));
            }

            if (expenseRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseRepositoryEF"));
            }

            if (expenseCategoryRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseCategoryRepositoryEF"));
            }

            if (expenseFactory == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseFactory"));
            }

            if (commentFactory == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseFactory"));
            }

            this.unitOfWork = unitOfWork;
            this.expenseRepositoryEF = expenseRepositoryEF;
            this.expenseCategoryRepositoryEF = expenseCategoryRepositoryEF;
            this.expenseFactory = expenseFactory;
            this.commentFactory = commentFactory;
        }

        public void AssignUserToExpense(string userId, Guid expenseId)
        {
            var expense = GetExpense(expenseId);
            expense.AssignUser(userId);

            this.expenseRepositoryEF.Update(expense);
            this.unitOfWork.Commit();
        }

        public void CreateExpense(string userId, string name, Guid categoryId, Guid householdId, decimal expectedCost, DateTime dueDate, string comment, string assignedUserId)
        {
            var expense = this.expenseFactory.CreateExpense(name, categoryId, userId, householdId, expectedCost, dueDate, DateTime.Now);

            if (!String.IsNullOrEmpty(assignedUserId))
            {
                expense.AssignUser(assignedUserId);
            }

            if (!String.IsNullOrEmpty(comment))
            {
                expense.AddComment(this.commentFactory.CreateComment(userId, comment, DateTime.Now, expense.Id));
            }

            this.expenseRepositoryEF.Add(expense);
            this.unitOfWork.Commit();
        }

        public Expense GetExpense(Guid expenseId)
        {
            var expense = this.expenseRepositoryEF.GetById(expenseId);
            return expense;
        }

        public IEnumerable<ExpenseCategory> GetExpenseCategories()
        {
            var categories = this.expenseCategoryRepositoryEF.GetAll();
            return categories;
        }

        public IEnumerable<Expense> GetExpenses(Guid householdId)
        {
            throw new NotImplementedException();
        }
    }
}
