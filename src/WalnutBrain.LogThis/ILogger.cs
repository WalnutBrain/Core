namespace WalnutBrain.LogThis
{
    public interface ILogger
    {
        void Write(LogLevel level, LogEvent logEvent);
    }
}