using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Printing
{
    public class PrinterManager : IPrinterManager
    {
        public static IPrinterManager NewInstance()
        {
            return new PrinterManager();
        }

        public string[] GetPrinters()
        {
            var printers = PrinterSettings.InstalledPrinters;

            if (printers !=  null)
            {
                IList<string> printerInfoes = new List<string>();

                foreach (var printer in printers)
                {
                    printerInfoes.Add(printer.ToString());
                }

                return printerInfoes.ToArray();
            }

            return null;
        }

        public void Print(string PrinterName, string TemplateName, IEnumerable Data, Func<object, object> ExtensionFunction)
        {
            ReportDocument reportDocument = new ReportDocument();

            string templateFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}Report{Path.DirectorySeparatorChar}{TemplateName}";

            reportDocument.Load(templateFilePath);
            reportDocument.SetDataSource(Data);

            PageMargins margins = reportDocument.PrintOptions.PageMargins;
            margins.bottomMargin = 350;
            margins.leftMargin = 350;
            margins.rightMargin = 350;
            margins.topMargin = 350;
            reportDocument.PrintOptions.ApplyPageMargins(margins);

            reportDocument.PrintOptions.PrinterName = PrinterName;

            if (ExtensionFunction != null)
            {
                ExtensionFunction(reportDocument);
            }

            reportDocument.PrintToPrinter(1, false, 0, 0);
        }

        public void Print(ReportSetting ReportSetting, PrintingArgument PrintingArgument, IEnumerable Data, Func<object, object> ExtensionFunction)
        {
            ReportDocument reportDocument = new ReportDocument();

            string templateFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}Report{Path.DirectorySeparatorChar}{ReportSetting.TemplateName}";

            reportDocument.Load(templateFilePath);
            reportDocument.SetDataSource(Data);

            PageMargins margins = reportDocument.PrintOptions.PageMargins;
            margins.bottomMargin = PrintingArgument.BottomMargin;
            margins.leftMargin = PrintingArgument.LeftMargin;
            margins.rightMargin = PrintingArgument.RightMargin;
            margins.topMargin = PrintingArgument.TopMargin;
            reportDocument.PrintOptions.ApplyPageMargins(margins);

            reportDocument.PrintOptions.PrinterName = ReportSetting.Printer;

            if (ExtensionFunction != null)
            {
                ExtensionFunction(reportDocument);
            }

            reportDocument.PrintToPrinter(PrintingArgument.NumberOfCopies, PrintingArgument.IsCollated, PrintingArgument.StartPageNumber, PrintingArgument.EndPageNumber);
        }
    }
}
