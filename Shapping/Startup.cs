using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shapping.Startup))]
namespace Shapping
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
