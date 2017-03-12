using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Models
{
    public class Comment : BaseEntity
    {
        private Comment()
        {
        }

        public Comment(User user, string content, DateTime createdOn, Expense expense)
        {
            this.User = user;
            this.CommentContent = content;
            this.CreatedOnDate = createdOn;
            this.Expense = expense;
        }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; protected set; }

        public string CommentContent { get; protected set; }

        public DateTime CreatedOnDate { get; protected set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Expense")]
        public Guid ExpenseId { get; protected set; }

        public virtual Expense Expense { get; protected set; }
    }
}
