using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using Zen.Data;
using Zen.Log;
using ILoggerFactory = Zen.Log.ILoggerFactory;

namespace TC.Tests.Startup.Windsor
{
    public class TestInstaller : IWindsorInstaller
    {        
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Loggers can always be obtained from the Aspects, but this adds the ability to get a Logger/LoggerFactory 
            // from the IoC/DI container and any other registered components with a Logger/LoggerFactory dependency 
            // will have it injected automatically
            container.Register(Component.For<ILoggerFactory>()
                                   .ImplementedBy<Log4netLoggerFactory>()
                                   .LifestyleSingleton());
            container.Register(Component.For<ILogger>()
                                   .ImplementedBy<Log4netLogger>()
                                   .LifestyleSingleton());

            // DaoConfigStartup task sets the NHConfigurator.DbContext & SessionFactory
            container.Register(Component.For<IDbContext>()
                                   .Instance(NHConfigurator.DbContext)
                                   .LifestyleSingleton());
            container.Register(Component.For<ISessionFactory>()
                                   .Instance(NHConfigurator.SessionFactory)
                                   .LifestyleSingleton());
            container.Register(Component.For<IGenericDao>()
                                   .ImplementedBy<NHibernateDao>()
                                   .LifestyleSingleton());

        }
    }
}