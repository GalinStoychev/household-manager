using HouseholdManager.Logic.Contracts;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Logic.Services;
using HouseholdManager.Web.App_Start.Interceptors;
using HouseholdManager.Web.Controllers;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;
using System.IO;
using System.Reflection;

namespace HouseholdManager.Web.App_Start.BindingModules
{
    public class ServiceBindingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetAssembly(typeof(IUserService)).Location))
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<IHouseholdFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IExpenseFactory>()
               .ToFactory()
               .InSingletonScope();

            this.Bind<ICommentFactory>()
               .ToFactory()
               .InSingletonScope();

            this.Bind<IInvitationFactory>()
             .ToFactory()
             .InSingletonScope();

            this.Bind<IUserService>()
               .To<UserService>()
               .WhenInjectedExactlyInto<HomeController>()
               .Intercept()
               .With<CachedInterceptor>();

            this.Bind<IExpenseService>()
               .To<ExpenseService>()
               .WhenInjectedExactlyInto<HomeController>()
               .Intercept()
               .With<CachedInterceptor>();

            this.Bind<IHouseholdService>()
               .To<HouseholdService>()
               .WhenInjectedExactlyInto<HomeController>()
               .Intercept()
               .With<CachedInterceptor>();
        }
    }
}