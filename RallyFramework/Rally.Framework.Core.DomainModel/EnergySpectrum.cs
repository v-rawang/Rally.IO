using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class EnergySpectrum : List<double[]>
    {
        public int ChannelCount { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float CoefficientA { get; set; }
        public float CoefficientB { get; set; }
        public float CoefficientC { get; set; }
        public string Operator { get; set; }
        public string Instrument { get; set; }
        public string Detector { get; set; }

        //public int BackgroundDataStartIndex { get; set; }
        //public int BackgroundDataEndIndex { get; set; }
    }
}
