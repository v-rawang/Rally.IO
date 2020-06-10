using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Rally.Lib.Signal.Core;
using Rally.Lib.Signal.Core.Parameter;

namespace Rally.Lib.Signal.Bluetooth
{
    public class BluetoothSocket : ISocket
    {
        private BluetoothClient bluetoothClient;
        private BluetoothListener bluetoothListener;
        private BluetoothEndPoint remoteBluetoothEndPoint, localBluetoothEndPoint;
        private BluetoothParameter parameters;

        public IDictionary<string, object> Options { get; set; }
        public Func<object, object> ExtensionFunction { get; set ; }

        public static ISocket NewInstance()
        {
            return new BluetoothSocket();
        }

        public void Accept()
        {
            if (this.parameters.BluetoothMode == BluetoothModeEnum.Server && this.bluetoothListener != null)
            {
                this.bluetoothClient = this.bluetoothListener.AcceptBluetoothClient();
            }         
        }

        public void Close()
        {
            if (this.parameters.BluetoothMode == BluetoothModeEnum.Server && this.bluetoothListener != null)
            {
                this.bluetoothListener.Stop();
            }

            this.bluetoothClient.Close();
        }

        public void Connect()
        {
            if (this.parameters.BluetoothMode == BluetoothModeEnum.Client && this.bluetoothClient != null)
            {
                this.bluetoothClient.Connect(this.remoteBluetoothEndPoint);
            }       
        }

        public void Initialize(ParameterBase Parameters)
        {
            this.parameters = Parameters as BluetoothParameter;

            if (!String.IsNullOrEmpty(this.parameters.RemoteAddress))
            {
                //this.remoteBluetoothEndPoint = new BluetoothEndPoint(new BluetoothAddress(long.Parse(this.parameters.RemoteAddress)), Guid.Parse(this.parameters.RemoteServiceIdentifier), this.parameters.RemotePort);
                this.remoteBluetoothEndPoint = new BluetoothEndPoint(BluetoothAddress.Parse(this.parameters.RemoteAddress), Guid.Parse(this.parameters.RemoteServiceIdentifier), this.parameters.RemotePort);
                //this.remoteBluetoothEndPoint = new BluetoothEndPoint(BluetoothAddress.Parse(this.parameters.RemoteAddress), BluetoothService.Handsfree, this.parameters.RemotePort);
            }

            if (!String.IsNullOrEmpty(this.parameters.LocalAddress))
            {
                //this.localBluetoothEndPoint = new BluetoothEndPoint(new BluetoothAddress(long.Parse(this.parameters.LocalAddress)), Guid.Parse(this.parameters.LocalServiceIdentifier), this.parameters.LocalPort);
                this.localBluetoothEndPoint = new BluetoothEndPoint(BluetoothAddress.Parse(this.parameters.LocalAddress), Guid.Parse(this.parameters.LocalServiceIdentifier), this.parameters.LocalPort);
                //this.localBluetoothEndPoint = new BluetoothEndPoint(BluetoothAddress.Parse(this.parameters.LocalAddress), BluetoothService.Empty, this.parameters.LocalPort);
            }

            if (this.parameters.BluetoothMode == BluetoothModeEnum.Client)
            {
                this.bluetoothClient = this.localBluetoothEndPoint != null ? new BluetoothClient(this.localBluetoothEndPoint) : new BluetoothClient();

                if (!string.IsNullOrEmpty(this.parameters.DevicePIN))
                {
                    this.bluetoothClient.SetPin(this.parameters.DevicePIN);
                }
            }
            else if (this.parameters.BluetoothMode == BluetoothModeEnum.Server)
            {
                this.bluetoothListener = new BluetoothListener(this.localBluetoothEndPoint);

                if (!String.IsNullOrEmpty(this.parameters.ServiceName))
                {
                    this.bluetoothListener.ServiceName = this.parameters.ServiceName;
                }
            }    
        }

        public void Listen()
        {
            if (this.parameters.BluetoothMode == BluetoothModeEnum.Server && this.bluetoothListener != null)
            {
                this.bluetoothListener.Start(this.parameters.MaxConnectionQueueLength);
            }       
        }

        public byte[] Receive(out int BytesReceived)
        {
            byte[] returnValue = null;

            using (NetworkStream stream = this.bluetoothClient.GetStream())
            {
                BytesReceived = (int)stream.Length;

                stream.Read(returnValue, 0, BytesReceived);
            }

            return returnValue;
        }

        public int Send(byte[] Data)
        {
            int totalBytes = Data.Length;

            if (this.ExtensionFunction != null)
            {
                this.ExtensionFunction(Data);
            }

            using (NetworkStream stream = this.bluetoothClient.GetStream())
            {
                stream.Write(Data, 0, totalBytes);
            }

            return totalBytes;
        }
    }
}
