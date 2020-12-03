using System;
using System.IO;
using System.Text;

namespace FileForgeDP.Loggers
{
    public class AuditLogger : ILogger
    {

        internal string FormatString { get; set; }
        internal string PathToFile { get; set; }

        public AuditLogger(){}
        public void Log(string log)
        {   
            saveToFIle(log, this.PathToFile);
        }

        public void Debug(string Actor, ActionEnum EnumActionType,string Action, string ActionStatus)
        {
            var logBuilder = new StringBuilder(FormatString);
            logBuilder.Replace(LogerOptionsConstants.USE_ACTION, Action)
                .Replace(LogerOptionsConstants.USE_ACTION_STATUS, ActionStatus)
                .Replace(LogerOptionsConstants.USE_ACTOR, Actor)
                .Replace(LogerOptionsConstants.USE_TIMESTAMP, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                .Replace(LogerOptionsConstants.USE_ACTION_TYPE, Enum.GetName(typeof(ActionEnum), EnumActionType));

            Log(logBuilder.ToString());
            logBuilder.Clear();
        }
        public async void saveToFIle(string log, string path)
        {   
            using (StreamWriter sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))             
            {   
                // Feel free to format output
                sw.WriteLine(log);
            }
        }
    }
}
