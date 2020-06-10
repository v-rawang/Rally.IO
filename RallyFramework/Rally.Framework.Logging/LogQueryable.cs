using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core;
using Rally.Lib.Persistence.Core;

namespace Rally.Framework.Logging
{
    public class LogQueryable : ILogQueryable
    {
        public LogQueryable(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static ILogQueryable NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new LogQueryable(DMLOperable, DBType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TableName"></param>
        /// <param name="KeyName"></param>
        /// <param name="SortKeyName"></param>
        /// <param name="ColumnNames"></param>
        /// <param name="CurrentIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalPageCount"></param>
        /// <param name="TotalRecords"></param>
        /// <param name="ExtraData"></param>
        /// <param name="QueryCriteriaFunction"></param>
        /// <param name="ExtensionFunction"></param>
        /// <returns></returns>
        public IList<IDictionary<string, object>> QueryLog<T>(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, out T ExtraData, Func<object, object> QueryCriteriaFunction, Func<object, T> ExtensionFunction)
        {
            TotalPageCount = -1;
            TotalRecords = -1;
            ExtraData = default(T);

            IList<IDictionary<string, object>> results = null;

            if (this.dmlOperable == null)
            {
                return null;
            }

            results = this.dmlOperable.ExeReaderWithPaging(TableName, KeyName, SortKeyName, ColumnNames, CurrentIndex, PageSize, out TotalPageCount, out TotalRecords, QueryCriteriaFunction);

            if (ExtensionFunction != null)
            {
                ExtraData = ExtensionFunction(results);
            }

            return results;
        }

        public System.Data.DataTable GetLogs()
        {
            return this.dmlOperable.GetDataTable(ModuleConfiguration.SQL_CMD_Logs);
        }

        public string DeleteLogs(string SettingID)
        {
            //if (string.IsNullOrEmpty(SettingID))
            //{
            //    throw new IDNullException("ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteLogs;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(SettingID) } });

            return SettingID;
        }
    }
}
