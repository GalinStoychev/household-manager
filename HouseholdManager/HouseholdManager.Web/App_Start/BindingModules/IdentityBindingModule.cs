using HouseholdManager.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Web;

namespace HouseholdManager.Web.App_Start.BindingModules
{
    public class IdentityBindingModule : NinjectModule
    {
        public override void Load()
        {
            //this.Bind<ApplicationSignInManager>()
            //    .ToSelf();

            //this.Bind<ApplicationUserManager>()
            //    .ToSelf();

            //this.Bind<IAuthenticationManager>().ToMethod(c =>
            //     HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            //this.Bind(typeof(IUserStore<>))
            //    .To(typeof(UserStore<>));
        }
    }
}