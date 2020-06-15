using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Nuclide
    {
        public string ID { get; set; }
        public string Name { get; set; }
        //public string Code { get; set; }
        public string SerialNumber { get; set; }
        public string Symbol { get; set; }
        //public decimal Value { get; set; }
        public int? Type { get; set; }
        public int? Category { get; set; }
        public List<EnergyChannel> EnergyChannels { get; set; }
        public double? HalfLife { get; set; }
        public string HalfLifeUnit { get; set; }
        public string Description { get; set; }
        public double Credibility { get; set; }
        public int Index { get; set; }
    }
}
