using HouseholdManager.Domain.Contracts.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Models
{
    public class Expense: BaseDomain, IExpense, IIdentifiable
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public IHousehold Household { get; set; }

        public decimal Cost { get; set; }

        public decimal ExpectedCost { get; set; }

        public bool IsPaid { get; set; }

        public IUser AssignedUser { get; set; }

        public IUser PaidBy { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PaidOnDate { get; set; }

        public DateTime CreateOnDate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<IComment> Comments { get; set; }
    }
}
