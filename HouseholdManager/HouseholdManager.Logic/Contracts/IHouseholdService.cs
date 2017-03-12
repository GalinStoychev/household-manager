using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Contracts
{
    public interface IHouseholdService
    {
        Household GetHousehold(Guid id);

        void CreateHousehold(string name, string address, byte[] image, string userId);
    }
}
