using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newford.Framework.Core;
using Newford.Framework.Camera;
using Newford.Lib.Camera.Core;

namespace UnitTestCamera
{
    public class Program
    {
        static void Main(string[] args)
        {
            ICameraManager cameraManager = new CameraManager();

            var cameraSettings = cameraManager.LoadCameraSettingMeta(AppDomain.CurrentDomain.BaseDirectory);

            if (cameraSettings != null)
            {
                for (int i = 0; i < cameraSettings.Count; i++)
                {
                    Console.WriteLine($"{cameraSettings[i].AssemblyFilePath}:{cameraSettings[i].AssemblyName}:{cameraSettings[i].Brand}:{cameraSettings[i].Model}");
                }
            }

            Console.Read();
        }
    }
}
