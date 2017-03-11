using HouseholdManager.Domain.Contracts.Models;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Models
{
    public class Household : BaseDomain, IHousehold, IIdentifiable
    {
        public Household(string name, string address, byte[] image)
        {
            this.Name = name;
            this.Address = address;
            this.Image = image;
            this.Users = new List<IUser>();
            this.Expenses = new List<IExpense>();
        }

        public string Name { get; private set; }

        public byte[] Image { get; private set; }

        public string Address { get; private set; }

        public bool IsDeleted { get; set; }

        public ICollection<IUser> Users { get; set; }

        public ICollection<IExpense> Expenses { get; set; }
    }
}
