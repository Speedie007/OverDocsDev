using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDocsDev.Startup))]
namespace WebDocsDev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
