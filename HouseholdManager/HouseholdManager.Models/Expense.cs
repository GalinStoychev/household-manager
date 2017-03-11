using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Models
{
    public class Expense: BaseEntity
    {
        private ICollection<Comment> comments;

        public Expense()
        {
            this.comments = new HashSet<Comment>();
        }

        public Expense(string name, ExpenseCategory category, Household household, decimal expectedCost, DateTime dueDate, DateTime createdOn)
        {
            this.Name = name;
            this.ExpenseCategory = category;
            this.Household = household;
            this.ExpectedCost = expectedCost;
            this.DueDate = dueDate;
            this.Comments = new List<Comment>();
            this.CreatedOn = createdOn;
            this.comments = new HashSet<Comment>();
        }

        [Required]
        public string Name { get; set; }

        [ForeignKey("ExpenseCategory")]
        public Guid CategoryId { get; set; }

        public virtual ExpenseCategory ExpenseCategory { get; set; }

        [ForeignKey("Household")]
        public Guid HouseholdId { get; set; }

        public virtual Household Household { get; set; }

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

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public void Pay(User user, DateTime payDate)
        {
            if (this.IsPaid)
            {
                throw new ApplicationException("The expense is already paid!");
            }

            if (user == null)
            {
                throw new ArgumentNullException("user cannot be null!");
            }

            this.IsPaid = true;
            this.PaidBy = user;
            this.PaidOnDate = payDate;
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment cannot be null!");
            }

            this.Comments.Add(comment);
        }
    }
}
