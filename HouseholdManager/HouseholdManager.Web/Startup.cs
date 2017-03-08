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
        }
    }
}
