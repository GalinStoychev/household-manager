using HouseholdManager.Domain.Contracts.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Models
{
    public class Comment : BaseDomain, IComment, IIdentifiable
    {
        public IUser User { get; set; }

        public string CommentContent { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<IExpense> Expenses { get; set; }
    }
}
