using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Lib.Signal.Core
{
    public enum PortStatusEnum
    {
        Opened = 0,
        Idel = 1,
        Busy = 2,
        Closed = 3,
        Error = 4
    }
}
