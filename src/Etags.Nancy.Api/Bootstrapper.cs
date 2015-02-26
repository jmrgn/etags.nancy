using System.IO;
using System.Net;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Etags.Nancy.Api.Cache;
using Etags.Nancy.Api.Persistence;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Windsor;
using Nancy.Routing;
using NHibernate;
using ServiceStack.Redis;
using StackExchange.Redis;

namespace Etags.Nancy.Api
{
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ApplicationStartup(IWindsorContainer container, IPipelines pipelines)
        {
            pipelines.BeforeRequest += (ctx) =>
            {
                var util = container.Resolve<CacheUtility>();
                util.CheckForIfNonMatch(ctx);
                return ctx.Response;
            };
            base.ApplicationStartup(container, pipelines); 
        }
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            existingContainer.Register(Component.For<CachingInterceptor>()
                                        .LifeStyle.Transient);

            existingContainer.Register(
                Component.For<ConnectionMultiplexer>()
                .UsingFactoryMethod(GetConnectionMultiplexer).LifestyleSingleton());

            existingContainer.Register(Component.For<ICache>()
                .ImplementedBy<RedisCache>()
                .LifeStyle.Transient);
            
            existingContainer.Register(
                Component.For<IPersonRepository>()
                .ImplementedBy<PersonRepository>()
                .Interceptors<CachingInterceptor>()
                .LifeStyle.Transient);

            existingContainer.Register(Component.For<CacheUtility>().LifeStyle.Transient);

            base.ConfigureApplicationContainer(existingContainer);
        }



        public static ConnectionMultiplexer GetConnectionMultiplexer()
        {
            var multiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            return multiplexer;
        }

    }
}
