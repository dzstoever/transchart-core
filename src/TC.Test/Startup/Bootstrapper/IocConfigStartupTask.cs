using System;
using System.Diagnostics;
using Bootstrap.Extensions.StartupTasks;
using TC.Tests.Startup.Windsor;
using Zen;
using Zen.Ioc;
using Zen.Log;

namespace TC.Tests.Startup.Bootstrapper
{
    public class IocConfigStartupTask : IStartupTask
    {
        private static void ConfigureWindsorDI()
        {
            // use all the WindsorInstallers from this assembly
            // WindsorDI.ConfigureFromAssembly = typeof(StartupFixture).Assembly;

            WindsorDI.ConfigureFromInstallers = new object[]{ new ZenTestInstaller() };

            var windsorDI = Aspects.GetIocDI() as WindsorDI;
            Debug.Assert(windsorDI != null, "windsorDI != null");
            try
            {
                windsorDI.Initialize();// run all installers
            }
            catch (Exception ex)
            {
                "WindsorDI.Initialize() failed.{0}{1}".LogMe(LogLevel.Fatal, Environment.NewLine, ex.FullMessage());
                throw new ConfigException("Could not initialize dependency injection.", ex);
            }
            "IocConfigStartupTask complete.".LogMe(LogLevel.Debug);           
        }

        //todo: call this when appropriate?
        //try
        //{ var king = windsorDI.Resolve<IViewFactory>(); }// 2. resolve the king and all his subjects
        //catch (Exception ex)
        //{
        //    "IocDI.Resolve<> failed.{0}{1}".LogMe(LogLevel.Fatal, Environment.NewLine, ex.FullMessage());
        //    throw new DependencyException("Could not resolve the king.", ex);
        //}


        public void Run()
        {
            ConfigureWindsorDI();
        }

        public void Reset()
        {
            ConfigureWindsorDI();
        }
    }
}