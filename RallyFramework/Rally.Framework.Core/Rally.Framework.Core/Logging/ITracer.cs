using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public interface ITracer
    {
        void Trace(object[] Data, string TraceSourceName);
    }
}
