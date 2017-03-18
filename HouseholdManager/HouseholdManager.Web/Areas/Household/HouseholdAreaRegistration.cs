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
                name: "Household_expenses",
                url: "Household/{name}/Expenses",
                defaults: new { action = "Index", controller = "Expenses" }
            );

            context.MapRoute(
               name: "Household_expenses_history",
               url: "Household/{name}/Expenses/History",
               defaults: new { action = "History", controller = "Expenses" }
           );

            //    context.MapRoute(
            //    name: "Household_expenses_with_pattern",
            //    url: "Household/{name}/Expenses/{pattern}/{page}",
            //    defaults: new { action = "Index", controller = "Expenses", page = UrlParameter.Optional }
            //);


            context.MapRoute(
               name: "Household_single_expense",
               url: "Household/{name}/Expenses/id/{id}",
               defaults: new { action = "Index", controller = "Expense" }
         );

            // TODO: Fix Script to click on on the EXPENSE and fix the redirection

            //    context.MapRoute(
            //      name: "Household_single_expense",
            //      url: "Household/{name}/Expenses/{id}/Edit",
            //      defaults: new { action = "Create", controller = "Expenses" }
            //);

            context.MapRoute(
                name: "Household_add_expense",
                url: "Household/{name}/Add",
                defaults: new { action = "Create", controller = "Expense" }
          );

            context.MapRoute(
               name: "Household_single",
               url: "Household/{name}",
               defaults: new { action = "Index", controller = "Household" }
           );

            context.MapRoute(
                name: "Household_default",
                url: "Household/{controller}/{action}",
                defaults: new { action = "Index" }
            );
        }
    }
}