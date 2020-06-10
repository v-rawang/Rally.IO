using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Platform1
    {

        public string PlatformName { get; set; }

        public string PlatformIpAddress { get; set; }

        public int? PlatformPortNumber { get; set; }

        public string PlatformProtocol { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
