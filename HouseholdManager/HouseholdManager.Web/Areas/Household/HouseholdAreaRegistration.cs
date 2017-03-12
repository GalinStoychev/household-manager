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
              url: "Household/Create",
              defaults: new { action = "Create", controller = "Household" }
            );

            context.MapRoute(
                name: "Household_single",
                url: "Household/{name}",
                defaults: new { action = "Index", controller = "Household"}
            );

            context.MapRoute(
                name: "Household_expenses",
                url: "Household/{name}/Expenses",
                defaults: new { action = "Index", controller = "Expenses"}
            );

            context.MapRoute(
              name: "Household_add_expense",
              url: "Household/{name}/Add",
              defaults: new { action = "Create", controller = "Expenses" }
          );

            context.MapRoute(
                name: "Household_default",
                url: "Household/{controller}/{action}/{name}",
                defaults: new { action = "Index", name = UrlParameter.Optional }
            );
        }
    }
}