using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public class AuthorizableAttribute : Attribute
    {
        public string[] Operations { get; set; }

        public string DataType { get; set; }
    }
}
