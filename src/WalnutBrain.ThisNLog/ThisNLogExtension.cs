using System;
using NLog;

namespace NLog.Fluent
{
    public static class ThisNLogExtension
    {
        private static ILogger GetLogger(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            var logger = target as ILogger;
            if (logger != null) return logger;
            string loggerName;
            if (target is string)
                loggerName = target.ToString();
            else if (target is Type)
                loggerName = ((Type)target).FullName;
            else
                loggerName = target.GetType().FullName;
            return LogManager.GetLogger(loggerName);
        }

        public static LogBuilder Log(this object target, LogLevel logLevel)
        {
            var builder = new LogBuilder(GetLogger(target), logLevel);
            return builder;
        }

        public static LogBuilder Trace(this object target)
        {
            var builder = new LogBuilder(GetLogger(target), LogLevel.Trace);
            return builder;
        }

        public static LogBuilder Debug(this object target)
        {
            var builder = new LogBuilder(GetLogger(target), LogLevel.Debug);
            return builder;
        }

        public static LogBuilder Info(this object target)
        {
            var builder = new LogBuilder(GetLogger(target), LogLevel.Info);
            return builder;
        }

        public static LogBuilder Warn(this object target)
        {
            var builder = new LogBuilder(GetLogger(target), LogLevel.Warn);
            return builder;
        }

        public static LogBuilder Error(this object target)
        {
            var builder = new LogBuilder(GetLogger(target), LogLevel.Error);
            return builder;
        }

        public static LogBuilder Fatal(this object target)
        {
            var builder = new LogBuilder(GetLogger(target), LogLevel.Fatal);
            return builder;
        }
    }
}