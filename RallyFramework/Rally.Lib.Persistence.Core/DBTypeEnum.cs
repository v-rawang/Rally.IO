using System;

namespace Rally.Lib.Persistence.Core
{
	/// <summary>
	/// 数据库类型
	/// </summary>
	public enum DBTypeEnum
	{
		///// <summary>
		///// OleDB 数据库
		///// </summary>
		//OleDB,
		///// <summary>
		///// ODBC 数据库
		///// </summary>
		//ODBC,
		/// <summary>
		/// Oracle 数据库
		/// </summary>
		Oracle = 0,
		/// <summary>
		/// SQL Server 数据库
		/// </summary>
		SQLServer = 1,
        /// <summary>
        /// MySQL 数据库
        /// </summary>
        MySQL = 2,
        /// <summary>
        /// SQLite 数据库
        /// </summary>
        SQLite = 3
        ///// <summary>
        ///// ACCESS 数据库
        ///// </summary>
        //ACCESS
	}
}
