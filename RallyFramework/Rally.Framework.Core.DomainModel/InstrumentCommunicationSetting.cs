using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class InstrumentCommunicationSetting
    {
        public string ID { get; set; }
        public Instrument Instrument { get; set; }
        public CommunicationTypeEnum CommunicationType { get; set; }
        public string IpAddress { get; set; }
        public int? PortNumber { get; set; }
        public string SerialPortName { get; set; }
        public int? SerialPortBaudRate { get; set; }
        public string BluetoothDeviceName { get; set; }
        public string BluetoothAddress { get; set; }
        public string BluetoothKey { get; set; }
        public string Protocol { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyFilePath { get; set; }
        public string ClassName { get; set; }
        public string Version { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Dictionary<string, object> DynamicProperties { get; set; }
    }
}
