using HouseholdManager.Data;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Data.Contracts.Factories;
using HouseholdManager.Data.Repositories;
using HouseholdManager.Domain.Contracts;
using HouseholdManager.Domain.Contracts.Repositories;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Web.Common;
using System.IO;
using System.Reflection;

namespace HouseholdManager.Web.App_Start.BindingModules
{
    public class DataBindingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetAssembly(typeof(IHouseholdManagerDbContext)).Location))
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Rebind<IHouseholdManagerDbContext>()
                 .To<HouseholdManagerDbContext>()
                 .InRequestScope();

            //this.Bind<IUnitOfWork>()
            //    .To<UnitOfWork>();

            this.Bind<IModelsFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}