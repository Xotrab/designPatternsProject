using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileForgeDP.Loggers
{
    public class AuditLoggerBuilder : ILoggerBuilder
    {
        private AuditLogger mResult = new AuditLogger();
        private StringBuilder mFormatStringBuilder = new StringBuilder();
        public ILogger Build()
        {
            this.mResult.FormatString = mFormatStringBuilder.ToString();
            return mResult;
        }

        public ILoggerBuilder BuildAction()
        {
            mFormatStringBuilder.Append(LogerOptionsConstants.USE_ACTION);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildActionStatus()
        {
            mFormatStringBuilder.Append(LogerOptionsConstants.USE_ACTION_STATUS);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildActionType()
        {
            mFormatStringBuilder.Append(LogerOptionsConstants.USE_ACTION_TYPE);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildActor()
        {
            mFormatStringBuilder.Append(LogerOptionsConstants.USE_ACTOR);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildTimeStamp()
        {
            mFormatStringBuilder.Append(LogerOptionsConstants.USE_TIMESTAMP);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuilLogPaths(string path)
        {
            this.mResult.PathToFile = path;
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder Reset()
        {
            this.mFormatStringBuilder.Clear();
            return this;
        }


    }
}
