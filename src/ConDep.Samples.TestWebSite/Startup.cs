using Microsoft.Owin;
using Owin;
using Startup = ConDep.Samples.WwwApplication.Startup;

[assembly: OwinStartup(typeof(Startup))]
namespace ConDep.Samples.WwwApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
