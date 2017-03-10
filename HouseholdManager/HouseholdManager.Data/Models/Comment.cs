using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Data.Models
{
    public class Comment: BaseEntity
    {
        private ICollection<Expense> expenses;

        public Comment()
        {
            this.expenses = new HashSet<Expense>();
        }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string CommentContent { get; set; }

        public DateTime CreatedOnDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Expense> Expenses
        {
            get { return this.expenses; }
            set { this.expenses = value; }
        }
    }
}
