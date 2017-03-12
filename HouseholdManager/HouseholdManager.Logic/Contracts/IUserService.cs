using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Contracts
{
    public interface IUserService
    {
        User GetUserInfo(string id);

        Household GetCurrentHousehold(string id);

        void SetCurrentHousehold(Guid householdId, string userId);

        void AddHousehold(Household household, string username);

        void UpdateUserInfo(string username, string firstName, string lastName, string phoneNumber);
    }
}
