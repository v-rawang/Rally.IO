using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Rally.Lib.Signal.Core;

namespace Rally.Lib.Signal.Core.Parameter
{
    public class BluetoothParameter: NetworkParameter
    {
        private string remoteServiceIdentifier;
        private string localServiceIdentifier;
        private string serviceClass;
        private string serviceName;
        private string devicePin;
        private BluetoothModeEnum bluetoothMode;

        public string RemoteServiceIdentifier { get { return this.remoteServiceIdentifier; } set { this.remoteServiceIdentifier = value; } }
        public string LocalServiceIdentifier { get { return this.localServiceIdentifier; } set { this.localServiceIdentifier = value; } }
        public string ServiceClass { get { return this.serviceClass; } set { this.serviceClass = value; } }
        public string ServiceName { get { return this.serviceName; } set { this.serviceName = value; } }
        public string DevicePIN { get { return this.devicePin; } set { this.devicePin = value; } }

        [DefaultValue(BluetoothModeEnum.Server)]
        public BluetoothModeEnum BluetoothMode { get { return this.bluetoothMode; }set { this.bluetoothMode = value; } }
    }
}
