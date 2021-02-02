using System.Net;

namespace FileForgeDP.Loggers
{
    public interface ILogger
    {
        void Log(string log);
        void Debug(string actor, ActionEnum enumActionType, string action, HttpStatusCode actionStatus, string target = null);
    }
}
