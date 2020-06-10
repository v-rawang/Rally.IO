using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public interface IUserManager
    {
        T AddUser<T>(string UserName, string Password, Func<object, T> ExtensionFunction);

        T AddUser<T>(string UserID, string UserName, string Password, Func<object, T> ExtensionFunction);

        bool UserExists(string UserID, string UserName);

        T DeleteUser<T>(string UserID, string UserName, Func<object, T> ExtensionFunction);

        T ChangeUserName<T>(string UserID, string NewUserName, Func<object, T> ExtensionFunction);
    }
}
