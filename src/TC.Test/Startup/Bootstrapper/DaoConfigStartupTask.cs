using System;
using Bootstrap.Extensions.StartupTasks;
using Zen;
using Zen.Data;
using Zen.Log;

namespace TC.Tests.Startup.Bootstrapper
{
    public class DaoConfigStartupTask : IStartupTask
    {
        private static void ConfigureNHibernateDao()
        {            
            try
            {
                //NHConfigurator.Configure(new TestDbContext());
                //NHConfigurator.Configure("", typeof(Zen.Test.Maps.PersonMap).Assembly, null);
                //NHConfigurator.Configure("nh.cfg.xml", typeof(Zen.Test.Maps.PersonMap).Assembly, null);
            }
            catch (Exception ex)
            {
                "NHConfigurator.Configure() failed.{0}{1}".LogMe(LogLevel.Fatal, Environment.NewLine, ex.FullMessage());
                throw new ConfigException("Could not configure data access.", ex);
            }
            "DaoConfigStartupTask complete.".LogMe(LogLevel.Debug);            
        }

        public void Run()
        {
            ConfigureNHibernateDao();
        }

        public void Reset()
        {
            ConfigureNHibernateDao();
        }
    }
}