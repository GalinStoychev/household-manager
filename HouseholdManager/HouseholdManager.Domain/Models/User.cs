using HouseholdManager.Domain.Contracts.Models;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Models
{
    public class User : BaseDomain, IUser, IIdentifiable
    {
        public User(string firstName, string lastName, string username, string email, string phoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.ExpensesWillPay = new List<IExpense>();
            this.ExpensesPaid = new List<IExpense>();
            this.Comments = new List<IComment>();
            this.Households = new List<IHousehold>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<IExpense> ExpensesWillPay { get; set; }

        public ICollection<IExpense> ExpensesPaid { get; set; }

        public ICollection<IComment> Comments { get; set; }

        public ICollection<IHousehold> Households { get; set; }
    }
}
