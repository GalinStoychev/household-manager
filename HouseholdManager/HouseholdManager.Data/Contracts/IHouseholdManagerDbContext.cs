using HouseholdManager.Models;
using System.Data.Entity;

namespace HouseholdManager.Data.Contracts
{
    public interface IHouseholdManagerDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Comment> Comment { get; set; }

        IDbSet<Expense> Expense { get; set; }

        IDbSet<ExpenseCategory> ExpenseCategory { get; set; }

        IDbSet<Household> Household { get; set; }

        void SetEntryState(object entity, EntityState entityState);

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
