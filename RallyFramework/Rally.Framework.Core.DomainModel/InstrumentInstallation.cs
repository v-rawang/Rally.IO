using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class InstrumentInstallation
    {
       public Instrument Instrument { get; set; }
       public string Location { get; set; }
       public string Latitude { get; set; }
       public string Longitude { get; set; }
       public DateTime? InstallationDate { get; set; }
       public DateTime? AcceptanceDate { get; set; }

       [System.Xml.Serialization.XmlIgnore]
       public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
