using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Contracts
{
    public interface IUserService
    {
        User GetUserInfo(string username);

        void SetCurrentHousehold(Guid Id);

        void AddHousehold(Household household, string username);

        void UpdateUserInfo(string username, string firstName, string lastName, string phoneNumber);
    }
}
