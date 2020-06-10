using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Rally.Lib.Signal.Core;
using Rally.Lib.Signal.Core.Parameter;

namespace Rally.Lib.Signal.Tcp
{
    public class TcpSocket : ISocket
    {
        private Socket socket;

        private IPEndPoint remoteIPEndPoint;

        private IPEndPoint localIPEndPoint;

        private Socket client;

        private NetworkParameter parameters;

        public static ISocket NewInstance()
        {
            return new TcpSocket();
        }

        public IDictionary<string, object> Options { get; set; }
        public Func<object, object> ExtensionFunction { get; set; }

        public void Initialize(ParameterBase Parameters)
        {
            this.parameters = Parameters as NetworkParameter;

            string localAddress = this.parameters.LocalAddress, remoteAddress = this.parameters.RemoteAddress;
            int localPort = this.parameters.LocalPort, remotePort = this.parameters.RemotePort, sendTimeout = this.parameters.SendTimeout, receiveTimeout = this.parameters.ReceiveTimeout;
            short timeToLive = (short)parameters.TimeToLive;
            bool shouldEnableSocketOptions = this.parameters.ShouldEnableSocketOptions;
            bool shouldKeepAlive = this.parameters.ShouldKeepAlive;
            bool shouldReuseAddress = this.parameters.ShouldReuseAddress;
            bool shouldReusePort = this.parameters.ShouldReusePort;

            IPAddress localIP = String.IsNullOrEmpty(localAddress) ? IPAddress.Any : IPAddress.Parse(localAddress);

            IPAddress remoteIP = String.IsNullOrEmpty(remoteAddress) ? IPAddress.None : IPAddress.Parse(remoteAddress);

            this.localIPEndPoint = new IPEndPoint(localIP, localPort);

            this.remoteIPEndPoint = new IPEndPoint(remoteIP, remotePort);

            this.socket = new Socket(this.localIPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            this.socket.SendTimeout = sendTimeout;
            this.socket.ReceiveTimeout = receiveTimeout;

            this.socket.Ttl = timeToLive;

            if (shouldEnableSocketOptions)
            {
                this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, shouldKeepAlive);
                this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, shouldReuseAddress);
                this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseUnicastPort, shouldReusePort);
            }

            this.socket.Bind(this.localIPEndPoint);
        }


        public void Accept()
        {
            this.client = this.socket.Accept();
        }

        public void Close()
        {
            if (this.client != null)
            {
                this.client.Close();
            }

            this.socket.Close();
        }

        public void Connect()
        {
            this.socket.Connect(remoteIPEndPoint);

            if (this.ExtensionFunction != null)
            {
                this.ExtensionFunction(this.socket);
            }
        } 

        public void Listen()
        {
            int maxQueueLength = this.parameters.MaxConnectionQueueLength > 0 ? this.parameters.MaxConnectionQueueLength : 1000;
            this.socket.Listen(maxQueueLength);
        }

        public byte[] Receive(out int BytesReceived)
        {
            int bufferSize = this.parameters.BufferSize > 0 ? this.parameters.BufferSize : 1024;

            byte[] data = new byte[bufferSize];

            BytesReceived = -1; //this.client != null ? this.client.Receive(data) : this.socket.Receive(data);

            if (this.client != null && this.client.Connected)
            {
                BytesReceived = this.client.Receive(data);
            }
            else if(this.socket.Connected)
            {
                BytesReceived = this.socket.Receive(data);
            }

            return data;
        }

        public int Send(byte[] Data)
        {
            if (this.ExtensionFunction != null)
            {
                this.ExtensionFunction(Data);
            }

            int bytesSent = -1;

            if (this.client != null && this.client.Connected)
            {
                bytesSent = this.client.Send(Data);
            }
            else if(this.socket.Connected)
            {
                bytesSent = this.socket.SendTo(Data, SocketFlags.None, this.remoteIPEndPoint);
            }

            return bytesSent; //bthis.client != null ? this.client.Send(Data) : this.socket.SendTo(Data, SocketFlags.None, this.remoteIPEndPoint);
        }
    }
}
