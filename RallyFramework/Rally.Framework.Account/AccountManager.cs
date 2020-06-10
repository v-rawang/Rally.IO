using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;
using Rally.Lib.Utility.Common;

namespace Rally.Framework.Account
{
    public class AccountManager : IAccountManager
    {
        public AccountManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static IAccountManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new AccountManager(DMLOperable, DBType);
        }

        public string AddAccount(Core.DomainModel.Account Account)
        {
            if (String.IsNullOrEmpty(Account.ID))
            {
                //throw (new AccountIDNullException("账户唯一标识不可为空值！"));
                Account.ID = Guid.NewGuid().ToString();
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertAccount;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@Id", Account.ID },
                { "@Name",Account.Name},
                { "@FirstName", Account.FirstName},
                { "@LastName", Account.LastName},
                { "@NickName", Account.NickName},
                { "@Gender", Account.Gender},
                { "@BirthDate", Account.BirthDate},
                { "@Title", Account.Title},
                { "@SID", Account.SID},
                { "@Address", Account.Address},
                { "@ZipCode", Account.ZipCode},
                { "@Email", Account.Email},
                { "@Mobile", Account.Mobile},
                { "@HeadImageFileID", Account.HeadImage != null ? Account.HeadImage.ID : null},
                { "@ModifiedDate", DateTime.Now},
                { "@Alias", Account.Alias },
                { "@PhoneNumber", Account.PhoneNumber},
                { "@PoliticsStatus", Account.PoliticsStatus },
                { "@Organization", Account.Organization},
                { "@Department", Account.Department},
                { "@WorkGroup", Account.WorkGroup},
                { "@Position", Account.Position},
            });

            return Account.ID;
        }

        public Core.DomainModel.Account SetAccount(string ID, Core.DomainModel.Account Account)
        {
            if (String.IsNullOrEmpty(Account.ID))
            {
                throw (new AccountIDNullException("账户唯一标识不可为空值！"));
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateAccount;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@Id", Account.ID },
                //{ "@Name",Account.Name},
                { "@FirstName", Account.FirstName},
                { "@LastName", Account.LastName},
                { "@NickName", Account.NickName},
                { "@Gender", Account.Gender},
                { "@BirthDate", Account.BirthDate},
                { "@Title", Account.Title},
                { "@SID", Account.SID},
                { "@Address", Account.Address},
                { "@ZipCode", Account.ZipCode},
                { "@Email", Account.Email},
                { "@Mobile", Account.Mobile},
                { "@BloodType", Account.BloodType},
                { "@Constellation", Account.Constellation},
                { "@Hobby", Account.Hobby},
                { "@Education", Account.Education},
                { "@Industry", Account.Industry},
                { "@Organization", Account.Organization},
                { "@Department", Account.Department},
                { "@WorkGroup", Account.WorkGroup},
                { "@Position", Account.Position},
                { "@Description", Account.Description},
                { "@Headline", Account.Headline},
                { "@HeadImageFileID", Account.HeadImage != null ? Account.HeadImage.ID : null},
                { "@ModifiedDate", DateTime.Now},
                { "@Alias", Account.Alias },
                { "@PhoneNumber", Account.PhoneNumber},
                { "@PoliticsStatus", Account.PoliticsStatus }
            });

            return Account;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Account"></param>
        /// <param name="ShouldUpdateName"></param>
        /// <returns></returns>
        public Core.DomainModel.Account SetAccount(string ID, Core.DomainModel.Account Account, bool ShouldUpdateName)
        {
            if (String.IsNullOrEmpty(Account.ID))
            {
                throw (new AccountIDNullException("账户唯一标识不可为空值！"));
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateAccount;

            IDictionary<string, object> sqlParams = new Dictionary<string, object>() {
                { "@Id", Account.ID },
                { "@FirstName", Account.FirstName},
                { "@LastName", Account.LastName},
                { "@NickName", Account.NickName},
                { "@Gender", Account.Gender},
                { "@BirthDate", Account.BirthDate},
                { "@Title", Account.Title},
                { "@SID", Account.SID},
                { "@Address", Account.Address},
                { "@ZipCode", Account.ZipCode},
                { "@Email", Account.Email},
                { "@Mobile", Account.Mobile},
                { "@BloodType", Account.BloodType},
                { "@Constellation", Account.Constellation},
                { "@Hobby", Account.Hobby},
                { "@Education", Account.Education},
                { "@Industry", Account.Industry},
                { "@Organization", Account.Organization},
                { "@Department", Account.Department},
                { "@WorkGroup", Account.WorkGroup},
                { "@Position", Account.Position},
                { "@Description", Account.Description},
                { "@Headline", Account.Headline},
                { "@HeadImageFileID", Account.HeadImage != null ? Account.HeadImage.ID : null},
                { "@ModifiedDate", DateTime.Now},
                { "@Alias", Account.Alias },
                { "@PhoneNumber", Account.PhoneNumber},
                { "@PoliticsStatus", Account.PoliticsStatus }
            };

            if (ShouldUpdateName)
            {
                sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateAccountWithUserName;
                sqlParams.Add("@Name",Account.Name);
            }

            this.dmlOperable.ExeSql(sqlCommandText, sqlParams);

            return Account;
        }

        public Core.DomainModel.Account GetAccount(string ID)
        {
            Core.DomainModel.Account account = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectAccountById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() {{"@Id", ID }});

            int gender = 0;

            if (dbResult != null && dbResult.Count == 1)
            {
                account = new Core.DomainModel.Account()
                {
                    ID = (string)dbResult[0]["Id"],
                    Name = (string)dbResult[0]["Name"],
                    Address = (string)dbResult[0]["Address"],
                    BirthDate = (DateTime?)dbResult[0]["BirthDate"],
                    Email = (string)dbResult[0]["Email"],
                    FirstName = (string)dbResult[0]["FirstName"],
                    LastName = (string)dbResult[0]["LastName"],
                    Gender = dbResult[0]["Gender"] == null ? 0 : int.TryParse(dbResult[0]["Gender"].ToString(), out gender) ? gender :0,
                    Mobile = (string)dbResult[0]["Mobile"],
                    NickName = (string)dbResult[0]["NickName"],
                    SID = (string)dbResult[0]["SID"],
                    Title = (string)dbResult[0]["Title"],
                    ZipCode = (string)dbResult[0]["ZipCode"],
                    HeadImage = new Attachment() { ID = (string)dbResult[0]["HeadImageFileID"] }, //reader.IsDBNull(reader.GetOrdinal("HeadImageFileID")) ? new Attachment() : new Attachment() { ID = reader.GetString("HeadImageFileID") },
                    Headline = (string)dbResult[0]["Headline"],
                    Description = (string)dbResult[0]["Description"], //reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),

                    BloodType = (string)dbResult[0]["BloodType"],//reader.IsDBNull(reader.GetOrdinal("BloodType")) ? null : reader.GetString("BloodType"),
                    Constellation = (string)dbResult[0]["Constellation"],//reader.IsDBNull(reader.GetOrdinal("Constellation")) ? null : reader.GetString("Constellation"),
                    Hobby = (string)dbResult[0]["Hobby"],//reader.IsDBNull(reader.GetOrdinal("Hobby")) ? null : reader.GetString("Hobby"),
                    Education = (string)dbResult[0]["Education"],//reader.IsDBNull(reader.GetOrdinal("Education")) ? null : reader.GetString("Education"),
                    Industry = (string)dbResult[0]["Industry"], //reader.IsDBNull(reader.GetOrdinal("Industry")) ? null : reader.GetString("Industry"),

                    Organization = (string)dbResult[0]["Organization"],//reader.IsDBNull(reader.GetOrdinal("Organization")) ? null : reader.GetString("Organization"),
                    Department = (string)dbResult[0]["Department"],
                    WorkGroup = (string)dbResult[0]["WorkGroup"],

                    Alias = (string)dbResult[0]["Alias"],
                    PhoneNumber = (string)dbResult[0]["PhoneNumber"],
                    PoliticsStatus = (string)dbResult[0]["PoliticsStatus"],

                    Position = (string)dbResult[0]["Position"]//reader.IsDBNull(reader.GetOrdinal("Position")) ? null : reader.GetString("Position")
                };
            }

            return account;
        }

        public string AddAccountProvider(AccountProvider Provider)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertAccountProvider;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@Id", Provider.ID },
                { "@Name",Provider.Name},
                { "@AppId", Provider.AppID},
                { "@AppSecret", Provider.AppSecret},
                { "@AdditionalInfo", Provider.DynamicProperties != null ? System.Text.Encoding.UTF8.GetString(JsonUtility.JsonSerialize(Provider.DynamicProperties, new Type[] { typeof(Dictionary<string, object>) }, "root")) : null},
                { "@ModifiedDate", DateTime.Now}
            });

            return Provider.ID;
        }

        public string AddExternalAccount(string AccountID, string ProviderID, string Identifier, object Meta)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertExternalAccount;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@AccountId", AccountID },
                { "@ProviderId",ProviderID},
                { "@Identifier", Identifier},
                { "@AdditionalInfo", Meta != null ? System.Text.Encoding.UTF8.GetString(JsonUtility.JsonSerialize(Meta, null, "root")) : null},
                { "@ModifiedDate", DateTime.Now}
            });

            return Identifier;
        }

        public Core.DomainModel.Account GetAccountByMobile(string Mobile)
        {
            Core.DomainModel.Account account = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectAccountByMobile;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@Mobile", Mobile } });

            if (dbResult != null && dbResult.Count == 1)
            {
                int gender = 0;
                
                account = new Core.DomainModel.Account()
                {
                    ID = (string)dbResult[0]["Id"],
                    Name = (string)dbResult[0]["Name"],
                    Address = (string)dbResult[0]["Address"],
                    BirthDate = (DateTime?)dbResult[0]["BirthDate"],
                    Email = (string)dbResult[0]["Email"],
                    FirstName = (string)dbResult[0]["FirstName"],
                    LastName = (string)dbResult[0]["LastName"],
                    Gender = dbResult[0]["Gender"] == null ? 0 : int.TryParse(dbResult[0]["Gender"].ToString(), out gender) ? gender : 0, //dbResult[0]["Gender"] == null ? 0 : int.Parse(dbResult[0]["Gender"].ToString()),
                    Mobile = (string)dbResult[0]["Mobile"],
                    NickName = (string)dbResult[0]["NickName"],
                    SID = (string)dbResult[0]["SID"],
                    Title = (string)dbResult[0]["Title"],
                    ZipCode = (string)dbResult[0]["ZipCode"],
                    HeadImage = new Attachment() { ID = (string)dbResult[0]["HeadImageFileID"] }, //reader.IsDBNull(reader.GetOrdinal("HeadImageFileID")) ? new Attachment() : new Attachment() { ID = reader.GetString("HeadImageFileID") },
                    Headline = (string)dbResult[0]["Headline"],
                    Description = (string)dbResult[0]["Description"], 

                    BloodType = (string)dbResult[0]["BloodType"],//reader.IsDBNull(reader.GetOrdinal("BloodType")) ? null : reader.GetString("BloodType"),
                    Constellation = (string)dbResult[0]["Constellation"],//reader.IsDBNull(reader.GetOrdinal("Constellation")) ? null : reader.GetString("Constellation"),
                    Hobby = (string)dbResult[0]["Hobby"],//reader.IsDBNull(reader.GetOrdinal("Hobby")) ? null : reader.GetString("Hobby"),
                    Education = (string)dbResult[0]["Education"],//reader.IsDBNull(reader.GetOrdinal("Education")) ? null : reader.GetString("Education"),
                    Industry = (string)dbResult[0]["Industry"], //reader.IsDBNull(reader.GetOrdinal("Industry")) ? null : reader.GetString("Industry"),

                    Organization = (string)dbResult[0]["Organization"],//reader.IsDBNull(reader.GetOrdinal("Organization")) ? null : reader.GetString("Organization"),
                    Department = (string)dbResult[0]["Department"],
                    WorkGroup = (string)dbResult[0]["WorkGroup"],

                    Alias = (string)dbResult[0]["Alias"],
                    PhoneNumber = (string)dbResult[0]["PhoneNumber"],
                    PoliticsStatus = (string)dbResult[0]["PoliticsStatus"],

                    Position = (string)dbResult[0]["Position"]//reader.IsDBNull(reader.GetOrdinal("Position")) ? null : reader.GetString("Position")
                };
            }

            return account;
        }

        public Core.DomainModel.Account GetAccountByNickName(string NickName)
        {
            Core.DomainModel.Account account = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectAccountByNickName;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@NickName", NickName} });

            if (dbResult != null && dbResult.Count == 1)
            {
                int gender = 0;

                account = new Core.DomainModel.Account()
                {
                    ID = (string)dbResult[0]["Id"],
                    Name = (string)dbResult[0]["Name"],
                    Address = (string)dbResult[0]["Address"],
                    BirthDate = (DateTime?)dbResult[0]["BirthDate"],
                    Email = (string)dbResult[0]["Email"],
                    FirstName = (string)dbResult[0]["FirstName"],
                    LastName = (string)dbResult[0]["LastName"],
                    Gender = dbResult[0]["Gender"] == null ? 0 : int.TryParse(dbResult[0]["Gender"].ToString(), out gender) ? gender : 0, //dbResult[0]["Gender"] is null ? 0 :int.Parse(dbResult[0]["Gender"].ToString()),
                    Mobile = (string)dbResult[0]["Mobile"],
                    NickName = (string)dbResult[0]["NickName"],
                    SID = (string)dbResult[0]["SID"],
                    Title = (string)dbResult[0]["Title"],
                    ZipCode = (string)dbResult[0]["ZipCode"],
                    HeadImage = new Attachment() { ID = (string)dbResult[0]["HeadImageFileID"] }, //reader.IsDBNull(reader.GetOrdinal("HeadImageFileID")) ? new Attachment() : new Attachment() { ID = reader.GetString("HeadImageFileID") },
                    Headline = (string)dbResult[0]["Headline"],
                    Description = (string)dbResult[0]["Description"], 

                    BloodType = (string)dbResult[0]["BloodType"],//reader.IsDBNull(reader.GetOrdinal("BloodType")) ? null : reader.GetString("BloodType"),
                    Constellation = (string)dbResult[0]["Constellation"],//reader.IsDBNull(reader.GetOrdinal("Constellation")) ? null : reader.GetString("Constellation"),
                    Hobby = (string)dbResult[0]["Hobby"],//reader.IsDBNull(reader.GetOrdinal("Hobby")) ? null : reader.GetString("Hobby"),
                    Education = (string)dbResult[0]["Education"],//reader.IsDBNull(reader.GetOrdinal("Education")) ? null : reader.GetString("Education"),
                    Industry = (string)dbResult[0]["Industry"], //reader.IsDBNull(reader.GetOrdinal("Industry")) ? null : reader.GetString("Industry"),

                    Organization = (string)dbResult[0]["Organization"],//reader.IsDBNull(reader.GetOrdinal("Organization")) ? null : reader.GetString("Organization"),
                    Department = (string)dbResult[0]["Department"],
                    WorkGroup = (string)dbResult[0]["WorkGroup"],

                    Alias = (string)dbResult[0]["Alias"],
                    PhoneNumber = (string)dbResult[0]["PhoneNumber"],
                    PoliticsStatus = (string)dbResult[0]["PoliticsStatus"],

                    Position = (string)dbResult[0]["Position"]//reader.IsDBNull(reader.GetOrdinal("Position")) ? null : reader.GetString("Position")
                };
            }

            return account;
        }

        public Core.DomainModel.Account GetAccountHeadImage(string ID)
        {
            Core.DomainModel.Account account = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectAccountHeadImageById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@Id", ID } });

            if (dbResult != null && dbResult.Count == 1)
            {
                account = new Core.DomainModel.Account()
                {
                    ID = (string)dbResult[0]["Id"],
                    //Name = (string)dbResult[0]["Name"],
                    //Address = (string)dbResult[0]["Address"],
                    //BirthDate = (DateTime)dbResult[0]["BirthDate"],
                    //Email = (string)dbResult[0]["Email"],
                    //FirstName = (string)dbResult[0]["FirstName"],
                    //LastName = (string)dbResult[0]["LastName"],
                    //Gender = (int)dbResult[0]["Gender"],
                    //Mobile = (string)dbResult[0]["Mobile"],
                    //NickName = (string)dbResult[0]["NickName"],
                    ////QQNumber = reader.IsDBNull(reader.GetOrdinal("QQNumber")) ? null : reader.GetString("QQNumber"),
                    //SID = (string)dbResult[0]["SID"],
                    //Title = (string)dbResult[0]["Title"],
                    ////WeChatNumber = reader.IsDBNull(reader.GetOrdinal("WeChatNumber")) ? null : reader.GetString("WeChatNumber"),
                    //ZipCode = (string)dbResult[0]["ZipCode"],
                    HeadImage = new Attachment() { ID = (string)dbResult[0]["HeadImageFileID"] }, //reader.IsDBNull(reader.GetOrdinal("HeadImageFileID")) ? new Attachment() : new Attachment() { ID = reader.GetString("HeadImageFileID") },

                    //BloodType = (string)dbResult[0]["BloodType"],//reader.IsDBNull(reader.GetOrdinal("BloodType")) ? null : reader.GetString("BloodType"),
                    //Constellation = (string)dbResult[0]["Constellation"],//reader.IsDBNull(reader.GetOrdinal("Constellation")) ? null : reader.GetString("Constellation"),
                    //Hobby = (string)dbResult[0]["BloodType"],//reader.IsDBNull(reader.GetOrdinal("Hobby")) ? null : reader.GetString("Hobby"),
                    //Education = (string)dbResult[0]["Education"],//reader.IsDBNull(reader.GetOrdinal("Education")) ? null : reader.GetString("Education"),
                    //Industry = (string)dbResult[0]["Industry"], //reader.IsDBNull(reader.GetOrdinal("Industry")) ? null : reader.GetString("Industry"),

                    //Organization = (string)dbResult[0]["Organization"],//reader.IsDBNull(reader.GetOrdinal("Organization")) ? null : reader.GetString("Organization"),
                    //Position = (string)dbResult[0]["Position"]//reader.IsDBNull(reader.GetOrdinal("Position")) ? null : reader.GetString("Position")
                };
            }

            return account;
        }

        public AccountProvider GetAccountProvider(string ID)
        {
            AccountProvider provider = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectAccountProviderById;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@Id", ID } });

            if (dbResult != null && dbResult.Count == 1)
            {
                provider = new AccountProvider()
                {
                    ID = (string)dbResult[0]["Id"],//reader.GetString("Id"),
                    Name = (string)dbResult[0]["Name"],//reader.GetString("Name"),
                    AppID = (string)dbResult[0]["AppId"],//reader.IsDBNull(reader.GetOrdinal("AppId")) ? null : reader.GetString("AppId"),
                    AppSecret = (string)dbResult[0]["AppSecret"], //reader.IsDBNull(reader.GetOrdinal("AppSecret")) ? null : reader.GetString("AppSecret"),
                    DynamicProperties = JsonUtility.JsonDeserialize(Encoding.UTF8.GetBytes((string)dbResult[0]["AdditionalInfo"]), typeof(Dictionary<string, object>), null, "root") as Dictionary<string, object> //reader.IsDBNull(reader.GetOrdinal("AdditionalInfo")) ? null : JsonUtility.JsonDeserialize(Encoding.UTF8.GetBytes(reader.GetString("AdditionalInfo")), typeof(Dictionary<string, object>), null, "root") as Dictionary<string, object>,
                };
            }

            return provider;
        }

        public AccountProvider GetAccountProviderByAppID(string AppID)
        {
            AccountProvider provider = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectAccountProviderByAppId;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@AppId", AppID } });

            if (dbResult != null && dbResult.Count == 1)
            {
                provider = new AccountProvider()
                {
                    ID = (string)dbResult[0]["Id"],//reader.GetString("Id"),
                    Name = (string)dbResult[0]["Name"],//reader.GetString("Name"),
                    AppID = (string)dbResult[0]["AppId"],//reader.IsDBNull(reader.GetOrdinal("AppId")) ? null : reader.GetString("AppId"),
                    AppSecret = (string)dbResult[0]["AppSecret"], //reader.IsDBNull(reader.GetOrdinal("AppSecret")) ? null : reader.GetString("AppSecret"),
                    DynamicProperties = JsonUtility.JsonDeserialize(Encoding.UTF8.GetBytes((string)dbResult[0]["AdditionalInfo"]), typeof(Dictionary<string, object>), null, "root") as Dictionary<string, object> //reader.IsDBNull(reader.GetOrdinal("AdditionalInfo")) ? null : JsonUtility.JsonDeserialize(Encoding.UTF8.GetBytes(reader.GetString("AdditionalInfo")), typeof(Dictionary<string, object>), null, "root") as Dictionary<string, object>,
                };
            }

            return provider;
        }

        public IDictionary<string, object> GetExternalAccounts(string AccountID, out IDictionary<string, object> ProviderInfoes, out IDictionary<string, object> AdditionalInfoes)
        {
            Dictionary<string, object> externalAccounts = null;
            externalAccounts = null;
            ProviderInfoes = null;
            AdditionalInfoes = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectExternalAccountsByAccountId;

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@AccountId", AccountID } });

            if (dbResult != null && dbResult.Count> 0)
            {
                externalAccounts = new Dictionary<string, object>();
                ProviderInfoes = new Dictionary<string, object>();
                AdditionalInfoes = new Dictionary<string, object>();

                string providerId = "";

                for (int i = 0; i < dbResult.Count; i++)
                {
                    providerId = (string)dbResult[i]["ProviderId"];

                    if (!externalAccounts.ContainsKey(providerId))
                    {
                        externalAccounts.Add(providerId, (string)dbResult[i]["Identifier"]);
                    }

                    if (!ProviderInfoes.ContainsKey(providerId))
                    {
                        ProviderInfoes.Add(providerId, this.GetAccountProvider(providerId));
                    }

                    if (dbResult[i]["AdditionalInfo"] != null && !AdditionalInfoes.ContainsKey(providerId))
                    {
                        AdditionalInfoes.Add(providerId, (string)dbResult[i]["AdditionalInfo"]);
                    }
                }
            }

            return externalAccounts;
        }

        public byte[] GetHeadImage(string UID, Func<object, object> ProcessingFunction)
        {
            Core.DomainModel.Account account = this.GetAccountHeadImage(UID);

            byte[] imageBytes = account.HeadImage.Bytes;

            if (ProcessingFunction != null)
            {
                imageBytes = ProcessingFunction(account.HeadImage.ID) as  byte[];
            }

            return imageBytes;
        }

        public byte[] GetImage(string ImageID, Func<object, object> ProcessingFunction)
        {
            byte[] imageBytes = null;

            if (ProcessingFunction != null)
            {
                imageBytes = ProcessingFunction(ImageID) as byte[];
            }

            return imageBytes;
        }

        public string SetImage(Attachment Envolope, byte[] ImageData, Func<object, object> ProcessingFunction)
        {
            if (Envolope == null)
            {
                Envolope = new Core.DomainModel.Attachment();
            }

            Envolope.Bytes = ImageData;

            string imageID = String.Empty;

            if (ProcessingFunction != null)
            {
                ProcessingFunction(Envolope);
            }

            if (Envolope != null)
            {
               imageID = Envolope.ID;
            }

            return imageID;
        }

        public IList<Core.DomainModel.Account> GetAccounts(int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, Func<object, object> ExtensionFunction)
        {
            List<Core.DomainModel.Account> accounts = null;
            TotalPageCount = 0;
            TotalRecords = 0;

            //string[] columnNames = new string[] { "Id", "Name", "FirstName", "LastName", "NickName", "Gender", "BirthDate", "Title", "SID", "Address", "ZipCode", "Email", "Mobile", "BloodType", "Constellation", "Hobby", "Education", "Industry", "Organization", "Department", "WorkGroup", "Position", "Headline", "Description", "HeadImageFileID", "Alias", "PhoneNumber", "PoliticsStatus" };

            string[] columnNames = new string[] { "Id", "Name", "FirstName", "LastName", "NickName", "Gender", "BirthDate", "Title", "SID", "Address", "ZipCode", "Email", "Mobile", "BloodType", "Constellation", "Hobby", "Education", "Industry", "Organization", "Department", "WorkGroup", "Position", "Headline", "Description", "HeadImageFileID", "Alias", "PhoneNumber", "PoliticsStatus" };

            var dbResult = this.dmlOperable.ExeReaderWithPaging("Accounts", "Id", "Id", columnNames, CurrentIndex, PageSize, out TotalPageCount, out TotalRecords, ExtensionFunction);

            if (dbResult != null && dbResult.Count > 0)
            {
                accounts = new List<Core.DomainModel.Account>();

                int gender = 0;

                for (int i = 0; i < dbResult.Count; i++)
                {
                    accounts.Add(new Core.DomainModel.Account()
                    {
                        ID = (string)dbResult[i]["Id"],
                        Name = (string)dbResult[i]["Name"],
                        Address = (string)dbResult[i]["Address"],
                        BirthDate = (DateTime?)dbResult[i]["BirthDate"],
                        Email = (string)dbResult[i]["Email"],
                        FirstName = (string)dbResult[i]["FirstName"],
                        LastName = (string)dbResult[i]["LastName"],
                        Gender = dbResult[i]["Gender"] == null ? 0 : int.TryParse(dbResult[i]["Gender"].ToString(), out gender) ? gender : 0, 
                        Mobile = (string)dbResult[i]["Mobile"],
                        NickName = (string)dbResult[i]["NickName"],
                        SID = (string)dbResult[i]["SID"],
                        Title = (string)dbResult[i]["Title"],
                        ZipCode = (string)dbResult[i]["ZipCode"],
                        HeadImage = new Attachment() { ID = (string)dbResult[i]["HeadImageFileID"] }, 
                        Headline = (string)dbResult[0]["Headline"],
                        Description = (string)dbResult[i]["Description"],

                        BloodType = (string)dbResult[i]["BloodType"],
                        Constellation = (string)dbResult[i]["Constellation"],
                        Hobby = (string)dbResult[i]["Hobby"],
                        Education = (string)dbResult[i]["Education"],
                        Industry = (string)dbResult[i]["Industry"],

                        Organization = (string)dbResult[i]["Organization"],
                        Department = (string)dbResult[i]["Department"],
                        WorkGroup = (string)dbResult[i]["WorkGroup"],

                        Alias = (string)dbResult[i]["Alias"],
                        PhoneNumber = (string)dbResult[i]["PhoneNumber"],
                        PoliticsStatus = (string)dbResult[i]["PoliticsStatus"],

                        Position = (string)dbResult[i]["Position"]
                    });
                }
            }

            return accounts;
        }

        public IList<Core.DomainModel.Account> GetAccounts()
        {
            List<Core.DomainModel.Account> accounts = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectAccountsAll;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, null);

            if (dbResult != null && dbResult.Count > 0)
            {
                accounts = new List<Core.DomainModel.Account>();

                int gender = 0;

                for (int i = 0; i < dbResult.Count; i++)
                {
                    accounts.Add(new Core.DomainModel.Account()
                    {
                        ID = (string)dbResult[i]["Id"],
                        Name = (string)dbResult[i]["Name"],
                        Address = (string)dbResult[i]["Address"],
                        BirthDate = (DateTime?)dbResult[i]["BirthDate"],
                        Email = (string)dbResult[i]["Email"],
                        FirstName = (string)dbResult[i]["FirstName"],
                        LastName = (string)dbResult[i]["LastName"],
                        Gender = dbResult[i]["Gender"] == null ? 0 : int.TryParse(dbResult[i]["Gender"].ToString(), out gender) ? gender : 0,
                        Mobile = (string)dbResult[i]["Mobile"],
                        NickName = (string)dbResult[i]["NickName"],
                        SID = (string)dbResult[i]["SID"],
                        Title = (string)dbResult[i]["Title"],
                        ZipCode = (string)dbResult[i]["ZipCode"],
                        HeadImage = new Attachment() { ID = (string)dbResult[i]["HeadImageFileID"] },
                        Headline = (string)dbResult[0]["Headline"],
                        Description = (string)dbResult[i]["Description"],

                        BloodType = (string)dbResult[i]["BloodType"],
                        Constellation = (string)dbResult[i]["Constellation"],
                        Hobby = (string)dbResult[i]["Hobby"],
                        Education = (string)dbResult[i]["Education"],
                        Industry = (string)dbResult[i]["Industry"],

                        Organization = (string)dbResult[i]["Organization"],
                        Department = (string)dbResult[i]["Department"],
                        WorkGroup = (string)dbResult[i]["WorkGroup"],

                        Alias = (string)dbResult[i]["Alias"],
                        PhoneNumber = (string)dbResult[i]["PhoneNumber"],
                        PoliticsStatus = (string)dbResult[i]["PoliticsStatus"],

                        Position = (string)dbResult[i]["Position"]
                    });
                }
            }

            return accounts;
        }

        public T DeleteAccount<T>(string ID, Func<object, T> ExtensionFunction)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return default(T);
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteAccount;

            var dbResult = this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@Id", ID } });

            if (dbResult >= 0)
            {
                if (ExtensionFunction != null)
                {
                   return ExtensionFunction(ID);
                }

                return (T)(object)true;
            }

            return (T)(object)false;
        }
    }
}
