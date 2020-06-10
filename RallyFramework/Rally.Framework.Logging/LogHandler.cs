using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NLog;
using Rally.Framework.Core;
using Rally.Lib.Persistence.Core;

namespace Rally.Framework.Logging
{
    public class LogHandler : ILogManager
    {

        public static ILogManager NewInstance()
        {
            return new LogHandler();
        }

        public LogHandler()
        {
        }

        /// <summary>
        /// Log count per file
        /// </summary>
        public const int LogsPerFile = 500;
        /// <summary>
        /// Category name of system logs
        /// </summary>
        public const string SystemCategoryName = "System";
        /// <summary>
        /// Category name of user operation logs
        /// </summary>
        public const string UserOperationCategoryName = "Operation-User";

        /// <summary>
        /// Category name of service operation logs
        /// </summary>
        public const string ServiceOperationCategoryName = "Operation-Service";

        public static string DefaultLoggerName = "DefaultLogger";

        /// <summary>
        /// Get current method name
        /// </summary>
        /// <returns></returns>
        public string GetMethodName()
        {
            return new StackTrace().GetFrame(1).GetMethod().Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Prefix"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string GetOperationLogTitle(string Prefix, string UserName)
        {
            string title = String.Format("OPR-{0}", Prefix);

            if (!string.IsNullOrEmpty(UserName))
            { 
                title = string.Format("{0} - {1}", title, UserName);
            }

            return title;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Message"></param>
        public void LogUserOperation(string UserName, string Message)
        {
            //LogMessage(GetOperationLogTitle("User", userName), message, UserOperationCategoryName, TraceEventType.Information);

            var logger = NLog.LogManager.GetLogger(LogHandler.DefaultLoggerName);

            //logger.Info(new LogItem() { Title = GetOperationLogTitle("User", UserName), Message = Message, Category = UserOperationCategoryName, Level = LogLevel.Info.ToString(), MachineName = Environment.MachineName, TimeStamp = DateTime.UtcNow, UserName = UserName }.ToString());

            logger.Info(new LogItem("|") { Title = GetOperationLogTitle("User", UserName), Message = Message, Category = UserOperationCategoryName, Level = LogLevel.Info.ToString(), MachineName = Environment.MachineName, TimeStamp = DateTime.UtcNow, UserName = UserName }.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Message"></param>
        public void LogServiceOperation(string UserName, string Message)
        {
            //LogMessage(GetOperationLogTitle("Service", userName), message, ServiceOperationCategoryName, TraceEventType.Information);

            var logger = NLog.LogManager.GetLogger(LogHandler.DefaultLoggerName);

            logger.Info(new LogItem() { Title = GetOperationLogTitle("Service", UserName), Message = Message, Category = ServiceOperationCategoryName, Level = LogLevel.Info.ToString(), MachineName = Environment.MachineName, TimeStamp = DateTime.UtcNow, UserName = UserName }.ToString());
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="Title"></param>
       /// <param name="Message"></param>
        public void LogSystemError(string Title, string Message)
        {
            //LogSystemRunning(title, message, TraceEventType.Error);

            var logger = NLog.LogManager.GetLogger(LogHandler.DefaultLoggerName);

            logger.Info(new LogItem() { Title = Title, Message = Message, Category = SystemCategoryName, Level = LogLevel.Error.ToString(), MachineName = Environment.MachineName, TimeStamp = DateTime.UtcNow }.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Message"></param>
        public void LogSystemRunning(string Title, string Message)
        {
            //LogMessage(title, message, SystemCategoryName, TraceEventType.Information);

            var logger = NLog.LogManager.GetLogger(LogHandler.DefaultLoggerName);

            logger.Info(new LogItem() { Title = Title, Message = Message, Category = SystemCategoryName, Level = LogLevel.Info.ToString(), MachineName = Environment.MachineName, TimeStamp = DateTime.UtcNow }.ToString());
        }
    }
}
