using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Camera.Core;
using Rally.Lib.Camera.Core.Parameter;
using Rally.Lib.Camera.Facade;

namespace UnitTestCamera
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FormCameraTest formCameraTest = new FormCameraTest();

            System.Windows.Forms.Application.Run(formCameraTest);

            //TestSaveMeta();

            //TestLoadMeta();

            //TestLoadAssembly();

            //Console.Read();
        }

        static void TestSaveMeta()
        {
            CameraMeta cameraMeta = new CameraMeta() {
                MetaItems = new List<CameraMetaItem>(new CameraMetaItem[] {
                    new CameraMetaItem(){ ID= Guid.NewGuid().ToString(), Name = "HC", Description="海康威视",  AssemblyFilePath="Rally.Lib.Camera.HC.dll", AssemblyName="Rally.Lib.Camera.HC", TypeName="HCCamera", Version="1.0.0.0" },
                    new CameraMetaItem(){ ID= Guid.NewGuid().ToString(), Name = "CT", Description="智鑫安盾", AssemblyFilePath="Rally.Lib.Camera.CT.dll", AssemblyName="Rally.Lib.Camera.CT", TypeName="CTCamera", Version="1.0.0.0" }
                })
            };

            CameraFacade.SaveMeta(cameraMeta, "cameras.xml");
        }

        static void TestLoadMeta()
        {
            IDictionary<string, CameraMetaItem> cameraMetaItemDict =  CameraFacade.LoadMeta("cameras.xml");

            if (cameraMetaItemDict != null)
            {
                foreach (string key in cameraMetaItemDict.Keys)
                {
                    Console.WriteLine($"{key}:{cameraMetaItemDict[key].Description}:{cameraMetaItemDict[key].AssemblyName}:{cameraMetaItemDict[key].AssemblyFilePath}:{cameraMetaItemDict[key].TypeName}:{cameraMetaItemDict[key].ID}:{cameraMetaItemDict[key].Version}");
                }
            }
        }

        static void TestLoadAssembly()
        {
            IDictionary<string, CameraMetaItem> cameraMetaItemDict = CameraFacade.LoadMeta("cameras.xml");

            IVideoCamera camera = null;
            IDictionary<string, object> cameraStatus = null;

            if (cameraMetaItemDict != null)
            {
                foreach (string key in cameraMetaItemDict.Keys)
                {
                    //Console.WriteLine($"{key}:{cameraMetaItemDict[key].Description}:{cameraMetaItemDict[key].AssemblyName}:{cameraMetaItemDict[key].AssemblyFilePath}:{cameraMetaItemDict[key].TypeName}:{cameraMetaItemDict[key].ID}:{cameraMetaItemDict[key].Version}");

                    camera =  CameraFacade.LoadCamera($"{AppDomain.CurrentDomain.BaseDirectory}{System.IO.Path.DirectorySeparatorChar}{cameraMetaItemDict[key].AssemblyFilePath}", cameraMetaItemDict[key].TypeName);
                    cameraStatus = camera.Get();

                    foreach (string item in cameraStatus.Keys)
                    {
                        Console.WriteLine($"{item}:{cameraStatus[item]}");
                    }
                }
            }
        }
    }
}
