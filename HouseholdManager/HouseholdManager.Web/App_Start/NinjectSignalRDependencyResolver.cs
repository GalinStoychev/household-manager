using Microsoft.AspNet.SignalR;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HouseholdManager.Web.App_Start
{
    public class NinjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        public override object GetService(Type serviceType)
        {
            return NinjectKernelInstanceProvider.Instance.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return NinjectKernelInstanceProvider.Instance.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}