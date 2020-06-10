using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IInstrumentManager
    {
        string AddInstrument(Instrument Instrument);

        Instrument SetInstrument(string ID, Instrument Instrument);

        Instrument GetInstrument(string ID);

        Instrument GetInstrument(string ID, out InstrumentInstallation Installation, out InstrumentDistribution Distribution);

        string DeleteInstrument(string ID);

        //string AddInstrumentDistribution(InstrumentDistribution InstrumentDistribution);

        InstrumentDistribution SetInstrumentDistribution(string InstrumentID, InstrumentDistribution Distribution);

        InstrumentDistribution GetInstrumentDistribution(string InstrumentID);

        InstrumentInstallation SetInstrumentInstallation(string InstrumentID, InstrumentInstallation Installation);

        InstrumentInstallation GetInstrumentInstallation(string InstrumentID);

        string AddInstrumentCommunication(InstrumentCommunicationSetting CommunicationSetting);

        InstrumentCommunicationSetting SetInstrumentCommunication(string SettingID, InstrumentCommunicationSetting CommunicationSetting);

        IList<InstrumentCommunicationSetting> GetInstrumentCommunicationSettings(string InstrumentID);

        string DeleteInstrumentCommunication(string SettingID);

        string AddInstrumentCamera(InstrumentCameraSetting CameraSetting);

        InstrumentCameraSetting SetInstrumentCamera(string SettingID, InstrumentCameraSetting CameraSetting);

        IList<InstrumentCameraSetting> GetInstrumentCameraSettings(string InstrumentID);

        string DeleteInstrumentCamera(string SettingID);

        string GenerateInstrumentID(int Increment);

        string GenerateInstrumentCommunicationSettingID(int Increment);

        string GenerateInstrumentCameraSettingID(int Increment);

        IList<Instrument> GetInstruments();

        IList<Instrument> GetInstruments(int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, Func<object, object> ExtensionFunction);

        System.Data.DataTable GetInstrumentDataTable();

        InstrumentCommunicationSetting GetInstrumentCommunicationSetting(string SettingID);

        InstrumentCameraSetting GetInstrumentCameraSetting(string SettingID, int order);

        string GetName(string strName, string strID);
    }
}
