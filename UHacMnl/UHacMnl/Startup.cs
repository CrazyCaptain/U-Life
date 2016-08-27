using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UHacMnl.Startup))]
namespace UHacMnl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
