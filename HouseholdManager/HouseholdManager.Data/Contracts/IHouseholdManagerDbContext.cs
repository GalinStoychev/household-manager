using HouseholdManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdManager.Data.Contracts
{
    public interface IHouseholdManagerDbContext
    {
        IDbSet<User> Users { get; set; }

        void SetEntryState(object entity, EntityState entityState);

        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
