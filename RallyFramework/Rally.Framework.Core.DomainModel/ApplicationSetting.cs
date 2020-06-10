using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class ApplicationSetting
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Attachment Banner { get; set; }

        public List<Platform1> Platform { get; set; }

        //public string PlatformName { get; set; }
        //public string PlatformIpAddress { get; set; }
        //public int? PlatformPortNumber { get; set; }
        //public string PlatformProtocol { get; set; }

        //public string PlatformName2 { get; set; }
        //public string PlatformIpAddress2 { get; set; }
        //public int? PlatformPortNumber2 { get; set; }
        //public string PlatformProtocol2 { get; set; }

        //public string PlatformName3 { get; set; }
        //public string PlatformIpAddress3 { get; set; }
        //public int? PlatformPortNumber3 { get; set; }
        //public string PlatformProtocol3 { get; set; }

        //public string PlatformName4 { get; set; }
        //public string PlatformIpAddress4 { get; set; }
        //public int? PlatformPortNumber4 { get; set; }
        //public string PlatformProtocol4 { get; set; }

        public int? AlarmMode { get; set; }
        public string AlarmSound { get; set; }
        public int? NotificationMode { get; set; }
        public string NotificationSound { get; set; }
        public int? Language { get; set; }
        //新增播放时长
        public string Alarmduration { get; set; }
        public string Notificationduration { get; set; }
        //新增主题
        public string theme { get; set; }

        public string Time { get; set; }
        public string Type { get; set; }

        public int? BackgroundDataArchivingFrequency { get; set; }
        public int? ImageCapturingMode { get; set; }
        public int? ImageCapturingCount { get; set; }
        public int? VideoCapturingMode { get; set; }
        public int? VideoCapturingLength { get; set; }
        
        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
