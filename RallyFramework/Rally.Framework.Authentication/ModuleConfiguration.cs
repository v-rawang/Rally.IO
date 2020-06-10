using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Authentication
{
    public class ModuleConfiguration
    {
        public static int Default_Password_Expiration_Days = 30;

        public static int Default_Temp_Token_Expiration_Minutes = 20;

        public static string DefaultFixedUserPassword = "P@ssword1";

        public static string DefaultFixedUserEmailTemplate = "{0}@default.org";
    }
}
