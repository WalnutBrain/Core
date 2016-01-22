using System;
using NLog;
using NLog.Fluent;

namespace WalnutBrain.LogThis.NLog
{
    public class NLogLoggerProvider : ILoggerProvider
    {
        public ILogger GetLogger(object target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            string loggerName;
            if (target is string)
                loggerName = target.ToString();
            else if (target is Type)
                loggerName = ((Type)target).FullName;
            else
                loggerName = target.GetType().FullName;
            return new NLogLogger(LogManager.GetLogger(loggerName));
        }

        private class NLogLogger : ILogger
        {
            private readonly Logger _logger;

            public NLogLogger(Logger logger)
            {
                _logger = logger;
            }

            public void Write(LogLevel level, LogEvent logEvent)
            {
                LogBuilder fluent;
                switch (level)
                {
                    case LogLevel.Trace:
                        fluent = _logger.Trace();
                        break;
                    case LogLevel.Info:
                        fluent = _logger.Info();
                        break;
                    case LogLevel.Warning:
                        fluent = _logger.Warn();
                        break;
                    case LogLevel.Error:
                        fluent = _logger.Error();
                        break;
                    case LogLevel.Critical:
                        fluent = _logger.Fatal();
                        break;
                    default:
                        throw new ApplicationException("Invalid log level");
                }

                if (!string.IsNullOrWhiteSpace(logEvent.Message))
                    fluent.Message(logEvent.Message);
                if (logEvent.Exception != null)
                    fluent.Exception(logEvent.Exception);
                foreach (var parameter in logEvent.Parameters)
                {
                    fluent.Property(parameter.Key, parameter.Value);
                }
                fluent.Write();
            }
        }
    }
}