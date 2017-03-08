using System;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IComment: IIdentifiable
    {
        IUser User { get; set; }

        string CommentContent { get; set; }

        DateTime CreatedOnDate { get; set; }

        bool IsDeleted { get; set; }

        ICollection<IExpense> Expenses { get; set; }
    }
}
