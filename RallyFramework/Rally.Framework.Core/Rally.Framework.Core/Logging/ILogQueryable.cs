using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public interface ILogQueryable
    {
        IList<IDictionary<string, object>> QueryLog<T>(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, out T ExtraData, Func<object, object> QueryCriteriaFunction, Func<object, T> ExtensionFunction);

        System.Data.DataTable GetLogs();

        string DeleteLogs(string ID);
    }
}
