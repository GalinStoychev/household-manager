using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HouseholdManager.Domain.Contracts
{
    public interface IRepository<EntityType>
    where EntityType : class
    {
        IEnumerable<EntityType> GetAll();

        IEnumerable<T1> GetAll<T1>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> selectExpression);

        IEnumerable<T2> GetAll<T1, T2>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> sortExpression, Expression<Func<EntityType, T2>> selectExpression);

        EntityType GetById(object id);

        void Add(EntityType entity);

        void AddRange(IList<EntityType> entities);

        void Delete(EntityType entity);

        void Update(EntityType entity);
    }
}
