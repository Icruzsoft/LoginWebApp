using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginWebApp.Startup))]
namespace LoginWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
