using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class InstrumentFault
    {
        public Instrument Instrument { get; set; }
        public Fault Fault { get; set; }
        public DateTime? FaultTime { get; set; }
    }
}
