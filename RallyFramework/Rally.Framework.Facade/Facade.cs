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
using Rally.Framework.Printing;
using Rally.Framework.Logging;
using Rally.Framework.System;
using Rally.Framework.Instrument;
using Rally.Framework.Maintenance;
using Rally.Framework.Camera;

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
            return AuthenticationManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IUserManager CreateUserManager()
        {
            return UserManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true));
        }

        public static IToken CreateTokenManager()
        {
            return AuthenticationManager.NewInstance(Factory.CreateDMLOperable(Global.CurrentDBConnectionString, Global.CurrentDBType), (DBTypeEnum)Enum.Parse(typeof(DBTypeEnum), Global.CurrentDBType, true)) as IToken;
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

        public static IPrinterManager CreatePrinterManager()
        {
            return PrinterManager.NewInstance();
        }

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
    }
}
