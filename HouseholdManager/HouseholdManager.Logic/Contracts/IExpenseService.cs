using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface IExpenseService
    {
        void CreateExpense(string name, string category, Guid householdId, decimal expectedCost, DateTime dueDate, DateTime createdOn);

        IEnumerable<ExpenseCategory> GetExpenseCategories();

        void AssignUserToExpense(string userId, Guid expenseId);

        Expense GetExpense(Guid expenseId);

        IEnumerable<Expense> GetExpenses (Guid householdId);
    }
}
