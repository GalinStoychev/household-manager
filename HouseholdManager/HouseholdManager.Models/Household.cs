using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Models
{
    public class Household : BaseEntity
    {
        private ICollection<User> users;
        private ICollection<Expense> expenses;

        private Household()
        {
            this.users = new HashSet<User>();
            this.expenses = new HashSet<Expense>();
        }

        public Household(string name, string address, byte[] image)
        {
            this.Name = name;
            this.Address = address;
            this.Image = image;
            this.users = new HashSet<User>();
            this.expenses = new HashSet<Expense>();
        }

        [Required]
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        [InverseProperty("Households")]
        public virtual ICollection<User> Users
        {
            get { return this.users; }
            protected set { this.users = value; }
        }

        public virtual ICollection<Expense> Expenses
        {
            get { return this.expenses; }
            protected set { this.expenses = value; }
        }
    }
}
