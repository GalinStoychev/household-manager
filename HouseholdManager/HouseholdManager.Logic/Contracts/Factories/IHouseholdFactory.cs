using HouseholdManager.Models;

namespace HouseholdManager.Logic.Contracts.Factories
{
    public interface IHouseholdFactory
    {
        Household CreateHousehold(string name, string address, byte[] image);
    }
}
