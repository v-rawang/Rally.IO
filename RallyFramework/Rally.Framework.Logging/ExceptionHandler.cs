using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using NLog;
using Rally.Framework.Core;

namespace Rally.Framework.Logging
{
    public class ExceptionHandler : IExHandler
    {
        public static string DefaultPolicyName = "DefaultExceptionPolicy";

        public static IExHandler NewInstance()
        {
            return new ExceptionHandler();
        }

        /// <summary>
        /// Handle an exception
        /// </summary>
        /// <param name="Ex"></param>
        public void HandleException(Exception Ex)
        {
            //ExceptionPolicy.HandleException(ex, DefaultPolicyName);

            var logger = LogManager.GetLogger(DefaultPolicyName);

            logger.Fatal(Ex, new LogItem() { Title = "Exception", Message = Ex.ToString(), Category = "Exception", Level = LogLevel.Fatal.ToString(), MachineName = Environment.MachineName, TimeStamp = DateTime.UtcNow }.ToString());
        }

        public void HandleException(Exception Ex, string Policy)
        {
            //ExceptionPolicy.HandleException(ex, policy);

            var logger = LogManager.GetLogger(Policy);

            logger.Fatal(Ex, new LogItem() { Title = "Exception", Message = Ex.ToString(), Category = "Exception", Level = LogLevel.Fatal.ToString(), MachineName = Environment.MachineName, TimeStamp = DateTime.UtcNow }.ToString());
        }
    }
}
