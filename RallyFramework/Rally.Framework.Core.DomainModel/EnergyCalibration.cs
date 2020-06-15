using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class EnergyCalibration
    {
        public IList<EnergyChannel> EnergyChannels { get; set; }
        public float? CoefficientA { get; set; }
        public float? CoefficientB { get; set; }
        public float? CoefficientC { get; set; }
        public float? EngergyResolution { get; set; }

        public string ID { get; set; }
        public string InsturmentSerial { get; set; }
        public string NuclideBoardSerial { get; set; }
        public string OperatorID { get; set; }
        public DateTime? CalibrationTime { get; set; }
    }
}
