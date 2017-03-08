using HouseholdManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HouseholdManager.Domain.Contracts
{
    public interface IRepository<DomainType, EntityType>
    where DomainType : BaseDomain
    {
        IEnumerable<DomainType> GetAll();

        IEnumerable<DomainType> GetAll<T1>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> selectExpression);

        IEnumerable<DomainType> GetAll<T1, T2>(Expression<Func<EntityType, bool>> filterExpression, Expression<Func<EntityType, T1>> sortExpression, Expression<Func<EntityType, T2>> selectExpression);

        DomainType GetById(object id);

        void Add(DomainType entity);

        void Delete(DomainType entity);

        void Update(DomainType entity);
    }
}
