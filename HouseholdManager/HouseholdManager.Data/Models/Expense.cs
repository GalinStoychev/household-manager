using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdManager.Data.Models
{
    public class Expense
    {
        private ICollection<Comment> comments;

        public Expense()
        {
            this.comments = new HashSet<Comment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("ExpenseCategory")]
        public Guid CategoryId { get; set; }

        public virtual ExpenseCategory ExpenseCategory { get; set; }

        [ForeignKey("Household")]
        public Guid HouseholdId { get; set; }

        public decimal Cost { get; set; }

        public decimal ExpectedCost { get; set; }

        public bool IsPaid { get; set; }

        [ForeignKey("AssignedUser")]
        public string AssignedUserId { get; set; }

        public virtual User AssignedUser { get; set; }

        [ForeignKey("PaidBy")]
        public string PaidById { get; set; }

        public virtual User PaidBy { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PaidOnDate { get; set; }

        public DateTime CreateOnDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Household Household { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
