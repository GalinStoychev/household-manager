﻿using HouseholdManager.Logic.Contracts;
using HouseholdManager.Logic.Contracts.Factories;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
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
        }
    }
}