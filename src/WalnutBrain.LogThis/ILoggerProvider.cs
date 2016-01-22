namespace WalnutBrain.LogThis
{
    public interface ILoggerProvider
    {
        ILogger GetLogger(object target);
    }
}