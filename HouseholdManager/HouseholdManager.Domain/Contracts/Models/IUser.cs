using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IUser : IIdentifiable
    {
       string FirstName { get; set; }

       string LastName { get; set; }

       string Username { get; set; }

       string Email { get; set; }

       string PhoneNumber { get; set; }

       bool IsDeleted { get; set; }

       ICollection<IExpense> ExpensesWillPay { get; set; }

       ICollection<IExpense> ExpensesPaid { get; set; }

       ICollection<IComment> Comments { get; set; }

       ICollection<IHousehold> Households { get; set; }
    }
}
