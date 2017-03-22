using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User GetUserInfo(string id);

        Household GetCurrentHousehold(string email);

        void SetCurrentHousehold(string householdName, string userId);

        void SetCurrentHousehold(Guid householdId, string userId);

        void AddHousehold(Household household, string username);

        void UpdateUserInfo(string id, string firstName, string lastName, string phoneNumber);

        void Delete(string id, bool isDeleted);
    }
}
