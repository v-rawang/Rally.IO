using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Instrument
    {
        public string ID { get;set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int? Type { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SKU { get; set; }
        public int? WarrantyPeriod { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string Remarks { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
