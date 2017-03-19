using System;
using System.Web;

namespace HouseholdManager.Web.WebHelpers.Contracts
{
    public interface IWebHelper 
    {
        string GetHouseholdNameFromCookie();

        Guid GetHouseholdIdFromCookie();

        void SetHouseholdCookie(string name, string id);

        void DeleteHouseholdCookie();

        bool CheckIfAjaxCall(HttpContextBase context);

        string GetUserId();

        string GetUserName();
    }
}
