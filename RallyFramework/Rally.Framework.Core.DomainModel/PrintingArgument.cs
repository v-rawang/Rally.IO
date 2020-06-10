using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class PrintingArgument
    {
        public int TopMargin { get; set; }
        public int BottomMargin { get; set; }
        public int LeftMargin { get; set; }
        public int RightMargin { get; set;}
        public int NumberOfCopies { get; set; }
        public int StartPageNumber { get; set; }
        public int EndPageNumber { get; set; }
        public bool IsCollated { get; set; }
    }
}
