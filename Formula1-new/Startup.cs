using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Formula1_new.Startup))]
namespace Formula1_new
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
