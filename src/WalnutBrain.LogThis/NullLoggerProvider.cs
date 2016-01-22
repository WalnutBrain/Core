namespace WalnutBrain.LogThis
{
    internal class NullLoggerProvider : ILoggerProvider
    {
        public ILogger GetLogger(object target)
        {
            return new NullLogger();
        }

        internal class NullLogger : ILogger
        {
            public void Write(LogLevel level, LogEvent logEvent)
            {
            }
        }
    }
}