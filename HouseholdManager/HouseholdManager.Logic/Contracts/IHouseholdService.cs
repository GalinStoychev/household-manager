using HouseholdManager.Models;
using System;

namespace HouseholdManager.Logic.Contracts
{
    public interface IHouseholdService
    {
        Household GetHousehold(Guid id);

        void CraeteHousehold(string name, string address, byte[] image);
    }
}
