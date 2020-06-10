using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Content
{
    public class ReportManager : IReportManager
    {
        public ReportManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static IReportManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new ReportManager(DMLOperable, DBType);
        }

        public string AddReportSetting(ReportSetting Setting)
        {
            //if (Setting == null || string.IsNullOrEmpty(Setting.ID))
            //{
            //    throw new IDNullException("报告设置ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertReportSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(Setting.ID)},
                { "@Title", Setting.Title},
                { "@TemplateID", Setting.Template.ID},
                { "@Printer",Setting.Printer},
                { "@AutoPrintOnAlarm", Setting.AutoPrintOnAlarm},
                { "@AutoPrintOnMeasurement", Setting.AutoPrintOnMeasurement},
                { "@TemplateName", Setting.TemplateName}
            });

            return GenerateReportSettingID(0); //Setting.ID;
        }

        public ReportSetting UpdateReportSetting(string ID, ReportSetting Setting)
        {
            if (Setting == null || string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("报告设置ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateReportSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(ID)},
                { "@Title", Setting.Title},
                { "@TemplateID", Setting.Template.ID},
                { "@Printer",Setting.Printer},
                { "@AutoPrintOnAlarm", Setting.AutoPrintOnAlarm},
                { "@AutoPrintOnMeasurement", Setting.AutoPrintOnMeasurement},
                { "@TemplateName", Setting.TemplateName}
            });

            return Setting;
        }

        public string RemoveReportSetting(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("报告设置ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteReportSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {{ "@ID", long.Parse(ID)}});

            return ID;
        }

        public ReportSetting GetReportSetting(string ID)
        {
            ReportSetting reportSetting = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_GetReportSettingById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                reportSetting = new ReportSetting()
                {
                    ID = dbResult[0]["ID"].ToString(),
                    Title = (string)dbResult[0]["Title"],
                    Template = new Attachment() { ID = (string)dbResult[0]["@TemplateID"] },
                    AutoPrintOnAlarm = (int?)dbResult[0]["AutoPrintOnAlarm"],
                    AutoPrintOnMeasurement = (int?)dbResult[0]["AutoPrintOnMeasurement"],
                    Printer = (string)dbResult[0]["Printer"],
                    TemplateName = (string)dbResult[0]["TemplateName"]
                };
            }

            return reportSetting;
        }

        public IList<ReportSetting> GetReportSettings()
        {
            IList<ReportSetting> reportSettings = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_GetReportSettings;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, null);

            if (dbResult != null && dbResult.Count > 0)
            {
                reportSettings = new List<ReportSetting>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    reportSettings.Add(new ReportSetting()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        Title = (string)dbResult[i]["Title"],
                        Template = new Attachment() { ID = dbResult[i]["TemplateID"].ToString() },
                        AutoPrintOnAlarm = (int?)dbResult[i]["AutoPrintOnAlarm"],
                        AutoPrintOnMeasurement = (int?)dbResult[i]["AutoPrintOnMeasurement"],
                        Printer = (string)dbResult[i]["Printer"],
                        TemplateName = (string)dbResult[i]["TemplateName"]
                    });
                }
            }

            return reportSettings;
        }

        public string GenerateReportSettingID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxReportSettingID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }
    }
}
