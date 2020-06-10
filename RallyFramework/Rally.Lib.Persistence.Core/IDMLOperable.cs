using System;
using System.Data;	// ����һЩͨ�����ݿ�����磺DataSet��DataTable���������ռ�
using System.Collections.Generic;

namespace Rally.Lib.Persistence.Core
{
	/// <summary>
	/// �������ݿ������ ��ժҪ˵����
	/// �˴�����Ϊ�����࣬���е����з���Ҳ��Ϊ���󷽷��Ա����า��
	/// </summary>
	public interface IDMLOperable
	{
        /// <summary>
        /// �õ���������
        /// </summary>
        IDbConnection Connection{get;}

		/// <summary>
		/// �����ݿ�����
		/// </summary>
		void Open();

		/// <summary>
		/// �ر����ݿ�����
		/// </summary>
		void Close();

		/// <summary>
		/// ��ʼһ������
		/// </summary>
		void BeginTrans();

		/// <summary>
		/// �ύһ������
		/// </summary>
		void CommitTrans();

		/// <summary>
		/// �ع�һ������
		/// </summary>
		void RollbackTrans();


        /// <summary>
        /// ִ��SQL��䣬û�з���ֵ
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        int ExeSql(string CommandText);


        /// <summary>
        /// ִ��һ��SQL���(UPDATE,INSERT) ��������
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters"> sql����еĲ�������</param>
        int ExeSql(string CommandText, IDictionary<String, object> Parameters);


        /// <summary>
        /// ִ��Insert SQL��䷵�ص�ǰID
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Argument">��ʱ����</param>
        /// <returns>��ǰID</returns>
        //int ExeSql(string CommandText, int Argument);

        /// <summary>
        /// ִ��SQL��䷵�ص�һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <returns>��һ�е�һ�е�ֵ</returns>
        string ExeSqlScalar(string CommandText);

        /// <summary>
        /// ִ��SQL��䷵�ص�һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <returns>��һ�е�һ�е�ֵ</returns>
        string ExeSqlScalar(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// ��ȡDataSet
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <returns>DataSet</returns>
        DataSet GetDataSet(string CommandText);

        /// <summary>
        /// ��ȡDataSet
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">SQL����еĲ�������</param>
        /// <returns>DataSet</returns>
        DataSet GetDataSet(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// ��ȡDataSet
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name=" CurrentIndex">��ǰҳ</param>
        /// <param name="PageSize">ҳ����</param>
        /// <returns></returns>
        DataSet GetDataSet(string CommandText, int CurrentIndex, int PageSize, string TableName);

        /// <summary>
        /// ��ȡDataTable
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <returns>DataTable</returns>
        DataTable GetDataTable(string CommandText);

        /// <summary>
        /// ��ȡDataTable
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <returns>DataTable</returns>
        DataTable GetDataTable(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="ProcedureName">�洢������</param>
        /// <param name="Parameters">�洢���̲���</param>
        int ExeProcedure(string ProcedureName, IDictionary<String, object> Parameters);


        /// <summary>
        /// ִ�д洢���̻�ȡ���ݼ�
        /// </summary>
        /// <param name="ProcedureName">�洢������</param>
        /// <param name="Parameters">�洢���̲���</param>
        /// <returns>DataSet</returns>
        DataSet ExeProcedureGetRecords(string ProcedureName, IDictionary<String, object> Parameters);

        /// <summary>
        /// ִ�д洢���̻�ȡ���ݼ�
        /// </summary>
        /// <param name="ProcedureName">�洢������</param>
        /// <param name="Parameters">�洢���̲���</param>
        /// <param name="TableIndex">DataTable �� Index</param>
        /// <returns>DataTable</returns>
        DataTable ExeProcedureGetRecords(string ProcedureName, IDictionary<String, object> Parameters, int TableIndex);

        /// <summary>
        /// ����SQL����SQL��ѯ�����������ݽ������
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <returns>���ݽ������</returns>
        IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// ����SQL����SQL��ѯ�����������ݽ������
        /// </summary>
        /// <param name="CommandText">SQL���</param>
        /// <param name="Parameters">����</param>
        /// <param name="ExtensionFunction">��չ����</param>
        /// <returns>���ݽ������</returns>
        IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<String, object> Parameters, Func<object,object> ExtensionFunction);

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
        IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB);

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
        IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction);


        /// <summary>
        /// ���������ơ�ҳ���ҳ�ߴ磬������ҳ�������ݿ��ܼ�¼��
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="CurrentIndex">��ǰҳ��</param>
        /// <param name="PageSize">ҳ�ߴ�</param>
        /// <param name="TotalRecordCountInDB">���ݿ����ܼ�¼��</param>
        /// <returns>��ҳ��</returns>
        int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB);

        /// <summary>
        /// ���������ơ�ҳ���ҳ�ߴ磬������ҳ�������ݿ��ܼ�¼��
        /// </summary>
        /// <param name="TableName">������</param>
        /// <param name="CurrentIndex">��ǰҳ��</param>
        /// <param name="PageSize">ҳ�ߴ�</param>
        /// <param name="TotalRecordCountInDB">���ݿ����ܼ�¼��</param>
        /// <param name="ExtensionFunction">��չ����</param>
        /// <returns>��ҳ��</returns>
        int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction);
    }
}
