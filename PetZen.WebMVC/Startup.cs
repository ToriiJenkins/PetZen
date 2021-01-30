using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Owin;
using PetZen.Contracts;
using PetZen.Services;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(PetZen.WebMVC.Startup))]
namespace PetZen.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterType<PetService>().As<IPetService>();
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            ConfigureAuth(app);
        }
    }
}
