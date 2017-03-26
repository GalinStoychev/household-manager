using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HouseholdManager.Web.Startup))]
namespace HouseholdManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new MyIdProvider());
            app.MapSignalR();

        }
    }

    public interface IUserIdProvider
    {
        string GetUserId(IRequest request);
    }

    public class MyIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.User.Identity.GetUserId();
            //return "32c08157-c177-4ba6-9bcc-3e5433192ca8";
        }
    }
}
