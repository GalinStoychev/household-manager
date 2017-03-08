using HouseholdManager.Domain.Contracts.Models;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Models
{
    public class User : BaseDomain, IUser, IIdentifiable
    {
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
