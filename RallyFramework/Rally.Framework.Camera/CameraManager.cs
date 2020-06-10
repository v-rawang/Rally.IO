using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Lib.Camera.Core;
using Rally.Lib.Utility.Common;

namespace Rally.Framework.Camera
{
    public class CameraManager : ICameraManager
    {
        public static ICameraManager NewInstance()
        {
            return new CameraManager();
        }

        public T CreateCameraController<T>(InstrumentCameraSetting CameraSetting)
        {
            if (CameraSetting == null)
            {
                return default(T);
            }

            T instance = CommonUtility.EmitObjectFromFile<T>(CameraSetting.AssemblyFilePath, CameraSetting.ClassName);

            return instance;
        }

        public IList<InstrumentCameraSetting> LoadCameraSettingMeta(string Repository)
        {
            List<InstrumentCameraSetting> cameraSettings = null;

            var assemblies = CommonUtility.ScanAssemblyFromDirectory<IVideoCamera>(Repository, "Rally.Lib.Camera.*.dll");

            if (assemblies != null && assemblies.Length > 0)
            {
                cameraSettings = new List<InstrumentCameraSetting>();

                InstrumentCameraSetting cameraSetting = null;

                foreach (var assembly in assemblies)
                {
                    cameraSetting = new InstrumentCameraSetting()
                    {
                        AssemblyFilePath = assembly.Location,
                        AssemblyName = assembly.FullName
                    };

                    var types = assembly.GetExportedTypes();

                    if (types != null && types.Length > 0)
                    {
                        foreach (var type in types)
                        {
                            if (type.GetInterface(typeof(IVideoCamera).FullName) == typeof(IVideoCamera))
                            {
                                cameraSetting.ClassName = type.FullName;
                                break;
                            }
                        }
                    }

                    var instance = CommonUtility.EmitObjectFromFile<IVideoCamera>(cameraSetting.AssemblyFilePath, cameraSetting.ClassName);

                    if (instance != null)
                    {
                        var info = instance.Get();

                        if (info != null)
                        {
                            cameraSetting.Brand = (string)info["Brand"];
                            cameraSetting.Manufacturer = (string)info["Manufacturer"];
                            cameraSetting.Model = (string)info["Model"];
                            cameraSetting.SKU = (string)info["Specification"];
                            cameraSetting.Version = (string)info["Version"];
                        }
                    }

                    cameraSettings.Add(cameraSetting);
                }
            }

            return cameraSettings;
        }
    }
}
