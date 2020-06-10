using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Rally.Lib.Signal.Core;

namespace Rally.Lib.Signal.Bluetooth
{
    public class BluetoothDeviceDiscoverer : IDiscoverer
    {
        public static IDiscoverer NewInstance()
        {
            return new BluetoothDeviceDiscoverer();
        }

        public IDictionary<string, object> Discover()
        {
            IDictionary<string, object> deviceInfoDict = null;

            BluetoothRadio.PrimaryRadio.Mode = RadioMode.Discoverable;
            BluetoothClient bluetoothClient = new BluetoothClient();

            deviceInfoDict = new Dictionary<string, object>() { { BluetoothRadio.PrimaryRadio.Name, BluetoothRadio.PrimaryRadio.LocalAddress.ToString("P") } };

            BluetoothDeviceInfo[] bluetoothDeviceInfos = bluetoothClient.DiscoverDevices();

            if (bluetoothDeviceInfos != null)
            {
                foreach (var info in bluetoothDeviceInfos)
                {
                    deviceInfoDict.Add(info.DeviceName, info.DeviceAddress.ToInt64());
                }
            }

            return deviceInfoDict;
        }

        public IDictionary<string, object> Discover<T>(Func<object, T> ExtensionFunction, out T OutputObject)
        {
            IDictionary<string, object> deviceInfoDict = null;
            OutputObject = default(T);

            BluetoothRadio.PrimaryRadio.Mode = RadioMode.Discoverable;
            BluetoothClient bluetoothClient = new BluetoothClient();

            deviceInfoDict = new Dictionary<string, object>() { { BluetoothRadio.PrimaryRadio.Name, BluetoothRadio.PrimaryRadio.LocalAddress.ToString("P")} };

            BluetoothDeviceInfo[] bluetoothDeviceInfos = bluetoothClient.DiscoverDevices();

            if (bluetoothDeviceInfos != null)
            {
                if (ExtensionFunction != null)
                {
                   OutputObject = ExtensionFunction(bluetoothDeviceInfos);
                }

                foreach (var info in bluetoothDeviceInfos)
                {
                    deviceInfoDict.Add(info.DeviceName, info.DeviceAddress.ToInt64());
                }
            }

            return deviceInfoDict;
        }
    }
}
