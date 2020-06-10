using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IAccountManager
    {
        string AddAccount(Account Account);

        Account GetAccount(string ID);

        Account SetAccount(string ID, Account Account);

        Account SetAccount(string ID, Account Account, bool ShouldUpdateName);

        Account GetAccountByMobile(string Mobile);

        Account GetAccountByNickName(string NickName);

        IList<Account> GetAccounts(int CurrentIndex, int PageSize, out int TotalPageCount, out int TotalRecords, Func<object, object> ExtensionFunction);

        IList<Account> GetAccounts();

        T DeleteAccount<T>(string ID, Func<object, T> ExtensionFunction);

        string AddAccountProvider(AccountProvider Provider);

        AccountProvider GetAccountProvider(string ID);

        AccountProvider GetAccountProviderByAppID(string AppID);

        string AddExternalAccount(string AccountID, string ProviderID, string Identifier, object Meta);

        IDictionary<string, object> GetExternalAccounts(string AccountID, out IDictionary<string, object> ProviderInfoes, out IDictionary<string, object> AdditionalInfoes);

        Account GetAccountHeadImage(string ID);

        byte[] GetImage(string ImageID, Func<object, object> ProcessingFunction);

        byte[] GetHeadImage(string UID, Func<object, object> ProcessingFunction);

        string SetImage(Rally.Framework.Core.DomainModel.Attachment Envolope, byte[] ImageData, Func<object, object> ProcessingFunction);
    }
}
