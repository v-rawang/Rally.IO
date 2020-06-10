using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.File
{
    public class ModuleConfiguration
    {
        //public static string SQL_CMD_InsertFile = "INSERT INTO files (FilePath, FileName, FileType, FileSize, Version, FileCreationTime, FileOwner) VALUES (@FilePath, @FileName, @FileType, @FileSize, @Version, @FileCreationTime, @FileOwner);";
        //public static string SQL_CMD_UpdateFile = "UPDATE files SET FilePath = @FilePath, FileName = @FileName, FileType = @FileType, FileSize = @FileSize, Version = @Version, FileCreationTime = @FileCreationTime, FileOwner = @FileOwner WHERE ID = @ID";
        //public static string SQL_CMD_DeleteFile = "DELETE FROM  files WHERE ID = @ID;";
        //public static string SQL_CMD_SelectFileById = "SELECT ID, FilePath, FileName, FileType, FileSize, Version, FileCreationTime, FileOwner FROM  files WHERE ID = @ID;";
        //public static string SQL_CMD_SelectFile = "SELECT ID, FilePath, FileName, FileType, FileSize, Version, FileCreationTime, FileOwner FROM  files;";
        //public static string SQL_CMD_SelectMaxFileID = "SELECT MAX(ID) FROM files";
        //public static string SQL_CMD_SelectFileData = "SELECT M.ID, FilePath, FileName, FileType, FileSize, Version, M.FileCreationTime, FileOwner,OrderRefID FROM  maintenance_order_attachments M,files A,maintenance_orders MO where A.ID = M.FileID and MO.ID = M.OrderID AND M.OrderID = @OrderID;";

        //public static string SQL_CMD_InsertFile = "INSERT INTO tb_mon_files (File_FilePath, File_FileName, File_FileType, File_FileSize, File_Version, File_FileCreationTime, File_FileOwner) VALUES (@FilePath, @FileName, @FileType, @FileSize, @Version, @FileCreationTime, @FileOwner);";
        //public static string SQL_CMD_UpdateFile = "UPDATE tb_mon_files SET File_FilePath = @FilePath, File_FileName = @FileName, File_FileType = @FileType, File_FileSize = @FileSize, File_Version = @Version, File_FileCreationTime = @FileCreationTime, File_FileOwner = @FileOwner WHERE File_ID = @ID";
        //public static string SQL_CMD_DeleteFile = "DELETE FROM  tb_mon_files WHERE File_ID = @ID;";
        //public static string SQL_CMD_SelectFileById = "SELECT File_ID as ID, File_FilePath as FilePath, File_FileName as FileName, File_FileType as FileType, File_FileSize as FileSize, File_Version as Version, File_FileCreationTime as FileCreationTime, File_FileOwner as FileOwner FROM tb_mon_files WHERE File_ID = @ID;";
        //public static string SQL_CMD_SelectFile = "SELECT ID, File_ID as ID, File_FilePath as FilePath, File_FileName as FileName, File_FileType as FileType, File_FileSize as FileSize, File_Version as Version, File_FileCreationTime as FileCreationTime, File_FileOwner as FileOwner FROM tb_mon_files;";
        //public static string SQL_CMD_SelectMaxFileID = "SELECT MAX(File_ID) FROM tb_mon_files";
        //public static string SQL_CMD_SelectFileData = "SELECT M.File_FileID as ID, File_FilePath as FilePath, File_FileName as FileName, File_FileType as FileType, File_FileSize as FileSize, File_Version as Version, M.mat_FileCreationTime as FileCreationTime, File_FileOwner as FileOwner, mor_OrderRefID as OrderRefID FROM  tb_mon_MaintenanceOrderAttachments M,tb_mon_files A,tb_mon_MaintenanceOrders MO where A.File_ID = M.File_FileID and MO.mor_ID = M.mor_OrderID AND M.mor_OrderID = @OrderID;";

        public static string SQL_CMD_InsertFile = "INSERT INTO files (FilePath, FileName, FileType, FileSize, Version, FileCreationTime, FileOwner) VALUES (@FilePath, @FileName, @FileType, @FileSize, @Version, @FileCreationTime, @FileOwner);";
        public static string SQL_CMD_UpdateFile = "UPDATE files SET FilePath = @FilePath, FileName = @FileName, FileType = @FileType, FileSize = @FileSize, Version = @Version, FileCreationTime = @FileCreationTime, FileOwner = @FileOwner WHERE ID = @ID";
        public static string SQL_CMD_DeleteFile = "DELETE FROM  files WHERE ID = @ID;";
        public static string SQL_CMD_SelectFileById = "SELECT ID as ID, FilePath as FilePath, FileName as FileName, FileType as FileType, FileSize as FileSize, Version as Version, FileCreationTime as FileCreationTime, FileOwner as FileOwner FROM files WHERE ID = @ID;";
        public static string SQL_CMD_SelectFile = "SELECT ID, ID as ID, FilePath as FilePath, FileName as FileName, FileType as FileType, FileSize as FileSize, Version as Version, FileCreationTime as FileCreationTime, FileOwner as FileOwner FROM files;";
        public static string SQL_CMD_SelectMaxFileID = "SELECT MAX(ID) FROM files";
        public static string SQL_CMD_SelectFileData = "SELECT M.FileID as ID, FilePath as FilePath, FileName as FileName, FileType as FileType, FileSize as FileSize, Version as Version, M.mat_FileCreationTime as FileCreationTime, FileOwner as FileOwner, mor_OrderRefID as OrderRefID FROM  MaintenanceOrderAttachments M,files A,MaintenanceOrders MO where A.ID = M.FileID and MO.mor_ID = M.mor_OrderID AND M.mor_OrderID = @OrderID;";

    }
}
