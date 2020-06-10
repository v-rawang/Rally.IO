using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceProcess;

namespace Rally.Lib.Utility.Common
{
    public class OSUtility
    {
        public static bool IsServiceInstalled(string ServiceName)
        {
            // get list of Windows services
            ServiceController[] services = ServiceController.GetServices();

            // try to find service name
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == ServiceName)
                {
                    return true;
                }
            }
            return false;
        }

        public static ServiceController[] GetServices()
        {
            ServiceController[] services = ServiceController.GetServices();

            return services;
        }

        public static Process[] GetProcesses()
        {
            Process[] processes = Process.GetProcesses();

            return processes;
        }

        public static string GetProcessExecutablePath(string ProcessName)
        {
            Process[] processes = Process.GetProcessesByName(ProcessName);

            if (processes != null && processes.Length > 0)
            {
                return processes[0].MainModule.FileName;
            }

            return null;
        }
    }
}
