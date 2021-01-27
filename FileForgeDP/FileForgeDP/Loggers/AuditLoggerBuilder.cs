using System.Text;

namespace FileForgeDP.Loggers
{
    public class AuditLoggerBuilder : ILoggerBuilder
    {
        private AuditLogger mResult = new AuditLogger();
        private StringBuilder mFormatStringBuilder = new StringBuilder();

        public ILogger Build()
        {
            mResult.FormatString = mFormatStringBuilder.ToString();
            return mResult;
        }

        public ILoggerBuilder BuildAction()
        {
            mFormatStringBuilder.Append(LoggerOptionsConstants.USE_ACTION);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildActionStatus()
        {
            mFormatStringBuilder.Append(LoggerOptionsConstants.USE_ACTION_STATUS);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildActionType()
        {
            mFormatStringBuilder.Append(LoggerOptionsConstants.USE_ACTION_TYPE);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildActor()
        {
            mFormatStringBuilder.Append(LoggerOptionsConstants.USE_ACTOR);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuildTimeStamp()
        {
            mFormatStringBuilder.Append(LoggerOptionsConstants.USE_TIMESTAMP);
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder BuilLogPaths(string path)
        {
            mResult.PathToFile = path;
            mFormatStringBuilder.Append(" ");
            return this;
        }

        public ILoggerBuilder Reset()
        {
            mFormatStringBuilder.Clear();
            return this;
        }
    }
}
