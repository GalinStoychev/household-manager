using HouseholdManager.Data.Contracts;
using HouseholdManager.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HouseholdManager.Data
{
    public class HouseholdManagerDbContext : IdentityDbContext<User>, IHouseholdManagerDbContext
    {
        public HouseholdManagerDbContext()
            : base("HouseholdManagerDb", throwIfV1Schema: false)
        {

        }

        public virtual DbSet<Comment> Comment { get; set; }

        public virtual DbSet<Expense> Expense { get; set; }

        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; }

        public virtual DbSet<Household> Household { get; set; }

        public static HouseholdManagerDbContext Create()
        {
            return new HouseholdManagerDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            modelBuilder.Entity<Expense>().HasOptional(m => m.PaidBy)
              .WithMany(m => m.ExpensesPaid).HasForeignKey(m => m.PaidById);

            modelBuilder.Entity<Expense>().HasOptional(m => m.AssignedUser)
             .WithMany(m => m.ExpensesWillPay).HasForeignKey(m => m.AssignedUserId);
        }

        public void SetEntryState(object entity, EntityState entityState)
        {
            var entry = this.Entry(entity);
            entry.State = entityState;
        }
    }
}
