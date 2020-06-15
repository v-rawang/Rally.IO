using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Nuclide
{
    public class ModuleConfiguration
    {
        //public static string SQL_CMD_InsertEnergyCalibration = "INSERT INTO tb_mon_energycalibration (Ecl_ID,Ecl_Time,Ecl_OperatorID,Ins_InstrumentNo,Ecl_NuclideBoardNo,Ecl_Channel1,Ecl_Energy1,Ecl_Channel2,Ecl_Energy2,Ecl_Channel3,Ecl_Energy3,Ecl_Channel4,Ecl_Energy4,Ecl_Channel5,Ecl_Energy5,Ecl_CoefficientA,Ecl_CoefficientB,Ecl_CoefficientC,Ecl_EnergyResolution)VALUES(@ID,@Time,@OperatorID,@InstrumentNo,@NuclideBoardNo,@Channel1,@Energy1,@Channel2,@Energy2,@Channel3,@Energy3,@Channel4,@Energy4,@Channel5,@Energy5,@CoefficientA,@CoefficientB,@CoefficientC,@EnergyResolution);";

        //public static string SQL_CMD_GetEngergyCalibrationByInstrumentSerial = "SELECT Ecl_ID,Ecl_Time,Ecl_OperatorID,Ins_InstrumentNo,Ecl_NuclideBoardNo,Ecl_Channel1,Ecl_Energy1,Ecl_Channel2,Ecl_Energy2,Ecl_Channel3,Ecl_Energy3,Ecl_Channel4,Ecl_Energy4,Ecl_Channel5,Ecl_Energy5,Ecl_CoefficientA,Ecl_CoefficientB,Ecl_CoefficientC,Ecl_EnergyResolution FROM tb_mon_energycalibration WHERE Ins_InstrumentNo = @InstrumentNo";

        //public static string SQL_CMD_InsertNuclide = "INSERT INTO tb_mon_nuclide(Nuc_ID,Nuc_No,Nuc_Name,Nuc_Symbol,Nuc_Type,Nuc_Category,Nuc_Energy1,Nuc_BranchingRatio1,Nuc_Channel1,Nuc_Energy2,Nuc_BranchingRatio2,Nuc_Channel2,Nuc_Energy3,Nuc_BranchingRatio3,Nuc_Channel3,Nuc_HalfLife,Nuc_HalfLifeUnit)VALUES(@ID,@No,@Name,@Symbol,@Type,@Category,@Energy1,@BranchingRatio1,@Channel1,@Energy2,@BranchingRatio2,@Channel2,@Energy3,@BranchingRatio3,@Channel3,@HalfLife,@HalfLifeUnit);";

        //public static string SQL_CMD_UpdateNuclide = "UPDATE tb_mon_nuclide SET Nuc_No = @No, Nuc_Name = @Name, Nuc_Symbol = @Symbol, Nuc_Type = @Type, Nuc_Category = @Category, Nuc_Energy1 = @Energy1, Nuc_BranchingRatio1 = @BranchingRatio1, Nuc_Channel1 = @Channel1, Nuc_Energy2 = @Energy2, Nuc_BranchingRatio2 = @BranchingRatio2, Nuc_Channel2 = @Channel2, Nuc_Energy3 = @Energy3, Nuc_BranchingRatio3 = @BranchingRatio3, Nuc_Channel3 = @Channel3, Nuc_HalfLife = @HalfLife, Nuc_HalfLifeUnit = @HalfLifeUnit WHERE Nuc_ID = @ID;";

        //public static string SQL_CMD_DeleteNuclide = "DELETE FROM tb_mon_nuclide WHERE Nuc_ID = @ID;";

        //public static string SQL_CMD_SelectNiclides = "SELECT Nuc_ID,Nuc_No,Nuc_Name,Nuc_Symbol,Nuc_Type,Nuc_Category,Nuc_Energy1,Nuc_BranchingRatio1,Nuc_Channel1,Nuc_Energy2,Nuc_BranchingRatio2,Nuc_Channel2,Nuc_Energy3,Nuc_BranchingRatio3,Nuc_Channel3,Nuc_HalfLife,Nuc_HalfLifeUnit FROM tb_mon_nuclide;";

        //public static string SQL_CMD_SelectNiclideById = "SELECT Nuc_ID,Nuc_No,Nuc_Name,Nuc_Symbol,Nuc_Type,Nuc_Category,Nuc_Energy1,Nuc_BranchingRatio1,Nuc_Channel1,Nuc_Energy2,Nuc_BranchingRatio2,Nuc_Channel2,Nuc_Energy3,Nuc_BranchingRatio3,Nuc_Channel3,Nuc_HalfLife,Nuc_HalfLifeUnit FROM tb_mon_nuclide WHERE Nuc_ID = @ID;";

        public static string SQL_CMD_InsertEnergyCalibration = "INSERT INTO energycalibration (ID,Time,OperatorID,InstrumentNo,NuclideBoardNo,Channel1,Energy1,Channel2,Energy2,Channel3,Energy3,Channel4,Energy4,Channel5,Energy5,CoefficientA,CoefficientB,CoefficientC,EnergyResolution)VALUES(@ID,@Time,@OperatorID,@InstrumentNo,@NuclideBoardNo,@Channel1,@Energy1,@Channel2,@Energy2,@Channel3,@Energy3,@Channel4,@Energy4,@Channel5,@Energy5,@CoefficientA,@CoefficientB,@CoefficientC,@EnergyResolution);";

        public static string SQL_CMD_GetEngergyCalibrationByInstrumentSerial = "SELECT ID,Time,OperatorID,InstrumentNo,NuclideBoardNo,Channel1,Energy1,Channel2,Energy2,Channel3,Energy3,Channel4,Energy4,Channel5,Energy5,CoefficientA,CoefficientB,CoefficientC,EnergyResolution FROM energycalibration WHERE InstrumentNo = @InstrumentNo";

        public static string SQL_CMD_InsertNuclide = "INSERT INTO nuclide(ID,No,Name,Symbol,Type,Category,Energy1,BranchingRatio1,Channel1,Energy2,BranchingRatio2,Channel2,Energy3,BranchingRatio3,Channel3,HalfLife,HalfLifeUnit)VALUES(@ID,@No,@Name,@Symbol,@Type,@Category,@Energy1,@BranchingRatio1,@Channel1,@Energy2,@BranchingRatio2,@Channel2,@Energy3,@BranchingRatio3,@Channel3,@HalfLife,@HalfLifeUnit);";

        public static string SQL_CMD_UpdateNuclide = "UPDATE nuclide SET No = @No, Name = @Name, Symbol = @Symbol, Type = @Type, Category = @Category, Energy1 = @Energy1, BranchingRatio1 = @BranchingRatio1, Channel1 = @Channel1, Energy2 = @Energy2, BranchingRatio2 = @BranchingRatio2, Channel2 = @Channel2, Energy3 = @Energy3, BranchingRatio3 = @BranchingRatio3, Channel3 = @Channel3, HalfLife = @HalfLife, HalfLifeUnit = @HalfLifeUnit WHERE ID = @ID;";

        public static string SQL_CMD_DeleteNuclide = "DELETE FROM nuclide WHERE ID = @ID;";

        public static string SQL_CMD_SelectNiclides = "SELECT ID,No,Name,Symbol,Type,Category,Energy1,BranchingRatio1,Channel1,Energy2,BranchingRatio2,Channel2,Energy3,BranchingRatio3,Channel3,HalfLife,HalfLifeUnit FROM nuclide;";

        public static string SQL_CMD_SelectNiclideById = "SELECT ID,No,Name,Symbol,Type,Category,Energy1,BranchingRatio1,Channel1,Energy2,BranchingRatio2,Channel2,Energy3,BranchingRatio3,Channel3,HalfLife,HalfLifeUnit FROM nuclide WHERE ID = @ID;";
    }
}
