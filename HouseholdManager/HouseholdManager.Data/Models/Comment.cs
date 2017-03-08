using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Data.Models
{
    public class Comment
    {
        private ICollection<Expense> expenses;

        public Comment()
        {
            this.expenses = new HashSet<Expense>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

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
