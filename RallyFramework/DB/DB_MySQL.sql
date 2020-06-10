CREATE TABLE `tb_mon_Users` (
  `Users_Id` varchar(128) NOT NULL,
  `Users_UserName` varchar(256) NOT NULL,
  `Users_UserType` int(1) NULL,
  `Users_Email` varchar(256) DEFAULT NULL,
  `Users_EmailConfirmed` tinyint(1) NOT NULL,
  `Users_PasswordHash` longtext,
  `Users_PasswordSalt` varchar(128) NOT NULL,
  `Users_PasswordRev` int(11) NOT NULL,
  `Users_SecurityStamp` longtext,
  `Users_PhoneNumber` longtext,
  `Users_PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `Users_TwoFactorEnabled` tinyint(1) NOT NULL,
  `Users_LockoutEndDateUtc` datetime DEFAULT NULL,
  `Users_LockoutEnabled` tinyint(1) NOT NULL,
  `Users_AccessFailedCount` int(18) NOT NULL,  
  `Users_Description` longtext NULL,
  `Users_CreateDate` datetime,
  `Users_ConfirmationToken` varchar(128),
  `Users_IsConfirmed` tinyint(1) NOT NULL,
  `Users_LastPasswordFailureDate` datetime,
  `Users_PasswordFailuresSinceLastSuccess` int,
  `Users_PasswordChangedDate` datetime,
  `Users_PasswordVerificationToken` varchar(128),
  `Users_PasswordVerificationTokenExpirationDate` datetime,
  PRIMARY KEY (`Users_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_Accounts` (
  `Acc_Id` varchar(128) NOT NULL,
  `Acc_Name` varchar(252) NOT NULL,
  `Acc_FirstName` varchar(252) NULL,
  `Acc_LastName` varchar(252) NULL,
  `Acc_NickName` varchar(252) NOT NULL,
  `Acc_Gender` tinyint(1) DEFAULT NULL,
  `Acc_BirthDate` datetime DEFAULT NULL,
  `Acc_Title` varchar(252) NULL,
  `Acc_SID` varchar(252) NULL,
  `Acc_Alias` varchar(252) NULL,
  `Acc_Address` varchar(252) NULL,
  `Acc_ZipCode` varchar(252) NULL,
  `Acc_Email` varchar(252) DEFAULT NULL,
  `Acc_Mobile` varchar(252) DEFAULT NULL,
  `Acc_PhoneNumber` varchar(252) DEFAULT NULL,
  `Acc_BloodType` varchar(4) DEFAULT NULL,
  `Acc_Constellation` varchar(16) DEFAULT NULL,
  `Acc_Hobby` varchar(100) DEFAULT NULL,
  `Acc_PoliticsStatus` varchar(152) DEFAULT NULL,
  `Acc_Education` varchar(152) DEFAULT NULL,
  `Acc_Industry` varchar(152) DEFAULT NULL,
  `Acc_Organization` varchar(252) DEFAULT NULL,
  `Acc_Department` varchar(252) DEFAULT NULL,
  `Acc_WorkGroup` varchar(252) DEFAULT NULL,
  `Acc_Position` varchar(252) DEFAULT NULL,
  `Acc_Headline` varchar(252) DEFAULT NULL,
  `Acc_Biography` longtext DEFAULT NULL,
  `Acc_Description` varchar(500) DEFAULT NULL,
  `Acc_HeadImageFileID` varchar(252) NULL,
  `Acc_ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Acc_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_Roles` 
(
  `Roles_Id` VARCHAR(250) NOT NULL,
  `Roles_Name` VARCHAR(250) NOT NULL,
  `Roles_RoleType` VARCHAR(250) NOT NULL,
  `Roles_Description` VARCHAR(250) NOT NULL,
   PRIMARY KEY (`Roles_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_RoleActors` 
(
  `Rol_Id` VARCHAR(250) NOT NULL,
  `Rol_RoleId` VARCHAR(250) NOT NULL,
  `Rol_ActorId` VARCHAR(250) NOT NULL,
   PRIMARY KEY (`Rol_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_Operations` 
(
  `Ope_Id` VARCHAR(250) NOT NULL,
  `Ope_Name` VARCHAR(250) NOT NULL,
  `Ope_DataType` VARCHAR(250) NOT NULL,
   PRIMARY KEY (`Ope_Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_DataScopes` (
  `Dat_Id` VARCHAR(250) NOT NULL COMMENT '',
  `Dat_ScopeName` VARCHAR(250) NOT NULL COMMENT '',
  `Dat_ScopeType` VARCHAR(250) NOT NULL COMMENT '',
  `Dat_DataType` VARCHAR(250) NOT NULL COMMENT '',
  `Dat_DataIdentifier` VARCHAR(250) NOT NULL COMMENT '',
   PRIMARY KEY (`Dat_Id`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_ObjectOperationAuthItems` (
  `OOA_Id` VARCHAR(250) NOT NULL COMMENT '',
  `OOA_ObjectId` VARCHAR(250) NOT NULL COMMENT '',
  `OOA_ActorId` VARCHAR(250) NOT NULL COMMENT '',
  `OOA_OperationId` VARCHAR(250) NOT NULL COMMENT '',
  PRIMARY KEY (`OOA_Id`),
  INDEX `IX_FK_OperationObjectOperationAuthItem` (`OOA_OperationId` ASC),
  CONSTRAINT `FK_OperationObjectOperationAuthItem`
    FOREIGN KEY (`OOA_OperationId`)
    REFERENCES `tb_mon_Operations` (`Ope_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_RoleOperations` (
  `Rop_Id` VARCHAR(250) NOT NULL COMMENT '',
  `Rop_RoleId` VARCHAR(250) NOT NULL COMMENT '',
  `Rop_OperationId` VARCHAR(250) NOT NULL COMMENT '',
  PRIMARY KEY (`Rop_Id`),
  INDEX `IX_FK_OperationRoleOperation` (`Rop_OperationId` ASC),
  CONSTRAINT `FK_OperationRoleOperation`
    FOREIGN KEY (`Rop_OperationId`)
    REFERENCES `tb_mon_Operations` (`Ope_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
)ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE `tb_mon_RoleDataScopes` (
  `RDS_Id` VARCHAR(250) NOT NULL COMMENT '',
  `RDS_RoleId` VARCHAR(250) NOT NULL COMMENT '',
  `RDS_DataScopeId` VARCHAR(250) NOT NULL COMMENT '',
  `RDS_ScopeValue` VARCHAR(250) NOT NULL COMMENT '',
  PRIMARY KEY (`RDS_Id`),
  INDEX `IX_FK_DataScopeRoleDataScope` (`RDS_DataScopeId` ASC),
  CONSTRAINT `FK_DataScopeRoleDataScope`
    FOREIGN KEY (`RDS_DataScopeId`)
    REFERENCES `tb_mon_DataScopes` (`Dat_Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_Files` (
  `File_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `File_FilePath` varchar(1200) NOT NULL,
  `File_FileName` varchar(250) NOT NULL,
  `File_FileType` varchar(20) NOT NULL,
  `File_FileSize` varchar(20) DEFAULT NULL,
  `File_Version` varchar(20) DEFAULT NULL,
  `File_FileCreationTime` datetime NOT NULL,
  `File_FileOwner` varchar(128)  NOT NULL,
   PRIMARY KEY (`File_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_Logs` (
  `Logs_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Logs_TimeStamp` datetime NOT NULL, 
  `Logs_Category` varchar(25) NULL,
  `Logs_LogLevel` varchar(25) NOT NULL,
  `Logs_CallSite` varchar(5000) DEFAULT NULL,
  `Logs_Message` longtext,
  `Logs_StackTrace` varchar(5000) DEFAULT NULL,
  `Logs_Exception` varchar(5000) DEFAULT NULL,
  `Logs_MachineName` varchar(30) DEFAULT NULL,
  `Logs_Identity` varchar(40) DEFAULT NULL,
  `Logs_ProcessName` varchar(40) DEFAULT NULL,
  `Logs_ThreadName` varchar(40) DEFAULT NULL,
  `Logs_LoggerName` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`Logs_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;


CREATE TABLE `tb_mon_ApplicationSettings` (
  `app_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `app_PlatformIpAddress` varchar(20) DEFAULT NULL,
  `app_PlatformPortNumber` int DEFAULT NULL,
  `app_AlarmMode` int DEFAULT NULL,
  `app_AlarmSound` varchar(20) DEFAULT NULL,
  `app_NotificationMode` int DEFAULT NULL,
  `app_NotificationSound` varchar(20) DEFAULT NULL,
  `app_Language` int DEFAULT NULL,
   PRIMARY KEY (`app_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_ReportSettings` (
  `rep_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `rep_Title` varchar(20) DEFAULT NULL,
  `rep_TemplateID` bigint(20) NOT NULL,
  `rep_TemplateName` varchar(200),
  `rep_AlarmMode` int DEFAULT NULL,
  `rep_Printer` varchar(200) DEFAULT NULL,
  `rep_AutoPrintOnAlarm` int NOT NULL,
  `rep_AutoPrintOnMeasurement` int NOT NULL,
   PRIMARY KEY (`rep_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_instruments` (
  `Ins_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Ins_Name` varchar(20) NOT NULL,
  `Ins_Alias` varchar(20) DEFAULT NULL,
  `Ins_Type` int(1) DEFAULT NULL,
  `Ins_Model` varchar(20) DEFAULT NULL,
  `Ins_SerialNumber` varchar(20) DEFAULT NULL,
  `Ins_Manufacturer` varchar(20) DEFAULT NULL,
  `Ins_Brand` varchar(20) DEFAULT NULL,
  `Ins_SKU` varchar(20) DEFAULT NULL,
  `Ins_WarrantyPeriod` int(11) DEFAULT NULL,
  `Ins_ShipmentDate` datetime DEFAULT NULL,
  `Ins_PurchaseDate` datetime DEFAULT NULL,
  `Ins_Latitude` varchar(20) DEFAULT NULL,
  `Ins_Longitude` varchar(20) DEFAULT NULL,
  `Ins_Location` varchar(20) DEFAULT NULL,
  `Ins_InstallationDate` datetime DEFAULT NULL,
  `Ins_AcceptanceDate` datetime DEFAULT NULL,
  `Ins_Organization` varchar(20) DEFAULT NULL,
  `Ins_Department` varchar(20) DEFAULT NULL,
  `Ins_WorkGroup` varchar(20) DEFAULT NULL,
  `Ins_Remarks` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`Ins_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_InstrumentCommnunicationSettings` (
  `ics_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Ins_InstrumentID` bigint(20) NOT NULL,
  `ics_Type` int(1) DEFAULT NULL,
  `ics_IpAddress` varchar(20) DEFAULT NULL,
  `ics_PortNumber` int DEFAULT NULL,
  `ics_SerialPortName` varchar(20) DEFAULT NULL,
  `ics_SerialPortBaudRate` int DEFAULT NULL,
  `ics_BluetoothDeviceName` varchar(20) DEFAULT NULL,
  `ics_BluetoothAddress` varchar(20) DEFAULT NULL,
  `ics_BluetoothKey` varchar(20) DEFAULT NULL,
  `ics_Remarks` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`ics_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_InstrumentCameraSettings` (
  `ics_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Ins_InstrumentID` bigint(20) NOT NULL,
  `ics_CameraType` int(1) DEFAULT NULL,
  `ics_ConnectionType` int(1) DEFAULT NULL,
  `ics_Model` varchar(20) DEFAULT NULL,
  `ics_SerialNumber` varchar(20) DEFAULT NULL,
  `ics_Manufacturer` varchar(20) DEFAULT NULL,
  `ics_Brand` varchar(20) DEFAULT NULL,
  `ics_ics_SKU` varchar(20) DEFAULT NULL,
  `ics_IpAddress` varchar(20) DEFAULT NULL,
  `ics_PortNumber` int DEFAULT NULL,
  `ics_LoginName` varchar(20) DEFAULT NULL,
  `ics_Password` varchar(20) DEFAULT NULL,
  `ics_AssemblyName` varchar(20) DEFAULT NULL,
  `ics_AssemblyPath` varchar(20) DEFAULT NULL,
  `ics_ClassName` varchar(20) DEFAULT NULL,
  `ics_Version` varchar(20) DEFAULT NULL,
  `ics_Remarks` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`ics_Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_InstrumentFaults` (
  `ifa_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Ins_InstrumentID` bigint(20) NOT NULL,
  `ifa_FaultTime` datetime NOT NULL,
  `ifa_FaultType` int NOT NULL,
  `ifa_FaultCode` varchar(20) DEFAULT NULL,
  `ifa_FaultMessage` varchar(20) NOT NULL,
   PRIMARY KEY (`ifa_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_MaintenanceOrders` (
  `mor_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Ins_InstrumentID` bigint(20) NOT NULL,
  `mor_OrderRefID` varchar(20) NOT NULL,
  `mor_OrderDate` datetime NOT NULL,
  `mor_FaultTime` datetime NOT NULL,
  `mor_WarrantyStatus` int(1) NOT NULL,
  `mor_Cost` varchar(20) DEFAULT NULL,
  `mor_FaultCode` varchar(20) DEFAULT NULL,
  `mor_FaultDescription` varchar(20) DEFAULT NULL,
  `mor_RepairResult` varchar(20) DEFAULT NULL,
  `mor_RepairDescription` varchar(200) DEFAULT NULL,
  `mor_UserComment` varchar(20) DEFAULT NULL,
  `mor_CommentContent` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`mor_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_MaintenanceOrderLineitems` (
  `mol_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `mor_OrderID` bigint(20) NOT NULL,
  `mol_FulfillmentDate` datetime NOT NULL,
  `mol_Notes` varchar(200) DEFAULT NULL,
  `mol_RepairResult` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`mol_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_MaintenanceOrderAttachments` (
  `mat_ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `mor_OrderID` bigint(20) NOT NULL,
  `File_FileID` bigint(20) NOT NULL,
  `mat_FileCreationTime` datetime NOT NULL,
   PRIMARY KEY (`mat_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;