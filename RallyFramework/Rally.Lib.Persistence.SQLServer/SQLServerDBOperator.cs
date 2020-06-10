using System;
using System.Data;
using System.Data.SqlClient;	// ��װSQL Sevrer �ķ��ʷ����������ռ�
using System.Collections.Generic;
using Rally.Lib.Persistence.Core;

namespace Rally.Lib.Persistence.SQLServer
{
	/// <summary>
	/// ʵ��SQL Server���ݿ���� ��ժҪ˵����
	/// </summary>
	public class SQLServerDBOperator : IDMLOperable
	{
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		private SqlConnection conn;
		/// <summary>
		/// ��������
		/// </summary>
		private SqlTransaction trans;
		/// <summary>
		/// ��ȡ��ǰ�Ƿ����������У�Ĭ��ֵfalse
		/// </summary>
		private bool isTransaction = false;

        public static IDMLOperable NewInstance(string ConnectionString)
        {
            return new SQLServerDBOperator(ConnectionString);
        }

        public SQLServerDBOperator(string strConnection)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			this.conn = new SqlConnection(strConnection);
		}

		/// <summary>
		/// ��ȡ��ǰSQL Server����
		/// </summary>
		public IDbConnection Connection
		{
			get
			{
				return this.conn;
			}
		}

		/// <summary>
		/// ��SQL Server����
		/// </summary>
		public void Open()
		{
			if (conn.State != ConnectionState.Open)
			{
				try
				{
					conn.Open();
				}
                catch (Exception ex)
                {
                    throw ex;
                }
			}
		}

		/// <summary>
		/// �ر�SQL Server����
		/// </summary>
		public void Close()
		{
            if (conn.State == ConnectionState.Open && isTransaction == false)
			{
				try
				{
					conn.Close();
				}
                catch (Exception ex)
                {
                    throw ex;
                }
			}
		}

		/// <summary>
		/// ��ʼһ��SQL Server����
		/// </summary>
		public void BeginTrans()
		{
			if (conn.State != ConnectionState.Open)
				this.Open();
			trans = conn.BeginTransaction();
			isTransaction = true;
		}

		/// <summary>
		/// �ύһ��SQL Server����
		/// </summary>
		public void CommitTrans()
		{
			if (conn.State != ConnectionState.Open)
				this.Open();
			if (isTransaction)
				trans.Commit();
			isTransaction = false;
			this.Close();
		}

		/// <summary>
		/// �ع�һ��SQL Server����
		/// </summary>
		public void RollbackTrans()
		{
			if (isTransaction)
				trans.Rollback();
			isTransaction = false;
			this.Close();
		}

        /// <summary>
        /// ִ��SQL��䣬û�з���ֵ
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        public int ExeSql(string CommandText)
        {
			int v_ResultCount = -1;

			// ��
			this.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this.conn;
            cmd.CommandText = CommandText;

            if (isTransaction == true)
			{
				cmd.Transaction = this.trans;
			}		

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
        public int ExeSql(string CommandText, IDictionary<string, object> Parameters)
        {
            int v_ResultCount = -1;

            // ��
            this.Open();

            SqlCommand cmd = new SqlCommand();
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
                        if (Parameters[paramName] != null)
                        {
                            cmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter(paramName, System.DBNull.Value));
                        }             
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
        /// ִ��Insert SQL��䷵�ص�ǰID
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Argument">��ʱ����</param>
        /// <returns>��ǰID</returns>
        public int ExeSql(string CommandText, int Argument)
        {
			int identity = -1;
			
			// ��
			this.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this.conn;

			if (isTransaction == true)
			{
				cmd.Transaction = this.trans;
			}

			cmd.CommandText = CommandText + " select @@identity as 'identity'";

			try
			{
				// ��һ�е�һ�е�ֵΪ��ǰID
				SqlDataReader dr = cmd.ExecuteReader();
				
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
            //DataTable dt = null;
            //try
            //{
            //	dt = this.GetDataTable(CommandText);
            //	if (dt.Rows.Count > 0)
            //	{
            //		string v_Value = dt.Rows[0][0].ToString();
            //		dt.Dispose();
            //		vResult = v_Value;
            //	}
            //}
            //catch(Exception ex)
            //{
            //	throw ex;
            //}

            SqlCommand cmd = this.conn.CreateCommand();
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

            //DataTable dt = null;
            //try
            //{
            //    dt = this.GetDataTable(CommandText,Parameters);
            //    if (dt.Rows.Count > 0)
            //    {
            //        string v_Value = dt.Rows[0][0].ToString();
            //        dt.Dispose();
            //        vResult = v_Value;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            SqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = CommandText;
            cmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    //cmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));

                    if (Parameters[paramName] != null)
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName, System.DBNull.Value));
                    }
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

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this.conn;

			if (isTransaction == true)
			{
				cmd.Transaction = this.trans;
			}

			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter();
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
       public  DataSet GetDataSet(string CommandText, IDictionary<string, object> Parameters)
        {
            //��
            this.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.conn;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = CommandText;
            da.SelectCommand = cmd;

            try
            {               
                if (Parameters != null)
                {
                    foreach (string paramName in Parameters.Keys)
                    {
                        //cmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));

                        if (Parameters[paramName] != null)
                        {
                            cmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter(paramName, System.DBNull.Value));
                        }
                    }
                }              

                da.Fill(ds);
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
        /// ��ȡDataSet
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name=" CurrentIndex">��ǰҳ</param>
        /// <param name="PageSize">ҳ����</param>
        /// <returns></returns>
        public DataSet GetDataSet(string CommandText, int CurrentIndex, int PageSize, string TableName)
        {
			// ��
			this.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = this.conn;

			if (isTransaction == true)
			{
				cmd.Transaction = this.trans;
			}

			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter();
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
            DataSet ds = this.GetDataSet(CommandText, Parameters);
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

			SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.conn;
            cmd.CommandText = ProcedureName;	
            cmd.CommandType = CommandType.StoredProcedure;

            if (isTransaction == true)
            {
                cmd.Transaction = this.trans;
            }

			if (Parameters != null)
			{
                foreach (string paramName in Parameters.Keys)
                {
                    //cmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));

                    if (Parameters[paramName] != null)
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName, System.DBNull.Value));
                    }
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

            SqlDataAdapter da = new SqlDataAdapter(ProcedureName, this.conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    //da.SelectCommand.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));

                    if (Parameters[paramName] != null)
                    {
                        da.SelectCommand.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));
                    }
                    else
                    {
                        da.SelectCommand.Parameters.Add(new SqlParameter(paramName, System.DBNull.Value));
                    }
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

            SqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = CommandText;
            sqlCmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    //sqlCmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));

                    if (Parameters[paramName] != null)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));
                    }
                    else
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(paramName, System.DBNull.Value));
                    }
                }
            }

            using (SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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
                CommandText = (string)ExtensionFunction(new object[] { CommandText, Parameters });
            }

            this.Open();

            SqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = CommandText;
            sqlCmd.CommandType = CommandType.Text;

            if (Parameters != null)
            {
                foreach (string paramName in Parameters.Keys)
                {
                    //sqlCmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));

                    if (Parameters[paramName] != null)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(paramName, Parameters[paramName]));
                    }
                    else
                    {
                        sqlCmd.Parameters.Add(new SqlParameter(paramName, System.DBNull.Value));
                    }
                }
            }

            using (SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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

            SqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = sqlCmdText;
            sqlCmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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

            SqlCommand sqlCmd = this.conn.CreateCommand();
            sqlCmd.CommandText = sqlCmdText;
            sqlCmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.Default))
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

            SqlCommand cmd = this.conn.CreateCommand();
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

            this.Open();

            SqlCommand cmd = this.conn.CreateCommand();
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

                    if (i < (columnNames.Length - 1)){
                        returnValue += ", ";
                    }
                }
            }

            returnValue = String.Format("{0} from {1} where {2} in (select top {3} {4} from {5} where {6} not in (select top {7} {8} from {9} order by {10} desc) order by {11} desc) order by {12} asc;", returnValue, tableName, keyName, pageSize, keyName, tableName, keyName, workload, keyName, tableName, sortKeyName, sortKeyName, sortKeyName);

            return returnValue;
        }
    }
}
