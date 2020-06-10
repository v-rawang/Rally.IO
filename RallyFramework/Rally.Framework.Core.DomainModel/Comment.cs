using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class Comment :Content
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public int Score { get; set; }
    }
}
