using HouseholdManager.Domain.Contracts.Models;

namespace HouseholdManager.Data.Contracts.Factories
{
    public interface IModelsFactory
    {
        IUser CreateUser(string firstName, string lastName, string username, string email, string phoneNumber);

        IHousehold CreateHousehold(string name, string address);
    }
}
