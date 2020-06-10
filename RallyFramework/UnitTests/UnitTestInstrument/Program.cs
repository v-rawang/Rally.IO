using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Lib.Utility.Common;
using Rally.Framework.Facade;

namespace UnitTestInstrument
{
    class Program
    {
        static IInstrumentManager instrumentManager = null;
        static void Main(string[] args)
        {
            Global.CurrentDBType = "MySQL"; //"SQLite";//DBTypeEnum.SQLite;//DBTypeEnum.MySQL; //DBTypeEnum.SQLServer;
            Global.CurrentDBConnectionString = "server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";//@"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db"; //"server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";//@"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db";

            instrumentManager = Facade.CreateInstrumentManager();

            TestGetInstruments();

            //string ID = TestUpdateInstrument("1882767085");

            //string ID = TestInsertInstrument();

            //Console.WriteLine(ID);

            //TestGetInstrument("1882767085");

            //TestGetInstrument("1");

            //TestGetInstrument("1852473693");

            //TestUpdateInstrument("1852473693");

            //TestGetInstrument("1852473693");

            //TestGetInstrumentDistribution("1852473693");

            //TestGetInstrumentInstallation("1852473693");

            //IDMLOperable dMLOperable = MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True");

            //string id = dMLOperable.ExeSqlScalar("select max(Id) from instrument_camera_settings");

            //Console.WriteLine(id);

            //long idNum = -1;

            //if (long.TryParse(id, out idNum))
            //{
            //    Console.WriteLine(idNum++);
            //}
            //else
            //{
            //    Console.WriteLine(idNum);
            //}

            Console.Read();
        }


        static void TestGetInstruments()
        {
            var instruments = instrumentManager.GetInstruments();

            if (instruments != null && instruments.Count >0)
            {
                foreach (var instrument in instruments)
                {
                    if (instrument != null)
                    {
                        Console.WriteLine($"{instrument.ID}:{instrument.Name}:{instrument.Name}:{instrument.PurchaseDate}:{instrument.Type}");
                    }
                }
            }
        }

        static void TestGetInstrument(string ID) {

            //Global.CurrentDBType = DBTypeEnum.SQLite; //DBTypeEnum.SQLServer;
            //Global.CurrentDBConnectionString = @"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db"; //@"Data Source=localhost\sqlexpress;Initial Catalog=newford;User ID=sa;Password=W@lcome1";

            //IInstrumentManager instrumentManager = Facade.CreateInstrumentManager(); //InstrumentManager.NewInstance(MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True"), DBTypeEnum.MySQL);

            Instrument instrument = instrumentManager.GetInstrument(ID);

            if (instrument != null)
            {
                Console.WriteLine($"{instrument.ID}:{instrument.Name}:{instrument.Name}:{instrument.PurchaseDate}:{instrument.Type}");
            }
        }

        static void TestGetInstrumentDistribution(string ID)
        {
            //IInstrumentManager instrumentManager = InstrumentManager.NewInstance(MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True"), DBTypeEnum.MySQL);

            InstrumentDistribution distribution = instrumentManager.GetInstrumentDistribution(ID);

            if (distribution != null)
            {
                Console.WriteLine($"{distribution.Instrument.ID}:{distribution.Instrument.Name}:{distribution.Organization}:{distribution.Department}:{distribution.WorkGroup}");
            }
        }

        static void TestGetInstrumentInstallation(string ID)
        {
            //IInstrumentManager instrumentManager = InstrumentManager.NewInstance(MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True"), DBTypeEnum.MySQL);

            InstrumentInstallation installation = instrumentManager.GetInstrumentInstallation(ID);

            if (installation != null)
            {
                Console.WriteLine($"{installation.Instrument.ID}:{installation.Instrument.Name}:{installation.Latitude}:{installation.Latitude}:{installation.Location}:{installation.InstallationDate}:{installation.AcceptanceDate}");
            }
        }

        static string TestInsertInstrument()
        {
            //Global.CurrentDBType = DBTypeEnum.SQLite;//DBTypeEnum.MySQL; //DBTypeEnum.SQLServer;
            //Global.CurrentDBConnectionString = @"Data Source=D:\Rally\Documents\PlatformAndFramework\SourceCode\newford.db"; //"server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True";//@"Data Source=localhost\sqlexpress;Initial Catalog=newford;User ID=sa;Password=W@lcome1";
            //IInstrumentManager instrumentManager = Facade.CreateInstrumentManager();  /*InstrumentManager.NewInstance(MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True"), DBTypeEnum.MySQL);*/

            string ID = instrumentManager.AddInstrument(new Instrument() {
                //ID = instrumentManager.GenerateInstrumentID(), //new Random().Next().ToString(),
                Name = $"Test-Instrument{DateTime.Now.Millisecond}",
                Model = "Test01",
                SerialNumber = CommonUtility.GetMillisecondsOfCurrentDateTime(null),
                Brand = "Test01",
                SKU = "Test01",
                Manufacturer = "Test01",
                Alias = "Test01",     
                ShipmentDate = DateTime.Now,
                PurchaseDate = DateTime.Now,
                Type = 0,
                WarrantyPeriod = 36,
                Remarks = "Test01"
            });

            return ID;
        }

        static string TestUpdateInstrument(string ID)
        {
            //IInstrumentManager instrumentManager = InstrumentManager.NewInstance(MySQLDBOperator.NewInstance("server=localhost;user id=root;password=W@lcome1;persistsecurityinfo=True;database=newford;allowuservariables=True"), DBTypeEnum.MySQL);

            instrumentManager.SetInstrument(ID, new Instrument() {
                ID = ID,
                Name = $"Test-Instrument{DateTime.Now.Millisecond}",
                Model = "Test01",
                SerialNumber = CommonUtility.GetMillisecondsOfCurrentDateTime(null),
                //Brand = "Test01",
                //SKU = "Test01",
                Manufacturer = "Test01",
                //Alias = "Test01",
                ShipmentDate = DateTime.Now,
                PurchaseDate = DateTime.Now,
                Type = null,
                WarrantyPeriod = 48,
                //Remarks = "Test01"
            });

            return ID;
        }
    }
}
