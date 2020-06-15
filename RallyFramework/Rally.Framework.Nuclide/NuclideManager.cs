using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;

namespace Rally.Framework.Nuclide
{
    public class NuclideManager : INuclideManager
    {
        public NuclideManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static NuclideManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new NuclideManager(DMLOperable, DBType);
        }

        public EnergyCalibration DoCalibration(EnergyCalibration CalibrationObject, out double[] XData, out double[] YData)
        {
            XData = null;
            YData = null;

            if (CalibrationObject != null && CalibrationObject.EnergyChannels != null)
            {
                List<double> x = new List<double>(), y = new List<double>();

                foreach (var item in CalibrationObject.EnergyChannels)
                {
                    if (item.Channel != null && item.Energy != null)
                    {
                        x.Add((double)item.Channel);
                        y.Add((double)item.Energy);
                    }
                }

                XData = x.ToArray();
                YData = y.ToArray();

                //Tuple<double, double> tuple = SimpleRegression.Fit(XData, YData);

                //CalibrationObject.CoefficientA = 0;//(float)tuple.Item1;
                //CalibrationObject.CoefficientB = (float)tuple.Item1;
                //CalibrationObject.CoefficientC = (float)tuple.Item2;//0;

                double A, B, C;

                MathUtility.SolveLeastSquareCoefficient(XData, YData, out A, out B, out C);

                CalibrationObject.CoefficientA = (float)A;
                CalibrationObject.CoefficientB = (float)B;
                CalibrationObject.CoefficientC = (float)C;

                CalibrationObject.CalibrationTime = DateTime.Now;
            }

            return CalibrationObject;
        }

        public string SaveCalibration(EnergyCalibration CalibrationObject)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertEnergyCalibration;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>() {
                { "@ID", CalibrationObject.ID},
                { "@Time", CalibrationObject.CalibrationTime},
                { "@OperatorID", CalibrationObject.OperatorID},
                { "@InstrumentNo", CalibrationObject.InsturmentSerial},
                { "@NuclideBoardNo", CalibrationObject.NuclideBoardSerial},
                { "@EnergyResolution", CalibrationObject.EngergyResolution},
                { "@CoefficientA", CalibrationObject.CoefficientA},
                { "@CoefficientB", CalibrationObject.CoefficientB},
                { "@CoefficientC", CalibrationObject.CoefficientC},
            };

            if (CalibrationObject.EnergyChannels != null && CalibrationObject.EnergyChannels.Count > 0)
            {
                for (int i = 0; i < CalibrationObject.EnergyChannels.Count; i++)
                {
                    sqlParams.Add($"@Energy{i + 1}", CalibrationObject.EnergyChannels[i].Energy);
                    sqlParams.Add($"@Channel{i + 1}", CalibrationObject.EnergyChannels[i].Channel);
                }

                //if (CalibrationObject.EnergyChannels.Count < 5)
                //{
                //    for (int i = CalibrationObject.EnergyChannels.Count; i < 5; i++)
                //    {
                //        sqlParams.Add($"@Energy{i + 1}", 0);
                //        sqlParams.Add($"@Channel{i + 1}", 0);
                //    }
                //}
            }

            this.dmlOperable.ExeSql(sqlCommandText, sqlParams);

            return CalibrationObject.ID;
        }

        public IList<EnergyCalibration> GetEnergyCalibrations(string InstrumentSerialNumber)
        {
            IList<EnergyCalibration> energyCalibrations = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_GetEngergyCalibrationByInstrumentSerial;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() {
                { "@InstrumentNo",InstrumentSerialNumber}
            });

            if (dbResult != null && dbResult.Count > 0)
            {
                energyCalibrations = new List<EnergyCalibration>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    energyCalibrations.Add(new EnergyCalibration()
                    {
                        ID = dbResult[i]["Ecl_ID"].ToString(),
                        OperatorID = (string)dbResult[i]["Ecl_ID"],
                        CalibrationTime = (DateTime?)dbResult[i]["Ecl_Time"],
                        InsturmentSerial = (string)dbResult[i]["Ins_InstrumentNo"],
                        NuclideBoardSerial = (string)dbResult[i]["Ecl_NuclideBoardNo"],
                        CoefficientA = (float?)dbResult[i]["Ecl_CoefficientA"],
                        CoefficientB = (float?)dbResult[i]["Ecl_CoefficientB"],
                        CoefficientC = (float?)dbResult[i]["Ecl_CoefficientC"],
                        EngergyResolution = (float?)dbResult[i]["Ecl_EnergyResolution"],
                        EnergyChannels = new List<EnergyChannel>() {
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel1"], Energy =  (float?)dbResult[i]["Ecl_Energy1"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel2"], Energy =  (float?)dbResult[i]["Ecl_Energy2"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel3"], Energy =  (float?)dbResult[i]["Ecl_Energy3"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel4"], Energy =  (float?)dbResult[i]["Ecl_Energy4"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel5"], Energy =  (float?)dbResult[i]["Ecl_Energy5"]},
                        },
                    });
                }
            }

            return energyCalibrations;
        }

        public IList<EnergyCalibration> QueryEnergyCalibrations(string Filter, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, Func<object, object> ExtensionFunction)
        {
            IList<EnergyCalibration> energyCalibrations = null;

            TotalPageCount = 0;
            TotalRecords = 0;

            string[] columnNames = new string[] { "Ecl_ID", "Ecl_Time", "Ecl_OperatorID", "Ins_InstrumentNo", "Ecl_NuclideBoardNo", "Ecl_Channel1", "Ecl_Energy1", "Ecl_Channel2", "Ecl_Energy2", "Ecl_Channel3", "Ecl_Energy3", "Ecl_Channel4", "Ecl_Energy4", "Ecl_Channel5", "Ecl_Energy5", "Ecl_CoefficientA", "Ecl_CoefficientB", "Ecl_CoefficientC", "Ecl_EnergyResolution" };

            var dbResult = this.dmlOperable.ExeReaderWithPaging("tb_mon_energycalibration", "Ecl_ID", "Ecl_ID", columnNames, CurrentIndex, PageSize, out TotalPageCount, out TotalRecords, q =>
            {

                string sqlCommandText = q != null ? q.ToString() : null;

                if (!string.IsNullOrEmpty(sqlCommandText))
                {
                    switch (this.dBType)
                    {
                        case DBTypeEnum.Oracle:
                            break;
                        case DBTypeEnum.SQLServer:
                            sqlCommandText = sqlCommandText.Insert(sqlCommandText.LastIndexOf("order by"), $"and ({Filter}) ");
                            break;
                        case DBTypeEnum.MySQL:
                            sqlCommandText = sqlCommandText.Insert(sqlCommandText.IndexOf("order by"), $"where {Filter} ");
                            break;
                        case DBTypeEnum.SQLite:
                            sqlCommandText = sqlCommandText.Insert(sqlCommandText.IndexOf("order by"), $"where {Filter} ");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    sqlCommandText = $" where {Filter}";
                }

                return sqlCommandText;
            });

            if (dbResult != null && dbResult.Count > 0)
            {
                energyCalibrations = new List<EnergyCalibration>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    energyCalibrations.Add(new EnergyCalibration()
                    {
                        ID = dbResult[i]["Ecl_ID"].ToString(),
                        OperatorID = (string)dbResult[i]["Ecl_OperatorID"],
                        CalibrationTime = (DateTime?)dbResult[i]["Ecl_Time"],
                        InsturmentSerial = (string)dbResult[i]["Ins_InstrumentNo"],
                        NuclideBoardSerial = (string)dbResult[i]["Ecl_NuclideBoardNo"],
                        CoefficientA = (float?)dbResult[i]["Ecl_CoefficientA"],
                        CoefficientB = (float?)dbResult[i]["Ecl_CoefficientB"],
                        CoefficientC = (float?)dbResult[i]["Ecl_CoefficientC"],
                        EngergyResolution = (float?)dbResult[i]["Ecl_EnergyResolution"],
                        EnergyChannels = new List<EnergyChannel>() {
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel1"], Energy =  (float?)dbResult[i]["Ecl_Energy1"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel2"], Energy =  (float?)dbResult[i]["Ecl_Energy2"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel3"], Energy =  (float?)dbResult[i]["Ecl_Energy3"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel4"], Energy =  (float?)dbResult[i]["Ecl_Energy4"]},
                            new EnergyChannel() { Channel = (float?)dbResult[i]["Ecl_Channel5"], Energy =  (float?)dbResult[i]["Ecl_Energy5"]},
                        },
                    });
                }
            }

            if (ExtensionFunction != null)
            {
                ExtensionFunction(energyCalibrations);
            }

            return energyCalibrations;
        }

        public string AddNuclide(Core.DomainModel.Nuclide Nuclide)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertNuclide;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>() {
                {"@ID", Nuclide.ID },
                {"@No", Nuclide.SerialNumber},
                {"@Name",  Nuclide.Name},
                {"@Symbol", Nuclide.Symbol },
                {"@Type", Nuclide.Type},
                {"@Category",  Nuclide.Category},
                {"@HalfLife",  Nuclide.HalfLife},
                {"@HalfLifeUnit",  Nuclide.HalfLifeUnit},
            };

            if (Nuclide.EnergyChannels != null && Nuclide.EnergyChannels.Count > 0)
            {
                for (int i = 0; i < Nuclide.EnergyChannels.Count; i++)
                {
                    sqlParams.Add($"@Energy{Nuclide.EnergyChannels[i].Index}", Nuclide.EnergyChannels[i].Energy);
                    sqlParams.Add($"@Channel{Nuclide.EnergyChannels[i].Index}", Nuclide.EnergyChannels[i].Channel);
                    sqlParams.Add($"@BranchingRatio{Nuclide.EnergyChannels[i].Index}", Nuclide.EnergyChannels[i].BranchingRatio);
                }
            }

            this.dmlOperable.ExeSql(sqlCommandText, sqlParams);

            return Nuclide.ID;
        }

        public Core.DomainModel.Nuclide SetNuclide(string ID, Core.DomainModel.Nuclide Nuclide)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateNuclide;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>() {
                {"@ID", Nuclide.ID },
                {"@No", Nuclide.SerialNumber},
                {"@Name",  Nuclide.Name},
                {"@Symbol", Nuclide.Symbol },
                {"@Type", Nuclide.Type},
                {"@Category",  Nuclide.Category},
                {"@HalfLife",  Nuclide.HalfLife},
                {"@HalfLifeUnit",  Nuclide.HalfLifeUnit},
                {"@Description",  Nuclide.Description},
            };

            if (Nuclide.EnergyChannels != null && Nuclide.EnergyChannels.Count > 0)
            {
                for (int i = 0; i < Nuclide.EnergyChannels.Count; i++)
                {
                    sqlParams.Add($"@Energy{Nuclide.EnergyChannels[i].Index}", Nuclide.EnergyChannels[i].Energy);
                    sqlParams.Add($"@Channel{Nuclide.EnergyChannels[i].Index}", Nuclide.EnergyChannels[i].Channel);
                    sqlParams.Add($"@BranchingRatio{Nuclide.EnergyChannels[i].Index}", Nuclide.EnergyChannels[i].BranchingRatio);
                }
            }

            this.dmlOperable.ExeSql(sqlCommandText, sqlParams);

            return Nuclide;
        }

        public string DeleteNuclide(string ID)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteNuclide;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", ID } });

            return ID;
        }

        public IList<Core.DomainModel.Nuclide> GetNuclides()
        {
            IList<Core.DomainModel.Nuclide> nuclides = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectNiclides;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, null);

            if (dbResult != null && dbResult.Count > 0)
            {
                nuclides = new List<Core.DomainModel.Nuclide>();

                Core.DomainModel.Nuclide nuclide = null;

                for (int i = 0; i < dbResult.Count; i++)
                {
                    nuclide = new Core.DomainModel.Nuclide()
                    {
                        ID = dbResult[i]["Nuc_ID"].ToString(),
                        Name = (string)dbResult[i]["Nuc_Name"],
                        Category = dbResult[i]["Nuc_Category"] == null ? 0 : int.Parse(dbResult[i]["Nuc_Category"].ToString()),//(int?)dbResult[i]["Nuc_Category"],
                        Type = dbResult[i]["Nuc_Type"] == null ? 0 : int.Parse(dbResult[i]["Nuc_Type"].ToString()),
                        SerialNumber = (string)dbResult[i]["Nuc_No"],
                        Symbol = (string)dbResult[i]["Nuc_Symbol"],
                        HalfLife = dbResult[i]["Nuc_HalfLife"] == null ? 0 : double.Parse(dbResult[i]["Nuc_HalfLife"].ToString()),//(int?)dbResult[i]["Nuc_HalfLife"],
                        HalfLifeUnit = (string)dbResult[i]["Nuc_HalfLifeUnit"],
                        //Description = (string)dbResult[i]["Nuc_Description"]
                        EnergyChannels = new List<EnergyChannel>(),
                    };

                    if (dbResult[i]["Nuc_Energy1"] != null)
                    {
                        nuclide.EnergyChannels.Add(new EnergyChannel() { Index = 1, Energy = (float?)dbResult[i]["Nuc_Energy1"], BranchingRatio = (float?)dbResult[i]["Nuc_BranchingRatio1"], Channel = (float?)dbResult[i]["Nuc_Channel1"] });
                    }

                    if (dbResult[i]["Nuc_Energy2"] != null)
                    {
                        nuclide.EnergyChannels.Add(new EnergyChannel() { Index = 2, Energy = (float?)dbResult[i]["Nuc_Energy2"], BranchingRatio = (float?)dbResult[i]["Nuc_BranchingRatio2"], Channel = (float?)dbResult[i]["Nuc_Channel2"] });
                    }

                    if (dbResult[i]["Nuc_Energy3"] != null)
                    {
                        nuclide.EnergyChannels.Add(new EnergyChannel() { Index = 3, Energy = (float?)dbResult[i]["Nuc_Energy3"], BranchingRatio = (float?)dbResult[i]["Nuc_BranchingRatio3"], Channel = (float?)dbResult[i]["Nuc_Channel3"] });
                    }

                    nuclides.Add(nuclide);
                }
            }

            return nuclides;
        }

        public Core.DomainModel.Nuclide GetNuclide(string ID)
        {
            Core.DomainModel.Nuclide nuclide = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectNiclideById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", ID } });
            if (dbResult != null && dbResult.Count == 1)
            {
                nuclide = new Core.DomainModel.Nuclide()
                {
                    ID = dbResult[0]["Nuc_ID"].ToString(),
                    Name = (string)dbResult[0]["Nuc_Name"],
                    Category = dbResult[0]["Nuc_Category"] == null ? 0 : int.Parse(dbResult[0]["Nuc_Category"].ToString()),  //(int?)dbResult[0]["Nuc_Category"],
                    Type = dbResult[0]["Nuc_Type"] == null ? 0 : int.Parse(dbResult[0]["Nuc_Type"].ToString()), //(int?)dbResult[0]["Nuc_Type"],
                    SerialNumber = (string)dbResult[0]["Nuc_Type"],
                    Symbol = (string)dbResult[0]["Nuc_Symbol"],
                    HalfLife = dbResult[0]["Nuc_HalfLife"] == null ? 0 : double.Parse(dbResult[0]["Nuc_HalfLife"].ToString()),//(int?)dbResult[0]["Nuc_HalfLife"],
                    HalfLifeUnit = (string)dbResult[0]["Nuc_HalfLifeUnit"],
                    //Description = (string)dbResult[0]["Nuc_Description"]
                };

                if (dbResult[0]["Nuc_Energy1"] != null)
                {
                    nuclide.EnergyChannels.Add(new EnergyChannel() { Index = 1, Energy = (float?)dbResult[0]["Nuc_Energy1"], BranchingRatio = (float?)dbResult[0]["Nuc_BranchingRatio1"], Channel = (float?)dbResult[0]["Nuc_Channel1"] });
                }

                if (dbResult[0]["Nuc_Energy2"] != null)
                {
                    nuclide.EnergyChannels.Add(new EnergyChannel() { Index = 2, Energy = (float?)dbResult[0]["Nuc_Energy2"], BranchingRatio = (float?)dbResult[0]["Nuc_BranchingRatio2"], Channel = (float?)dbResult[0]["Nuc_Channel2"] });
                }

                if (dbResult[0]["Nuc_Energy3"] != null)
                {
                    nuclide.EnergyChannels.Add(new EnergyChannel() { Index = 3, Energy = (float?)dbResult[0]["Nuc_Energy3"], BranchingRatio = (float?)dbResult[0]["Nuc_BranchingRatio3"], Channel = (float?)dbResult[0]["Nuc_Channel3"] });
                }
            }

            return nuclide;
        }

        public Core.DomainModel.Nuclide RecognizeNuclide(IList<double[]> EnergySpectrumData, int ChannelCount, IList<Core.DomainModel.Nuclide> Nuclides, int Algorithm, out IDictionary<int, Core.DomainModel.Nuclide> RecognitionResults)
        {
            Core.DomainModel.Nuclide mostProbableNuclide = null;
            RecognitionResults = null;

            double[] spectrumSumMatrix = new double[ChannelCount], channels = new double[ChannelCount], X, Y;
            double A = 0, B = 0, C = 0;

            for (int i = 0; i < spectrumSumMatrix.Length; i++)
            {
                spectrumSumMatrix[i] = 0;
                channels[i] = (i + 1);
            }

            for (int i = 0; i < EnergySpectrumData.Count; i++)
            {
                for (int j = 0; j < EnergySpectrumData[i].Length; j++)
                {
                    spectrumSumMatrix[j] += EnergySpectrumData[i][j];
                }
            }

            //求系数
            MathUtility.SolveLeastSquareCoefficient(channels, spectrumSumMatrix, out A, out B, out C);

            //最小二乘法做曲线平滑
            MathUtility.CurveSmooth(ChannelCount, A, B, C, out X, out Y);

            //if (X != null && Y != null)
            //{
            //寻峰
            List<int> peaks;
            List<int> data = new List<int>();

            for (int i = 0; i < Y.Length; i++)
            {
                data.Add(decimal.ToInt32(decimal.Parse(Y[i].ToString())));
            }

            Peak.Peaks(data.ToArray(), ChannelCount, out peaks);

            //}

            if (peaks == null || peaks.Count <= 0)
            {
                return null;
            }

            //识别
            double energyAvg, energyVariance;
            Dictionary<string, double> nuclideEnergyVariances = new Dictionary<string, double>();
            List<double> perNuclideEnergyVariacesByPeak;
            double perNuclideEnergyVariaceMin;

            foreach (var nuclide in Nuclides)
            {
                perNuclideEnergyVariacesByPeak = new List<double>();
                energyAvg = (double)nuclide.EnergyChannels.Average(ec => ec.Energy);

                for (int i = 0; i < peaks.Count; i++)
                {
                    energyVariance = Math.Abs((spectrumSumMatrix[i] / ChannelCount) - energyAvg);
                    perNuclideEnergyVariacesByPeak.Add(energyVariance);
                }

                perNuclideEnergyVariaceMin = perNuclideEnergyVariacesByPeak.Min();

                nuclideEnergyVariances.Add(nuclide.ID, perNuclideEnergyVariaceMin);
            }

            double nuclideEnergyVariaceMin = nuclideEnergyVariances.Min(nv => nv.Value);
            string probableEnergyID = nuclideEnergyVariances.First(kv => kv.Value == nuclideEnergyVariaceMin).Key;

            mostProbableNuclide = Nuclides.First(n => n.ID == probableEnergyID);

            var rankedNuclides = nuclideEnergyVariances.OrderBy(kv => kv.Value);

            int rank = -1;
            RecognitionResults = new Dictionary<int, Core.DomainModel.Nuclide>();

            foreach (var nuclide in rankedNuclides)
            {
                rank += 1;
                RecognitionResults.Add(rank, Nuclides.FirstOrDefault(n => n.ID == nuclide.Key));
            }

            return mostProbableNuclide;
        }

        public IList<Core.DomainModel.Nuclide> RecognizeNuclide(IDictionary<int, double> Channels, IList<Core.DomainModel.Nuclide> Nuclides, double Range, double ETOL)
        {
            IList<Core.DomainModel.Nuclide> results = null;

            if (Channels != null && Nuclides != null)
            {
                results = new List<Core.DomainModel.Nuclide>();

                foreach (int channel in Channels.Keys)
                {
                    foreach (var nuclide in Nuclides)
                    {
                        var nuclideChannels = nuclide.EnergyChannels.Where(en => en.Channel == channel).ToList();

                        if (nuclideChannels != null && nuclideChannels.Count() > 0)
                        {
                            nuclide.Credibility = MathUtility.ComputeCredibility(new double[1] { Channels[channel] }, new double[1] { (double)nuclideChannels[0].Energy }, ETOL);

                            results.Add(nuclide);
                        }
                    }
                }
            }

            return results;
        }

        public IDictionary<int, Core.DomainModel.Nuclide> RecognizeNuclide(int[] Channels, IList<Core.DomainModel.Nuclide> Nuclides, EnergyCalibration EnergyCalibration)
        {
            IDictionary<int, Core.DomainModel.Nuclide> results = null;

            if (Channels != null && Nuclides != null)
            {
                results = new Dictionary<int, Core.DomainModel.Nuclide>();

                foreach (int channel in Channels)
                {
                    foreach (var nuclide in Nuclides)
                    {
                        var nuclideChannels = nuclide.EnergyChannels.Where(en => en.Channel == channel).ToList();

                        if (nuclideChannels != null && nuclideChannels.Count() > 0)
                        {
                            if (!results.ContainsKey(channel))
                            {
                                for (int i = 0; i < nuclide.EnergyChannels.Count; i++)
                                {
                                    nuclide.EnergyChannels[i].Energy = (float?)MathUtility.Compute(new double[] { (double)channel }, (double)EnergyCalibration.CoefficientA, (double)EnergyCalibration.CoefficientB, (double)EnergyCalibration.CoefficientC)[0];
                                    nuclide.Credibility = 1;
                                }

                                results.Add(channel, nuclide);
                            }                        
                        }
                    }
                }
            }

            return results;
        }
    }
}
