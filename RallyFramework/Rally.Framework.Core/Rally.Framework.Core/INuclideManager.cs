using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface INuclideManager
    {
        EnergyCalibration DoCalibration(EnergyCalibration CalibrationObject, out double[] XData, out double[] YData);
        string SaveCalibration(EnergyCalibration CalibrationObject);
        IList<EnergyCalibration> GetEnergyCalibrations(string InstrumentID);
        IList<EnergyCalibration> QueryEnergyCalibrations(string Filter, int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, Func<object, object> ExtensionFunction);
        string AddNuclide(DomainModel.Nuclide Nuclide);
        DomainModel.Nuclide SetNuclide(string ID, DomainModel.Nuclide Nuclide);
        string DeleteNuclide(string ID);
        IList<DomainModel.Nuclide> GetNuclides();
        DomainModel.Nuclide GetNuclide(string ID);
        DomainModel.Nuclide RecognizeNuclide(IList<double[]> EnergySpectrumData,int ChannelCount, IList<DomainModel.Nuclide> Nuclides, int Algorithm, out IDictionary<int, DomainModel.Nuclide> RecognitionResults);

        IList<DomainModel.Nuclide> RecognizeNuclide(IDictionary<int, double> Channels, IList<DomainModel.Nuclide> Nuclides, double Range, double ETOL);

        IDictionary<int, DomainModel.Nuclide> RecognizeNuclide(int[] Channels, IList<DomainModel.Nuclide> Nuclides, EnergyCalibration EnergyCalibration);
    }
}
