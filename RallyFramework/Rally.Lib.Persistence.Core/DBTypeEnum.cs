using System;

namespace Rally.Lib.Persistence.Core
{
	/// <summary>
	/// ���ݿ�����
	/// </summary>
	public enum DBTypeEnum
	{
		///// <summary>
		///// OleDB ���ݿ�
		///// </summary>
		//OleDB,
		///// <summary>
		///// ODBC ���ݿ�
		///// </summary>
		//ODBC,
		/// <summary>
		/// Oracle ���ݿ�
		/// </summary>
		Oracle = 0,
		/// <summary>
		/// SQL Server ���ݿ�
		/// </summary>
		SQLServer = 1,
        /// <summary>
        /// MySQL ���ݿ�
        /// </summary>
        MySQL = 2,
        /// <summary>
        /// SQLite ���ݿ�
        /// </summary>
        SQLite = 3
        ///// <summary>
        ///// ACCESS ���ݿ�
        ///// </summary>
        //ACCESS
	}
}
