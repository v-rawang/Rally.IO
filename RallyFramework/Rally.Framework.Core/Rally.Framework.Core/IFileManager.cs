using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Core
{
    public interface IFileManager
    {
        string AddFile(byte[] FileBytes, IDictionary<string, object> FileInfo);
        T AddFile<T>(byte[] FileBytes, IDictionary<string, object> FileInfo, Func<object, T> ExtensionFunction);
        string SetFile(byte[] FileBytes, IDictionary<string, object> FileInfo);
        T SetFile<T>(byte[] FileBytes, IDictionary<string, object> FileInfo, Func<object, T> ExtensionFunction);
        string RemoveFile(string FileID);
        byte[] GetFileBytes(string FileID);
        byte[] GetFileBytes<T>(string FileID, out T ExtraData, Func<object, T> ExtensionFunction);
        IDictionary<string, object> GetFileInfo(string FileID);
        IList<IDictionary<string, object>> GetFileInfoes();
        string GenerateFileID(int Increment);

        string AddFileInfo(IDictionary<string, object> FileInfo);
        System.Data.DataTable GetFileDataTable(string strID);
    }
}
