using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HouseholdManager.Data.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        private ICollection<Expense> expensesWillPay;

        private ICollection<Expense> expensesPaid;

        private ICollection<Comment> comments;

        private ICollection<Household> households;

        public User()
        {
            this.expensesWillPay = new HashSet<Expense>();
            this.expensesPaid = new HashSet<Expense>();
            this.comments = new HashSet<Comment>();
            this.households = new HashSet<Household>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Expense> ExpensesWillPay
        {
            get { return this.expensesWillPay; }
            set { this.expensesWillPay = value; }
        }

        public virtual ICollection<Expense> ExpensesPaid
        {
            get { return this.expensesPaid; }
            set { this.expensesPaid = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Household> Households
        {
            get { return this.households; }
            set { this.households = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
