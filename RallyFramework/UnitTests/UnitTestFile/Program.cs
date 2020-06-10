using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Framework.Facade;

namespace UnitTestFile
{
    class Program
    {
        static IFileManager fileManager = null;//FileManager.NewInstance(MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True"), DBTypeEnum.MySQL);

        static void Main(string[] args)
        {
            Global.CurrentDBType = "SQLite";//Rally.Lib.Persistence.Core.DBTypeEnum.SQLite; /*Rally.Lib.Persistence.Core.DBTypeEnum.SQLServer;*/
            Global.CurrentDBConnectionString = @"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db"; //@"Data Source=localhost\sqlexpress;Initial Catalog=newford;User ID=sa;Password=W@lcome1"; //"server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";

            fileManager = Facade.CreateFileManager();

            string fileId = TestAddFile();

            Console.WriteLine(fileId);

            Console.Read();
        }

        static string TestAddFile()
        {
            string fileSource = @"D:\Rally\Documents\PlatformAndFramework\Test";
            string fileTarget = $@"D:\Rally\Documents\PlatformAndFramework\Test\UnitTestData\{Guid.NewGuid().ToString()}";

            IEnumerable<string> files = Directory.EnumerateFiles(fileSource, "*.*", SearchOption.TopDirectoryOnly);

            string fileId = "";

            if (files != null)
            {
                string fileFullPath = "";

                foreach (var file in files)
                {
                    fileFullPath = file;
                    break;
                }

                FileInfo fileInfo = new FileInfo(fileFullPath);

                IDictionary<string, object> fileInfoDic = new Dictionary<string, object>() {
                    { "FilePath", fileTarget},
                    { "FileName", fileInfo.Name},
                    { "FileType", fileInfo.Extension},
                    { "Version", "1.0.0.1"},
                    { "FileOwner", "Test"}
                };

                byte[] fileBytes = File.ReadAllBytes(fileFullPath);

                fileId = fileManager.AddFile(fileBytes, fileInfoDic);
            }

            return fileId;
        }
    }
}
