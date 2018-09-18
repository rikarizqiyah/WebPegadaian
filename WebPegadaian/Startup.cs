using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebPegadaian.Startup))]
namespace WebPegadaian
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
