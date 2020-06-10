using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Account
{
    public class AccountIDNullException : ExistsException
    {
        public AccountIDNullException(string message) : base(message)
        {
        }
    }
}
