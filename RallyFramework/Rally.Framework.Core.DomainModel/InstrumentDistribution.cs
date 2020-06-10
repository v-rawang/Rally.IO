using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class InstrumentDistribution
    {
        public Instrument Instrument { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string WorkGroup { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
