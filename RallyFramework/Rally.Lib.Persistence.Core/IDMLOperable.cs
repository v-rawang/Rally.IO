using System;
using System.Data;	// 包括一些通用数据库对象（如：DataSet，DataTable）的命名空间
using System.Collections.Generic;

namespace Rally.Lib.Persistence.Core
{
	/// <summary>
	/// 抽象数据库操作类 的摘要说明。
	/// 此处声明为抽象类，类中的所有方法也均为抽象方法以便子类覆盖
	/// </summary>
	public interface IDMLOperable
	{
        /// <summary>
        /// 得到数据连接
        /// </summary>
        IDbConnection Connection{get;}

		/// <summary>
		/// 打开数据库连接
		/// </summary>
		void Open();

		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		void Close();

		/// <summary>
		/// 开始一个事务
		/// </summary>
		void BeginTrans();

		/// <summary>
		/// 提交一个事务
		/// </summary>
		void CommitTrans();

		/// <summary>
		/// 回滚一个事务
		/// </summary>
		void RollbackTrans();


        /// <summary>
        /// 执行SQL语句，没有返回值
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        int ExeSql(string CommandText);


        /// <summary>
        /// 执行一个SQL语句(UPDATE,INSERT) 带参数的
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters"> sql语句中的参数数组</param>
        int ExeSql(string CommandText, IDictionary<String, object> Parameters);


        /// <summary>
        /// 执行Insert SQL语句返回当前ID
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Argument">临时变量</param>
        /// <returns>当前ID</returns>
        //int ExeSql(string CommandText, int Argument);

        /// <summary>
        /// 执行SQL语句返回第一行第一列的值
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <returns>第一行第一列的值</returns>
        string ExeSqlScalar(string CommandText);

        /// <summary>
        /// 执行SQL语句返回第一行第一列的值
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <returns>第一行第一列的值</returns>
        string ExeSqlScalar(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <returns>DataSet</returns>
        DataSet GetDataSet(string CommandText);

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">SQL语句中的参数数组</param>
        /// <returns>DataSet</returns>
        DataSet GetDataSet(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name=" CurrentIndex">当前页</param>
        /// <param name="PageSize">页长度</param>
        /// <returns></returns>
        DataSet GetDataSet(string CommandText, int CurrentIndex, int PageSize, string TableName);

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <returns>DataTable</returns>
        DataTable GetDataTable(string CommandText);

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <returns>DataTable</returns>
        DataTable GetDataTable(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="Parameters">存储过程参数</param>
        int ExeProcedure(string ProcedureName, IDictionary<String, object> Parameters);


        /// <summary>
        /// 执行存储过程获取数据集
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="Parameters">存储过程参数</param>
        /// <returns>DataSet</returns>
        DataSet ExeProcedureGetRecords(string ProcedureName, IDictionary<String, object> Parameters);

        /// <summary>
        /// 执行存储过程获取数据集
        /// </summary>
        /// <param name="ProcedureName">存储过程名</param>
        /// <param name="Parameters">存储过程参数</param>
        /// <param name="TableIndex">DataTable 的 Index</param>
        /// <returns>DataTable</returns>
        DataTable ExeProcedureGetRecords(string ProcedureName, IDictionary<String, object> Parameters, int TableIndex);

        /// <summary>
        /// 给定SQL语句和SQL查询参数返回数据结果集合
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <returns>数据结果集合</returns>
        IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<String, object> Parameters);

        /// <summary>
        /// 给定SQL语句和SQL查询参数返回数据结果集合
        /// </summary>
        /// <param name="CommandText">SQL语句</param>
        /// <param name="Parameters">参数</param>
        /// <param name="ExtensionFunction">扩展函数</param>
        /// <returns>数据结果集合</returns>
        IList<IDictionary<string, object>> ExeReader(string CommandText, IDictionary<String, object> Parameters, Func<object,object> ExtensionFunction);

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
        IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB);

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
        IList<IDictionary<string, object>> ExeReaderWithPaging(string TableName, string KeyName, string SortKeyName, string[] ColumnNames, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction);


        /// <summary>
        /// 给定表名称、页码和页尺寸，返回总页数及数据库总记录数
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="CurrentIndex">当前页码</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="TotalRecordCountInDB">数据库中总记录数</param>
        /// <returns>总页数</returns>
        int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB);

        /// <summary>
        /// 给定表名称、页码和页尺寸，返回总页数及数据库总记录数
        /// </summary>
        /// <param name="TableName">表名称</param>
        /// <param name="CurrentIndex">当前页码</param>
        /// <param name="PageSize">页尺寸</param>
        /// <param name="TotalRecordCountInDB">数据库中总记录数</param>
        /// <param name="ExtensionFunction">扩展函数</param>
        /// <returns>总页数</returns>
        int GetTotalPageCount(string TableName, int CurrentIndex, int PageSize, out int TotalRecordCountInDB, Func<object, object> ExtensionFunction);
    }
}
