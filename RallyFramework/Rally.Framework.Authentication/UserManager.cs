using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Lib.Utility.Encryption;
using Rally.Lib.Utility.Common;

namespace Rally.Framework.Authentication
{
    public class UserManager : IUserManager
    {
        public UserManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dbType = DBType;
        }
        
        private IDMLOperable dmlOperable;
        private DBTypeEnum dbType;

        public static IUserManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new UserManager(DMLOperable, DBType);
        }

        public T AddUser<T>(string UserName, string Password, Func<object, T> ExtensionFunction)
        {
            T resultObj = default(T);

            //string sqlTxt = "INSERT INTO users (Id, UserName, UserType, PasswordHash, PasswordSalt, PasswordRev, PasswordChangedDate, LastPasswordFailureDate, PasswordFailuresSinceLastSuccess, AccessFailedCount, TwoFactorEnabled, LockoutEnabled, LockoutEndDateUtc, CreateDate, IsConfirmed, EmailConfirmed, PhoneNumberConfirmed) VALUES(@Id, @UserName, @UserType, @PasswordHash, @PasswordSalt, @PasswordRev, @PasswordChangedDate, @LastPasswordFailureDate, @PasswordFailuresSinceLastSuccess, @AccessFailedCount, @TwoFactorEnabled, @LockoutEnabled, @LockoutEndDateUtc, @CreateDate, @IsConfirmed, @EmailConfirmed, @PhoneNumberConfirmed)";

            string sqlTxt = "INSERT INTO users (Id, UserName, UserType, PasswordHash, PasswordSalt, PasswordRev, PasswordChangedDate, LastPasswordFailureDate, PasswordFailuresSinceLastSuccess, AccessFailedCount, TwoFactorEnabled, LockoutEnabled, LockoutEndDateUtc, CreateDate, IsConfirmed, EmailConfirmed, PhoneNumberConfirmed) VALUES(@Id, @UserName, @UserType, @PasswordHash, @PasswordSalt, @PasswordRev, @PasswordChangedDate, @LastPasswordFailureDate, @PasswordFailuresSinceLastSuccess, @AccessFailedCount, @TwoFactorEnabled, @LockoutEnabled, @LockoutEndDateUtc, @CreateDate, @IsConfirmed, @EmailConfirmed, @PhoneNumberConfirmed)";

            string userId = Guid.NewGuid().ToString();
            string passwordSalt = HashUtility.GenerateRandomString(4);
            string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{Password}:{passwordSalt}");
            DateTime currDate = DateTime.Now;

            int dbResult = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@Id", userId},
                { "@UserName", UserName},
                { "@UserType", 0},
                { "@PasswordHash", hashedPassword},
                { "@PasswordSalt", passwordSalt},
                { "@PasswordRev",0 },
                { "@PasswordChangedDate", currDate },
                { "@LastPasswordFailureDate", currDate },
                { "@PasswordFailuresSinceLastSuccess", 0},
                { "@AccessFailedCount",0 },
                { "@TwoFactorEnabled", 0},
                { "@LockoutEnabled", 0},
                { "@LockoutEndDateUtc", currDate.ToUniversalTime()},
                { "@CreateDate",currDate },
                { "@IsConfirmed", 0},
                { "@EmailConfirmed", 0},
                { "@PhoneNumberConfirmed", 0}});

            if (dbResult >= 0)
            {
                if (ExtensionFunction != null)
                {
                   resultObj = ExtensionFunction(userId);
                }
                else
                {
                    resultObj =  (T)((object)(new { ID = userId }));
                }
            }

            return resultObj;
        }

        public T AddUser<T>(string UserID, string UserName, string Password, Func<object, T> ExtensionFunction)
        {
            T resultObj = default(T);

            //string sqlTxt = "INSERT INTO users (Id, UserName, UserType, PasswordHash, PasswordSalt, PasswordRev, PasswordChangedDate, LastPasswordFailureDate, PasswordFailuresSinceLastSuccess, AccessFailedCount, TwoFactorEnabled, LockoutEnabled, LockoutEndDateUtc, CreateDate, IsConfirmed, EmailConfirmed, PhoneNumberConfirmed) VALUES(@Id, @UserName, @UserType, @PasswordHash, @PasswordSalt, @PasswordRev, @PasswordChangedDate, @LastPasswordFailureDate, @PasswordFailuresSinceLastSuccess, @AccessFailedCount, @TwoFactorEnabled, @LockoutEnabled, @LockoutEndDateUtc, @CreateDate, @IsConfirmed, @EmailConfirmed, @PhoneNumberConfirmed)";

            string sqlTxt = "INSERT INTO users (Id, UserName, UserType, PasswordHash, PasswordSalt, PasswordRev, PasswordChangedDate, LastPasswordFailureDate, PasswordFailuresSinceLastSuccess, AccessFailedCount, TwoFactorEnabled, LockoutEnabled, LockoutEndDateUtc, CreateDate, IsConfirmed, EmailConfirmed, PhoneNumberConfirmed) VALUES(@Id, @UserName, @UserType, @PasswordHash, @PasswordSalt, @PasswordRev, @PasswordChangedDate, @LastPasswordFailureDate, @PasswordFailuresSinceLastSuccess, @AccessFailedCount, @TwoFactorEnabled, @LockoutEnabled, @LockoutEndDateUtc, @CreateDate, @IsConfirmed, @EmailConfirmed, @PhoneNumberConfirmed)";


            string userId = !string.IsNullOrEmpty(UserID) ? UserID : Guid.NewGuid().ToString();
            string passwordSalt = HashUtility.GenerateRandomString(4);
            string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{Password}:{passwordSalt}");
            DateTime currDate = DateTime.Now;

            int dbResult = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@Id", userId},
                { "@UserName", UserName},
                { "@UserType", 0},
                { "@PasswordHash", hashedPassword},
                { "@PasswordSalt", passwordSalt},
                { "@PasswordRev",0 },
                { "@PasswordChangedDate", currDate },
                { "@LastPasswordFailureDate", currDate },
                { "@PasswordFailuresSinceLastSuccess", 0},
                { "@AccessFailedCount",0 },
                { "@TwoFactorEnabled", 0},
                { "@LockoutEnabled", 0},
                { "@LockoutEndDateUtc", currDate.ToUniversalTime()},
                { "@CreateDate",currDate },
                { "@IsConfirmed", 0},
                { "@EmailConfirmed", 0},
                { "@PhoneNumberConfirmed", 0}});

            if (dbResult >= 0)
            {
                if (ExtensionFunction != null)
                {
                    resultObj = ExtensionFunction(userId);
                }
                else
                {
                    resultObj = (T)((object)(new { ID = userId }));
                }
            }

            return resultObj;
        }

        public bool UserExists(string UserID, string UserName)
        {
            if (string.IsNullOrEmpty(UserID) && string.IsNullOrEmpty(UserName))
            {
                return false;
            }

            //string sqlTxt = "SELECT Id, UserName FROM users";

            string sqlTxt = "SELECT Id as Id, UserName as UserName FROM users";

            IDictionary<string, object> sqlParam = null;

            if (!string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(UserName))
            {
                //sqlTxt = "SELECT Id, UserName FROM users WHERE Id = @Id or UserName = @UserName";
                sqlTxt = "SELECT Id as Id, UserName as UserName FROM users WHERE Id = @Id or UserName = @UserName";
                sqlParam = new Dictionary<string, object> { { "@Id", UserID}, { "@UserName", UserName} };
            }
            else if (!string.IsNullOrEmpty(UserID) && string.IsNullOrEmpty(UserName))
            {
                //sqlTxt = "SELECT Id, UserName FROM users WHERE Id = @Id";
                sqlTxt = "SELECT Id as Id, UserName as UserName FROM users WHERE Id = @Id";

                sqlParam = new Dictionary<string, object> { { "@Id", UserID }};
            }
            else if (string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(UserName))
            {
                //sqlTxt = "SELECT Id, UserName FROM users WHERE UserName = @UserName";
                sqlTxt = "SELECT Id as Id, UserName as UserName FROM users WHERE UserName = @UserName";

                sqlParam = new Dictionary<string, object> { { "@UserName", UserName } };
            }

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, sqlParam);

            if (dbResult != null &&dbResult.Count > 0)
            {
                return true;
            }

            return false;
        }

        public T DeleteUser<T>(string UserID, string UserName, Func<object, T> ExtensionFunction)
        {
            if (string.IsNullOrEmpty(UserID) && string.IsNullOrEmpty(UserName))
            {
                return default(T);
            }

            //string sqlTxt = "DELETE FROM users";

            string sqlTxt = "DELETE FROM users";

            IDictionary<string, object> sqlParam = null;

            if (!string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(UserName))
            {
                //sqlTxt = "DELETE FROM users WHERE Id = @Id or UserName = @UserName";
                sqlTxt = "DELETE FROM users WHERE Id = @Id or UserName = @UserName";
                sqlParam = new Dictionary<string, object> { { "@Id", UserID }, { "@UserName", UserName } };
            }
            else if (!string.IsNullOrEmpty(UserID) && string.IsNullOrEmpty(UserName))
            {
                //sqlTxt = "DELETE FROM users WHERE Id = @Id";
                sqlTxt = "DELETE FROM users WHERE Id = @Id ";
                sqlParam = new Dictionary<string, object> { { "@Id", UserID } };
            }
            else if (string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(UserName))
            {
                //sqlTxt = "DELETE FROM users WHERE UserName = @UserName";
                sqlTxt = "DELETE FROM users WHERE UserName = @UserName";
                sqlParam = new Dictionary<string, object> { { "@UserName", UserName } };
            }

            var dbResult = this.dmlOperable.ExeSql(sqlTxt, sqlParam);

            if (dbResult >= 0)
            {
                if (ExtensionFunction != null)
                {
                    return ExtensionFunction(new { UserID, UserName });
                }

                return (T)(object)(true);
            }

            return (T)(object)(false);
        }

        public T ChangeUserName<T>(string UserID, string NewUserName, Func<object, T> ExtensionFunction)
        {
            if (string.IsNullOrEmpty(UserID) || string.IsNullOrEmpty(NewUserName))
            {
                return default(T);
            }

            if (this.UserExists(null, NewUserName))
            {
                return default(T);//return (T)(object)false;
            }

            //string sqlTxt = "UPDATE users SET UserName = @UserName WHERE Id = @Id";
            string sqlTxt = "UPDATE users SET UserName = @UserName WHERE Id = @Id";

            var dbResult = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object> {
                { "@Id", UserID },
                { "@UserName", NewUserName }
            });

            if (dbResult >= 0)
            {
                if (ExtensionFunction != null)
                {
                    return ExtensionFunction(new { UserID, NewUserName });
                }

                return (T)(object)(true);
            }

            return default(T);
        }
    }
}
