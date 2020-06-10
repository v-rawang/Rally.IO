using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.View
{
    public class ControllerBase :IController
    {
        public IView View(string ViewName)
        {
            throw new NotImplementedException();
        }

        public IView View(string ViewName, string ObjectID)
        {
            throw new NotImplementedException();
        }

        public IView View(string ViewName, object ViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
