using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IReportManager
    {
        string AddReportSetting(ReportSetting Setting);
        ReportSetting UpdateReportSetting(string ID, ReportSetting Setting);
        string RemoveReportSetting(string ID);
        ReportSetting GetReportSetting(string ID);
        IList<ReportSetting> GetReportSettings();
        string GenerateReportSettingID(int Increment);
    }
}
