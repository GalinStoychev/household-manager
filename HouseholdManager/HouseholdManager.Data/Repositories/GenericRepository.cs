using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HouseholdManager.Data.Repositories
{
    public class GenericRepository<EntityType> : IRepository<EntityType> where EntityType : class
    {
        public GenericRepository(IHouseholdManagerDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<EntityType>();
        }

        protected IHouseholdManagerDbContext Context { get; set; }

        protected DbSet<EntityType> DbSet { get; set; }

        public EntityType GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual IEnumerable<EntityType> GetAll()
        {
            return this.DbSet.ToList();
        }

        public IEnumerable<T1> GetAll<T1>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> selectExpression)
        {
            IQueryable<EntityType> result = this.DbSet;

            if (filterExpression != null)
            {
                result = result.Where(filterExpression);
            }
            if (selectExpression != null)
            {
                return result.Select(selectExpression).ToList();
            }
            else
            {
                return result.OfType<T1>().ToList();
            }
        }

        public IEnumerable<T2> GetAll<T1, T2>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> sortExpression, Expression<Func<EntityType, T2>> selectExpression)
        {
            IQueryable<EntityType> result = this.DbSet;

            if (filterExpression != null)
            {
                result = result.Where(filterExpression);
            }

            if (sortExpression != null)
            {
                result = result.OrderBy(sortExpression);
            }

            if (selectExpression != null)
            {
                return result.Select(selectExpression).ToList();
            }
            else
            {
                return result.OfType<T2>().ToList();
            }
        }

        public void Add(EntityType entity)
        {
            this.Context.SetEntryState(entity, EntityState.Added);
        }

        public void AddRange(IList<EntityType> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public void Delete(EntityType entity)
        {
            this.Context.SetEntryState(entity, EntityState.Deleted);
        }

        public void Update(EntityType entity)
        {
            this.Context.SetEntryState(entity, EntityState.Modified);
        }
    }
}
