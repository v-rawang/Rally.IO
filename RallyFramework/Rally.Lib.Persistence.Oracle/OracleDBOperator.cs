using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;

namespace Rally.Lib.Persistence.Oracle
{
    public class OracleDBOperator : IDMLOperable
    {
        public IDbConnection Connection => throw new NotImplementedException();

        public void BeginTrans()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void CommitTrans()
        {
            throw new NotImplementedException();
        }

        public int ExeProcedure(string ProcedureName, IDictionary<string, object> Parameters)
        {
            throw new NotImplementedException();
        }

        public DataSet ExeProcedureGetRecords(string ProcedureName, IDictionary<string, object> Parameters)
        {
            throw new NotImplementedException();
        }

        public DataTable ExeProcedureGetRecords(string ProcedureName, IDictionary<string, object> Parameters, int TableIndex)
        {
            throw new NotImplementedException();
        }

        public IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<string, object> Parameters)
        {
            throw new NotImplementedException();
        }

        public IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<string, object> Parameters, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB)
        {
            throw new NotImplementedException();
        }

        public IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public int ExeSql(string CommandText)
        {
            throw new NotImplementedException();
        }

        public int ExeSql(string CommandText, IDictionary<string, object> Parameters)
        {
            throw new NotImplementedException();
        }

        public string ExeSqlScalar(string CommandText)
        {
            throw new NotImplementedException();
        }

        public string ExeSqlScalar(string CommandText, IDictionary<string, object> Parameters)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataSet(string CommandText)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataSet(string CommandText, IDictionary<string, object> Parameters)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataSet(string CommandText, int CurrentIndex, int PageSize, string TableName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(string CommandText)
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(string CommandText, IDictionary<string, object> Parameters)
        {
            throw new NotImplementedException();
        }

        public int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB)
        {
            throw new NotImplementedException();
        }

        public int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction)
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void RollbackTrans()
        {
            throw new NotImplementedException();
        }
    }
}
