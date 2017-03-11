using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household
{
    public class HouseholdAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Household";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "Household_create",
              "Household/{action}",
              new { action = "Create", controller = "Household" }
            );

            context.MapRoute(
                "Household_default",
                "Household/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}