using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Facade;

namespace UnitTestAuthorization
{
    class Program
    {
        static void Main(string[] args)
        {
            Global.CurrentDBType = "SQLite";//Rally.Lib.Persistence.Core.DBTypeEnum.SQLite; //Rally.Lib.Persistence.Core.DBTypeEnum.SQLServer;
            Global.CurrentDBConnectionString = @"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db";//@"Data Source=localhost\sqlexpress;Initial Catalog=newford;User ID=sa;Password=W@lcome1";//"server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";
            Security.Regiser();
            Console.Read();
        }
    }
}
