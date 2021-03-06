﻿using HouseholdManager.Logic.Dtos;
using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface IExpenseService
    {
        void CreateExpense(string userId, string name, Guid categoryId, Guid householdId, decimal expectedCost, DateTime dueDate, string comment, string assignedUserId);

        void UpdateExpense(Guid expenseId, string name, Guid categoryId, decimal expectedCost, DateTime dueDate, string assignedUserId);

        void DeleteExpense(Guid expenseId, bool isDeleted);

        IEnumerable<ExpenseCategory> GetExpenseCategories();

        void AssignUserToExpense(string userId, Guid expenseId);

        Expense GetExpense(Guid expenseId);

        IEnumerable<Expense> GetExpenses(Guid householdId, int page, bool isPaid, string searchPattern);

        int GetExpensesCount(Guid householdId, bool isPaid, string pattern);

        void Pay(Guid expenseId, string userId, string comment, decimal cost);

        int GetExpensesCount();

        TotalMonthlyExpenses GetTotalExpenses(Guid householdId, int year, int month);
    }
}
