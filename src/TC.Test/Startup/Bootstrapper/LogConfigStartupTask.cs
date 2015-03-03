using System;
using Bootstrap.Extensions.StartupTasks;
using Zen;
using Zen.Log;

namespace TC.Tests.Startup.Bootstrapper
{
    public class LogConfigStartupTask : IStartupTask
    {
        /// <summary>
        /// Configure default logging options which are applied every time we startup...
        /// </summary>
        private static void ConfigureLogging()
        {
            Log4netConfigurator.RootLogLevel = LogLevel.Debug;
            Log4netConfigurator.TurnAllAppenders(OnOff.Off);
            Log4netConfigurator.TurnAppender(Appenders.Debug,    OnOff.On);
            Log4netConfigurator.TurnAppender(Appenders.Console,  OnOff.On);
            //Log4netConfigurator.TurnAppender(Appenders.File,     OnOff.On);            
            //Log4netConfigurator.TurnAppender(Appenders.Trace,    OnOff.On);
            Log4netConfigurator.TurnLoggerOff("NHibernate");// stop this external logger by default
            Log4netConfigurator.ErrorHandlerAppenders = new[] { Appenders.Debug, Appenders.Console };// any LoggingExceptions will go to these appenders
            Log4netConfigurator.ErrorHandler = typeof(LoggingErrorHandler);            
            try
            {
                Log4netConfigurator.Configure();    
            }
            catch (Exception ex)
            {
                throw new ConfigException("Could not configure logging.", ex);
            }
            "LogConfigStartupTask complete.".LogMe(LogLevel.Debug);              
        }

        public void Run()
        {
            ConfigureLogging();
        }

        public void Reset()
        {
            ConfigureLogging(); // reconfigure to undo anything that changed...
        }

        //nested class
        public class LoggingErrorHandler : Log4netErrorHandler, log4net.Core.IErrorHandler
        {
            public void Error(string message, Exception exc, log4net.Core.ErrorCode errorCode)
            { base.Error(message, exc, errorCode); }
        }

    }
}