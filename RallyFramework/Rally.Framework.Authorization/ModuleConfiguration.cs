using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Authorization
{
    public class ModuleConfiguration
    {
        public static string DefaultResourceACConfigurationFilePath = "ac-items-config.xml";

        public static bool ShouldDeleteObsoleteOperationsOnRegistration = true;

        public static bool ShouldDeleteObsoleteDataScopesOnRegistration = true;

        public static bool ShouldDeleteObsoleteRolesOnRegistration = false;

        public static bool ShouldDeleteObsoleteUsersOnRegistration = false;

        public static string DefaultSystemSupperUserID = "USR-SYS-BUILTIN-SUPPER";
    }
}
