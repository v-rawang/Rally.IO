using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public interface ILogManager
    {
        string GetMethodName();

        string GetOperationLogTitle(string Prefix, string UserName);

        void LogUserOperation(string UserName, string Message);

        void LogServiceOperation(string UserName, string Message);

        void LogSystemError(string Title, string Message);

        void LogSystemRunning(string Title, string Message);
    }
}
