using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(salt_web.Startup))]
namespace salt_web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
