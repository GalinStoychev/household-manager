using HouseholdManager.Data.Models;
using HouseholdManager.Domain.Contracts.Models;

namespace HouseholdManager.Data.Contracts
{
    public interface IEntityToDomainMapper
    {
        IUser MapUser(User userToMap);
    }
}
