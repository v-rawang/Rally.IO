using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Instrument
{
    public class InstrumentManager : IInstrumentManager
    {
        public InstrumentManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static IInstrumentManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new InstrumentManager(DMLOperable, DBType);
        }

        public string AddInstrument(Core.DomainModel.Instrument Instrument)
        {
            //if (Instrument == null || string.IsNullOrEmpty(Instrument.ID))
            //{
            //    throw new IDNullException("仪器ID不可为空！");

            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertInstrument;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(Instrument.ID)},
                { "@Name", Instrument.Name},
                { "@Alias", Instrument.Alias},
                { "@Type", Instrument.Type},
                { "@Model", Instrument.Model},
                { "@SerialNumber", Instrument.SerialNumber},
                { "@Manufacturer", Instrument.Manufacturer},
                { "@Brand", Instrument.Brand},
                { "@SKU", Instrument.SKU},
                { "@WarrantyPeriod", Instrument.WarrantyPeriod},
                { "@ShipmentDate", Instrument.ShipmentDate},
                { "@PurchaseDate", Instrument.PurchaseDate},
                { "@Remarks", Instrument.Remarks}
            });

            return GenerateInstrumentID(0);//Instrument.ID;
        }

        public Core.DomainModel.Instrument SetInstrument(string ID, Core.DomainModel.Instrument Instrument)
        {
            if (Instrument == null || string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateInstrument;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(ID)},
                { "@Name", Instrument.Name},
                { "@Alias", Instrument.Alias},
                { "@Type", Instrument.Type},
                { "@Model", Instrument.Model},
                { "@SerialNumber", Instrument.SerialNumber},
                { "@Manufacturer", Instrument.Manufacturer},
                { "@Brand", Instrument.Brand},
                { "@SKU", Instrument.SKU},
                { "@WarrantyPeriod", Instrument.WarrantyPeriod},
                { "@ShipmentDate", Instrument.ShipmentDate},
                { "@PurchaseDate", Instrument.PurchaseDate},
                { "@Remarks", Instrument.Remarks}
            });

            return Instrument;
        }

        public InstrumentInstallation SetInstrumentInstallation(string InstrumentID, InstrumentInstallation Installation)
        {
            if (string.IsNullOrEmpty(InstrumentID))
            {
                throw new IDNullException("仪器ID不可为空！");

            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateInstrumentInstallation;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(InstrumentID)},
                { "@Latitude", Installation.Latitude},
                { "@Longitude", Installation.Longitude},
                { "@Location", Installation.Location},
                { "@InstallationDate", Installation.InstallationDate},
                { "@AcceptanceDate", Installation.AcceptanceDate}
            });

            return Installation;
        }


        public InstrumentDistribution SetInstrumentDistribution(string InstrumentID, InstrumentDistribution Distribution)
        {
            if (string.IsNullOrEmpty(InstrumentID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateInstrumentDistribution;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(InstrumentID)},
                { "@Organization", Distribution.Organization},
                { "@Department", Distribution.Department},
                { "@WorkGroup", Distribution.WorkGroup},
            });

            return Distribution;
        }

        public string DeleteInstrument(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("仪器ID不可为空！");

            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteInstrument;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID) } });

            return ID;
        }

        /// <summary>
        /// 获取所有仪器
        /// </summary>
        /// <returns></returns>
        public IList<Core.DomainModel.Instrument> GetInstruments()
        {
            List<Core.DomainModel.Instrument> instruments = null;
            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstruments;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, null);

            if (dbResult != null && dbResult.Count > 0)
            {
                instruments = new List<Core.DomainModel.Instrument>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    instruments.Add(new Core.DomainModel.Instrument() {
                        ID = dbResult[i]["ID"].ToString(),
                        Name = (string)dbResult[i]["Name"],
                        Alias = (string)dbResult[i]["Alias"],
                        Brand = (string)dbResult[i]["Brand"],
                        Type = dbResult[i]["Type"] == null ? 0 : (int?)dbResult[i]["Type"],
                        Manufacturer = (string)dbResult[i]["Manufacturer"],
                        Model = (string)dbResult[i]["Model"],
                        SerialNumber = (string)dbResult[i]["SerialNumber"],
                        SKU = (string)dbResult[i]["SKU"],
                        WarrantyPeriod = dbResult[i]["WarrantyPeriod"] == null ? -1 : (int?)dbResult[i]["WarrantyPeriod"],
                        PurchaseDate = (DateTime?)dbResult[i]["PurchaseDate"],
                        ShipmentDate = (DateTime?)dbResult[i]["ShipmentDate"],
                        Remarks = (string)dbResult[i]["Remarks"]
                    });
                }
            }

            return instruments;
        }

        public IList<Core.DomainModel.Instrument> GetInstruments(int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, Func<object, object> ExtensionFunction)
        {
            List<Core.DomainModel.Instrument> instruments = null;
            TotalPageCount = 0;
            TotalRecords = 0;

            //string[] columnNames = new string[] { "ID", "Name", "Alias", "Brand", "Type", "Model", "SKU", "SerialNumber", "Manufacturer", "WarrantyPeriod", "PurchaseDate", "ShipmentDate", "Remarks" };

            string[] columnNames = new string[] { "ID", "Name", "Alias", "Brand", "Type", "Model", "SKU", "SerialNumber", "Manufacturer", "WarrantyPeriod", "PurchaseDate", "ShipmentDate", "Remarks" };

            var dbResult = this.dmlOperable.ExeReaderWithPaging("Instruments", "Id", "Id", columnNames, CurrentIndex, PageSize, out TotalPageCount, out TotalRecords, ExtensionFunction);

            if (dbResult != null && dbResult.Count > 0)
            {
                instruments = new List<Core.DomainModel.Instrument>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    instruments.Add(new Core.DomainModel.Instrument()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        Name = (string)dbResult[i]["Name"],
                        Alias = (string)dbResult[i]["Alias"],
                        Brand = (string)dbResult[i]["Brand"],
                        Type = dbResult[i]["Type"] == null ? 0 : (int?)dbResult[i]["Type"],
                        Manufacturer = (string)dbResult[i]["Manufacturer"],
                        Model = (string)dbResult[i]["Model"],
                        SerialNumber = (string)dbResult[i]["SerialNumber"],
                        SKU = (string)dbResult[i]["SKU"],
                        WarrantyPeriod = dbResult[i]["WarrantyPeriod"] == null ? -1 : (int?)dbResult[i]["WarrantyPeriod"],
                        PurchaseDate = (DateTime?)dbResult[i]["PurchaseDate"],
                        ShipmentDate = (DateTime?)dbResult[i]["ShipmentDate"],
                        Remarks = (string)dbResult[i]["Remarks"]
                    });
                }
            }

            return instruments;
        }

        /// <summary>
        /// 根据仪器ID获取仪器
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Core.DomainModel.Instrument GetInstrument(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            Core.DomainModel.Instrument instrument = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentBasicById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID)}});

            if (dbResult != null && dbResult.Count == 1)
            {
                instrument = new Core.DomainModel.Instrument() {
                     ID = dbResult[0]["ID"].ToString(),
                     Name = (string)dbResult[0]["Name"],
                     Alias = (string)dbResult[0]["Alias"],
                     Brand = (string)dbResult[0]["Brand"],
                     Type = dbResult[0]["Type"] == null ? 0 : (int?)dbResult[0]["Type"],
                     Manufacturer = (string)dbResult[0]["Manufacturer"],
                     Model = (string)dbResult[0]["Model"],
                     SerialNumber = (string)dbResult[0]["SerialNumber"],
                     SKU = (string)dbResult[0]["SKU"],
                     WarrantyPeriod = dbResult[0]["WarrantyPeriod"] == null ? -1 : (int?)dbResult[0]["WarrantyPeriod"],
                     PurchaseDate =(DateTime?)dbResult[0]["PurchaseDate"],
                     ShipmentDate = (DateTime?)dbResult[0]["ShipmentDate"], 
                     Remarks = (string)dbResult[0]["Remarks"]
                };
            }

            return instrument;
        }

        /// <summary>
        /// 根据仪器ID获取仪器信息，同时输出返回仪器的安装位置信息和派发使用信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Installation">仪器的安装位置信息</param>
        /// <param name="Distribution">仪器的派发使用信息</param>
        /// <returns></returns>
        public Core.DomainModel.Instrument GetInstrument(string ID, out InstrumentInstallation Installation, out InstrumentDistribution Distribution)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            Core.DomainModel.Instrument instrument = null;
            Installation = null;
            Distribution = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                instrument = new Core.DomainModel.Instrument()
                {
                    ID = dbResult[0]["ID"].ToString(), //(string)dbResult[0]["ID"],
                    Name = (string)dbResult[0]["Name"],
                    Alias = (string)dbResult[0]["Alias"],
                    Brand = (string)dbResult[0]["Brand"],
                    Type = dbResult[0]["Type"] == null ? 0 :(int?)dbResult[0]["Type"],
                    Manufacturer = (string)dbResult[0]["Manufacturer"],
                    Model = (string)dbResult[0]["Model"],
                    SerialNumber = (string)dbResult[0]["SerialNumber"],
                    SKU = (string)dbResult[0]["SKU"],
                    WarrantyPeriod = dbResult[0]["WarrantyPeriod"] == null ? 0 : (int?)dbResult[0]["WarrantyPeriod"],
                    PurchaseDate = (DateTime?)dbResult[0]["PurchaseDate"],
                    ShipmentDate = (DateTime?)dbResult[0]["ShipmentDate"],
                    Remarks = (string)dbResult[0]["Remarks"]
                };

                Installation = new InstrumentInstallation(){
                    Instrument = instrument,
                    AcceptanceDate = (DateTime?)dbResult[0]["AcceptanceDate"],
                    InstallationDate = (DateTime?)dbResult[0]["InstallationDate"],
                    Latitude = (string)dbResult[0]["Latitude"],
                    Longitude = (string)dbResult[0]["Longitude"],
                    Location = (string)dbResult[0]["Location"]
                };

                Distribution = new InstrumentDistribution() {
                    Instrument = instrument,
                    Organization = (string)dbResult[0]["Organization"],
                    Department = (string)dbResult[0]["Department"],
                    WorkGroup = (string)dbResult[0]["WorkGroup"],
                };
            }

            return instrument;
        }
        /// <summary>
        /// 根据仪器派发使用ID获取仪器派发使用信息
        /// </summary>
        /// <param name="InstrumentID"></param>
        /// <returns></returns>
        public InstrumentDistribution GetInstrumentDistribution(string InstrumentID)
        {
            if (string.IsNullOrEmpty(InstrumentID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            Core.DomainModel.InstrumentDistribution distribution = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(InstrumentID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                distribution = new InstrumentDistribution(){
                    Instrument = new Core.DomainModel.Instrument(){
                        ID = dbResult[0]["ID"].ToString(),
                        Name = (string)dbResult[0]["Name"],
                        Alias = (string)dbResult[0]["Alias"],
                        Brand = (string)dbResult[0]["Brand"],
                        Type = dbResult[0]["Type"] == null ? 0 :(int?)dbResult[0]["Type"],
                        Manufacturer = (string)dbResult[0]["Manufacturer"],
                        Model = (string)dbResult[0]["Model"],
                        SerialNumber = (string)dbResult[0]["SerialNumber"],
                        SKU = (string)dbResult[0]["SKU"],
                        WarrantyPeriod = dbResult[0]["WarrantyPeriod"] == null ? 0 : (int?)dbResult[0]["WarrantyPeriod"],
                        PurchaseDate = (DateTime?)dbResult[0]["PurchaseDate"],
                        ShipmentDate = (DateTime?)dbResult[0]["ShipmentDate"],
                        Remarks = (string)dbResult[0]["Remarks"]},
                    Organization = (string)dbResult[0]["Organization"],
                    Department = (string)dbResult[0]["Department"],
                    WorkGroup = (string)dbResult[0]["WorkGroup"],
                };
            }

            return distribution;
        }

        /// <summary>
        /// 根据仪器安装位置信息ID获取仪器安装位置信息
        /// </summary>
        /// <param name="InstrumentID"></param>
        /// <returns></returns>
        public InstrumentInstallation GetInstrumentInstallation(string InstrumentID)
        {
            if (string.IsNullOrEmpty(InstrumentID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            Core.DomainModel.InstrumentInstallation installation = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(InstrumentID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                installation = new InstrumentInstallation(){
                    Instrument = new Core.DomainModel.Instrument(){
                        ID = dbResult[0]["ID"].ToString(),
                        Name = (string)dbResult[0]["Name"],
                        Alias = (string)dbResult[0]["Alias"],
                        Brand = (string)dbResult[0]["Brand"],
                        Type = dbResult[0]["Type"] == null ? 0 : (int?)dbResult[0]["Type"],
                        Manufacturer = (string)dbResult[0]["Manufacturer"],
                        Model = (string)dbResult[0]["Model"],
                        SerialNumber = (string)dbResult[0]["SerialNumber"],
                        SKU = (string)dbResult[0]["SKU"],
                        WarrantyPeriod = dbResult[0]["WarrantyPeriod"] == null ? 0 : (int?)dbResult[0]["WarrantyPeriod"],
                        PurchaseDate = (DateTime?)dbResult[0]["PurchaseDate"],
                        ShipmentDate = (DateTime?)dbResult[0]["ShipmentDate"],
                        Remarks = (string)dbResult[0]["Remarks"]},
                    AcceptanceDate = (DateTime?)dbResult[0]["AcceptanceDate"],
                    InstallationDate = (DateTime?)dbResult[0]["InstallationDate"],
                    Latitude = (string)dbResult[0]["Latitude"],
                    Longitude = (string)dbResult[0]["Longitude"],
                    Location = (string)dbResult[0]["Location"]
                };
            }
            return installation;
        }
        public string AddInstrumentCamera(InstrumentCameraSetting CameraSetting)
        {
            //if (CameraSetting == null || string.IsNullOrEmpty(CameraSetting.ID))
            //{
            //    throw new IDNullException("摄像头设置ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertInstrumentCameraSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(CameraSetting.ID)},
                { "@InstrumentID", CameraSetting.Instrument.ID},
                { "@CameraType", CameraSetting.CameraType },
                { "@ConnectionType", CameraSetting.VideoConnectionType},
                { "@Model", CameraSetting.Model},
                { "@SerialNumber", CameraSetting.SerialNumber},
                { "@Manufacturer", CameraSetting.Manufacturer},
                { "@Brand", CameraSetting.Brand},
                { "@SKU", CameraSetting.SKU},
                { "@IpAddress", CameraSetting.CameraIpAddress},
                { "@PortNumber", CameraSetting.CameraPortNumber},
                { "@LoginName", CameraSetting.CameraLoginName},
                { "@Password", CameraSetting.CameraPassword },
                { "@AssemblyName", CameraSetting.AssemblyName},
                { "@AssemblyPath", CameraSetting.AssemblyFilePath },
                { "@ClassName", CameraSetting.ClassName},
                { "@Version", CameraSetting.Version},
                 { "@Index", CameraSetting.Index}
                
            });

            return GenerateInstrumentCameraSettingID(0); //CameraSetting.ID;
        }

        public InstrumentCameraSetting SetInstrumentCamera(string SettingID, InstrumentCameraSetting CameraSetting)
        {
            if (string.IsNullOrEmpty(SettingID))
            {
                throw new IDNullException("仪器设置ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateInstrumentCameraSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(CameraSetting.ID)},
                { "@InstrumentID", CameraSetting.Instrument.ID},
                { "@CameraType", CameraSetting.CameraType },
                { "@ConnectionType", CameraSetting.VideoConnectionType},
                { "@Model", CameraSetting.Model},
                { "@SerialNumber", CameraSetting.SerialNumber},
                { "@Manufacturer", CameraSetting.Manufacturer},
                { "@Brand", CameraSetting.Brand},
                { "@SKU", CameraSetting.SKU},
                { "@IpAddress", CameraSetting.CameraIpAddress},
                { "@PortNumber", CameraSetting.CameraPortNumber},
                { "@LoginName", CameraSetting.CameraLoginName},
                { "@Password", CameraSetting.CameraPassword },
                { "@AssemblyName", CameraSetting.AssemblyName},
                { "@AssemblyPath", CameraSetting.AssemblyFilePath },
                { "@ClassName", CameraSetting.ClassName},
                { "@Version", CameraSetting.Version},
                 { "@Index", CameraSetting.Index}
            });

            return CameraSetting;
        }

        public string DeleteInstrumentCamera(string SettingID)
        {
            if (string.IsNullOrEmpty(SettingID))
            {
                throw new IDNullException("仪器设置ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteInstrumentCameraSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {{ "@ID", long.Parse(SettingID)}});

            return SettingID;
        }

        public IList<InstrumentCameraSetting> GetInstrumentCameraSettings(string InstrumentID)
        {
            if (string.IsNullOrEmpty(InstrumentID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            List<InstrumentCameraSetting> settings = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() {{ "@InstrumentID", long.Parse(InstrumentID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                settings = new List<InstrumentCameraSetting>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    settings.Add(new InstrumentCameraSetting() {
                          ID = dbResult[i]["ID"].ToString(),
                          Instrument = new Core.DomainModel.Instrument() { ID = dbResult[i]["InstrumentID"] != null ? dbResult[i]["InstrumentID"].ToString() : null},
                          Manufacturer = (string)dbResult[i]["Manufacturer"],
                          Model = (string)dbResult[i]["Model"],
                          SerialNumber = (string)dbResult[i]["SerialNumber"],
                          SKU = (string)dbResult[i]["SKU"],
                          Brand = (string)dbResult[i]["Brand"],
                          CameraType = (CameraTypeEnum)dbResult[i]["CameraType"],
                          VideoConnectionType = (VideoConnectionTypeEnum)dbResult[i]["ConnectionType"],
                          CameraIpAddress = (string)dbResult[i]["IpAddress"],
                          CameraPortNumber = dbResult[i]["PortNumber"] == null ? 0 : (int?)dbResult[i]["PortNumber"],
                          CameraLoginName = (string)dbResult[i]["LoginName"],
                          CameraPassword = (string)dbResult[i]["Password"],
                          AssemblyFilePath = (string)dbResult[i]["AssemblyPath"],
                          AssemblyName = (string)dbResult[i]["AssemblyName"],
                          ClassName = (string)dbResult[i]["ClassName"],
                          Version = (string)dbResult[i]["Version"],
                        Index = (int?)dbResult[i]["Index"],
                        DynamicProperties = new Dictionary<string, object>() { { "Remarks", (string)dbResult[i]["Remarks"] } }
                    });
                }
            }

            return settings;
        }

        public string AddInstrumentCommunication(InstrumentCommunicationSetting CommunicationSetting)
        {
            //if (CommunicationSetting == null || string.IsNullOrEmpty(CommunicationSetting.ID))
            //{
            //    throw new IDNullException("通讯设置ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertInstrumentCommunicationSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(CommunicationSetting.ID)},
                { "@InstrumentID", CommunicationSetting.Instrument.ID},
                { "@Type", CommunicationSetting.CommunicationType},
                { "@IpAddress", CommunicationSetting.IpAddress},
                { "@PortNumber", CommunicationSetting.PortNumber},
                { "@SerialPortName", CommunicationSetting.SerialPortName},
                { "@SerialPortBaudRate", CommunicationSetting.SerialPortBaudRate},
                { "@BluetoothDeviceName", CommunicationSetting.BluetoothDeviceName},
                { "@BluetoothAddress", CommunicationSetting.BluetoothAddress},
                { "@BluetoothKey", CommunicationSetting.BluetoothKey},
                { "@Protocol",  CommunicationSetting.Protocol},
                { "@AssemblyName",  CommunicationSetting.AssemblyName},
                { "@AssemblyPath",  CommunicationSetting.AssemblyFilePath },
                { "@ClassName",  CommunicationSetting.ClassName},
                { "@Version",  CommunicationSetting.Version},
            });

            return GenerateInstrumentCommunicationSettingID(0); //CommunicationSetting.ID;
        }

        public InstrumentCommunicationSetting SetInstrumentCommunication(string SettingID, InstrumentCommunicationSetting CommunicationSetting)
        {
            if (string.IsNullOrEmpty(SettingID))
            {
                throw new IDNullException("仪器设置ID不可为空！");
            }
            int intI = 0;
            if (CommunicationSetting.CommunicationType == CommunicationTypeEnum.TcpIpOverEthernet)
            {
                intI = 3;
            }
            else if (CommunicationSetting.CommunicationType == CommunicationTypeEnum.SerialPort)
            {
                intI = 1;
            }
            else if (CommunicationSetting.CommunicationType == CommunicationTypeEnum.Bluetooth)
            {
                intI = 2;
            }

            //            string sqlCommandText = "UPDATE InstrumentCommnunicationSettings SET " + "ics_Type = " + intI + ", " +
            //"ics_IpAddress = '" + CommunicationSetting.IpAddress + "', " +
            //"ics_PortNumber = " + CommunicationSetting.PortNumber + ", " +
            //"ics_SerialPortName ='" + CommunicationSetting.SerialPortName + "', " +
            //"ics_SerialPortBaudRate =" + CommunicationSetting.SerialPortBaudRate + ", " +
            //"ics_BluetoothDeviceName = '" + CommunicationSetting.BluetoothDeviceName + "'," +
            //"ics_BluetoothAddress = '" + CommunicationSetting.BluetoothAddress + "', " +
            //"ics_BluetoothKey = '" + CommunicationSetting.BluetoothKey + "' WHERE InstrumentID = " + SettingID + "; ";

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateInstrumentCommunicationSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@InstrumentID", CommunicationSetting.Instrument.ID},
                { "@Type", intI},
                { "@IpAddress", CommunicationSetting.IpAddress},
                { "@PortNumber", CommunicationSetting.PortNumber},
                { "@SerialPortName", CommunicationSetting.SerialPortName},
                { "@SerialPortBaudRate", CommunicationSetting.SerialPortBaudRate},
                { "@BluetoothDeviceName", CommunicationSetting.BluetoothDeviceName},
                { "@BluetoothAddress", CommunicationSetting.BluetoothAddress},
                { "@BluetoothKey", CommunicationSetting.BluetoothKey},
                { "@Protocol",  CommunicationSetting.Protocol},
                { "@AssemblyName",  CommunicationSetting.AssemblyName},
                { "@AssemblyPath",  CommunicationSetting.AssemblyFilePath },
                { "@ClassName",  CommunicationSetting.ClassName},
                { "@Version",  CommunicationSetting.Version},
            });

            return CommunicationSetting;
        }


        public string DeleteInstrumentCommunication(string SettingID)
        {
            if (string.IsNullOrEmpty(SettingID))
            {
                throw new IDNullException("仪器设置ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteInstrumentCommunicationSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(SettingID) } });

            return SettingID;
        }

        public IList<InstrumentCommunicationSetting> GetInstrumentCommunicationSettings(string InstrumentID)
        {
            if (string.IsNullOrEmpty(InstrumentID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            List<InstrumentCommunicationSetting> settings = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentCommunicationSettingById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@InstrumentID", long.Parse(InstrumentID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                settings = new List<InstrumentCommunicationSetting>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    settings.Add(new InstrumentCommunicationSetting()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        Instrument = new Core.DomainModel.Instrument() { ID = dbResult[i]["InstrumentID"].ToString() },
                        CommunicationType = (CommunicationTypeEnum)dbResult[i]["Type"],
                        IpAddress = (string)dbResult[i]["IpAddress"],
                        PortNumber = dbResult[i]["PortNumber"] == null ? null : (int?)dbResult[i]["PortNumber"],
                        SerialPortName = (string)dbResult[i]["SerialPortName"],
                        SerialPortBaudRate = (int?)dbResult[i]["SerialPortBaudRate"],
                        BluetoothAddress = (string)dbResult[i]["BluetoothAddress"],
                        BluetoothDeviceName = (string)dbResult[i]["BluetoothDeviceName"],
                        BluetoothKey = (string)dbResult[i]["BluetoothKey"],
                        Protocol = (string)dbResult[0]["Protocol"],
                        AssemblyFilePath = (string)dbResult[0]["AssemblyPath"],
                        AssemblyName = (string)dbResult[0]["AssemblyName"],
                        ClassName = (string)dbResult[0]["ClassName"],
                        Version = (string)dbResult[0]["Version"],
                        DynamicProperties = new Dictionary<string, object>() { { "Remarks", (string)dbResult[i]["Remarks"] } }
                    });
                }
            }

            return settings;
        }

        public System.Data.DataTable GetInstrumentDataTable()
        {
            return this.dmlOperable.GetDataTable(ModuleConfiguration.SQL_CMD_SelectDateTableInstrumentCamera);
        }

        public string GetName(string strName, string strID)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentCountByName;

            string strNameCount = this.dmlOperable.ExeSqlScalar(sqlCommandText, new Dictionary<string, object>() { { "@Name", strName }, { "@ID", strID } });

            return strNameCount;
        }
        //根据仪器ID获取通讯信息
        public InstrumentCommunicationSetting GetInstrumentCommunicationSetting(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            Core.DomainModel.InstrumentCommunicationSetting instrument = null;
            string sqlCommandText = "";

                sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentCommunicationSettingById;


            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@InstrumentID", long.Parse(ID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                instrument = new Core.DomainModel.InstrumentCommunicationSetting()
                {
                    ID = dbResult[0]["ID"].ToString(),
                    CommunicationType = (CommunicationTypeEnum)dbResult[0]["Type"],
                    IpAddress = (string)dbResult[0]["IpAddress"],
                    PortNumber = (int?)dbResult[0]["PortNumber"],
                    SerialPortName = (string)dbResult[0]["SerialPortName"],
                    SerialPortBaudRate = (int?)dbResult[0]["SerialPortBaudRate"],
                    BluetoothAddress = (string)dbResult[0]["BluetoothAddress"],
                    BluetoothDeviceName = (string)dbResult[0]["BluetoothDeviceName"],
                    BluetoothKey = (string)dbResult[0]["BluetoothKey"],
                    Protocol = (string)dbResult[0]["Protocol"],
                    AssemblyFilePath = (string)dbResult[0]["AssemblyPath"],
                    AssemblyName = (string)dbResult[0]["AssemblyName"],
                    ClassName = (string)dbResult[0]["ClassName"],
                    Version = (string)dbResult[0]["Version"],
                    DynamicProperties = new Dictionary<string, object>() { { "Remarks", (string)dbResult[0]["Remarks"] } }
                };
            }

            return instrument;
        }

        //根据仪器ID获取视频信息
        public InstrumentCameraSetting GetInstrumentCameraSetting(string InstrumentID,int Index)
        {
            if (string.IsNullOrEmpty(InstrumentID))
            {
                throw new IDNullException("仪器ID不可为空！");
            }

            Core.DomainModel.InstrumentCameraSetting distribution = null;

            string sqlCommandText = "";
            if (Index == 1)
            {
                sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById1;
            }
            else
            {
                sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById2;
            }
            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@InstrumentID", long.Parse(InstrumentID) } });
            if (dbResult != null && dbResult.Count == 1)
            {
                distribution = new Core.DomainModel.InstrumentCameraSetting()
                {
                    ID = dbResult[0]["ID"].ToString(),
                    Manufacturer = (string)dbResult[0]["Manufacturer"],
                    Model = (string)dbResult[0]["Model"],
                    SerialNumber = (string)dbResult[0]["SerialNumber"],
                    SKU = (string)dbResult[0]["SKU"],
                    Brand = (string)dbResult[0]["Brand"],
                    CameraType = (CameraTypeEnum)dbResult[0]["CameraType"],
                    VideoConnectionType = (VideoConnectionTypeEnum)dbResult[0]["ConnectionType"],
                    CameraIpAddress = (string)dbResult[0]["IpAddress"],
                    CameraPortNumber = (int?)dbResult[0]["PortNumber"],
                    CameraLoginName = (string)dbResult[0]["LoginName"],
                    CameraPassword = (string)dbResult[0]["Password"],
                    AssemblyFilePath = (string)dbResult[0]["AssemblyPath"],
                    AssemblyName = (string)dbResult[0]["AssemblyName"],
                    ClassName = (string)dbResult[0]["ClassName"],
                    Version = (string)dbResult[0]["Version"],
                    Index = (int?)dbResult[0]["Index"],
                    DynamicProperties = new Dictionary<string, object>() { { "Remarks", (string)dbResult[0]["Remarks"] } }
                };
            }
            return distribution;
        }

        public string GenerateInstrumentID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxInstrumentID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }

        public string GenerateInstrumentCommunicationSettingID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxInstrumentCommunicationSettingID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }

        public string GenerateInstrumentCameraSettingID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxInstrumentCameraSettingID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }
    }
}
