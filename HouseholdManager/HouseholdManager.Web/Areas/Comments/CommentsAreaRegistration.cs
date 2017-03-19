using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Comments
{
    public class CommentsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Comments";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Comemnts_create",
                url: "Comemnts/Create",
                defaults: new { action = "Create", controller = "Comments" }
            );

            context.MapRoute(
                name: "Comemnts_show",
                url: "Comemnts/{expenseId}",
                defaults: new { action = "Index", controller = "Comments" }
            );

            //context.MapRoute(
            //    "Comments_default",
            //    "Comments/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}