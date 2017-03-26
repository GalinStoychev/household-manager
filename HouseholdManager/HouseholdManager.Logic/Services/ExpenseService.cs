using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Logic.Dtos;
using HouseholdManager.Models;
using System;
using System.Collections.Generic;
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
                   .OrderByDescending(x => x.CreatedOn)
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
                  .OrderByDescending(x => x.CreatedOn)
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

            if (!String.IsNullOrEmpty(comment))
            {
                expense.AddComment(this.commentFactory.CreateComment(userId, comment, DateTime.Now, expense.Id));
            }

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

        public void DeleteExpense(Guid expenseId, bool isDeleted)
        {
            var expense = this.expenseRepositoryEF.GetById(expenseId);
            expense.Delete(isDeleted);

            this.expenseRepositoryEF.Update(expense);
            this.unitOfWork.Commit();
        }

        public int GetExpensesCount()
        {
            var count = this.expenseRepositoryEF.GetAll().Count();
            return count;
        }

        public TotalMonthlyExpenses GetTotalExpenses(Guid householdId, int year, int month)
        {
            var totalExpenses = new TotalMonthlyExpenses();

            var allExpences = this.expenseRepositoryEF.GetAll<Expense>(
                x => x.PaidOnDate.Value.Year == year &&
                x.PaidOnDate.Value.Month == month &&
                x.HouseholdId == householdId,
                null, 
                x => x.PaidBy);

            if (allExpences.Count() != 0)
            {
                foreach (Expense exp in allExpences)
                {
                    totalExpenses.Total += exp.Cost;
                    var user = $"{exp.PaidBy.FirstName} {exp.PaidBy.LastName}";
                    if (!totalExpenses.MoneyPaid.ContainsKey(user))
                    {
                        totalExpenses.MoneyPaid.Add(user, 0);
                    }

                    totalExpenses.MoneyPaid[user] += exp.Cost;
                }

                var averageMoneyPerUser = totalExpenses.Total / totalExpenses.MoneyPaid.Count;
                foreach (var user in totalExpenses.MoneyPaid.Keys)
                {
                    var amountPaid = totalExpenses.MoneyPaid[user];
                    if (!totalExpenses.MoneyResult.ContainsKey(user))
                    {
                        totalExpenses.MoneyResult.Add(user, 0);
                    }

                    totalExpenses.MoneyResult[user] = averageMoneyPerUser - amountPaid;
                }
            }

            return totalExpenses;
        }
    }
}
