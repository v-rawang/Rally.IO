using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class MaintenanceOrder
    {
        public string ID { get; set; }
        public string OrderRefID { get; set; }
        public Instrument Instrument { get; set; }
        public Fault Fault { get; set; }
        public DateTime? FaultTime { get; set; }
        public string Description { get; set; }        
        public DateTime? OrderDate { get; set; }
        public int? WarrantyStatus { get; set; }
        public decimal? Cost { get; set; }
        public Comment Result { get; set; }
        public Comment Comment { get; set; }

        public List<MaintenanceOrderLineItem> OrderLineItems { get; set; }
        public List<MaintenanceAttachment> Attachments { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
