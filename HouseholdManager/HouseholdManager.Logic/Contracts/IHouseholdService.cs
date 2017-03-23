using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface IHouseholdService
    {
        Household GetHousehold(Guid id);

        IEnumerable<Household> GetAll();

        int GetHouseholdsCount();

        IEnumerable<User> GetHouseholdUsers(Guid id);

        void CreateHousehold(string name, string address, byte[] image, string userId);

        void UpdateHouseholdInfo(Guid id, string name, string address);

        void Delete(Guid id, bool isDeleted);
    }
}
