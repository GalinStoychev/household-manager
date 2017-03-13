using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Models
{
    public class Comment : BaseEntity
    {
        private Comment()
        {
        }

        public Comment(string userId, string content, DateTime createdOn, Guid expenseId)
        {
            this.UserId = userId;
            this.CommentContent = content;
            this.CreatedOnDate = createdOn;
            this.ExpenseId = expenseId;
        }

        [ForeignKey("User")]
        public string UserId { get; protected set; }

        public virtual User User { get; protected set; }

        public string CommentContent { get; protected set; }

        public DateTime CreatedOnDate { get; protected set; }

        public bool IsDeleted { get; protected set; }

        [ForeignKey("Expense")]
        public Guid ExpenseId { get; protected set; }

        public virtual Expense Expense { get; protected set; }
    }
}
