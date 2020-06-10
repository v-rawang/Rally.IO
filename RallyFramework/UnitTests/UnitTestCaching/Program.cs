using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Caching;

namespace UnitTestCaching
{
    class Program
    {
        static void Main(string[] args)
        {
            CacheManager cacheManager = new CacheManager("127.0.0.1", 6379, "", 0);

            cacheManager.Subscribe(new string[] { "Channel-1"}, (o) => {
                Console.WriteLine($"{((dynamic)(o)).Channel}:{((dynamic)(o)).Message}");
                return o;
            });

            //string ID = Guid.NewGuid().ToString(); //"09f8318e-857d-4c3a-84ac-829d8b05e442";//Guid.NewGuid().ToString();

            RadiationMeasurementDataItem radiationMeasurementDataItem = new RadiationMeasurementDataItem() { Id = Guid.NewGuid(), Latitude = "1", Longitude = "1", CameraNo = "1" };

            bool result = cacheManager.SetCache<RadiationMeasurementDataItem>(radiationMeasurementDataItem.Id.ToString(), radiationMeasurementDataItem);

            RadiationMeasurementDataItem radiationMeasurementDataItem2 = cacheManager.GetCache<RadiationMeasurementDataItem>(radiationMeasurementDataItem.Id.ToString());

            //object obj = cacheManager.GetCache(ID);

            //Console.WriteLine(radiationMeasurementDataItem2.Id);

            Console.Read();
        }
    }
}
