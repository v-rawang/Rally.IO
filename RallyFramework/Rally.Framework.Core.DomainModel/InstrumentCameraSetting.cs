using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class InstrumentCameraSetting
    {
       public string ID { get; set; }
       public Instrument Instrument { get; set; }
       public CameraTypeEnum CameraType { get; set; }
       public VideoConnectionTypeEnum VideoConnectionType { get; set; }
       public string CameraIpAddress { get; set; }
       public int? CameraPortNumber { get; set; }
       public string CameraLoginName { get; set; }
       public string CameraPassword { get; set; }
       public string SerialNumber { get; set; }
       public string Manufacturer { get; set; }
       public string Brand { get; set; }
       public string Model { get; set; }
       public string SKU { get; set; }
       public string AssemblyName { get; set; }
       public string AssemblyFilePath { get; set; }
       public string ClassName { get; set; }
       public string Version { get; set; }
        public int? Index { get; set; }
        [System.Xml.Serialization.XmlIgnore]
       public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
