using HouseholdManager.Logic.Contracts;
using System;
using System.Collections.Generic;
using HouseholdManager.Models;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Common.Constants;

namespace HouseholdManager.Logic.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Expense> expenseRepositoryEF;
        private readonly IRepository<ExpenseCategory> expenseCategoryRepositoryEF;

        public ExpenseService(IUnitOfWork unitOfWork, IRepository<Expense> expenseRepositoryEF, IRepository<ExpenseCategory> expenseCategoryRepositoryEF)
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

            this.unitOfWork = unitOfWork;
            this.expenseRepositoryEF = expenseRepositoryEF;
            this.expenseCategoryRepositoryEF = expenseCategoryRepositoryEF;
        }

        public void AssignUserToExpense(string userId, Guid expenseId)
        {
            throw new NotImplementedException();
        }

        public void CreateExpense(string name, string category, Guid householdId, decimal expectedCost, DateTime dueDate, DateTime createdOn)
        {
            throw new NotImplementedException();
        }

        public Expense GetExpense(Guid expenseId)
        {
            throw new NotImplementedException();
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
