using System;
using System.IO;
using System.Net;
using System.Text;

namespace FileForgeDP.Loggers
{
    public class AuditLogger : ILogger
    {
        private readonly object mFileLock = new object();

        internal string FormatString { get; set; }
        internal string PathToFile { get; set; }

        public void Log(string log)
        {   
            SaveToFile(log, PathToFile);
        }

        public void Debug(string actor, ActionEnum enumActionType, string action, HttpStatusCode actionStatus, string target = null)
        {
            var logBuilder = new StringBuilder(FormatString);
            logBuilder.Replace(LoggerOptionsConstants.USE_ACTION, action)
                      .Replace(LoggerOptionsConstants.USE_ACTION_STATUS, ((int)actionStatus).ToString())
                      .Replace(LoggerOptionsConstants.USE_ACTOR, actor)
                      .Replace(LoggerOptionsConstants.USE_TIMESTAMP, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                      .Replace(LoggerOptionsConstants.USE_ACTION_TYPE, Enum.GetName(typeof(ActionEnum), enumActionType))
                      .Replace(LoggerOptionsConstants.USE_TARGET, target ?? string.Empty);

            Log(logBuilder.ToString());
            logBuilder.Clear();
        }

        private void SaveToFile(string log, string path)
        {
            lock (mFileLock)
            {
                using var sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path);
                sw.WriteLine(log);
            }
        }
    }
}
