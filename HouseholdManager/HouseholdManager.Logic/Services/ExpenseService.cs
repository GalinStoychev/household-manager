using HouseholdManager.Logic.Contracts;
using System;
using System.Collections.Generic;
using HouseholdManager.Models;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Common.Constants;
using HouseholdManager.Logic.Contracts.Factories;
using System.Data.Entity;
using System.Linq;

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
            var expense = this.expenseRepositoryEF.GetFirst(
                x => x.Id == expenseId,
                x => x.CreatedBy,
                x => x.AssignedUser,
                x => x.PaidBy,
                x => x.ExpenseCategory);

            return expense;
        }

        public IEnumerable<ExpenseCategory> GetExpenseCategories()
        {
            var categories = this.expenseCategoryRepositoryEF.GetAll();
            return categories;
        }

        public IEnumerable<Expense> GetExpenses(Guid householdId, int page, bool isPaid, string searchPattern)
        {
            IEnumerable<Expense> expenses = null;
            if (!String.IsNullOrEmpty(searchPattern))
            {
                var patternToLower = searchPattern.ToLower();
                expenses = this.expenseRepositoryEF.All
                   .Where(x => x.HouseholdId == householdId &&
                   x.IsPaid == isPaid &&
                   x.IsDeleted == false &&
                   (x.Name.ToLower().IndexOf(patternToLower) > -1 ||
                       x.ExpenseCategory.Name.ToLower().IndexOf(patternToLower) > -1 ||
                       x.AssignedUser.FirstName.ToLower().IndexOf(patternToLower) > -1 ||
                       x.AssignedUser.LastName.ToLower().IndexOf(patternToLower) > -1))
                   .Include(x => x.AssignedUser)
                   .Include(x => x.ExpenseCategory)
                   .OrderBy(x => x.DueDate)
                   .Skip((page - 1) * CommonConstants.DefaultPageSize)
                   .Take(CommonConstants.DefaultPageSize)
                   .ToList();
            }
            else
            {
                expenses = this.expenseRepositoryEF.All
                  .Where(x => x.HouseholdId == householdId && x.IsPaid == isPaid && x.IsDeleted == false)
                  .Include(x => x.AssignedUser)
                  .Include(x => x.ExpenseCategory)
                  .OrderBy(x => x.DueDate)
                  .Skip((page - 1) * CommonConstants.DefaultPageSize)
                  .Take(CommonConstants.DefaultPageSize)
                  .ToList();
            }

            return expenses;
        }

        public int GetExpensesCount(Guid householdId, bool isPaid, string pattern)
        {
            int count = 0;
            if (!String.IsNullOrEmpty(pattern))
            {
                var patternToLower = pattern.ToLower();
                count = this.expenseRepositoryEF.GetAll<Expense>(
                    x => x.HouseholdId == householdId &&
                    x.IsPaid == isPaid &&
                    x.IsDeleted == false &&
                    (x.ExpenseCategory.Name.ToLower().IndexOf(patternToLower) > -1 ||
                    x.Name.ToLower().IndexOf(patternToLower) > -1 ||
                    x.AssignedUser.FirstName.ToLower().IndexOf(patternToLower) > -1 ||
                    x.AssignedUser.LastName.ToLower().IndexOf(patternToLower) > -1)
                    , null).Count();
            }
            else
            {
                count = this.expenseRepositoryEF.GetAll<Expense>(
                    x => x.HouseholdId == householdId && x.IsPaid == isPaid && x.IsDeleted == false, null).Count();
            }

            return count;
        }

        public void Pay(Guid expenseId, string userId, string comment, decimal cost)
        {
            var expense = this.GetExpense(expenseId);
            expense.Pay(userId, DateTime.Now, cost);
            expense.AddComment(this.commentFactory.CreateComment(userId, comment, DateTime.Now, expense.Id));

            this.expenseRepositoryEF.Update(expense);
            this.unitOfWork.Commit();
        }

        public void UpdateExpense(Guid expenseId, string name, Guid categoryId, decimal expectedCost, DateTime dueDate, string assignedUserId)
        {
            var expense = this.expenseRepositoryEF.GetById(expenseId);
            expense.Update(name, categoryId, expectedCost, dueDate, assignedUserId);

            this.expenseRepositoryEF.Update(expense);
            this.unitOfWork.Commit();
        }

        public void Delete(Guid expenseId, bool isDeleted)
        {
            var expense = this.expenseRepositoryEF.GetById(expenseId);
            expense.Delete(isDeleted);

            this.expenseRepositoryEF.Update(expense);
            this.unitOfWork.Commit();
        }
    }
}
