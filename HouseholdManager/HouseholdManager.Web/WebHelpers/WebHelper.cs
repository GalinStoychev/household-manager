using HouseholdManager.Common.Constants;
using HouseholdManager.Web.WebHelpers.Contracts;
using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace HouseholdManager.Web.WebHelpers
{
    public class WebHelper : IWebHelper
    {
        public Guid GetHouseholdIdFromCookie()
        {
            var householdId = HttpContext.Current.Request.Cookies[CommonConstants.CurrentHousehold]?[CommonConstants.CurrentHouseholdId];
            if (householdId == null)
            {
                throw new ArgumentNullException("Household not found.");
            }

            return Guid.Parse(householdId);
        }

        public string GetHouseholdNameFromCookie()
        {
            {
                var householdName = HttpContext.Current.Request.Cookies[CommonConstants.CurrentHousehold]?[CommonConstants.CurrentHouseholdName];
                if (householdName == null)
                {
                    throw new ArgumentNullException("Household not found.");
                }

                return householdName.ToString();
            }
        }

        public void SetHouseholdCookie(string name, string id)
        {
            var cookie = new HttpCookie(CommonConstants.CurrentHousehold);
            cookie.Values.Add(CommonConstants.CurrentHouseholdName, name);
            cookie.Values.Add(CommonConstants.CurrentHouseholdId, id);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public void DeleteHouseholdCookie()
        {
            var householdCookie = HttpContext.Current.Response.Cookies.Get(CommonConstants.CurrentHousehold);
            householdCookie.Expires = DateTime.Now.AddDays(-1);
        }

        public string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public string GetUserName()
        {
            return HttpContext.Current.User.Identity.GetUserName();
        }
    }
}