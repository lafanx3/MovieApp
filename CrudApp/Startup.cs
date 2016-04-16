using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudApp.Startup))]
namespace CrudApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
