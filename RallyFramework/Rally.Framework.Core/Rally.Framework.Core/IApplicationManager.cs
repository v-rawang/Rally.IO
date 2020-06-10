using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IApplicationManager
    {
        string AddApplicationSetting(ApplicationSetting Setting);
        ApplicationSetting UpdateApplicationSetting(string ID, ApplicationSetting Setting);
        ApplicationSetting UpdateSetting(string ID, ApplicationSetting Setting);
        
        ApplicationSetting GetApplicationSetting(string ID);
        IList<ApplicationSetting> GetApplicationSettings();
        string RemoveApplicationSetting(string ID);
        string GenerateApplicationSettingID(int Increment);
    }
}
