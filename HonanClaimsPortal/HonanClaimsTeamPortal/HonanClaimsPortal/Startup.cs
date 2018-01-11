using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HonanClaimsPortal.Startup))]
namespace HonanClaimsPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
