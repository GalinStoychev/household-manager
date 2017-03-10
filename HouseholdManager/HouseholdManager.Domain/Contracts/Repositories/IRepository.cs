using HouseholdManager.Domain.Contracts.Models;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Repositories
{
    public interface IRepository<DomainType>
    where DomainType : IIdentifiable
    {
        IEnumerable<DomainType> GetAll();

        DomainType GetById(object id);

        void Add(DomainType entity);

        void Delete(DomainType entity);

        void Update(DomainType entity);
    }
}
