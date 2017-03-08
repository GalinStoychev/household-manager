using HouseholdManager.Data;
using HouseholdManager.Data.Contracts;
using Ninject.Modules;
using Ninject.Web.Common;

namespace HouseholdManager.Web.App_Start.BindingModules
{
    public class DataBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IHouseholdManagerDbContext>()
                 .To<HouseholdManagerDbContext>()
                 .InRequestScope();
        }
    }
}