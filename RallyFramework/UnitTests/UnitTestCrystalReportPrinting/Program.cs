using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Rally.Nuclide.Facade;
using Rally.Nuclide.Core;
using Rally.Nuclide.Core.DomainModel;

namespace UnitTestCrystalReportPrinting
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] values = new int[1024];

            //int[] values2 = new int[1024];


            //for (int i = 0; i < 1024; i++)
            //{
            //    values[i] = i;
            //    values2[i] = i * 2;
            //}

            //Parallel.For(0, 1024, i => {
            //    values[i] += values2[i];
            //    //Console.WriteLine(values[i]);
            //});

            //for (int i = 0; i < 1024; i++)
            //{
            //    Console.WriteLine(values[i]);
            //}

            Console.WriteLine(143 + 50 - 143 % 50);

            Console.Read();
        }

        static void testPrinting()
        {
            foreach (var printer in PrinterSettings.InstalledPrinters)
            {
                Console.WriteLine(printer);
            }

            //IMeasurementManager measurementManager = Facade.CreateMeasurementManager();

            Global.CurrentDBConnectionString = "server=localhost;port=3306;user id=root;password=W@lcome1;persistsecurityinfo=True;database=db_monitor_system_nuclide;allowuservariables=True";
            Global.CurrentDBType = "MySQL";

            //IRecognitionManager recognitionManager = Facade.CreateRecognitionManager();

            //IList<Nuclide> nuclides = recognitionManager.GetNuclides();

            ReportDocument reportDocument = new ReportDocument();

            //reportDocument.Load("CrystalReport2.rpt");

            //reportDocument.SetDataSource(nuclides);

            reportDocument.Load("RecordCN.rpt");

            RCVLDataSet rCVLDataSet = new RCVLDataSet();
            var row = rCVLDataSet.AlarmData.NewAlarmDataRow();
            row.Alarmid = 0;
            row.AlarmMark = "ALM";
            row.autoid = 1;
            row.a_value = 10;
            row.bg_value = 12;
            row.ch_id = "1";
            row.ch_name = "ch01";
            row.ch_type = "type01";
            row.la_id = 1;
            row.MeasId = 1;
            row.R_DateTime = DateTime.Now;
            row.R_flag = "1";

            rCVLDataSet.AlarmData.AddAlarmDataRow(row);

            reportDocument.SetDataSource(rCVLDataSet);

            PageMargins margins;
            margins = reportDocument.PrintOptions.PageMargins;
            margins.bottomMargin = 350;
            margins.leftMargin = 350;
            margins.rightMargin = 350;
            margins.topMargin = 350;
            reportDocument.PrintOptions.ApplyPageMargins(margins);
            reportDocument.PrintOptions.PrinterName = PrinterSettings.InstalledPrinters[3];

            reportDocument.PrintToPrinter(1, false, 0, 0);
        }
    }
}
