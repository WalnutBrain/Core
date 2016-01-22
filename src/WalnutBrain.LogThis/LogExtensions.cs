using System;

namespace WalnutBrain.LogThis
{
    public static class LogExtensions
    {
        public static LogEvent Message(this LogEvent @event, string message, params object[] args)
        {
            @event.Message = string.Format(message, args);
            return @event;
        }

        public static LogEvent Exception(this LogEvent @event, Exception ex)
        {
            @event.Exception = ex;
            return @event;
        }

        public static LogEvent Property(this LogEvent @event,  string name, object value)
        {
            @event.Parameters[name] = value;
            return @event;
        }

        private static ILoggerProvider _provider = new NullLoggerProvider();

        public static void Set(this ILoggerProvider provider)
        {
            _provider = provider;
        }

        public static void WriteLog(this object target, LogLevel level, Func<LogEvent, LogEvent> producer, Func<bool> when = null)
        {
            if(when != null && !when())
                return;
            var logger = _provider.GetLogger(target);
            logger.Write(level, producer(new LogEvent(level)));
        }

        public static void LogTrace(this object target, Func<LogEvent, LogEvent> producer, Func<bool> when = null)
        {
            target.WriteLog(LogLevel.Trace, producer);
        }

        public static void LogInfo(this object target, Func<LogEvent, LogEvent> producer, Func<bool> when = null)
        {
            target.WriteLog(LogLevel.Info, producer);
        }

        public static void LogWarning(this object target, Func<LogEvent, LogEvent> producer, Func<bool> when = null)
        {
            target.WriteLog(LogLevel.Warning, producer);
        }

        public static void LogError(this object target, Func<LogEvent, LogEvent> producer, Func<bool> when = null)
        {
            target.WriteLog(LogLevel.Error, producer);
        }

        public static void LogCritical(this object target, Func<LogEvent, LogEvent> producer, Func<bool> when = null)
        {
            target.WriteLog(LogLevel.Critical, producer);
        }

    }
}