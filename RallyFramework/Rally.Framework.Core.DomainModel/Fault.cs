using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Fault
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int? FaultType { get; set; }
        public string FaultMessage { get; set; }
        public string FaultCode { get; set; }
        public string Description { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
