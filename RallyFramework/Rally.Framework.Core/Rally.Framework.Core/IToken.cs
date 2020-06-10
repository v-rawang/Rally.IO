using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public interface IToken
    {
        T CreateToken<T>(string Argument);
        bool ValidateToken<T>(T Token);
        bool ExpireToken<T>(T Token);
    }
}
