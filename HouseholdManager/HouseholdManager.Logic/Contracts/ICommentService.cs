using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetExpenseComments(Guid expenseId);

        void AddComment(Guid expenseId, string userId, string content);
    }
}
