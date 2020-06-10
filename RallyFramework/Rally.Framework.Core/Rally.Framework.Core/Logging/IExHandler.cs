using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public interface IExHandler
    {
        void HandleException(Exception Ex);

        void HandleException(Exception Ex, string Policy);
    }
}
