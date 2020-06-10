using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Framework.Facade;
using Rally.Lib.Utility.Hardware;

namespace UnitTestAuthentication
{
    class Program
    {
        static IUserManager userManager = null;
        static IAccountManager accountManager = null;
        static IAuthentication authentication = null;
        
        static void Main(string[] args)
        {
            Global.CurrentDBType = "SQLite"; //Newford.Lib.Persistence.Core.DBTypeEnum.SQLite; /*Newford.Lib.Persistence.Core.DBTypeEnum.SQLServer;*/
            Global.CurrentDBConnectionString = @"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db"; //@"Data Source=localhost\sqlexpress;Initial Catalog=newford;User ID=sa;Password=W@lcome1"; //"server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";
            userManager = Facade.CreateUserManager();
            accountManager = Facade.CreateAccountManager();
            authentication = Facade.CreateAuthenticationManager();

            //TestAddUser();

            //TestDeleteUser("a1f2a193-01d7-4256-a944-2855be3d570c");

            //TestUserExsits("a1f2a193-01d7-4256-a944-2855be3d570c");

            //TestUserExsits("983f7288-20cb-4be8-bd73-cebf61b6aa42", "Test001");

            //TestLogin("Test001", "Test001PWD");

            //TestDeleteUser("983f7288-20cb-4be8-bd73-cebf61b6aa42", "Test001");

            //TestDeleteUser(null, "Test001");

            //TestValidateUser("Test001", "Test001PWD");

            //TestValidateToken("7bd5fc0c59bbfe288c93845edf5d98804b774e76");

            //TestValidateToken("0f95e60c07849b37c2b8df6b0b872d18f2e5c0f3");

            //int result = DateTime.Now.CompareTo(DateTime.Now.AddDays(-31));

            //Console.WriteLine(result);

            //TestChangePassword("Test001", "P@ssword2");

            //TestLogin("Test001", "Test001PWD");

            //TestLogin("Test001", "P@ssword1");

            //TestValidateToken("0f95e60c07849b37c2b8df6b0b872d18f2e5c0f3");

            //TestLogin("Test001", "P@ssword2");

            //TestValidateToken("0f95e60c07849b37c2b8df6b0b872d18f2e5c0f3");

            //TestValidateToken("2556a08ac994416af7060bef44f1ef955d9936f3");


            //TestValidateToken("6e500b77a0734ec01b96dbff9b87db749abed4c9");

            //TestValidateUser("Test001", "P@ssword2");

            TestValidateToken("e2f1fc9470d56561d8e074da5e452d68824dd9c8");

            Console.Read();
        }

        static void TestAddUser()
        {
           var acct = userManager.AddUser<Account>("Test001", "Test001PWD", a => {

                string userId = a.ToString();

                Account account = new Account() { ID = userId, Name = "Test001", NickName = "Test001", FirstName = "Test", LastName = "001"};
                accountManager.AddAccount(account);
                return account;
            });

            if (acct != null)
            {
                Console.WriteLine($"{acct.ID}:{acct.FirstName}:{acct.LastName}:{acct.NickName}");
            }
        }

        static void TestDeleteUser(string ID, string UserName = null)
        {
            bool result = userManager.DeleteUser<bool>(ID, UserName, null);

            Console.WriteLine(result);
        }

        static void TestUserExsits(string ID, string UserName = null)
        {
            bool result = userManager.UserExists(ID, UserName);
            Console.WriteLine(result);
        }

        static void TestLogin(string UserName, string Password)
        {
            bool result = authentication.Login(UserName, Password);
            Console.WriteLine(result);
        }

        static void TestValidateUser(string UserName, string Password, string Key = null)
        {
            string key = String.IsNullOrEmpty(Key) ? HardwareInfoUtility.GetProcessorId() : Key;

            var token = authentication.ValidateUser(UserName, Password, key);

            if (token != null)
            {
                Console.WriteLine($"{token.UserName}:{token.AccessToken}:{token.ExpiresIn}:{token.RefreshToken}:{token.Scope}");
            }
        }

        static void TestValidateToken(string Token, string Key = null)
        {
            string key = String.IsNullOrEmpty(Key) ? HardwareInfoUtility.GetProcessorId() : Key;

            bool result = authentication.ValidateToken(Token, key);

            Console.WriteLine(result);
        }

        static void TestChangePassword(string UserName, string Password)
        {
            bool result = authentication.ChangePassword(UserName, Password);

            Console.WriteLine(result);
        }
    }
}
