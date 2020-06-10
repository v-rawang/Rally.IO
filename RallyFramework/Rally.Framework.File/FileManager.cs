using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Lib.Utility.Common;

namespace Rally.Framework.File
{
    public class FileManager : IFileManager
    {
        public FileManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static IFileManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new FileManager(DMLOperable, DBType);
        }

        public string AddFile(byte[] FileBytes, IDictionary<string, object> FileInfo)
        {
            if (FileBytes == null || FileBytes.Length <= 0)
            {
                return null;
            }

            string filePath = (string)FileInfo["FilePath"], fileName = (string)FileInfo["FileName"], fileOwner = (string)FileInfo["FileOwner"], fileVersion = (string)FileInfo["Version"], fileType = (string)FileInfo["FileType"];

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileFullPath = $"{filePath}{Path.DirectorySeparatorChar}{fileName}"; 

            DateTime fileCreationTime = DateTime.Now;
            //string fileID = CommonUtility.GetMillisecondsByDateTime(fileCreationTime, null, true);
            int fileSize = FileBytes.Length;

            using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                fileStream.Write(FileBytes, 0, fileSize);
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertFile;

            int result = this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(fileID)},
                { "@FilePath", filePath},
                { "@FileName", fileName},
                { "@FileType", fileType},
                { "@FileSize",fileSize},
                { "@Version", fileVersion},
                { "@FileOwner", fileOwner},
                { "@FileCreationTime",fileCreationTime}
            });

            if (result >= 0)
            {
                return GenerateFileID(0); //fileID;
            }

            return null;
        }

        public T AddFile<T>(byte[] FileBytes, IDictionary<string, object> FileInfo, Func<object, T> ExtensionFunction)
        {
            if (FileBytes == null || FileBytes.Length <= 0)
            {
                return default(T);
            }

            string filePath = (string)FileInfo["FilePath"], fileName = (string)FileInfo["FileName"], fileOwner = (string)FileInfo["FileOwner"], fileVersion = (string)FileInfo["Version"], fileType = (string)FileInfo["FileType"];

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileFullPath = $"{filePath}{Path.DirectorySeparatorChar}{fileName}";
            DateTime fileCreationTime = DateTime.Now;
            //string fileID = CommonUtility.GetMillisecondsByDateTime(fileCreationTime, null, true);
            int fileSize = FileBytes.Length;

            using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fileStream.Write(FileBytes, 0, fileSize);
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertFile;

            int result = this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(fileID)},
                { "@FilePath", filePath},
                { "@FileName", fileName},
                { "@FileType", fileType},
                { "@FileSize",fileSize},
                { "@Version", fileVersion},
                { "@FileOwner", fileOwner},
                { "@FileCreationTime", fileCreationTime}
            });

            if (result >= 0)
            {
                string fileID = GenerateFileID(0);

                if (ExtensionFunction != null)
                {
                    return ExtensionFunction(new { FileID = fileID, FileData = FileBytes, FileFullPath = fileFullPath, FileSize = fileSize, FileOwner = fileOwner, Version = fileVersion });
                }

                return (T)((object)(new { ID = fileID }));

            }

            return default(T);
        }

        public byte[] GetFileBytes(string FileID)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectFileById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(FileID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                string filePath = (string)dbResult[0]["FilePath"], fileName = (string)dbResult[0]["FileName"], fileOwner = (string)dbResult[0]["FileOwner"];
                string fileFullPath = $"{filePath}{Path.DirectorySeparatorChar}{fileName}";
                byte[] fileBytes = null;

                using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fileBytes = new byte[fileStream.Length];
                    fileStream.Read(fileBytes, 0, fileBytes.Length);
                }

                return fileBytes;
            }

            return null;
        }

        public byte[] GetFileBytes<T>(string FileID, out T ExtraData, Func<object, T> ExtensionFunction)
        {
            ExtraData = default(T);

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectFileById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(FileID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                string filePath = (string)dbResult[0]["FilePath"], fileName = (string)dbResult[0]["FileName"], fileOwner = (string)dbResult[0]["FileOwner"], fileVersion = (string)dbResult[0]["Version"];
                string fileFullPath = $"{filePath}{Path.DirectorySeparatorChar}{fileName}";
                byte[] fileBytes = null;

                using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fileBytes = new byte[fileStream.Length];
                    fileStream.Read(fileBytes, 0, fileBytes.Length);
                }

                if (ExtensionFunction != null)
                {
                    ExtraData = ExtensionFunction(new { FileID = FileID, FileData = fileBytes, FileFullPath = fileFullPath, FileSize = fileBytes.Length, FileOwner = fileOwner, Version = fileVersion });
                }

                return fileBytes;
            }
            return null;
        }
        public IDictionary<string, object> GetFileInfo(string FileID)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectFileById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(FileID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                return dbResult[0];
            }

            return null;
        }

        public IList<IDictionary<string, object>> GetFileInfoes()
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectFile;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, null);

            return dbResult;
        }

        public string RemoveFile(string FileID)
        {
            IDictionary<string, object> fileInfo = this.GetFileInfo(FileID);

            if (fileInfo != null)
            {
                string fileFullPath = $"{fileInfo["FilePath"]}{Path.DirectorySeparatorChar}{fileInfo["FileName"]}";

                if (System.IO.File.Exists(fileFullPath))
                {
                    System.IO.File.Delete(fileFullPath);
                }

                string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteFile;

                var dbResult = this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(FileID) } });

                if (dbResult >= 0)
                {
                    return FileID;
                }
            }

            return null;
        }

        public string SetFile(byte[] FileBytes, IDictionary<string, object> FileInfo)
        {
            if (FileBytes == null || FileBytes.Length <= 0)
            {
                return null;
            }

            string filePath = (string)FileInfo["FilePath"], fileName = (string)FileInfo["FileName"], fileOwner = (string)FileInfo["FileOwner"], fileVersion = (string)FileInfo["Version"], fileType = (string)FileInfo["FileType"];

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileFullPath = $"{filePath}{Path.DirectorySeparatorChar}{fileName}";
            DateTime fileCreationTime = DateTime.Now;
            string fileID = FileInfo["ID"].ToString();
            int fileSize = FileBytes.Length;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateFile;

            int result = this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(fileID)},
                { "@FilePath", filePath},
                { "@FileName", fileName},
                { "@FileType", fileType},
                { "@FileSize",fileSize},
                { "@Version", fileVersion},
                { "@FileOwner", fileOwner},
                { "@FileCreationTime", fileCreationTime}
            });

            if (result >= 0)
            {
                using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    fileStream.Write(FileBytes, 0, fileSize);
                }

                return fileID;
            }

            return null;
        }

        public T SetFile<T>(byte[] FileBytes, IDictionary<string, object> FileInfo, Func<object, T> ExtensionFunction)
        {
            if (FileBytes == null || FileBytes.Length <= 0)
            {
                return default(T);
            }

            string filePath = (string)FileInfo["FilePath"], fileName = (string)FileInfo["FileName"], fileOwner = (string)FileInfo["FileOwner"], fileVersion = (string)FileInfo["Version"], fileType = (string)FileInfo["FileType"];

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string fileFullPath = $"{filePath}{Path.DirectorySeparatorChar}{fileName}";
            DateTime fileCreationTime = DateTime.Now;
            string fileID = FileInfo["ID"].ToString();
            int fileSize = FileBytes.Length;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateFile;

            int result = this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(fileID)},
                { "@FilePath", filePath},
                { "@FileName", fileName},
                { "@FileType", fileType},
                { "@FileSize",fileSize},
                { "@Version", fileVersion},
                { "@FileOwner", fileOwner},
                { "@FileCreationTime", fileCreationTime}
            });

            if (result >= 0)
            {
                using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    fileStream.Write(FileBytes, 0, fileSize);
                }

                if (ExtensionFunction != null)
                {
                    return ExtensionFunction(new { FileID = fileID, FileData = FileBytes, FileFullPath = fileFullPath, FileSize = FileBytes.Length, FileOwner = fileOwner, Version = fileVersion });
                }

                return (T)(object)(new { FileID = fileID });
            }

            return default(T);
        }

        public string GenerateFileID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxFileID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }

        public string AddFileInfo(IDictionary<string, object> FileInfo)
        {
            string filePath = (string)FileInfo["FilePath"];
            string fileName = (string)FileInfo["FileName"];
            string fileOwner = (string)FileInfo["FileOwner"];
            string fileVersion = (string)FileInfo["Version"];
            string fileType = (string)FileInfo["FileType"];
            string fileSize = (string)FileInfo["FileSize"];
            DateTime fileCreationTime = (DateTime)FileInfo["FileCreationTime"];

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertFile;

            int result = this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(fileID)},
                { "@FilePath", filePath},
                { "@FileName", fileName},
                { "@FileType", fileType},
                { "@FileSize", fileSize},
                { "@Version", fileVersion},
                { "@FileOwner", fileOwner},
                { "@FileCreationTime",fileCreationTime}
            });

            if (result >= 0)
            {
                return GenerateFileID(0); //fileID;
            }

            return null;
        }


        public System.Data.DataTable GetFileDataTable(string strID)
        {
            return this.dmlOperable.GetDataTable(ModuleConfiguration.SQL_CMD_SelectFileData, new Dictionary<string, object>() { { "@OrderID", strID } });
        }
    }
}
