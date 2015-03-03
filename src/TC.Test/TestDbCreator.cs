using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentAssertions;
using NHibernate;
using TC.Maps.Entitites;
using Xunit;
using Zen;
using Zen.Data;
using Zen.Data.QueryModel;
using Zen.Ioc;
using Zen.Log;

namespace TC.Test
{
    /// <summary>
    /// Use this class to create the TestDb for use by the other tests
    /// It will also map the domain entities and provide examples of a 
    /// dao configuration along with logging and dependency injection 
    /// using the StartupFixture 
    /// </summary>
    /// <remarks>
    /// When testing RDBMS  we generally want to test only three things:
    ///  *  1) Properties are persisted 
    ///  *  2) Cascade works as expected
    ///  *  3) Queries return the correct result 
    /// 
    /// Why test using a Mock Database instead of Mock Objects?
    ///     In order to do all of those, we generally have to talk to a real database, 
    ///     trying to fake any of those at this level is futile and going to be very complicated.
    ///     We could use a standard in memory database(SQLite) in order to get very speedy tests.
    /// </remarks>
    /// <see cref="http://ayende.com/blog/3983/nhibernate-unit-testing"/>
    public class TestDbCreator : UseStartupFixture
    {
        private const bool UseNHPRof = true;
        
        public TestDbCreator()
        {
            _di = Aspects.GetIocDI();
            _log = _di.Resolve<ILogger>();
            _dao = _di.Resolve<IGenericDao>();
            
            if(UseNHPRof) NHConfigurator.TurnOnNHProfiler(false);
        }

        ~TestDbCreator()
        {
            if(_dao!=null) _dao.Dispose();
            if(_di!=null) _di.Dispose();
            if (UseNHPRof) NHConfigurator.TurnOffNHProfiler();
        }


        private readonly IocDI _di;
        private readonly ILogger _log;
        private readonly IGenericDao _dao;

        const int ExpectedClassMappings = 66;
        const int ExpectedCollectionMappings = 30;
        const int ExpectedNamedQueries = 18;
        const int ExpectedNamedSqlQueries = 12;

        // Note: The following tests demonstrate different ways to configure data access.
        //       We are skipping them because they would affect all subsequent tests.
        //       Our _dao is being configured in the Bootstrapper.DaoConfigStartupTask...
        
        [Fact(Skip ="affects data access for all subsequent tests")]//
        public void ConfigureFromIDbCnnFactory()
        {
            NHConfigurator.Configure(new ZenTestDbContext()); 
            var cnnFactory = _di.Resolve<IDbContext>();
            _log.Debug(cnnFactory.ToString());
            
            var sessionFactory = _di.Resolve<ISessionFactory>();
            sessionFactory.Should().NotBeNull();
            var session = _dao.StartUnitOfWork();
            session.Should().NotBeNull();

            NHConfigurator.SessionFactoryImpl.Settings.SessionFactoryName.Should().BeNull();
            NHConfigurator.Cfg.ClassMappings.Count.Should().Be(ExpectedClassMappings);
            NHConfigurator.Cfg.CollectionMappings.Count.Should().Be(ExpectedCollectionMappings);
            NHConfigurator.Cfg.NamedQueries.Count.Should().Be(ExpectedNamedQueries);
            NHConfigurator.Cfg.NamedSQLQueries.Count.Should().Be(ExpectedNamedSqlQueries);
        }
        
        [Fact(Skip ="affects data access for all subsequent tests")]//
        public void ConfigureFromAppConfig()
        {
            NHConfigurator.Configure("", typeof(PersonMap).Assembly, null);

            var sessionFactory = _di.Resolve<ISessionFactory>();
            sessionFactory.Should().NotBeNull();
            var session = _dao.StartUnitOfWork();
            session.Should().NotBeNull();

            NHConfigurator.SessionFactoryImpl.Settings.SessionFactoryName.Should().Be("AppConfig.SessionFactory");
            NHConfigurator.Cfg.ClassMappings.Count.Should().Be(ExpectedClassMappings);
            NHConfigurator.Cfg.CollectionMappings.Count.Should().Be(ExpectedCollectionMappings);
            NHConfigurator.Cfg.NamedQueries.Count.Should().Be(ExpectedNamedQueries);
            NHConfigurator.Cfg.NamedSQLQueries.Count.Should().Be(ExpectedNamedSqlQueries);
        }
        
        [Fact(Skip ="affects data access for all subsequent tests")]//
        public void ConfigureFromCfgXml()
        {
            NHConfigurator.Configure("nh.cfg.xml", typeof(PersonMap).Assembly, null);//this will add the embedded .hbms

            var windsorDI = Aspects.GetIocDI() as WindsorDI;
            Debug.Assert(windsorDI != null, "windsorDI != null");
            var container = windsorDI.Container as IWindsorContainer;
            Debug.Assert(container != null, "container != null");
            container.Register(Component.For<ISessionFactory>()
                                   .Instance(NHConfigurator.SessionFactory)
                                   .LifestyleSingleton()
                                   .IsDefault().Named("SessionFactoryOverride"));

            var sessionFactory = _di.Resolve<ISessionFactory>();
            sessionFactory.Should().NotBeNull();

            var session = _dao.StartUnitOfWork();
            session.Should().NotBeNull();

            NHConfigurator.SessionFactoryImpl.Settings.SessionFactoryName.Should().Be("Test.SessionFactory");
            NHConfigurator.Cfg.ClassMappings.Count.Should().Be(ExpectedClassMappings);
            NHConfigurator.Cfg.CollectionMappings.Count.Should().Be(ExpectedCollectionMappings);
            NHConfigurator.Cfg.NamedQueries.Count.Should().Be(ExpectedNamedQueries);
            NHConfigurator.Cfg.NamedSQLQueries.Count.Should().Be(ExpectedNamedSqlQueries);            
        }


        [Fact(Skip = "drops all dbs [bkcol, courtis, noc, probe, and ZenTestDB] using a named sql query")]//
        public void DropAllDbs()
        {
            using (_dao.StartUnitOfWork())
                _dao.ExecuteNonQuery(new Query(QueryTypes.Sql, true, "DropAllDbs"));
        }

        [Fact(Skip = "drops and recreates empty dbs, not the schemas, using a named sql query")]//
        public void RecreateAllDbs()
        {
            using (_dao.StartUnitOfWork())
                _dao.ExecuteNonQuery(new Query(QueryTypes.Sql, true, "DropAndRecreateAllDbs"));
        }
        

        [Fact]//(Skip = "only drops the schema for the db set as 'Inital Catalog'") ...this is by design
        public void DropInitialCatalogSchema()
        {
            NHConfigurator.DropDbSchema();
        }

        [Fact(Skip = "creates schemas for all dbs [bkcol, courtis, noc, probe, and ZenTestDB]")]//
        public void CreateAllDbSchemas()
        {
            NHConfigurator.CreateDbSchema();
        }

        [Fact]//(Skip = "updates schemas for all dbs [bkcol, courtis, noc, probe, and ZenTestDB]")
        public void UpdateAllDbSchemas()
        {
            NHConfigurator.UpdateDbSchema();
        }

        

       
        
    }
}