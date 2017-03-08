using System;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IExpense : IIdentifiable
    {
        string Name { get; set; }

        string Category { get; set; }

        IHousehold Household { get; set; }

        decimal Cost { get; set; }

        decimal ExpectedCost { get; set; }

        bool IsPaid { get; set; }

        IUser AssignedUser { get; set; }

        IUser PaidBy { get; set; }

        DateTime DueDate { get; set; }

        DateTime PaidOnDate { get; set; }

        DateTime CreateOnDate { get; set; }

        bool IsDeleted { get; set; }

        ICollection<IComment> Comments { get; set; }
    }
}
