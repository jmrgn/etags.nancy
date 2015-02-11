using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Etags.Nancy.Api.Cache;
using Etags.Nancy.Api.Persistence;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Windsor;
using Nancy.Routing;

namespace Etags.Nancy.Api
{
    public class Bootstrapper : WindsorNancyBootstrapper
    {

        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            existingContainer.Register(Component.For<CachingInterceptor>()
                                        .LifeStyle.Transient);
            existingContainer.Register(
                Component.For<IPersonRepository>()
                .ImplementedBy<PersonRepository>()
                .Interceptors<CachingInterceptor>()
                .LifeStyle.Transient);
            
            base.ConfigureApplicationContainer(existingContainer);
        }

    }
}
