using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.View
{
    public interface IView
    {
        string Title { get; set; }
        object Banner { get; set; }
        void SetModel(object ViewModel);
        void SetLayout(object ViewLayout);
    }
}
