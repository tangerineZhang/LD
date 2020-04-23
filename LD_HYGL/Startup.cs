using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LD_HYGL.Startup))]
namespace LD_HYGL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
