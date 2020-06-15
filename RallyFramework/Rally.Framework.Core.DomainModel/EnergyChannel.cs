using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class EnergyChannel
    {
        public int Index { get; set; }
        public float? Energy { get; set; }
        public float? Channel { get; set; }
        public float? BranchingRatio { get; set; }
    }
}
