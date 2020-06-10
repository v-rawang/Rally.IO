using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Rally.Lib.Signal.Core.Parameter
{
    public class NetworkParameter : ParameterBase
    {
        private CommunicationType communicationType = CommunicationType.MulticastSocket;

        private string remoteAddress = "224.100.0.1";

        private int remotePort = 9050;

        private string localAddress = "127.0.0.1";

        private int localPort = 9051;

        private int timeToLive = 50;

        private int bufferSize = 1024;

        private int maxConnectionQueueLength = 800;

        private int sendTimeout = -1;

        private int receiveTimeout = -1;

        private string pipeName = "DefaultPipe";

        private bool shouldEnableSocketOptions = false;

        private bool shouldKeepAlive = true;

        private bool shouldResueAddress = true;

        private bool shouldResuePort = true;

        /// <summary>
        ///Approach of communication: Unicast via Socket, Multicast via Socket, Named Pipe
        /// </summary>
        [DefaultValue(CommunicationType.MulticastSocket)]
        public CommunicationType CommunicationType { get { return this.communicationType; } set { this.communicationType = value; } }

        /// <summary>
        /// The IP address that is bound to the remote host
        /// </summary>
        [DefaultValue("224.100.0.1")]
        public string RemoteAddress { get { return this.remoteAddress; } set { this.remoteAddress = value; } }

        /// <summary>
        /// The TCP port number that is bound to the remote host
        /// </summary>
        [DefaultValue(9050)]
        public int RemotePort { get { return this.remotePort; } set { this.remotePort = value; } }

        /// <summary>
        /// The IP address that is bound to the local host
        /// </summary>
        [DefaultValue("127.0.0.1")]
        public string LocalAddress { get { return this.localAddress; } set { this.localAddress = value; } }

        /// <summary>
        /// The TCP port number that is bound to the local host
        /// </summary>
        [DefaultValue(9051)]
        public int LocalPort { get { return this.localPort; } set { this.localPort = value; } }

        /// <summary>
        /// The value for the Time-To-Live of the transmission
        /// </summary>
        [DefaultValue(50)]
        public int TimeToLive { get { return this.timeToLive; } set { this.timeToLive = value; } }

        /// <summary>
        /// The pipe name used in the a Named Pipe communication
        /// </summary>
        [DefaultValue("DefaultPipe")]
        public string PipeName { get { return this.pipeName; } set { this.pipeName = value; } }

        [DefaultValue(1024)]
        public int BufferSize { get { return this.bufferSize; } set { this.bufferSize = value; } }

        [DefaultValue(800)]
        public int MaxConnectionQueueLength { get { return this.maxConnectionQueueLength; } set { this.maxConnectionQueueLength = value; } }

        [DefaultValue(-1)]
        public int SendTimeout { get { return this.sendTimeout; } set { this.sendTimeout = value; } }

        [DefaultValue(-1)]
        public int ReceiveTimeout { get { return this.receiveTimeout; } set { this.receiveTimeout = value; } }

        [DefaultValue(false)]
        public bool ShouldEnableSocketOptions { get => this.shouldEnableSocketOptions; set => this.shouldEnableSocketOptions = value; }

        [DefaultValue(true)]
        public bool ShouldKeepAlive { get { return this.shouldKeepAlive; } set { this.shouldKeepAlive = value; } }

        [DefaultValue(true)]
        public bool ShouldReuseAddress { get { return this.shouldResueAddress; } set { this.shouldResueAddress = value; } }

        [DefaultValue(true)]
        public bool ShouldReusePort { get { return this.shouldResuePort; } set { this.shouldResuePort = value; } }

        public override string ToString()
        {
            //return base.ToString();

            return "Network";
        }
    }

    public enum CommunicationType
    {
        UnicastSocket = 0,
        MulticastSocket = 1,
        NamedPipe = 2,
        UnicastServer = 3,
        UnicastClient = 4
    }
}
