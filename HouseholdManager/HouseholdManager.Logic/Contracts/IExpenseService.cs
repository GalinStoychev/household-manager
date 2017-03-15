﻿using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface IExpenseService
    {
        void CreateExpense(string userId, string name, Guid categoryId, Guid householdId, decimal expectedCost, DateTime dueDate, string comment, string assignedUserId);

        IEnumerable<ExpenseCategory> GetExpenseCategories();

        void AssignUserToExpense(string userId, Guid expenseId);

        Expense GetExpense(Guid expenseId);

        IEnumerable<Expense> GetExpenses(Guid householdId, int page);

        int GetExpensesCount();

        void Pay(Guid expenseId, string userId, string comment, decimal cost);
    }
}
