using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Lib.Signal.Core
{
    public interface IDiscoverer
    {
        IDictionary<string, object> Discover();

        IDictionary<string, object> Discover<T>(Func<object, T> ExtensionFunction, out T OutputObject);
    }
}
