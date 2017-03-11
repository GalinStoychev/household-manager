using HouseholdManager.Domain.Contracts.Models;
using System;

namespace HouseholdManager.Logic.Contracts
{
    public interface IUserService
    {
        IUser GetUserInfo(string username);

        void SetCurrentHousehold(Guid Id);

        void AddHousehold(string name, string address, byte[] image, string username);

        void UpdateUserInfo(string username, string firstName, string lastName, string phoneNumber);
    }
}
