using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DokterPraktekV3.Startup))]
namespace DokterPraktekV3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
