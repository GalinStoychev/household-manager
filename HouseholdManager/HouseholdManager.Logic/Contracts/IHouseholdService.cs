using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface IHouseholdService
    {
        Household GetHousehold(Guid id);

        IEnumerable<User> GetHouseholdUsers(Guid id);

        void CreateHousehold(string name, string address, byte[] image, string userId);
    }
}
