using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Utility.Common;

namespace UnitTestUtilityCommon
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = OSUtility.GetProcessExecutablePath("mysqld");

            Console.WriteLine(dir);

            Console.Read();
        }
    }
}
