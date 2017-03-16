using System;

namespace HouseholdManager.Web.WebHelpers.Contracts
{
    public interface IWebHelper 
    {
        string GetHouseholdNameFromCookie();

        Guid GetHouseholdIdFromCookie();

        void SetHouseholdCookie(string name, string id);

        void DeleteHouseholdCookie();

        string GetUserId();
    }
}
