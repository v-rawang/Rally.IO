using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Author
    {
        public string ID { get; set; }

        public string Name { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
