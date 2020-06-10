using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IPrinterManager
    {
        string[] GetPrinters();

        void Print(ReportSetting ReportSetting, PrintingArgument PrintingArgument, System.Collections.IEnumerable Data, Func<object, object> ExtensionFunction);

        void Print(string PrinterName, string TemplateName, System.Collections.IEnumerable Data, Func<object, object> ExtensionFunction);
    }
}
