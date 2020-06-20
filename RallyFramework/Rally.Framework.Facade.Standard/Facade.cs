using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Framework.Account;
using Rally.Framework.Authentication;
using Rally.Framework.Authorization;
using Rally.Framework.Content;
using Rally.Framework.File;
//using Rally.Framework.Printing;
using Rally.Framework.Logging;
using Rally.Framework.System;
using Rally.Framework.Instrument;
using Rally.Framework.Maintenance;
using Rally.Framework.Camera;
using Rally.Framework.Protocol;
using Rally.Framework.Nuclide;
using System.Net;
using Rally.Lib.Utility.Common;

namespace Rally.Framework.Facade
{
    public class Facade
    {
        public static IAccountManager CreateAccountManager()
        {
            return AccountManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IAuthentication CreateAuthenticationManager()
        {
            return Rally.Framework.Authentication.AuthenticationManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IUserManager CreateUserManager()
        {
            return UserManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IToken CreateTokenManager()
        {
            return Rally.Framework.Authentication.AuthenticationManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true)) as IToken;
        }

        public static IAuthorization CreateAuthorizationManager()
        {
            return AuthorizationManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IReportManager CreateReportManager()
        {
            return ReportManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IFileManager CreateFileManager()
        {
            return FileManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        //public static IPrinterManager CreatePrinterManager()
        //{
        //    return PrinterManager.NewInstance();
        //}

        public static IApplicationManager CreateApplicationManager()
        {
            return SystemManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IInstrumentManager CreateInstrumentManager()
        {
            //return InstrumentManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), Global.CurrentDBType);
            return InstrumentManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IMaintenanceManager CreateMaintenanceManager()
        {
            return MaintenanceManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static ICameraManager CreateCameraManager()
        {
            return CameraManager.NewInstance();
        }

        public static ILogManager CreateLogManager()
        {
            return LogHandler.NewInstance();
        }

        public static ILogQueryable CreateLogQueryable()
        {
            return LogQueryable.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IExHandler CreateExceptionHandler()
        {
            return ExceptionHandler.NewInstance();
        }

        public static ITracer CreateTracer()
        {
            return Tracer.NewInstance();
        }

        public static IProtocolManager CreateProtocolManager() 
        {
            return ProtocolManager.NewInstance();
        }

        public static INuclideManager CreateNuclideManager() 
        {
            return NuclideManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static void SetModuleSQLStatements() 
        {
            if (Global.CurrentDBType == "PostgreSQL")
            {
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountById);
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountByMobile = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountByMobile);
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountByNickName = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountByNickName);
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountHeadImageById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountHeadImageById);
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountProviderByAppId = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountProviderByAppId);
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountProviderById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountProviderById);
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountsAll = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectAccountsAll);
                Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectExternalAccountsByAccountId = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Account.ModuleConfiguration.SQL_CMD_SelectExternalAccountsByAccountId);

                Rally.Framework.Content.ModuleConfiguration.SQL_CMD_SelectMaxReportSettingID = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Content.ModuleConfiguration.SQL_CMD_SelectMaxReportSettingID);

                Rally.Framework.File.ModuleConfiguration.SQL_CMD_SelectFile = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.File.ModuleConfiguration.SQL_CMD_SelectFile);
                Rally.Framework.File.ModuleConfiguration.SQL_CMD_SelectFileById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.File.ModuleConfiguration.SQL_CMD_SelectFileById);
                Rally.Framework.File.ModuleConfiguration.SQL_CMD_SelectFileData = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.File.ModuleConfiguration.SQL_CMD_SelectFileData);

                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentBasicById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentBasicById);
                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentBasicById);
                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectDateTableInstrumentCamera = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectDateTableInstrumentCamera);
                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById);
                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById1 = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById1);
                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById2 = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCameraSettingById2);
                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCommunicationSettingById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstrumentCommunicationSettingById);
                Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstruments = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Instrument.ModuleConfiguration.SQL_CMD_SelectInstruments);

                Rally.Framework.Logging.ModuleConfiguration.SQL_CMD_Logs = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Logging.ModuleConfiguration.SQL_CMD_Logs);

                Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectFaultById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectFaultById);
                Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectInstrumentFault = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectInstrumentFault);
                Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrder = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrder);
                Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrderLineItem = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrderLineItem);
                Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderAttachment = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderAttachment);
                Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderById);
                Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_MaintenanceOrderData = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Maintenance.ModuleConfiguration.SQL_CMD_MaintenanceOrderData);

                Rally.Framework.Nuclide.ModuleConfiguration.SQL_CMD_GetEngergyCalibrationByInstrumentSerial = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Nuclide.ModuleConfiguration.SQL_CMD_GetEngergyCalibrationByInstrumentSerial);
                Rally.Framework.Nuclide.ModuleConfiguration.SQL_CMD_SelectNiclideById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Nuclide.ModuleConfiguration.SQL_CMD_SelectNiclideById);
                Rally.Framework.Nuclide.ModuleConfiguration.SQL_CMD_SelectNiclides = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.Nuclide.ModuleConfiguration.SQL_CMD_SelectNiclides);

                Rally.Framework.System.ModuleConfiguration.SQL_CMD_SelectApplicationSettingById = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.System.ModuleConfiguration.SQL_CMD_SelectApplicationSettingById);
                Rally.Framework.System.ModuleConfiguration.SQL_CMD_SelectApplicationSettings = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(Rally.Framework.System.ModuleConfiguration.SQL_CMD_SelectApplicationSettings);
            }
        }
    }
}
