using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.View
{
    interface IController
    {
        IView View(string ViewName);

        IView View(string ViewName, string ObjectID);

        IView View(string ViewName, object ViewModel); 
    }
}
