using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Elmatgar.Startup))]
namespace Elmatgar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
