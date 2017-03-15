using HouseholdManager.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Models
{
    public class Expense : BaseEntity
    {
        private ICollection<Comment> comments;

        private Expense()
        {
            this.comments = new HashSet<Comment>();
        }

        public Expense(string name, Guid categoryId, string createdById, Guid householdId, decimal expectedCost, DateTime dueDate, DateTime createdOn)
        {
            this.Name = name;
            this.CategoryId = categoryId;
            this.CreatedById = createdById;
            this.HouseholdId = householdId;
            this.ExpectedCost = expectedCost;
            this.DueDate = dueDate;
            this.CreatedOn = createdOn;
            this.comments = new HashSet<Comment>();
        }

        [Required]
        public string Name { get; protected set; }

        [ForeignKey("ExpenseCategory")]
        public Guid CategoryId { get; protected set; }

        public virtual ExpenseCategory ExpenseCategory { get; protected set; }

        [ForeignKey("Household")]
        public Guid HouseholdId { get; protected set; }

        public virtual Household Household { get; protected set; }

        public decimal Cost { get; set; }

        public decimal ExpectedCost { get; set; }

        public bool IsPaid { get; protected set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; protected set; }

        public virtual User CreatedBy { get; protected set; }

        [ForeignKey("AssignedUser")]
        public string AssignedUserId { get; protected set; }

        public virtual User AssignedUser { get; protected set; }

        [ForeignKey("PaidBy")]
        public string PaidById { get; protected set; }

        public virtual User PaidBy { get; protected set; }

        public DateTime DueDate { get; protected set; }

        public DateTime? PaidOnDate { get; protected set; }

        public DateTime CreatedOn { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public void Pay(string userId, DateTime payDate, decimal cost)
        {
            if (this.IsPaid)
            {
                throw new ApplicationException("The expense is already paid!");
            }

            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNullOrEmpty, "userId"));
            }

            if (cost < 0)
            {
                throw new ArgumentOutOfRangeException("cost cannot be less than zero.");
            }

            this.IsPaid = true;
            this.PaidById = userId;
            this.PaidOnDate = payDate;
            this.Cost = cost;
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "comment"));
            }

            this.Comments.Add(comment);
        }

        public void AssignUser(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNullOrEmpty, "userId"));
            }

            this.AssignedUserId = userId;
        }
    }
}
