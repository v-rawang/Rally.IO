using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class ProtocolMeta
    {
        public string Protocol { get; set; }
        public string Description { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyFilePath { get; set; }
        public string ClassName { get; set; }
        public string Version { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
