using HouseholdManager.Data;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Data.Repositories;
using HouseholdManager.Models;
using Ninject.Extensions.Conventions;
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

            this.Bind<IRepository<User>>()
                .To<GenericRepositoryEF<User>>();

            this.Bind<IRepository<Household>>()
                .To<GenericRepositoryEF<Household>>();

            this.Bind<IRepository<Expense>>()
              .To<GenericRepositoryEF<Expense>>();

            this.Bind<IRepository<ExpenseCategory>>()
                .To<GenericRepositoryEF<ExpenseCategory>>();

            this.Bind<IRepository<Comment>>()
                .To<GenericRepositoryEF<Comment>>();
        }
    }
}