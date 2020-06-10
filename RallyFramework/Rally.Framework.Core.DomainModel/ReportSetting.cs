using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class ReportSetting
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public Attachment Template { get; set; }
        public string Printer { get; set; }
        public int? AutoPrintOnAlarm { get; set; }
        public int? AutoPrintOnMeasurement { get; set; }
        public string TemplateName { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
