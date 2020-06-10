using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using System.Collections;

namespace Rally.Framework.System
{
    public class SystemManager : IApplicationManager
    {
        public SystemManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static IApplicationManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new SystemManager(DMLOperable, DBType);
        }

        public string AddApplicationSetting(ApplicationSetting Setting)
        {
            //if (Setting == null || string.IsNullOrEmpty(Setting.ID))
            //{
            //    throw new IDNullException("应用程序设置ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertApplicationSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(Setting.ID)},
                //{ "@PlatformIpAddress", Setting.PlatformIpAddress},
                //{ "@PlatformPortNumber", Setting.PlatformPortNumber},
                { "@AlarmMode",Setting.AlarmMode},
                { "@AlarmSound", Setting.AlarmSound},
                { "@NotificationMode", Setting.NotificationMode},
                { "@NotificationSound", Setting.NotificationSound},
                { "@Language", Setting.Language}
            });

            return GenerateApplicationSettingID(0); //Setting.ID;
        }

        public ApplicationSetting UpdateApplicationSetting(string ID, ApplicationSetting Setting)
        {
            if (Setting == null || string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("应用程序设置ID不可为空！");
            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateApplicationSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(ID)},
                { "@PlatformIpAddress", Setting.Platform[0].PlatformIpAddress},
                { "@PlatformPortNumber", Setting.Platform[0].PlatformPortNumber},
                { "@PlatformName", Setting.Platform[0].PlatformName},
                { "@PlatformProtocol", Setting.Platform[0].PlatformProtocol},
                { "@PlatformName2", Setting.Platform[1].PlatformName},
                { "@PlatformIpAddress2", Setting.Platform[1].PlatformIpAddress},
                { "@PlatformPortNumber2", Setting.Platform[1].PlatformPortNumber},
                { "@PlatformProtocol2", Setting.Platform[1].PlatformProtocol},
                { "@PlatformName3", Setting.Platform[2].PlatformName},
                { "@PlatformIpAddress3", Setting.Platform[2].PlatformIpAddress},
                { "@PlatformPortNumber3", Setting.Platform[2].PlatformPortNumber},
                { "@PlatformProtocol3", Setting.Platform[2].PlatformProtocol},
                { "@PlatformName4", Setting.Platform[3].PlatformName},
                { "@PlatformIpAddress4", Setting.Platform[3].PlatformIpAddress},
                { "@PlatformPortNumber4", Setting.Platform[3].PlatformPortNumber},
                { "@PlatformProtocol4", Setting.Platform[3].PlatformProtocol},
                { "@PlatformTheme", Setting.theme},
                { "@PlatformTime", Setting.Time},
                { "@PlatformType", Setting.Type},
                { "@PlatformDuration2", Setting.Notificationduration},
                { "@PlatformDuration1", Setting.Alarmduration},
                { "@AlarmMode",Setting.AlarmMode},
                { "@AlarmSound", Setting.AlarmSound},
                { "@NotificationMode", Setting.NotificationMode},
                { "@NotificationSound", Setting.NotificationSound},
                { "@Language", Setting.Language},
                { "@BgValArchFrequency", Setting.BackgroundDataArchivingFrequency},
                { "@ImageCapturingMode", Setting.ImageCapturingMode},
                { "@ImageCapturingCount", Setting.ImageCapturingCount},
                { "@VideoCapturingMode", Setting.VideoCapturingMode},
                { "@VideoCapturingLength", Setting.VideoCapturingLength}
            });
            return Setting;
        }

        public ApplicationSetting UpdateSetting(string ID, ApplicationSetting Setting)
        {
            if (Setting == null || string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("应用程序设置ID不可为空！");
            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateSetting;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(ID)},
                { "@PlatformTime", Setting.Time},
                { "@PlatformType", Setting.Type},
            });
            return Setting;
        }


        public ApplicationSetting GetApplicationSetting(string ID)
        {
            ApplicationSetting apl = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectApplicationSettingById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID) } });

            if (dbResult != null && dbResult.Count == 1)
            {

                apl = new ApplicationSetting();
                apl.ID = dbResult[0]["ID"].ToString();
                Platform1 pl1 = new Platform1();
                Platform1 pl2 = new Platform1();
                Platform1 pl3 = new Platform1();
                Platform1 pl4 = new Platform1();

                apl.ID = dbResult[0]["ID"].ToString();
                pl1.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress"];
                pl1.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber"]);
                pl1.PlatformName = (string)dbResult[0]["PlatformName"];
                pl1.PlatformProtocol = (string)dbResult[0]["PlatformProtocol"];
                pl2.PlatformName = (string)dbResult[0]["PlatformName2"];
                pl2.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress2"];
                pl2.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber2"]);
                pl2.PlatformProtocol = (string)dbResult[0]["PlatformProtocol2"];
                pl3.PlatformName = (string)dbResult[0]["PlatformName3"];
                pl3.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress3"];
                pl3.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber3"]);
                pl3.PlatformProtocol = (string)dbResult[0]["PlatformProtocol3"];
                pl4.PlatformName = (string)dbResult[0]["PlatformName4"];
                pl4.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress4"];
                pl4.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber4"]);


                apl.Platform.Add(pl1);
                apl.Platform.Add(pl2);
                apl.Platform.Add(pl3);
                apl.Platform.Add(pl4);
                apl.theme = (string)dbResult[0]["theme"];
                apl.Time = (string)dbResult[0]["Time"];
                apl.Type = (string)dbResult[0]["Type"];
                apl.Notificationduration = (string)dbResult[0]["Notificationduration"];
                apl.Alarmduration = (string)dbResult[0]["Alarmduration"];
                apl.AlarmMode = (int?)dbResult[0]["AlarmMode"];
                apl.AlarmSound = (string)dbResult[0]["AlarmSound"];
                apl.NotificationMode = (int?)dbResult[0]["NotificationMode"];
                apl.NotificationSound = (string)dbResult[0]["NotificationSound"];
                apl.Language = (int?)dbResult[0]["Language"];

                apl.BackgroundDataArchivingFrequency = (int?)dbResult[0]["BgValArchFrequency"];
                apl.ImageCapturingMode = (int?)dbResult[0]["ImageCapturingMode"];
                apl.ImageCapturingCount = (int?)dbResult[0]["ImageCapturingCount"];
                apl.VideoCapturingMode = (int?)dbResult[0]["VideoCapturingMode"];
                apl.VideoCapturingLength = (int?)dbResult[0]["VideoCapturingLength"];

            }

            return apl;
        }

        public IList<ApplicationSetting> GetApplicationSettings()
        {
            IList<ApplicationSetting> apl = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectApplicationSettings;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, null);

            if (dbResult != null && dbResult.Count > 0)
            {
                apl = new List<ApplicationSetting>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    ApplicationSetting applicationSetting = new ApplicationSetting();
                    List<Platform1> pl11 = new List<Platform1>();
                    Platform1 pl1 = new Platform1();
                    Platform1 pl2 = new Platform1();
                    Platform1 pl3 = new Platform1();
                    Platform1 pl4 = new Platform1();

                    applicationSetting.ID = dbResult[0]["ID"].ToString();
                    pl1.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress"];
                    pl1.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber"]);
                    pl1.PlatformName = (string)dbResult[0]["PlatformName"];
                    pl1.PlatformProtocol = (string)dbResult[0]["PlatformProtocol"];
                    pl2.PlatformName = (string)dbResult[0]["PlatformName2"];
                    pl2.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress2"];
                    pl2.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber2"]);
                    pl2.PlatformProtocol = (string)dbResult[0]["PlatformProtocol2"];
                    pl3.PlatformName = (string)dbResult[0]["PlatformName3"];
                    pl3.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress3"];
                    pl3.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber3"]);
                    pl3.PlatformProtocol = (string)dbResult[0]["PlatformProtocol3"];
                    pl4.PlatformName = (string)dbResult[0]["PlatformName4"];
                    pl4.PlatformIpAddress = (string)dbResult[0]["PlatformIpAddress4"];
                    pl4.PlatformPortNumber = Convert.ToInt32(dbResult[0]["PlatformPortNumber4"]);
                    pl11.Add(pl1);
                    pl11.Add(pl2);
                    pl11.Add(pl3);
                    pl11.Add(pl4);
                    applicationSetting.Platform = pl11;

                    pl4.PlatformProtocol = (string)dbResult[0]["PlatformProtocol4"];
                    applicationSetting.theme = (string)dbResult[0]["PlatformTheme"];
                    applicationSetting.Time = (string)dbResult[0]["PlatformTime"];
                    applicationSetting.Type = (string)dbResult[0]["PlatformType"];
                    applicationSetting.Notificationduration = (string)dbResult[0]["Notificationduration"];
                    applicationSetting.Alarmduration = (string)dbResult[0]["Alarmduration"];
                    applicationSetting.AlarmMode = (int?)dbResult[0]["AlarmMode"];
                    applicationSetting.AlarmSound = (string)dbResult[0]["AlarmSound"];
                    applicationSetting.NotificationMode = (int?)dbResult[0]["NotificationMode"];
                    applicationSetting.NotificationSound = (string)dbResult[0]["NotificationSound"];
                    applicationSetting.Language = (int?)dbResult[0]["Language"];

                    applicationSetting.BackgroundDataArchivingFrequency = (int?)dbResult[0]["BgValArchFrequency"];
                    applicationSetting.ImageCapturingMode = (int?)dbResult[0]["ImageCapturingMode"];
                    applicationSetting.ImageCapturingCount = (int?)dbResult[0]["ImageCapturingCount"];
                    applicationSetting.VideoCapturingMode = (int?)dbResult[0]["VideoCapturingMode"];
                    applicationSetting.VideoCapturingLength = (int?)dbResult[0]["VideoCapturingLength"];

                    apl.Add(applicationSetting);

                }
            }

            return apl;
        }

        public string RemoveApplicationSetting(string ID)
        {
            throw new NotImplementedException();
        }

        public string GenerateApplicationSettingID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxApplicationSettingID);

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
