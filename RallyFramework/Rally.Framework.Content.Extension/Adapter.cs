using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Dynamic;
using Rally.Framework.Content.Extension.Report;

namespace Rally.Framework.Content.Extension
{
    public class Adapter
    {
        public DataSet AdaptToAlarmDataSet(ExpandoObject Expando)
        {
            RCVLDataSet dataSet = new RCVLDataSet();
            var row = dataSet.AlarmData.NewAlarmDataRow();
            row.Alarmid = ((dynamic)(Expando)).AlarmID;
            row.AlarmMark = ((dynamic)(Expando)).AlarmMark; 
            row.autoid = ((dynamic)(Expando)).AutoID;
            row.a_value = ((dynamic)(Expando)).AlarmValue;
            row.bg_value = ((dynamic)(Expando)).BackgourndValue;
            row.ch_id = ((dynamic)(Expando)).ChannelID;
            row.ch_name = ((dynamic)(Expando)).ChannelName;
            row.ch_type = ((dynamic)(Expando)).ChannelType;
            row.la_id = ((dynamic)(Expando)).LaneID;
            row.MeasId = ((dynamic)(Expando)).MessageID;
            row.R_DateTime = ((dynamic)(Expando)).ReportTime;
            row.R_flag = ((dynamic)(Expando)).ReportFlag;

            dataSet.AlarmData.AddAlarmDataRow(row);

            return dataSet;
        }
    }
}
