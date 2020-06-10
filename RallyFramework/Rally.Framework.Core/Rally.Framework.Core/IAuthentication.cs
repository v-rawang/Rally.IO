using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IAuthentication
    {
        bool Login(string UserName, string Password);
        bool Login<T>(string UserName, string Password, out T ExtraData, Func<object,T> ExtensionFunction);
        Token ValidateUser(string UserName, string Password, string Key);
        Token ValidateUser<T>(string UserName, string Password, string Key, out T ExtraData, Func<object, T> ExtensionFunction);
        bool ValidateToken(string AccessToken, string Key);
        bool ValidateToken<T>(string AccessToken, string Key, out T ExtraData, Func<object, T> ExtensionFunction);
        bool ChangePassword(string UserName, string NewPassword);
    }
}
