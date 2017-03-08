using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Models
{
    public interface IHousehold: IIdentifiable
    {
        string Name { get; set; }

        byte[] Image { get; set; }

        string Address { get; set; }

        bool IsDeleted { get; set; }

        ICollection<IUser> Users { get; set; }

        ICollection<IExpense> Expenses { get; set; }
    }
}
