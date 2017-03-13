using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Contracts.Factories
{
    public interface ICommentFactory
    {
        Comment CreateComment(string userId, string content, DateTime createdOn, Guid expenseId);
    }
}
