namespace FileForgeDP.Loggers
{
    public interface ILoggerBuilder
    {
        ILoggerBuilder Reset();
        ILoggerBuilder BuildTimeStamp();
        ILoggerBuilder BuilLogPaths(string path);
        ILoggerBuilder BuildActor();
        ILoggerBuilder BuildActionType();
        ILoggerBuilder BuildAction();
        ILoggerBuilder BuildActionStatus();
        ILogger Build();
    }
}
