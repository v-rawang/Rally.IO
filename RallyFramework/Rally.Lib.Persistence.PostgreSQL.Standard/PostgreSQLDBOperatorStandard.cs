using System;
using System.Collections.Generic;
using System.Data;
using Rally.Lib.Persistence.Core;
using Npgsql;

namespace Rally.Lib.Persistence.PostgreSQL.Standard
{
    public class PostgreSQLDBOperatorStandard : IDMLOperable
    {
        public static IDMLOperable NewInstance(string ConnectionString)
        {
            return new PostgreSQLDBOperatorStandard(ConnectionString);
        }

        public IDbConnection Connection { get => this.conn; }

        private NpgsqlConnection conn;

        private NpgsqlTransaction trans;

        private bool isInTransaction = false;

        public PostgreSQLDBOperatorStandard(string ConnectionString)
        {
            this.conn = new NpgsqlConnection(ConnectionString);
        }

        public void Open()
        {
            if (this.conn.State != ConnectionState.Open)
            {
                this.conn.Open();
            }
        }

        public void Close()
        {
            if (this.conn.State == ConnectionState.Open && isInTransaction == false)
            {
                this.conn.Close();
            }
        }

        public void BeginTrans()
        {
            if (this.conn.State != ConnectionState.Open)
            {
                this.Open();
            }

            this.trans = this.conn.BeginTransaction();
            this.isInTransaction = true;
        }

        public void RollbackTrans()
        {
            if (this.isInTransaction)
            {
                this.trans.Rollback();
            }

            this.isInTransaction = false;
            this.Close();
        }

        public void CommitTrans()
        {
            if (this.conn.State != ConnectionState.Open)
            {
                this.Open();
            }

            if (this.isInTransaction)
            {
                this.trans.Commit();
            }

            this.isInTransaction = false;
            this.Close();
        }

        public int ExeProcedure(string ProcedureName, IDictionary<string, object> Parameters)
        {
            int result = -1;

            this.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = ProcedureName;
            cmd.Connection = this.conn;

            if (this.isInTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add(new NpgsqlParameter(paramName, Parameters[paramName]));
                }
            }

            result = cmd.ExecuteNonQuery();

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return result;
        }

        public DataSet ExeProcedureGetRecords(string ProcedureName, IDictionary<string, object> Parameters)
        {
            this.Open();

            DataSet ds = new DataSet();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(ProcedureName, this.conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    da.SelectCommand.Parameters.Add(new NpgsqlParameter(paramName, Parameters[paramName]));
                }
            }

            da.Fill(ds);
            this.Close();

            return ds;
        }

        public DataTable ExeProcedureGetRecords(string ProcedureName, IDictionary<string, object> Parameters, int TableIndex)
        {
            DataSet ds = this.ExeProcedureGetRecords(ProcedureName, Parameters);

            if (ds.Tables.Count > 0 && TableIndex >= 0)
            {
                return ds.Tables[TableIndex];
            }
            else
            {
                return (new DataTable());
            }
        }

        public IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<string, object> Parameters)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;

            this.Open();

            NpgsqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = CommandText;
            sqlCmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    sqlCmd.Parameters.Add(new NpgsqlParameter(paramName, Parameters[paramName]));
                }
            }

            using (NpgsqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    returnValue = new List<IDictionary<string, object>>();

                    string fieldName;
                    object fieldValue;

                    while (reader.Read())
                    {
                        record = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            //record.Add(reader.GetName(i), reader.GetValue(i));

                            fieldName = reader.GetName(i);
                            fieldValue = reader.GetValue(i);

                            if (fieldValue is System.DBNull)//if(reader.IsDBNull(i))
                            {
                                record.Add(fieldName, null);
                            }
                            else
                            {
                                record.Add(fieldName, fieldValue);
                            }
                        }

                        returnValue.Add(record);
                    }
                }
            }

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        public IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<string, object> Parameters, Func<object, object> ExtensionFunction)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;

            if (ExtensionFunction != null)
            {
                CommandText = (string)ExtensionFunction(new object[] { CommandText, Parameters });
            }

            this.Open();

            NpgsqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = CommandText;
            sqlCmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    sqlCmd.Parameters.Add(new NpgsqlParameter(paramName, Parameters[paramName]));
                }
            }

            using (NpgsqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    returnValue = new List<IDictionary<string, object>>();

                    string fieldName;
                    object fieldValue;

                    while (reader.Read())
                    {
                        record = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fieldName = reader.GetName(i);
                            fieldValue = reader.GetValue(i);

                            if (fieldValue is System.DBNull)
                            {
                                record.Add(fieldName, null);
                            }
                            else
                            {
                                record.Add(fieldName, fieldValue);
                            }

                        }

                        returnValue.Add(record);
                    }
                }
            }

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        public IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;
            int totalPages = -1, totalRecords = 0;
            totalPages = this.GetTotalPageCount(TableName, CurrentIndex, PageSize, out totalRecords);

            string sqlCmdText = this.getPagingSQL(TableName, KeyName, SortKeyName, ColumnNames, CurrentIndex, PageSize);

            this.Open();

            NpgsqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = sqlCmdText;
            sqlCmd.CommandType = CommandType.Text;

            using (NpgsqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    returnValue = new List<IDictionary<string, object>>();

                    string fieldName;
                    object fieldValue;

                    while (reader.Read())
                    {
                        record = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            //record.Add(reader.GetName(i), reader.GetValue(i));

                            fieldName = reader.GetName(i);
                            fieldValue = reader.GetValue(i);

                            if (fieldValue is System.DBNull)
                            {
                                record.Add(fieldName, null);
                            }
                            else
                            {
                                record.Add(fieldName, fieldValue);
                            }
                        }

                        returnValue.Add(record);
                    }
                }
            }

            TotalPageCount = totalPages;
            TotalRecordCountInDB = totalRecords;

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        public IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;
            int totalPages = -1, totalRecords = 0;
            totalPages = this.GetTotalPageCount(TableName, CurrentIndex, PageSize, out totalRecords, ExtensionFunction);

            string sqlCmdText = this.getPagingSQL(TableName, KeyName, SortKeyName, ColumnNames, CurrentIndex, PageSize);

            if (ExtensionFunction != null)
            {
                sqlCmdText = (string)ExtensionFunction(sqlCmdText);
            }

            this.Open();

            NpgsqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = sqlCmdText;
            sqlCmd.CommandType = CommandType.Text;

            using (NpgsqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
            {
                if (reader.HasRows)
                {
                    returnValue = new List<IDictionary<string, object>>();

                    while (reader.Read())
                    {
                        record = new Dictionary<string, object>();

                        string fieldName;
                        object fieldValue;

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            //record.Add(reader.GetName(i), reader.GetValue(i));

                            fieldName = reader.GetName(i);
                            fieldValue = reader.GetValue(i);

                            if (fieldValue is System.DBNull)
                            {
                                record.Add(fieldName, null);
                            }
                            else
                            {
                                record.Add(fieldName, fieldValue);
                            }
                        }

                        returnValue.Add(record);
                    }
                }
            }

            TotalPageCount = totalPages;
            TotalRecordCountInDB = totalRecords;

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        public int ExeSql(string CommandText)
        {
            int v_ResultCount = -1;

            this.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = this.conn;

            if (this.isInTransaction == true)
            {
                cmd.Transaction = this.trans;
            }
            cmd.CommandText = CommandText;

            v_ResultCount = cmd.ExecuteNonQuery();

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return v_ResultCount;
        }

        public int ExeSql(string CommandText, IDictionary<string, object> Parameters)
        {
            int v_ResultCount = -1;

            this.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = this.conn;

            if (this.isInTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            cmd.CommandText = CommandText;
            cmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add((new NpgsqlParameter(paramName, Parameters[paramName])));
                }
            }

            v_ResultCount = cmd.ExecuteNonQuery();

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return v_ResultCount;
        }

        public string ExeSqlScalar(string CommandText)
        {
            string returnValue = string.Empty;

            this.Open();

            NpgsqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = CommandText;
            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                returnValue = result.ToString();
            }

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        public string ExeSqlScalar(string CommandText, IDictionary<string, object> Parameters)
        {
            string returnValue = string.Empty;

            this.Open();

            NpgsqlCommand cmd = this.conn.CreateCommand();

            cmd.CommandText = CommandText;

            cmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add(new NpgsqlParameter(paramName, Parameters[paramName]));
                }
            }

            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                returnValue = result.ToString();
            }

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        public DataSet GetDataSet(string CommandText)
        {
            this.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = this.conn;

            if (this.isInTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            DataSet ds = new DataSet();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();

            cmd.CommandText = CommandText;
            da.SelectCommand = cmd;

            da.Fill(ds);

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return ds;
        }

        public DataSet GetDataSet(string CommandText, IDictionary<string, object> Parameters)
        {
            this.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = this.conn;

            if (this.isInTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            DataSet ds = new DataSet();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();
            cmd.CommandText = CommandText;
            da.SelectCommand = cmd;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add(new NpgsqlParameter(paramName, Parameters[paramName]));
                }
            }

            da.Fill(ds);

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return ds;
        }

        public DataSet GetDataSet(string CommandText, int CurrentIndex, int PageSize, string TableName)
        {
            this.Open();

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = this.conn;

            if (this.isInTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            DataSet ds = new DataSet();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter();
            cmd.CommandText = CommandText;
            da.SelectCommand = cmd;

            try
            {
                da.Fill(ds, CurrentIndex, PageSize, TableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return ds;
        }

        public DataTable GetDataTable(string CommandText)
        {
            DataSet ds = this.GetDataSet(CommandText);

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return (new DataTable());
            }
        }

        public DataTable GetDataTable(string CommandText, IDictionary<string, object> Parameters)
        {
            DataSet ds = this.GetDataSet(CommandText, Parameters);

            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return (new DataTable());
            }
        }

        public int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB)
        {
            int returnValue = -1, totalRecords = 0;

            string sqlCmdText = String.Format("select count(*) from {0}", TableName);

            this.Open();

            NpgsqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = sqlCmdText;
            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out totalRecords))
            {
                returnValue = this.computePageCount(PageSize, totalRecords);
            }

            TotalRecordCountInDB = totalRecords;

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        public int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction)
        {
            int returnValue = -1, totalRecords = 0;

            string sqlCmdText = String.Format("select count(*) from {0}", TableName);

            if (ExtensionFunction != null)
            {
                sqlCmdText += ExtensionFunction(null);
            }

            this.Open();

            NpgsqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = sqlCmdText;
            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out totalRecords))
            {
                returnValue = this.computePageCount(PageSize, totalRecords);
            }

            TotalRecordCountInDB = totalRecords;

            if (!this.isInTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        private int computePageCount(int eachPageSize, int totalRecordsInDatabase)
        {
            int totalPageCount = -1;

            if (eachPageSize != 0)
            {
                totalPageCount = totalRecordsInDatabase / eachPageSize;

                if ((totalRecordsInDatabase % eachPageSize) > 0)
                {
                    totalPageCount += 1;
                }
            }
            else
            {
                totalPageCount = 0;
            }

            return totalPageCount;
        }


        private string getPagingSQL(string tableName, string keyName, string sortKeyName, string[] columnNames, int pageIndex, int pageSize)
        {
            int workload = (pageIndex > 0) ? pageSize * (pageIndex - 1) : 0;

            string returnValue = "select ";

            if (columnNames == null || columnNames.Length <= 0)
            {
                returnValue += "* ";
            }
            else
            {
                for (int i = 0; i < columnNames.Length; i++)
                {
                    returnValue += columnNames[i];

                    if (i < (columnNames.Length - 1))
                    {
                        returnValue += ", ";
                    }
                }
            }

            returnValue = String.Format("{0} from {1} order by {4} asc limit {2} offset {3};", returnValue, tableName, pageSize, workload, sortKeyName);

            return returnValue;
        }
    }
}
