using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data;	
using MySql.Data.MySqlClient;
using Rally.Lib.Persistence.Core;

namespace Rally.Lib.Persistence.MySQL.Standard
{
    public class MySQLDBOperatorStandard : IDMLOperable
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private MySqlConnection conn;
        /// <summary>
        /// 事务处理类
        /// </summary>
        private MySqlTransaction trans;
        /// <summary>
        /// 获取当前是否处于事务处理中，默认值false
        /// </summary>
        private bool isTransaction = false;

        public static IDMLOperable NewInstance(string ConnectionString)
        {
            return new MySQLDBOperatorStandard(ConnectionString);
        }

        public MySQLDBOperatorStandard(string ConnectionString)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.conn = new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// 获取当前MySQL连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return this.conn;
            }
        }

        /// <summary>
        /// 打开MySQL连接
        /// </summary>
        public void Open()
        {
            if (this.conn.State != ConnectionState.Open)
            {
                this.conn.Open();
            }
        }

        /// <summary>
        /// 关闭MySQL连接
        /// </summary>
        public void Close()
        {
            if (this.conn.State == ConnectionState.Open && isTransaction == false)
            {
                this.conn.Close();
            }
        }

        /// <summary>
        /// 开始一个MySQL事务
        /// </summary>
        public void BeginTrans()
        {
            if (this.conn.State != ConnectionState.Open)
                this.Open();
            this.trans = this.conn.BeginTransaction();
            isTransaction = true;
        }

        /// <summary>
        /// 提交一个MySQL事务
        /// </summary>
        public void CommitTrans()
        {
            if (this.conn.State != ConnectionState.Open)
                this.Open();
            if (isTransaction)
                this.trans.Commit();
            isTransaction = false;
            this.Close();
        }

        /// <summary>
        /// 回滚一个MySQL事务
        /// </summary>
        public void RollbackTrans()
        {
            if (isTransaction)
                this.trans.Rollback();
            isTransaction = false;
            this.Close();
        }

        /// <summary>
        /// 执行一个SQL语句(UPDATE,INSERT)
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        public int ExeSql(string CommandText)
        {
            int v_ResultCount = -1;

            // 打开
            this.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;
            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }
            cmd.CommandText = CommandText;

            v_ResultCount = cmd.ExecuteNonQuery();

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return v_ResultCount;
        }


        /// <summary>
        /// 执行一个SQL语句(UPDATE,INSERT) 带参数的
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters"> sql语句中的参数数组</param>
        public int ExeSql(string CommandText, IDictionary<String, object> Parameters)
        {
            int v_ResultCount = -1;

            // 打开
            this.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            cmd.CommandText = CommandText;
            cmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add((new MySqlParameter(paramName, Parameters[paramName])));
                }
            }

            v_ResultCount = cmd.ExecuteNonQuery();

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return v_ResultCount;
        }



        /// <summary>
        /// 执行一个SQL语句(INSERT)返回当前ID
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Argument">临时变量</param>
        /// <returns>当前ID</returns>
        public int ExeSql(string CommandText, int Argument)
        {
            int identity = -1;

            // 打开
            this.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            cmd.CommandText = CommandText + " select @@identity as 'identity'";

            // 第一行第一列的值为当前ID
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                identity = int.Parse(dr[0].ToString());
            }

            dr.Close();

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return identity;
        }

        /// <summary>
        /// 执行SQL语句返回第一行第一列的值
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <returns>第一行第一列的值</returns>
        public string ExeSqlScalar(string CommandText)
        {
            string returnValue = string.Empty;

            this.Open();

            MySqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = CommandText;
            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                returnValue = result.ToString();
            }

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// 执行SQL语句返回第一行第一列的值
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <returns>第一行第一列的值</returns>
        public string ExeSqlScalar(string CommandText, IDictionary<string, object> Parameters)
        {
            string returnValue = string.Empty;

            this.Open();

            MySqlCommand cmd = this.conn.CreateCommand();

            cmd.CommandText = CommandText;

            cmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add(new MySqlParameter(paramName, Parameters[paramName]));
                }
            }

            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                returnValue = result.ToString();
            }

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string CommandText)
        {
            // 打开
            this.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter();

            cmd.CommandText = CommandText;
            da.SelectCommand = cmd;

            da.Fill(ds);

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return ds;
        }

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">SQL语句中的参数数组</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string CommandText, IDictionary<string, object> Parameters)
        {
            //打开
            this.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter();
            cmd.CommandText = CommandText;
            da.SelectCommand = cmd;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add(new MySqlParameter(paramName, Parameters[paramName]));
                }
            }

            da.Fill(ds);

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return ds;
        }
        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name=" CurrentIndex">当前页</param>
        /// <param name="PageSize">页长度</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string CommandText, int CurrentIndex, int PageSize, string TableName)
        {
            // 打开
            this.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter();
            cmd.CommandText = CommandText;
            da.SelectCommand = cmd;

            da.Fill(ds, CurrentIndex, PageSize, TableName);

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return ds;
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string CommandText)
        {
            DataSet ds = this.GetDataSet(CommandText);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return (new DataTable());
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string CommandText, IDictionary<string, object> Parameters)
        {
            DataSet ds = this.GetDataSet(CommandText, Parameters);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return (new DataTable());
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="Parameters">存储过程参数</param>
        public int ExeProcedure(string ProcedureName, IDictionary<string, object> Parameters)
        {
            int vResult = -1;

            // 打开
            this.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = ProcedureName;
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    cmd.Parameters.Add(new MySqlParameter(paramName, Parameters[paramName]));
                }
            }

            try
            {
                vResult = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                vResult = -2;
                throw ex;
            }

            // 释放
            if (!isTransaction)
            {
                this.Close();
            }

            return vResult;
        }

        /// <summary>
        /// 执行存储过程获取数据集
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="Parameters">存储过程参数</param>
        /// <returns>DataSet</returns>
        public DataSet ExeProcedureGetRecords(string ProcedureName, IDictionary<string, object> Parameters)
        {
            this.Open();

            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(ProcedureName, this.conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    da.SelectCommand.Parameters.Add(new MySqlParameter(paramName, Parameters[paramName]));
                }
            }

            da.Fill(ds);
            this.Close();

            return ds;
        }

        /// <summary>
        /// 执行存储过程获取数据集
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="Parameters">存储过程参数</param>
        /// <param name="TableIndex">DataTable 的 Index</param>
        /// <returns>DataTable</returns>
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

        /// <summary>
        /// 给定SQL语句和SQL查询参数返回数据结果集合
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <returns>数据结果集合</returns>
        public IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<string, object> Parameters)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;

            this.Open();

            MySqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = CommandText;
            sqlCmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    sqlCmd.Parameters.Add(new MySqlParameter(paramName, Parameters[paramName]));
                }
            }

            using (MySqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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

            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// 给定SQL语句和SQL查询参数返回数据结果集合
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <param name="ExtensionFunction">扩展函数</param>
        /// <returns>数据结果集合</returns>
        public IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<string, object> Parameters, Func<object, object> ExtensionFunction)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;

            if (ExtensionFunction != null)
            {
                CommandText = (string)ExtensionFunction(new object[] { CommandText, Parameters });
            }

            this.Open();

            MySqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = CommandText;
            sqlCmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    sqlCmd.Parameters.Add(new MySqlParameter(paramName, Parameters[paramName]));
                }
            }

            using (MySqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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

            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// 给定表名称和列名称集合，执行DataReader，并按照给定的页码和页尺寸进行后台分页，返回数据结果集合及数据库总记录数
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="KeyName">唯一键名称</param>
        /// <param name="SortKeyName">排序键名称</param>
        /// <param name="ColumnNames">列名称集合</param>
        /// <param name="CurrentIndex">当前页码</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="TotalPageCount">总页数</param>
        /// <param name="TotalRecordCountInDB">数据库中总记录数</param>
        /// <returns>数据结果集合</returns>
        public IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;
            int totalPages = -1, totalRecords = 0;
            totalPages = this.GetTotalPageCount(TableName, CurrentIndex, PageSize, out totalRecords);

            string sqlCmdText = this.getPagingSQL(TableName, KeyName, SortKeyName, ColumnNames, CurrentIndex, PageSize);

            this.Open();

            MySqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = sqlCmdText;
            sqlCmd.CommandType = CommandType.Text;

            using (MySqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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

            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// 给定表名称和列名称集合，执行DataReader，并按照给定的页码和页尺寸进行后台分页，返回数据结果集合及数据库总记录数
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="KeyName">唯一键名称</param>
        /// <param name="SortKeyName">排序键名称</param>
        /// <param name="ColumnNames">列名称集合</param>
        /// <param name="CurrentIndex">当前页码</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="TotalPageCount">总页数</param>
        /// <param name="TotalRecordCountInDB">数据库中总记录数</param>
        /// <param name="ExtensionFunction">扩展函数</param>
        /// <returns>数据结果集合</returns>
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

            MySqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = sqlCmdText;
            sqlCmd.CommandType = CommandType.Text;

            using (MySqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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

            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// 给定表名称、页码和页尺寸，返回总页数及数据库总记录数
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="CurrentIndex">当前页码</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="TotalRecordCountInDB">数据库中总记录数</param>
        /// <returns>总页数</returns>
        public int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB)
        {
            int returnValue = -1, totalRecords = 0;

            string sqlCmdText = String.Format("select count(*) from {0}", TableName);

            this.Open();

            MySqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = sqlCmdText;
            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out totalRecords))
            {
                returnValue = this.computePageCount(PageSize, totalRecords);
            }

            TotalRecordCountInDB = totalRecords;

            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// 给定表名称、页码和页尺寸，返回总页数及数据库总记录数
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="CurrentIndex">当前页码</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="TotalRecordCountInDB">数据库中总记录数</param>
        /// <param name="ExtensionFunction">扩展函数</param>
        /// <returns>总页数</returns>
        public int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction)
        {
            int returnValue = -1, totalRecords = 0;

            string sqlCmdText = String.Format("select count(*) from {0}", TableName);

            if (ExtensionFunction != null)
            {
                sqlCmdText += ExtensionFunction(null);
            }

            this.Open();

            MySqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = sqlCmdText;
            cmd.CommandType = CommandType.Text;

            object result = cmd.ExecuteScalar();

            if (result != null && int.TryParse(result.ToString(), out totalRecords))
            {
                returnValue = this.computePageCount(PageSize, totalRecords);
            }

            TotalRecordCountInDB = totalRecords;

            if (!isTransaction)
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
