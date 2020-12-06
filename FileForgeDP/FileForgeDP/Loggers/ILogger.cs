namespace FileForgeDP.Loggers
{
    public interface ILogger
    {
        void Log(string log);
        void Debug(string Actor, ActionEnum EnumActionType, string Action, string ActionStatus);
    }
}
