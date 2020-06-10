using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Content
    {
        public string ID { get; set; }

        public string Body { get; set; }

        public DateTime? CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }

        public ContentStatus Status { get; set; }

        public Author Author { get; set; }

        public string Version { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
