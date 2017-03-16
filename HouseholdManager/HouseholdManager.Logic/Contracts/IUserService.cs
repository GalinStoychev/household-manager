using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Contracts
{
    public interface IUserService
    {
        User GetUserInfo(string id);

        Household GetCurrentHousehold(string email);

        void SetCurrentHousehold(string householdName, string userId);

        void SetCurrentHousehold(Guid householdId, string userId);

        void AddHousehold(Household household, string username);

        void UpdateUserInfo(string firstName, string lastName, string phoneNumber);
    }
}
