﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HouseholdManager.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        private ICollection<Expense> expensesWillPay;

        private ICollection<Expense> expensesPaid;

        private ICollection<Comment> comments;

        private ICollection<Household> households;

        private ICollection<Invitation> invitations;

        public User()
        {
            this.expensesWillPay = new HashSet<Expense>();
            this.expensesPaid = new HashSet<Expense>();
            this.comments = new HashSet<Comment>();
            this.households = new HashSet<Household>();
            this.invitations = new HashSet<Invitation>();
        }

        public User(string username, string email)
            : this()
        {
            this.UserName = username;
            this.Email = email;
        }

        public User(string username, string email, string firstName, string lastName, string phoneNumber)
            : this(username, email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
        }

        public string FirstName { get; protected set; }

        public string LastName { get; protected set; }

        [ForeignKey("CurrentHousehold")]
        public Guid? CurrentHouseholdId { get; protected set; }

        public virtual Household CurrentHousehold { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public virtual ICollection<Expense> ExpensesWillPay
        {
            get { return this.expensesWillPay; }
            protected set { this.expensesWillPay = value; }
        }

        public virtual ICollection<Expense> ExpensesPaid
        {
            get { return this.expensesPaid; }
            protected set { this.expensesPaid = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            protected set { this.comments = value; }
        }

        public virtual ICollection<Household> Households
        {
            get { return this.households; }
            protected set { this.households = value; }
        }

        public virtual ICollection<Invitation> Invitations
        {
            get { return this.invitations; }
            protected set { this.invitations = value; }
        }

        public void Update(string firstName, string lastName, string phoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
        }

        public void Delete(bool isDeleted)
        {
            this.IsDeleted = isDeleted;
        }

        public void SetCurrentHousehold(Household household)
        {
            this.CurrentHousehold = household;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
