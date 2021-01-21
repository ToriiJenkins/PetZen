using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetZen.WebMVC.Startup))]
namespace PetZen.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
