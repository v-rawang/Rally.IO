using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class MaintenanceOrderLineItem
    {
        public string ID { get; set; }
        public string OrderID { get; set; }
        public DateTime? FulfillmentDate { get; set; }
        public Content Notes { get; set; }
        public string RepairResult { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
