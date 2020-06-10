using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProtocol
{
    public class MessageTest
    {
        public byte Address { get; set; }
        public byte Receiver { get; set; }
        public byte[] OperationCode { get; set; }
        public byte DataLength { get; set; }
        public byte CRC { get; set; }
        public byte[] Header { get; set; }
    }
}
