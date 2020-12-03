using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
