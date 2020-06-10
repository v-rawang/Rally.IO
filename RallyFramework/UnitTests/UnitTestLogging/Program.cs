using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core;
using Rally.Framework.Facade;

namespace UnitTestLogging
{
    class Program
    {
        static ILogManager logManager = Facade.CreateLogManager();
        
        static void Main(string[] args)
        {
            //logManager.LogServiceOperation();

            //logManager.LogSystemRunning("Test", "Test, Test.");

            

            logManager.LogUserOperation("Test001", "Test");
        }
    }
}
