using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Utility.Common;

namespace UnitTestUtilityCommon
{
    class Program
    {
        static void Main(string[] args)
        {
            //string dir = OSUtility.GetProcessExecutablePath("mysqld");

            //Console.WriteLine(dir);

            string SQL_CMD_SelectNiclideById = "SELECT ID,No,Name,Symbol,Type,Category,Energy1,BranchingRatio1,Channel1,Energy2,BranchingRatio2,Channel2,Energy3,BranchingRatio3,Channel3,HalfLife,HalfLifeUnit FROM nuclide WHERE ID = @ID;";

            SQL_CMD_SelectNiclideById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(SQL_CMD_SelectNiclideById);

            Console.WriteLine(SQL_CMD_SelectNiclideById);

            string SQL_CMD_SelectNiclides = "SELECT ID as ID,No as No,Name as Name,Symbol as Symbol,Type as Type,Category as Category,Energy1 as Energy1,BranchingRatio1 as BranchingRatio1,Channel1 as Channel1,Energy2 as Energy2,BranchingRatio2 as BranchingRatio2,Channel2 as Channel2,Energy3 as Energy3,BranchingRatio3 as BranchingRatio3,Channel3 as Channel3,HalfLife as HalfLife,HalfLifeUnit as HalfLifeUnit FROM nuclide;";

            SQL_CMD_SelectNiclides = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(SQL_CMD_SelectNiclides);

            Console.WriteLine(SQL_CMD_SelectNiclides);

            Console.Read();
        }
    }
}
