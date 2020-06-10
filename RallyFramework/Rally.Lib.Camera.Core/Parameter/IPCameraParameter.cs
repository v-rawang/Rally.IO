using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Rally.Lib.Camera.Core.Parameter
{
    public class IPCameraParameter : CameraParameter
    {
        private string address;
        private ushort port;
        private string userName;
        private string password;
        private int deviceSpecification = 0;
        private int previewChannelID = 0;
        private int realPlayChannelID = 1;
        private int streamType = 0;
        private int netType = 0;

        public string Address { get => this.address; set => this.address = value; }
        public ushort Port { get => this.port; set => this.port = value; }
        public string UserName { get => this.userName; set => this.userName = value; }
        public string Password { get => this.password; set => this.password = value; }

        [DefaultValue(0)]
        public int DeviceSpecification { get => this.deviceSpecification; set => this.deviceSpecification = value; }

        [DefaultValue(0)]
        public int PreviewChannelID { get => this.previewChannelID; set => this.previewChannelID = value; }

        [DefaultValue(1)]
        public int RealPlayChannelID { get => this.realPlayChannelID; set => this.realPlayChannelID = value; }

        [DefaultValue(0)]
        public int StreamType { get => this.streamType; set => this.streamType = value; }

        [DefaultValue(0)]
        public int NetType { get => this.netType; set => this.netType = value; }
    }
}
