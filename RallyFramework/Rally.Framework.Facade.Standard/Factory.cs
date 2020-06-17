using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Lib.Persistence.MySQL.Standard;
using Rally.Lib.Persistence.Oracle.Standard;
using Rally.Lib.Persistence.PostgreSQL.Standard;
using Rally.Lib.Persistence.SQLite.Standard;
using Rally.Lib.Persistence.SQLServer.Standard;

namespace Rally.Framework.Facade
{
    public class Factory
    {
        //public static IDMLOperable CreateDMLOperable(string DBConnectionString, DBTypeEnum DBType)
        //{
        //    IDMLOperable dmlOperable = null;

        //    switch (DBType)
        //    {
        //        //case DBTypeEnum.Oracle:
        //        //    break;
        //        case DBTypeEnum.SQLServer:
        //            dmlOperable = SQLServerDBOperator.NewInstance(DBConnectionString);
        //            break;
        //        case DBTypeEnum.MySQL:
        //            dmlOperable = MySQLDBOperator.NewInstance(DBConnectionString);
        //            break;
        //        case DBTypeEnum.SQLite:
        //            dmlOperable = SQLiteDBOperator.NewInstance(DBConnectionString);
        //            break;
        //        default:
        //            break;
        //    }

        //    return dmlOperable;
        //}

        public static IDMLOperable CreateDMLOperable(string DBConnectionString, string DBType)
        {
            IDMLOperable dmlOperable = null;

            switch (DBType.ToLower())
            {              
                case "mysql":
                    dmlOperable = MySQLDBOperatorStandard.NewInstance(DBConnectionString); //MySQLDBOperator.NewInstance(DBConnectionString);
                    break;
                case "oracle":
                    dmlOperable = OracleDBOperatorStandard.NewInstance(DBConnectionString);
                    break;
                case "postgresql":
                    dmlOperable = PostgreSQLDBOperatorStandard.NewInstance(DBConnectionString);
                    break;
                case "sqlserver":
                    dmlOperable = SQLServerDBOperatorStandard.NewInstance(DBConnectionString);
                    break;
                case "sqlite":
                    dmlOperable = SQLiteDBOperatorStandard.NewInstance(DBConnectionString);
                    break;
                default:
                    break;
            }

            return dmlOperable;
        }
    }
}
