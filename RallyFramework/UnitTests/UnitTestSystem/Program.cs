using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newford.Lib.Persistence.Core;
using Newford.Framework.Core;
using Newford.Framework.Core.DomainModel;
using Newford.Framework.Facade;
using Newford.Lib.Utility.Common;

namespace UnitTestSystem
{
    class Program
    {
        static IApplicationManager applicationManager = null; //SystemManager.NewInstance(MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True"), DBTypeEnum.MySQL);

        static void Main(string[] args)
        {
            Global.CurrentDBType = "SQLite";//Newford.Lib.Persistence.Core.DBTypeEnum.SQLite; /*Newford.Lib.Persistence.Core.DBTypeEnum.SQLServer;*/
            Global.CurrentDBConnectionString = @"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db"; //@"Data Source=localhost\sqlexpress;Initial Catalog=newford;User ID=sa;Password=W@lcome1"; //"server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";

            applicationManager = Facade.CreateApplicationManager();

            string settingId = TestAddAppSetting();

            Console.WriteLine(settingId);

            TestGetAppSetting(settingId);

            settingId = TestUpdateAppSetting(settingId);

            TestGetAppSetting(settingId);

            Console.Read();
        }

        static string TestAddAppSetting()
        {
            return applicationManager.AddApplicationSetting(new ApplicationSetting() {
                ID = CommonUtility.GetMillisecondsOfCurrentDateTime(null, true),
                 AlarmMode = 0,
                  AlarmSound = "test.wav",
                   Title = "TestApp",
                   Language = 1
            });
        }

        static string TestUpdateAppSetting(string settingId)
        {
            ApplicationSetting applicationSetting = applicationManager.UpdateApplicationSetting(settingId, new ApplicationSetting()
            {
                ID = settingId,
                AlarmMode = 0,
                AlarmSound = "test01.wav",
                NotificationMode = 1,
                NotificationSound = "test02.wav",
                //PlatformIpAddress = "192.168.1.192",
                //PlatformPortNumber = 8080,
                Title = "TestApp",
                Language = 0,
            });

            return applicationSetting.ID;
        }

        static void TestGetAppSetting(string settingId)
        {
            var appSetting = applicationManager.GetApplicationSetting(settingId);

            if (appSetting != null)
            {
                Console.WriteLine($"{appSetting.ID}:{appSetting.Title}:{appSetting.AlarmMode}:{appSetting.NotificationMode}:{appSetting.Language}");
            }
        }
    }
}
