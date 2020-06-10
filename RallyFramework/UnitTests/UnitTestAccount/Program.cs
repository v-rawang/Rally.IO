using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Framework.Facade;

namespace UnitTestAccount
{
    class Program
    {
        static IAccountManager accountManager = null;

        static void Main(string[] args)
        {
            Global.CurrentDBType = "MySQL";//"SQLite"; //Rally.Lib.Persistence.Core.DBTypeEnum.SQLite; /*Rally.Lib.Persistence.Core.DBTypeEnum.SQLServer;*/
            Global.CurrentDBConnectionString = "server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";//@"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db"; //@"Data Source=localhost\sqlexpress;Initial Catalog=newford;User ID=sa;Password=W@lcome1"; //"server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";
            accountManager = Facade.CreateAccountManager();

            TestGetAccounts();

            TestGetAccountsNoPaging();

            Console.Read();
        }

        static void TestGetAccounts()
        {
            int pageCount = -1, totalRecords = -1;
            var accounts = accountManager.GetAccounts(0, 10, out pageCount, out totalRecords, null);

            if (accounts != null && accounts.Count > 0)
            {
                foreach (var account in accounts)
                {
                    Console.WriteLine($"{account.ID}:{account.Name}:{account.NickName}:{account.FirstName}:{account.LastName}");
                }
            }

            Console.WriteLine(pageCount);
            Console.WriteLine(totalRecords);
        }

        static void TestGetAccountsNoPaging()
        {
            var accounts = accountManager.GetAccounts();

            if (accounts != null && accounts.Count > 0)
            {
                foreach (var account in accounts)
                {
                    Console.WriteLine($"{account.ID}:{account.Name}:{account.NickName}:{account.FirstName}:{account.LastName}");
                }
            }
        }
    }
}
