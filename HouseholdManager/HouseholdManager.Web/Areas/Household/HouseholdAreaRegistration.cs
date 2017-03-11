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
              name: "Household_create",
              url: "Household/{action}",
              defaults: new { action = "Create", controller = "Household" }
            );

            context.MapRoute(
                name: "Household_default",
                url: "Household/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}