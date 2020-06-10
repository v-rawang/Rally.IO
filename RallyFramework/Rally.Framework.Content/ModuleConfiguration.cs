using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Content
{
    public class ModuleConfiguration
    {
        //public static string SQL_CMD_InsertReportSetting = "INSERT INTO report_settings (Title, TemplateID, Printer, AutoPrintOnAlarm, AutoPrintOnMeasurement, TemplateName) VALUES (@Title, @TemplateID, @Printer, @AutoPrintOnAlarm, @AutoPrintOnMeasurement, @TemplateName);";
        //public static string SQL_CMD_UpdateReportSetting = "UPDATE report_settings SET Title = @Title, TemplateID = @TemplateID, Printer = @Printer, AutoPrintOnAlarm = @AutoPrintOnAlarm, AutoPrintOnMeasurement = @AutoPrintOnMeasurement, TemplateName = @TemplateName WHERE ID = @ID;";
        //public static string SQL_CMD_GetReportSettings = "SELECT ID, Title, TemplateID, Printer, AutoPrintOnAlarm, AutoPrintOnMeasurement, TemplateName FROM report_settings;";
        //public static string SQL_CMD_GetReportSettingById = "SELECT ID, Title, TemplateID, Printer, AutoPrintOnAlarm, AutoPrintOnMeasurement, TemplateName FROM report_settings WHERE ID = @ID;";
        //public static string SQL_CMD_DeleteReportSetting = "DELETE FROM report_settings WHERE ID = @ID;";
        //public static string SQL_CMD_SelectMaxReportSettingID = "SELECT MAX(ID) FROM report_settings";

        public static string SQL_CMD_InsertReportSetting = "INSERT INTO tb_mon_ReportSettings (rep_Title, rep_TemplateID, rep_Printer, rep_AutoPrintOnAlarm, rep_AutoPrintOnMeasurement, rep_TemplateName) VALUES (@Title, @TemplateID, @Printer, @AutoPrintOnAlarm, @AutoPrintOnMeasurement, @TemplateName);";
        public static string SQL_CMD_UpdateReportSetting = "UPDATE tb_mon_ReportSettings SET rep_Title = @Title, rep_TemplateID = @TemplateID, rep_Printer = @Printer, rep_AutoPrintOnAlarm = @AutoPrintOnAlarm, rep_AutoPrintOnMeasurement = @AutoPrintOnMeasurement, rep_TemplateName = @TemplateName WHERE rep_ID = @ID;";
        public static string SQL_CMD_GetReportSettings = "SELECT rep_ID as ID, rep_Title as Title, rep_TemplateID as TemplateID, rep_Printer as Printer, rep_AutoPrintOnAlarm as AutoPrintOnAlarm, rep_AutoPrintOnMeasurement as AutoPrintOnMeasurement, rep_TemplateName as TemplateName FROM tb_mon_ReportSettings;";
        public static string SQL_CMD_GetReportSettingById = "SELECT rep_ID as ID, rep_Title as Title, rep_TemplateID as TemplateID, rep_Printer as Printer, rep_AutoPrintOnAlarm as AutoPrintOnAlarm, rep_AutoPrintOnMeasurement as AutoPrintOnMeasurement, rep_TemplateName as TemplateName FROM tb_mon_ReportSettings WHERE rep_ID = @ID;";
        public static string SQL_CMD_DeleteReportSetting = "DELETE FROM tb_mon_ReportSettings WHERE rep_ID = @ID;";
        public static string SQL_CMD_SelectMaxReportSettingID = "SELECT MAX(rep_ID) FROM tb_mon_ReportSettings";
    }
}
