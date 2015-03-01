using System;
using System.Windows.Forms;
using TC.Utility.Controls;
using Zen.Log;

namespace TC.Utility
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {

            Log4netConfigurator.RtbAppenderType = typeof(RtbAppender);

            // make all these appenders available to Zen.* loggers
            var appenders = new[] { Appenders.EventLog, Appenders.File, Appenders.Rtb, Appenders.Smtp, Appenders.Sql, Appenders.Trace };
            Log4netConfigurator.SetLoggerAppenders("TC", LogLevel.All, appenders);
            Log4netConfigurator.SetLoggerAppenders("Zen", LogLevel.All, appenders);
            Log4netConfigurator.SetLoggerAppenders("NHibernate", LogLevel.All, appenders);
            Log4netConfigurator.TurnAppenders(appenders, OnOff.On);
            // turn appenders on/off by default
            Log4netConfigurator.TurnAppender(Appenders.File, OnOff.On);
            Log4netConfigurator.TurnAppender(Appenders.Rtb, OnOff.On);
            Log4netConfigurator.TurnAppender(Appenders.EventLog, OnOff.Off);
            Log4netConfigurator.TurnAppender(Appenders.Smtp, OnOff.Off);
            Log4netConfigurator.TurnAppender(Appenders.Sql, OnOff.Off);
            Log4netConfigurator.TurnAppender(Appenders.Trace, OnOff.Off);

            // stop this external logger by default, in case we are using NHibernate
            // Log4netConfigurator.TurnLoggerOff("NHibernate");

            // any LoggingExceptions will go to these appenders (as long as the appenders are turned on)
            Log4netConfigurator.ErrorHandlerAppenders = new[] { Appenders.Debug, Appenders.Console, Appenders.File, Appenders.Rtb };
            Log4netConfigurator.ErrorHandler = typeof(LoggingErrorHandler);

            Log4netConfigurator.Configure();



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }
    }

    //nested class
    public class LoggingErrorHandler : Log4netErrorHandler, log4net.Core.IErrorHandler
    {
        public void Error(string message, Exception exc, log4net.Core.ErrorCode errorCode)
        {
            base.Error(message, exc, errorCode);
        }
    }
}