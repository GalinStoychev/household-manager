using HouseholdManager.Common;
using HouseholdManager.Common.Contracts;
using Ninject.Modules;

namespace HouseholdManager.Web.App_Start.BindingModules
{
    public class AutomapperBindingModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapingService>()
                .To<MappingService>();
        }
    }
}