using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdManager.Data.Models
{
    public class Household: BaseEntity
    {
        private ICollection<User> users;
        private ICollection<Expense> expenses;

        public Household()
        {
            this.users = new HashSet<User>();
            this.expenses= new HashSet<Expense>();
        }

        [Required]
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Address { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<Expense> Expenses
        {
            get { return this.expenses; }
            set { this.expenses = value; }
        }
    }
}
