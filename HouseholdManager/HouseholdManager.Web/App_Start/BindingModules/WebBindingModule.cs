using HouseholdManager.Web.WebHelpers;
using HouseholdManager.Web.WebHelpers.Contracts;
using Ninject.Modules;

namespace HouseholdManager.Web.App_Start.BindingModules
{
    public class WebBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IWebHelper>()
                .To<WebHelper>();
        }
    }
}