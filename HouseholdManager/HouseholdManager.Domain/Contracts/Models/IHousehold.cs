using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IHousehold: IIdentifiable
    {
        string Name { get; }

        byte[] Image { get; }

        string Address { get; }

        bool IsDeleted { get; set; }

        ICollection<IUser> Users { get; set; }

        ICollection<IExpense> Expenses { get; set; }
    }
}
