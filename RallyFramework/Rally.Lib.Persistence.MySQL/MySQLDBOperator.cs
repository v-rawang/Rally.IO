using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data;	// ��װMySQL �ķ��ʷ����������ռ�
using MySql.Data.MySqlClient;
using Rally.Lib.Persistence.Core;

namespace Rally.Lib.Persistence.MySQL
{
	/// <summary>
	/// ʵ��MySQL ���ݿ���� ��ժҪ˵����
	/// </summary>
	public class MySQLDBOperator : IDMLOperable
    {
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		private MySqlConnection conn;
		/// <summary>
		/// ��������
		/// </summary>
		private MySqlTransaction trans;
		/// <summary>
		/// ��ȡ��ǰ�Ƿ����������У�Ĭ��ֵfalse
		/// </summary>
		private bool isTransaction = false;

        public static IDMLOperable NewInstance(string ConnectionString)
        {
            return new MySQLDBOperator(ConnectionString);
        }

        public MySQLDBOperator(string ConnectionString)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			this.conn = new MySqlConnection(ConnectionString);
		}

        /// <summary>
        /// ��ȡ��ǰMySQL����
        /// </summary>
        public IDbConnection Connection
		{
			get
			{
				return this.conn;
			}
		}

		/// <summary>
		/// ��MySQL����
		/// </summary>
		public void Open()
		{
			if (this.conn.State != ConnectionState.Open)
			{
				try
				{
					this.conn.Open();
				}
                catch (Exception ex)
                {
                   throw ex;
                }
			}
		}

		/// <summary>
		/// �ر�MySQL����
		/// </summary>
		public void Close()
		{
            if (this.conn.State == ConnectionState.Open && isTransaction == false)
			{
				try
				{
					this.conn.Close();
				}
                catch (Exception ex)
                {
                    throw ex;
                }
			}
		}

		/// <summary>
		/// ��ʼһ��MySQL����
		/// </summary>
		public void BeginTrans()
		{
			if (this.conn.State != ConnectionState.Open)
				this.Open();
			this.trans = this.conn.BeginTransaction();
			isTransaction = true;
		}

		/// <summary>
		/// �ύһ��MySQL����
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
		/// �ع�һ��MySQL����
		/// </summary>
		public void RollbackTrans()
		{
			if (isTransaction)
				this.trans.Rollback();
			isTransaction = false;
			this.Close();
		}

        /// <summary>
        /// ִ��һ��SQL���(UPDATE,INSERT)
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        public int ExeSql(string CommandText)
		{
			int v_ResultCount = -1;

			// ��
			this.Open();

            MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = this.conn;
			if (isTransaction == true)
			{
				cmd.Transaction = this.trans;
			}
			cmd.CommandText = CommandText;
			try
			{
				v_ResultCount = cmd.ExecuteNonQuery();
			}
			catch(Exception ex)
			{
				throw ex;
			}

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }

			return v_ResultCount;
		}


        /// <summary>
        /// ִ��һ��SQL���(UPDATE,INSERT) ��������
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters"> sql����еĲ�������</param>
        public int ExeSql(string CommandText, IDictionary<String, object> Parameters)
        {
            int v_ResultCount = -1;

            // ��
            this.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }
            try
            {
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }

            return v_ResultCount;
        }



        /// <summary>
        /// ִ��һ��SQL���(INSERT)���ص�ǰID
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Argument">��ʱ����</param>
        /// <returns>��ǰID</returns>
        public int ExeSql(string CommandText, int Argument)
		{
			int identity = -1;
			
			// ��
			this.Open();

            MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = this.conn;

			if (isTransaction == true)
			{
				cmd.Transaction = this.trans;
			}

			cmd.CommandText = CommandText + " select @@identity as 'identity'";

			try
			{
				// ��һ�е�һ�е�ֵΪ��ǰID
				MySqlDataReader dr = cmd.ExecuteReader();
				
				if (dr.Read())
				{
					identity = int.Parse(dr[0].ToString());
				}

				dr.Close();
			}
			catch(Exception ex)
			{
				throw ex;
			}

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }

			return identity;
		}

        /// <summary>
        /// ִ��SQL��䷵�ص�һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <returns>��һ�е�һ�е�ֵ</returns>
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

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }

			return returnValue;
		}

        /// <summary>
        /// ִ��SQL��䷵�ص�һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <returns>��һ�е�һ�е�ֵ</returns>
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

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }

            return returnValue;
        }

        /// <summary>
        /// ��ȡDataSet
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string CommandText)
		{
			// ��
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

			try
			{
				da.Fill(ds);
			}
			catch(Exception ex)
			{
                throw ex;
			}

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }
			
			return ds;
		}

        /// <summary>
        /// ��ȡDataSet
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">SQL����еĲ�������</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string CommandText, IDictionary<string, object> Parameters)
        {
            //��
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

            try
            {               
                if (Parameters != null)
                {
                    foreach (string paramName in Parameters.Keys)
                    {
                        cmd.Parameters.Add(new MySqlParameter(paramName, Parameters[paramName]));
                    }
                }              

                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // �ͷ�
            if (!isTransaction){
                this.Close();
            }

            return ds;
        }
        /// <summary>
        /// ��ȡDataSet
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name=" CurrentIndex">��ǰҳ</param>
        /// <param name="PageSize">ҳ����</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string CommandText,int CurrentIndex,int PageSize,string TableName)
		{
			// ��
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

			try
			{
				da.Fill(ds,CurrentIndex,PageSize,TableName);
			}
            catch (Exception ex)
            {
                throw ex;
            }

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }

			return ds;
		}

		/// <summary>
		/// ��ȡDataTable
		/// </summary>
		/// <param name="CommandText">SQL���</param>
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
        /// ��ȡDataTable
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string CommandText, IDictionary<string, object> Parameters)
        {
            DataSet ds = this.GetDataSet(CommandText,Parameters);
            if (ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return (new DataTable());
        }

		/// <summary>
		/// ִ�д洢����
		/// </summary>
		/// <param name="ProcedureName">�洢������</param>
		/// <param name="Parameters">�洢���̲���</param>
		public int ExeProcedure(string ProcedureName, IDictionary<string, object> Parameters)
		{
			int vResult = -1;

			// ��
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
			catch(Exception ex)
			{
                vResult = -2;
                throw ex;
			}

            // �ͷ�
            if (!isTransaction)
            {
                this.Close();
            }

			return vResult;
		}

        /// <summary>
        /// ִ�д洢���̻�ȡ���ݼ�
        /// </summary>
        /// <param name="ProcedureName">�洢������</param>
        /// <param name="Parameters">�洢���̲���</param>
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
        /// ִ�д洢���̻�ȡ���ݼ�
        /// </summary>
        /// <param name="ProcedureName">�洢������</param>
        /// <param name="Parameters">�洢���̲���</param>
        /// <param name="TableIndex">DataTable �� Index</param>
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
        /// ����SQL����SQL��ѯ�����������ݽ������
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <returns>���ݽ������</returns>
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
        /// ����SQL����SQL��ѯ�����������ݽ������
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <param name="ExtensionFunction">��չ����</param>
        /// <returns>���ݽ������</returns>
        public IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<string, object> Parameters, Func<object, object> ExtensionFunction)
        {
            IList<IDictionary<string, object>> returnValue = null;
            IDictionary<string, object> record = null;

            if (ExtensionFunction != null)
            {
                CommandText = (string)ExtensionFunction(new object[] {CommandText, Parameters });
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
        /// ���������ƺ������Ƽ��ϣ�ִ��DataReader�������ո�����ҳ���ҳ�ߴ���к�̨��ҳ���������ݽ�����ϼ����ݿ��ܼ�¼��
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="KeyName">Ψһ������</param>
        /// <param name="SortKeyName">���������</param>
        /// <param name="ColumnNames">�����Ƽ���</param>
        /// <param name="CurrentIndex">��ǰҳ��</param>
        /// <param name="PageSize">ҳ�ߴ�</param>
        /// <param name="TotalPageCount">��ҳ��</param>
        /// <param name="TotalRecordCountInDB">���ݿ����ܼ�¼��</param>
        /// <returns>���ݽ������</returns>
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
        /// ���������ƺ������Ƽ��ϣ�ִ��DataReader�������ո�����ҳ���ҳ�ߴ���к�̨��ҳ���������ݽ�����ϼ����ݿ��ܼ�¼��
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="KeyName">Ψһ������</param>
        /// <param name="SortKeyName">���������</param>
        /// <param name="ColumnNames">�����Ƽ���</param>
        /// <param name="CurrentIndex">��ǰҳ��</param>
        /// <param name="PageSize">ҳ�ߴ�</param>
        /// <param name="TotalPageCount">��ҳ��</param>
        /// <param name="TotalRecordCountInDB">���ݿ����ܼ�¼��</param>
        /// <param name="ExtensionFunction">��չ����</param>
        /// <returns>���ݽ������</returns>
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
        /// ���������ơ�ҳ���ҳ�ߴ磬������ҳ�������ݿ��ܼ�¼��
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="CurrentIndex">��ǰҳ��</param>
        /// <param name="PageSize">ҳ�ߴ�</param>
        /// <param name="TotalRecordCountInDB">���ݿ����ܼ�¼��</param>
        /// <returns>��ҳ��</returns>
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
        /// ���������ơ�ҳ���ҳ�ߴ磬������ҳ�������ݿ��ܼ�¼��
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="CurrentIndex">��ǰҳ��</param>
        /// <param name="PageSize">ҳ�ߴ�</param>
        /// <param name="TotalRecordCountInDB">���ݿ����ܼ�¼��</param>
        /// <param name="ExtensionFunction">��չ����</param>
        /// <returns>��ҳ��</returns>
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
