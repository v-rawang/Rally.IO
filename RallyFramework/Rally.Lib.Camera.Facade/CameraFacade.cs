using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Rally.Lib.Camera.Core;
using Rally.Lib.Camera.Core.Parameter;

namespace Rally.Lib.Camera.Facade
{
    public class CameraFacade
    {
        public static IList<CameraMeta> LoadAssemblyMeta(string AssemblyRepository)
        {
            return null;
        }

        public static IVideoCamera LoadCamera(string AssemblyPath, string TypeName)
        {
            return Utility.EmitObjectFromFile<IVideoCamera>(AssemblyPath, TypeName);
        }

        public static IDictionary<string, CameraMetaItem> LoadMeta(string ConfigPath)
        {
            string xml = "";
            IDictionary<string, CameraMetaItem> cameraInfoDict = null;

            using (FileStream fileStream = new FileStream(ConfigPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    xml = streamReader.ReadToEnd();
                }
            }

            CameraMeta cameraMeta = Utility.XmlDeserialize<CameraMeta>(xml, new Type[] { typeof(CameraMetaItem), typeof(List<CameraMetaItem>) }, "utf-8");

            if (cameraMeta != null && cameraMeta.MetaItems != null)
            {
                cameraInfoDict = new Dictionary<string, CameraMetaItem>();

                foreach (var item in cameraMeta.MetaItems)
                {
                    cameraInfoDict.Add(item.Name, item);
                }
            }
            
            return cameraInfoDict;
        }

        public static void SaveMeta(CameraMeta Meta, string ConfigPath)
        {
            string xml = Utility.XmlSerialize(Meta, null, "utf-8");

            using (FileStream fileStream = new FileStream(ConfigPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(xml);
                }
            }
        }

        public static IPCameraParameter LoadParameter(string ConfigPath)
        {
            string xml = "";

            IPCameraParameter cameraParameter = null;

            using (FileStream fileStream = new FileStream(ConfigPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    xml = streamReader.ReadToEnd();
                }
            }

            cameraParameter = Utility.XmlDeserialize<IPCameraParameter>(xml, null, "utf-8");

            return cameraParameter;
        }

        public static void SaveParameter(IPCameraParameter Parameter, string ConfigPath)
        {
            string xml = Utility.XmlSerialize(Parameter, null, "utf-8");

            using (FileStream fileStream = new FileStream(ConfigPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(xml);
                }
            }
        }
    }
}
