using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Account
{
    public class AccountExistsException : ExistsException
    {
        public AccountExistsException(string message) : base(message)
        {
        }
    }
}
