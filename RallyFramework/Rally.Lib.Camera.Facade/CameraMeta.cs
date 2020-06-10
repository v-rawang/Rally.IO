using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Rally.Lib.Camera.Facade
{
    public class CameraMetaItem : ExpandableObjectConverter
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("assemblyName")]
        public string AssemblyName { get; set; }

        [XmlElement("assemblyFile")]
        public string AssemblyFilePath { get; set; }

        [XmlElement("typeName")]
        public string TypeName { get; set; }

        [XmlElement("version")]
        public string Version { get; set; }
    }

    [XmlRoot("cameras")]
    public class CameraMeta
    {
        [XmlElement("camera")]
        public List<CameraMetaItem> MetaItems {get;set;}
    }
}
