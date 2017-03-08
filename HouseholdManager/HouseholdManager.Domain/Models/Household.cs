using HouseholdManager.Domain.Contracts.Models;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Models
{
    public class Household: BaseDomain, IHousehold, IIdentifiable
    {
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<IUser> Users { get; set; }

        public ICollection<IExpense> Expenses { get; set; }
    }
}
