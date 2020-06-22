using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Lib.Utility.Encryption;
using Rally.Lib.Utility.Common;

namespace Rally.Framework.Authentication
{
    public class AuthenticationManager : IAuthentication, IToken
    {
        public AuthenticationManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dbType = DBType;
            this.dmlOperable = DMLOperable;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dbType;

        public static IAuthentication NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new AuthenticationManager(DMLOperable, DBType);
        }

        public bool ChangePassword(string UserName, string NewPassword)
        {
            //string sqlTxt = "update users set PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, PasswordRev = @PasswordRev, PasswordChangedDate = @PasswordChangedDate, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate where UserName = @UserName";

            string sqlTxt = "update users set PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt, PasswordRev = @PasswordRev, PasswordChangedDate = @PasswordChangedDate, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate where UserName = @UserName";

            string passwordSalt = HashUtility.GenerateRandomString(4);
            //string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{NewPassword}:{passwordSalt}");
            string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{NewPassword}:{passwordSalt}");

            //string passwordRev = this.dmlOperable.ExeSqlScalar("select PasswordRev from users where UserName = @UserName", new Dictionary<string, object> { { "@UserName", UserName} });

            string passwordRev = this.dmlOperable.ExeSqlScalar("select PasswordRev from users where UserName = @UserName", new Dictionary<string, object> { { "@UserName", UserName } });

            int passwordRevNumber = -1;

            if (int.TryParse(passwordRev, out passwordRevNumber))
            {
                passwordRevNumber++;
            }

            int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@UserName", UserName},
                { "@PasswordHash", hashedPassword},
                { "@PasswordSalt", passwordSalt},
                { "@PasswordRev",passwordRevNumber},
                { "@PasswordChangedDate", DateTime.Now },
                { "@PasswordVerificationTokenExpirationDate", DateTime.Now.AddDays(-2) }
            });

            return result >= 0;
        }


        public bool Login(string UserName, string Password)
        {

            //string sqlTxt = "select Id, UserName, PasswordHash, PasswordSalt from users where UserName = @UserName";

            string sqlTxt = "select Id as Id, UserName as UserName, PasswordHash as PasswordHash, PasswordSalt as PasswordSalt from users where UserName = @UserName";

            if (this.dbType == DBTypeEnum.PostgreSQL)
            {
                sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
            }

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@UserName", UserName } });

            if (dbResult != null && dbResult.Count == 1)
            {
                string userId = dbResult[0]["Id"].ToString();
                string hashedPasswordInDB = dbResult[0]["PasswordHash"].ToString();
                string passwordSalt = dbResult[0]["PasswordSalt"].ToString();
                string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{Password}:{passwordSalt}");

                if (hashedPassword == hashedPasswordInDB)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Login<T>(string UserName, string Password, out T ExtraData, Func<object, T> ExtensionFunction)
        {
            ExtraData = default(T);

            //string sqlTxt = "select Id, UserName, PasswordHash, PasswordSalt from users where UserName = @UserName";

            string sqlTxt = "select Id as Id, UserName as UserName, PasswordHash as PasswordHash, PasswordSalt as PasswordSalt from users where UserName = @UserName";

            if (this.dbType == DBTypeEnum.PostgreSQL)
            {
                sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
            }

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@UserName", UserName } });

            if (dbResult != null && dbResult.Count == 1)
            {
                string userId = dbResult[0]["Id"].ToString();
                string hashedPasswordInDB = dbResult[0]["PasswordHash"].ToString();
                string passwordSalt = dbResult[0]["PasswordSalt"].ToString();
                //string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{Password}:{passwordSalt}");
                string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{Password}:{passwordSalt}");

                if (hashedPassword == hashedPasswordInDB)
                {
                    if (ExtensionFunction != null)
                    {
                        ExtraData = ExtensionFunction(userId);
                    }

                    return true;
                }
            }

            return false;
        }

        public Token ValidateUser(string UserName, string Password, string Key)
        {
            //string sqlTxt = "select Id, UserName, UserType, PasswordHash, PasswordSalt from users where UserName = @UserName";

            string sqlTxt = "select Id as Id, UserName as UserName, UserType as UserType, PasswordHash as PasswordHash, PasswordSalt as PasswordSalt from users where UserName = @UserName";

            if (this.dbType == DBTypeEnum.PostgreSQL)
            {
                sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
            }

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { {"@UserName",UserName } });

            if (dbResult != null && dbResult.Count == 1)
            {
                string userId = dbResult[0]["Id"].ToString();
                string hashedPasswordInDB = dbResult[0]["PasswordHash"].ToString();
                string passwordSalt = dbResult[0]["PasswordSalt"].ToString();
                //string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{Password}:{passwordSalt}");
                string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{Password}:{passwordSalt}");

                if (hashedPassword == hashedPasswordInDB)
                {
                    string tokenString = $"{HashUtility.GenerateRandomString(8)}:{UserName}:{hashedPassword}";
                    //tokenString = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{userId}:{tokenString}");
                    tokenString = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{userId}:{tokenString}");
                    byte[] tokenBytes = Encoding.UTF8.GetBytes(tokenString);
                    string tokenData = HashUtility.CreateHmac<System.Security.Cryptography.HMACMD5>(Key, tokenBytes);//HashUtility.CreateHash<System.Security.Cryptography.SHA256Cng>($"{tokenString}:{Key}"); //
                    DateTime tokenExpirationDate = DateTime.Now.AddDays(ModuleConfiguration.Default_Password_Expiration_Days);

                    //string refreshToken = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{HashUtility.GenerateRandomString(10)}:{UserName}");
                    string refreshToken = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{HashUtility.GenerateRandomString(10)}:{UserName}");

                    //sqlTxt = "update users set SecurityStamp = @SecurityStamp, PasswordVerificationToken = @PasswordVerificationToken, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate, ConfirmationToken = @ConfirmationToken where Id = @Id and UserName = @UserName";

                    sqlTxt = "update users set SecurityStamp = @SecurityStamp, PasswordVerificationToken = @PasswordVerificationToken, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate, ConfirmationToken = @ConfirmationToken where Id = @Id and UserName = @UserName";

                    int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() { { "@SecurityStamp", Key }, { "@PasswordVerificationToken", tokenData }, { "@ConfirmationToken", refreshToken }, { "@PasswordVerificationTokenExpirationDate", tokenExpirationDate }, {"@Id", userId }, { "@UserName", UserName } });

                    if (result >= 0)
                    {
                        return new Token()
                        {
                            AccessToken = tokenString,
                            ExpiresIn = CommonUtility.GetMillisecondsByDateTime(tokenExpirationDate, null),
                            RefreshToken = refreshToken,
                            UserName = UserName,
                            Scope = "Newford"
                        };
                    }                
                }
            }

            return null;
        }

        public Token ValidateUser<T>(string UserName, string Password, string Key, out T ExtraData, Func<object, T> ExtensionFunction)
        {
            ExtraData = default(T);

            //string sqlTxt = "select Id, UserName, UserType, PasswordHash, PasswordSalt from users where UserName = @UserName";

            string sqlTxt = "select Id as Id, UserName as UserName, UserType as UserType, PasswordHash as PasswordHash, PasswordSalt as PasswordSalt from users where UserName = @UserName";

            if (this.dbType == DBTypeEnum.PostgreSQL)
            {
                sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
            }

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@UserName", UserName } });

            if (dbResult != null && dbResult.Count == 1)
            {
                string userId = dbResult[0]["Id"].ToString();
                string hashedPasswordInDB = dbResult[0]["PasswordHash"].ToString();
                string passwordSalt = dbResult[0]["PasswordSalt"].ToString();
                //string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{Password}:{passwordSalt}");
                string hashedPassword = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{Password}:{passwordSalt}");

                if (hashedPassword == hashedPasswordInDB)
                {
                    string tokenString = $"{HashUtility.GenerateRandomString(8)}:{UserName}:{hashedPassword}";
                    //tokenString = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{userId}:{tokenString}");
                    tokenString = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{userId}:{tokenString}");
                    byte[] tokenBytes = Encoding.UTF8.GetBytes(tokenString);
                    string tokenData = HashUtility.CreateHmac<System.Security.Cryptography.HMACMD5>(Key, tokenBytes);//HashUtility.CreateHash<System.Security.Cryptography.SHA256Cng>($"{tokenString}:{Key}"); //
                    DateTime tokenExpirationDate = DateTime.Now.AddDays(ModuleConfiguration.Default_Password_Expiration_Days);

                    //string refreshToken = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{HashUtility.GenerateRandomString(10)}:{UserName}");
                    string refreshToken = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{HashUtility.GenerateRandomString(10)}:{UserName}");

                    //sqlTxt = "update users set SecurityStamp = @SecurityStamp, PasswordVerificationToken = @PasswordVerificationToken, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate, ConfirmationToken = @ConfirmationToken where Id = @Id and UserName = @UserName";

                    sqlTxt = "update users set SecurityStamp = @SecurityStamp, PasswordVerificationToken = @PasswordVerificationToken, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate, ConfirmationToken = @ConfirmationToken where Id = @Id and UserName = @UserName";

                    int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() { { "@SecurityStamp", Key }, { "@PasswordVerificationToken", tokenData }, { "@ConfirmationToken", refreshToken }, { "@PasswordVerificationTokenExpirationDate", tokenExpirationDate }, { "@Id", userId }, { "@UserName", UserName } });

                    if (result >= 0)
                    {
                        if (ExtensionFunction != null)
                        {
                            ExtraData = ExtensionFunction(userId);
                        }

                        return new Token()
                        {
                            AccessToken = tokenString,
                            ExpiresIn = CommonUtility.GetMillisecondsByDateTime(tokenExpirationDate, null),
                            RefreshToken = refreshToken,
                            UserName = UserName,
                            Scope = "Default"
                        };
                    }
                }
            }

            return null;
        }

        public bool ValidateToken(string AccessToken, string Key)
        {
            //string sqlTxt = "select PasswordVerificationTokenExpirationDate from users where PasswordVerificationToken = @PasswordVerificationToken";

            string sqlTxt = "select PasswordVerificationTokenExpirationDate as PasswordVerificationTokenExpirationDate from users where PasswordVerificationToken = @PasswordVerificationToken";

            if (this.dbType == DBTypeEnum.PostgreSQL)
            {
                sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
            }

            byte[] tokenBytes = Encoding.UTF8.GetBytes(AccessToken);
            string tokenData = HashUtility.CreateHmac<System.Security.Cryptography.HMACMD5>(Key, tokenBytes);//HashUtility.CreateHash<System.Security.Cryptography.SHA256Cng>($"{AccessToken}:{Key}");//

            IList<IDictionary<string,object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@PasswordVerificationToken", tokenData} });

            if (dbResult != null && dbResult.Count == 1)
            {
                DateTime expirationDate = (DateTime)dbResult[0]["PasswordVerificationTokenExpirationDate"];

                return DateTime.Now.AddDays(ModuleConfiguration.Default_Password_Expiration_Days -1) <= expirationDate;
            }

            return false;
        }

        public bool ValidateToken<T>(string AccessToken, string Key, out T ExtraData, Func<object, T> ExtensionFunction)
        {
            ExtraData = default(T);

            //string sqlTxt = "select Id, PasswordVerificationTokenExpirationDate from users where PasswordVerificationToken = @PasswordVerificationToken";

            string sqlTxt = "select Id as Id, PasswordVerificationTokenExpirationDate as PasswordVerificationTokenExpirationDate from users where PasswordVerificationToken = @PasswordVerificationToken";

            if (this.dbType == DBTypeEnum.PostgreSQL)
            {
                sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
            }

            byte[] tokenBytes = Encoding.UTF8.GetBytes(AccessToken);
            string tokenData = HashUtility.CreateHmac<System.Security.Cryptography.HMACMD5>(Key, tokenBytes);//HashUtility.CreateHash<System.Security.Cryptography.SHA256Cng>($"{AccessToken}:{Key}");//

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@PasswordVerificationToken", tokenData } });

            if (dbResult != null && dbResult.Count == 1)
            {
                DateTime expirationDate = (DateTime)dbResult[0]["PasswordVerificationTokenExpirationDate"];
                
                if (DateTime.Now.AddDays(ModuleConfiguration.Default_Password_Expiration_Days - 1) <= expirationDate)
                {
                    if (ExtensionFunction != null)
                    {
                        string userId = (string)dbResult[0]["Id"];
                        ExtraData = ExtensionFunction(userId);
                    }

                    return true;
                }
            }

            return false;
        }

        public T CreateToken<T>(string Argument)
        {
            //string sqlTxt = "select Id, UserName from users where UserName = @UserName";

            string sqlTxt = "select Id as Id, UserName as UserName from users where UserName = @UserName";

            if (this.dbType == DBTypeEnum.PostgreSQL)
            {
                sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
            }

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@UserName", Argument } });

            if (dbResult != null && dbResult.Count > 0)
            {
                string userId = dbResult[0]["Id"].ToString();

                string tokenString = $"{HashUtility.GenerateRandomString(8)}:{Argument}";
                //tokenString = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{userId}:{tokenString}");
                tokenString = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{userId}:{tokenString}");
                byte[] tokenBytes = Encoding.UTF8.GetBytes(tokenString);
                string tokenData = HashUtility.CreateHmac<System.Security.Cryptography.HMACMD5>(Argument, tokenBytes);//HashUtility.CreateHash<System.Security.Cryptography.SHA256Cng>($"{tokenString}:{Key}"); //
                DateTime tokenExpirationDate = DateTime.Now.AddMinutes(ModuleConfiguration.Default_Temp_Token_Expiration_Minutes);

                //string refreshToken = HashUtility.CreateHash<System.Security.Cryptography.SHA1Cng>($"{HashUtility.GenerateRandomString(10)}:{Argument}");
                string refreshToken = HashUtility.CreateHash<System.Security.Cryptography.HMACSHA1>($"{HashUtility.GenerateRandomString(10)}:{Argument}");

                //sqlTxt = "update users set SecurityStamp = @SecurityStamp, PasswordVerificationToken = @PasswordVerificationToken, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate, ConfirmationToken = @ConfirmationToken where Id = @Id and UserName = @UserName";

                sqlTxt = "update users set SecurityStamp = @SecurityStamp, PasswordVerificationToken = @PasswordVerificationToken, PasswordVerificationTokenExpirationDate = @PasswordVerificationTokenExpirationDate, ConfirmationToken = @ConfirmationToken where Id = @Id and UserName = @UserName";

                int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() { { "@SecurityStamp", Argument }, { "@PasswordVerificationToken", tokenData }, { "@ConfirmationToken", refreshToken }, { "@PasswordVerificationTokenExpirationDate", tokenExpirationDate }, { "@Id", userId }, { "@UserName", Argument } });

                if (result >= 0)
                {
                    //string tokenEncrypted = $"{Argument}:{tokenString}";
                    //tokenEncrypted = Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenEncrypted));

                    return (T)(object)tokenString;
                }
            }

            return default(T);
        }

        public bool ValidateToken<T>(T Token)
        {
            string tokenRaw = Token.ToString();

            byte[] tokenRawBytes = Convert.FromBase64String(tokenRaw);

            tokenRaw = Encoding.UTF8.GetString(tokenRawBytes);

            if (!string.IsNullOrEmpty(tokenRaw) && tokenRaw.Contains(":"))
            {
                string[] tokenArgs = tokenRaw.Split(new string[] { ":" }, StringSplitOptions.None);

                //string sqlTxt = "select PasswordVerificationTokenExpirationDate from users where UserName = @UserName and PasswordVerificationToken = @PasswordVerificationToken";

                string sqlTxt = "select PasswordVerificationTokenExpirationDate as PasswordVerificationTokenExpirationDate from users where UserName = @UserName and PasswordVerificationToken = @PasswordVerificationToken";

                if (this.dbType == DBTypeEnum.PostgreSQL)
                {
                    sqlTxt = DBUtility.RebuildPostgreSQLSelectStatementForOriginalColumnNameCase(sqlTxt);
                }

                byte[] tokenBytes = Encoding.UTF8.GetBytes(tokenArgs[1]);
                string tokenData = HashUtility.CreateHmac<System.Security.Cryptography.HMACMD5>(tokenArgs[0], tokenBytes);//HashUtility.CreateHash<System.Security.Cryptography.SHA256Cng>($"{AccessToken}:{Key}");//

                IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@UserName", tokenArgs[0] }, { "@PasswordVerificationToken", tokenData } });

                if (dbResult != null && dbResult.Count == 1)
                {
                    DateTime expirationDate = (DateTime)dbResult[0]["PasswordVerificationTokenExpirationDate"];

                    return DateTime.Now.AddMinutes(ModuleConfiguration.Default_Temp_Token_Expiration_Minutes - 1) <= expirationDate;
                }
            }

            return false;
        }

        public bool ExpireToken<T>(T Token)
        {
            throw new NotImplementedException();
        }
    }
}
