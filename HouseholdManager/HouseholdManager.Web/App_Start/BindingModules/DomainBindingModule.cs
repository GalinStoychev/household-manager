using HouseholdManager.Domain.Contracts.Models;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using System.IO;
using System.Reflection;

namespace HouseholdManager.Web.App_Start.BindingModules
{
    public class DomainBindingModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x =>
            {
                x.FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetAssembly(typeof(IUser)).Location))
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}