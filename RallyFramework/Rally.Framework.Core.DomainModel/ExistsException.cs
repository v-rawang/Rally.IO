using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core.DomainModel
{
    public class ExistsException : Exception
    {
        public ExistsException(string message) : base(message)
        {
        }
    }
}
