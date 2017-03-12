using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HouseholdManager.Data.Repositories
{
    public class GenericRepositoryEF<T> : IRepository<T> where T : class
    {
        public GenericRepositoryEF(IHouseholdManagerDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public IQueryable<T> All
        {
            get
            {
                return this.DbSet;
            }
        }

        protected IHouseholdManagerDbContext Context { get; private set; }

        protected DbSet<T> DbSet { get; private set; }

        public void Add(T entity)
        {
            this.Context.SetEntryState(entity, EntityState.Added);
        }

        public void AddOrUpdate(T entity)
        {
            this.Context.SetEntryState(entity, EntityState.Modified);
        }

        public void Update(T entity)
        {
            this.Context.SetEntryState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            this.Context.SetEntryState(entity, EntityState.Deleted);
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet.ToList();
        }

        public IEnumerable<T1> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> selectExpression)
        {
            IQueryable<T> result = this.DbSet;

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

        public IEnumerable<T1> GetAll<T1>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, T1>> selectExpression,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = this.DbSet;

            if (includes != null)
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

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

        public T GetById(object id)
        {
            var entity =  this.DbSet.Find(id);
            return entity;
        }

        public T GetFirst(Expression<Func<T, bool>> filterExpression)
        {
            var foundEntity = this.DbSet.FirstOrDefault(filterExpression);
            return foundEntity;
        }

        public T GetFirst(Expression<Func<T, bool>> filterExpression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = this.DbSet;

            if (includes != null)
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

            return result.OfType<T>().FirstOrDefault(filterExpression);
        }
    }
}
