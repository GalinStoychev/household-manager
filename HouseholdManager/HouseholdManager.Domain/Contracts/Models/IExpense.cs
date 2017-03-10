using System;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IExpense : IIdentifiable
    {
        string Name { get; }

        string Category { get; }

        IHousehold Household { get; }

        decimal Cost { get; set; }

        decimal ExpectedCost { get; }

        bool IsPaid { get; }

        IUser AssignedUser { get; set; }

        IUser PaidBy { get; }

        DateTime DueDate { get; set; }

        DateTime PaidOnDate { get; }

        DateTime CreatedOnDate { get; }

        bool IsDeleted { get; set; }

        ICollection<IComment> Comments { get; }
    }
}
