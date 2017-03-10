using HouseholdManager.Domain.Contracts.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Models
{
    public class Expense : BaseDomain, IExpense, IIdentifiable
    {
        public Expense(string name, string category, IHousehold household, decimal expectedCost, DateTime dueDate, DateTime createdOn)
        {
            this.Name = name;
            this.Category = category;
            this.Household = household;
            this.ExpectedCost = expectedCost;
            this.DueDate = dueDate;
            this.Comments = new List<IComment>();
            this.CreatedOnDate = createdOn;
        }

        public string Name { get; private set; }

        public string Category { get; set; }

        public IHousehold Household { get; private set; }

        public decimal Cost { get; set; }

        public decimal ExpectedCost { get; set; }

        public bool IsPaid { get; private set; }

        public IUser AssignedUser { get; set; }

        public IUser PaidBy { get; private set; }

        public DateTime DueDate { get; set; }

        public DateTime PaidOnDate { get; private set; }

        public DateTime CreatedOnDate { get; private set; }

        public bool IsDeleted { get; set; }

        public ICollection<IComment> Comments { get; private set; }

        public void Pay(IUser user, DateTime payDate)
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

        public void AddComment(IComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment cannot be null!");
            }

            this.Comments.Add(comment);
        }
    }
}
