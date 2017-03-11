using HouseholdManager.Domain.Contracts.Models;
using System.Collections.Generic;

namespace HouseholdManager.Domain.Contracts.Repositories
{
    public interface IUserRepositoryEF
    {
        IEnumerable<IUser> GetAll();

        IUser GetByUsername(string username);

        void Add(IUser entity);

        void Delete(IUser entity);

        void Update(IUser entity);
    }
}
