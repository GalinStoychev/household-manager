using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Contracts.Factories
{
    public interface IExpenseFactory
    {
        Expense CreateExpense(string name, Guid categoryId, string createdById, Guid householdId, decimal expectedCost, DateTime dueDate, DateTime createdOn);
    }
}
