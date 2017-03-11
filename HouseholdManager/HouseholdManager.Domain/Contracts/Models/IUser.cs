using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IUser
    {
        string Id { get; set; }

        string FirstName { get; }

        string LastName { get; }

        string Username { get; }

        string Email { get; }

        string PhoneNumber { get; set; }

        IHousehold CurrentHousehold { get; set; }

        bool IsDeleted { get; set; }

        ICollection<IExpense> ExpensesWillPay { get; set; }

        ICollection<IExpense> ExpensesPaid { get; set; }

        ICollection<IComment> Comments { get; set; }

        ICollection<IHousehold> Households { get; set; }
    }
}
