using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Logging
{
    public class ModuleConfiguration
    {
        //public static string SQL_CMD_Logs = "SELECT ID,TimeStamp,Message,LoggerName FROM logs;";

        //public static string SQL_CMD_DeleteLogs = "DELETE FROM logs WHERE ID = @ID;";

        //public static string SQL_CMD_Logs = "SELECT Logs_ID,Logs_TimeStamp,Logs_Message,Logs_LoggerName FROM tb_mon_logs;";

        //public static string SQL_CMD_DeleteLogs = "DELETE FROM tb_mon_logs WHERE Logs_ID = @ID;";

        public static string SQL_CMD_Logs = "SELECT ID,TimeStamp,Message,LoggerName FROM logs;";

        public static string SQL_CMD_DeleteLogs = "DELETE FROM logs WHERE ID = @ID;";
    }
}
