using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Lib.Signal.Core.Parameter
{
    public class ModbusParameter: ParameterBase
    {
        private string tag;
        private byte address = 0;
        private ushort start = 0;
        private ushort registerCount = 0;

        public string Tag { get { return this.tag; } set { this.tag = value; } }
        public byte Address { get { return this.address; } set { this.address = value; } }
        public ushort Start { get { return this.start; } set { this.start = value; } }
        public ushort RegisterCount { get { return this.registerCount; } set { this.registerCount = value; } }
    }
}
