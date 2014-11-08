using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MotivateMe.Web.Startup))]
namespace MotivateMe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
