/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50729
Source Host           : localhost:3306
Source Database       : db_edge

Target Server Type    : MYSQL
Target Server Version : 50729
File Encoding         : 65001

Date: 2020-03-20 19:37:02
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `accounts`
-- ----------------------------
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE `accounts` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(252) NOT NULL,
  `FirstName` varchar(252) DEFAULT NULL,
  `LastName` varchar(252) DEFAULT NULL,
  `NickName` varchar(252) NOT NULL,
  `Gender` tinyint(1) DEFAULT NULL,
  `BirthDate` datetime DEFAULT NULL,
  `Title` varchar(252) DEFAULT NULL,
  `SID` varchar(252) DEFAULT NULL,
  `Alias` varchar(252) DEFAULT NULL,
  `Address` varchar(252) DEFAULT NULL,
  `ZipCode` varchar(252) DEFAULT NULL,
  `Email` varchar(252) DEFAULT NULL,
  `Mobile` varchar(252) DEFAULT NULL,
  `PhoneNumber` varchar(252) DEFAULT NULL,
  `BloodType` varchar(4) DEFAULT NULL,
  `Constellation` varchar(16) DEFAULT NULL,
  `Hobby` varchar(100) DEFAULT NULL,
  `PoliticsStatus` varchar(152) DEFAULT NULL,
  `Education` varchar(152) DEFAULT NULL,
  `Industry` varchar(152) DEFAULT NULL,
  `Organization` varchar(252) DEFAULT NULL,
  `Department` varchar(252) DEFAULT NULL,
  `WorkGroup` varchar(252) DEFAULT NULL,
  `Position` varchar(252) DEFAULT NULL,
  `Headline` varchar(252) DEFAULT NULL,
  `Biography` longtext,
  `Description` varchar(500) DEFAULT NULL,
  `HeadImageFileID` varchar(252) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of accounts
-- ----------------------------
INSERT INTO `accounts` VALUES ('USR-SYS-BUILTIN-ADMIN', 'admin', '管理员', '系统', '系统管理员', '0', '2020-03-20 17:59:27', null, null, '', null, null, '', '', '', null, null, null, null, null, null, '', '', null, '', null, null, null, null, '2020-03-20 18:00:54');
INSERT INTO `accounts` VALUES ('USR-SYS-BUILTIN-SUPPER', 'root', null, null, 'root', null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, '2020-03-20 17:59:04');

-- ----------------------------
-- Table structure for `applicationsettings`
-- ----------------------------
DROP TABLE IF EXISTS `applicationsettings`;
CREATE TABLE `applicationsettings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `PlatformIpAddress` varchar(20) DEFAULT NULL,
  `PlatformPortNumber` int(11) DEFAULT NULL,
  `AlarmMode` int(11) DEFAULT NULL,
  `AlarmSound` varchar(20) DEFAULT NULL,
  `NotificationMode` int(11) DEFAULT NULL,
  `NotificationSound` varchar(20) DEFAULT NULL,
  `Language` int(11) DEFAULT NULL,
  `Protocol` varchar(30) DEFAULT NULL,
  `name2` varchar(30) DEFAULT NULL,
  `PlatformIpAddress2` varchar(30) DEFAULT NULL,
  `PlatformPortNumber2` varchar(30) DEFAULT NULL,
  `Protocol2` varchar(30) DEFAULT NULL,
  `name3` varchar(30) DEFAULT NULL,
  `PlatformIpAddress3` varchar(30) DEFAULT NULL,
  `PlatformPortNumber3` varchar(30) DEFAULT NULL,
  `Protocol3` varchar(30) DEFAULT NULL,
  `name4` varchar(30) DEFAULT NULL,
  `PlatformIpAddress4` varchar(30) DEFAULT NULL,
  `PlatformPortNumber4` varchar(30) DEFAULT NULL,
  `Protocol4` varchar(30) DEFAULT NULL,
  `theme` varchar(30) DEFAULT NULL,
  `time` varchar(100) DEFAULT NULL,
  `type` varchar(100) DEFAULT NULL,
  `Duration2` varchar(30) DEFAULT NULL,
  `Duration1` varchar(30) DEFAULT NULL,
  `name` varchar(30) DEFAULT NULL,
  `BgValArchFrequency` int DEFAULT NULL comment '本底数据归档周期', 
  `ImageCapturingMode` int DEFAULT NULL comment '是否抓拍照片',
  `ImageCapturingCount` int DEFAULT NULL comment '抓拍照片数量',
  `VideoCapturingMode` int DEFAULT NULL comment '是否录制视频',
  `VideoCapturingLength` int DEFAULT NULL comment '录制视频长度',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of applicationsettings
-- ----------------------------
-- INSERT INTO `applicationsettings` VALUES ('00000000000000000002', null, '0', '1', 'ALARM2.WAV', '1', 'ALARM2.WAV', '2', null, null, null, '0', null, null, null, '0', null, null, null, '0', null, '默认', '每月|1|24|0|0', null, '0', '0', null);

INSERT INTO `applicationsettings` VALUES ('00000000000000000002', null, '0', '1', 'ALARM2.WAV', '1', 'ALARM2.WAV', '2', null, null, null, '0', null, null, null, '0', null, null, null, '0', null, '默认', '每月|1|24|0|0', null, '0', '0', null, 60, 0,3,0,5);

-- ----------------------------
-- Table structure for `datascopes`
-- ----------------------------
DROP TABLE IF EXISTS `datascopes`;
CREATE TABLE `datascopes` (
  `Id` varchar(250) NOT NULL,
  `ScopeName` varchar(250) NOT NULL,
  `ScopeType` varchar(250) NOT NULL,
  `DataType` varchar(250) NOT NULL,
  `DataIdentifier` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of datascopes
-- ----------------------------
INSERT INTO `datascopes` VALUES ('SCOPE-DEVICE-OWNER', 'Owner[@Role = $Role]', 'string', 'Device', 'ID');

-- ----------------------------
-- Table structure for `files`
-- ----------------------------
DROP TABLE IF EXISTS `files`;
CREATE TABLE `files` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `FilePath` varchar(1200) NOT NULL,
  `FileName` varchar(250) NOT NULL,
  `FileType` varchar(20) NOT NULL,
  `FileSize` varchar(20) DEFAULT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `FileCreationTime` datetime NOT NULL,
  `FileOwner` varchar(128) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of files
-- ----------------------------

-- ----------------------------
-- Table structure for `instrumentcamerasettings`
-- ----------------------------
DROP TABLE IF EXISTS `instrumentcamerasettings`;
CREATE TABLE `instrumentcamerasettings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `InstrumentID` bigint(20) NOT NULL,
  `CameraType` int(1) DEFAULT NULL,
  `ConnectionType` int(1) DEFAULT NULL,
  `Model` varchar(200) DEFAULT NULL,
  `SerialNumber` varchar(200) DEFAULT NULL,
  `Manufacturer` varchar(200) DEFAULT NULL,
  `Brand` varchar(200) DEFAULT NULL,
  `SKU` varchar(200) DEFAULT NULL,
  `IpAddress` varchar(200) DEFAULT NULL,
  `PortNumber` int(11) DEFAULT NULL,
  `LoginName` varchar(200) DEFAULT NULL,
  `Password` varchar(200) DEFAULT NULL,
  `AssemblyName` varchar(200) DEFAULT NULL,
  `AssemblyPath` varchar(200) DEFAULT NULL,
  `ClassName` varchar(200) DEFAULT NULL,
  `Version` varchar(200) DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
  `Index` int(2) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of instrumentcamerasettings
-- ----------------------------
INSERT INTO `instrumentcamerasettings` VALUES ('00000000000000000009', '8', '1', '2', '请选择', null, null, null, null, '', '0', '', '', null, null, null, null, null, '1');
INSERT INTO `instrumentcamerasettings` VALUES ('00000000000000000010', '8', '1', '2', '', null, null, null, null, '', '0', '', '', null, null, null, null, null, '2');

-- ----------------------------
-- Table structure for `instrumentcommnunicationsettings`
-- ----------------------------
DROP TABLE IF EXISTS `instrumentcommnunicationsettings`;
CREATE TABLE `instrumentcommnunicationsettings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `InstrumentID` bigint(200) NOT NULL,
  `Type` int(1) DEFAULT NULL,
  `IpAddress` varchar(200) DEFAULT NULL,
  `PortNumber` int(11) DEFAULT NULL,
  `SerialPortName` varchar(200) DEFAULT NULL,
  `SerialPortBaudRate` int(11) DEFAULT NULL,
  `BluetoothDeviceName` varchar(200) DEFAULT NULL,
  `BluetoothAddress` varchar(200) DEFAULT NULL,
  `BluetoothKey` varchar(200) DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
  `Protocol` varchar(200) DEFAULT NULL,
  `AssemblyName` varchar(200) DEFAULT NULL,
  `AssemblyPath` varchar(200) DEFAULT NULL,
  `ClassName` varchar(200) DEFAULT NULL,
  `Version` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of instrumentcommnunicationsettings
-- ----------------------------

-- ----------------------------
-- Table structure for `instrumentfaults`
-- ----------------------------
DROP TABLE IF EXISTS `instrumentfaults`;
CREATE TABLE `instrumentfaults` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `InstrumentID` bigint(20) NOT NULL,
  `FaultTime` datetime NOT NULL,
  `FaultType` int(11) NOT NULL,
  `FaultCode` varchar(20) DEFAULT NULL,
  `FaultMessage` varchar(20) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of instrumentfaults
-- ----------------------------

-- ----------------------------
-- Table structure for `instruments`
-- ----------------------------
DROP TABLE IF EXISTS `instruments`;
CREATE TABLE `instruments` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Name` varchar(20) NOT NULL,
  `Alias` varchar(20) DEFAULT NULL,
  `Type` int(1) DEFAULT NULL,
  `Model` varchar(20) DEFAULT NULL,
  `SerialNumber` varchar(20) DEFAULT NULL,
  `Manufacturer` varchar(20) DEFAULT NULL,
  `Brand` varchar(20) DEFAULT NULL,
  `SKU` varchar(20) DEFAULT NULL,
  `WarrantyPeriod` int(11) DEFAULT NULL,
  `ShipmentDate` datetime DEFAULT NULL,
  `PurchaseDate` datetime DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `Location` varchar(20) DEFAULT NULL,
  `InstallationDate` datetime DEFAULT NULL,
  `AcceptanceDate` datetime DEFAULT NULL,
  `Organization` varchar(20) DEFAULT NULL,
  `Department` varchar(20) DEFAULT NULL,
  `WorkGroup` varchar(20) DEFAULT NULL,
  `Index` int(2) DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of instruments
-- ----------------------------

-- ----------------------------
-- Table structure for `logs`
-- ----------------------------
DROP TABLE IF EXISTS `logs`;
CREATE TABLE `logs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TimeStamp` datetime NOT NULL,
  `Category` varchar(25) DEFAULT NULL,
  `LogLevel` varchar(25) NOT NULL,
  `CallSite` varchar(5000) DEFAULT NULL,
  `Message` longtext,
  `StackTrace` varchar(5000) DEFAULT NULL,
  `Exception` varchar(5000) DEFAULT NULL,
  `MachineName` varchar(30) DEFAULT NULL,
  `Identity` varchar(40) DEFAULT NULL,
  `ProcessName` varchar(40) DEFAULT NULL,
  `ThreadName` varchar(40) DEFAULT NULL,
  `LoggerName` varchar(40) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of logs
-- ----------------------------

-- ----------------------------
-- Table structure for `maintenanceorderattachments`
-- ----------------------------
DROP TABLE IF EXISTS `maintenanceorderattachments`;
CREATE TABLE `maintenanceorderattachments` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `OrderID` bigint(20) NOT NULL,
  `FileID` bigint(20) NOT NULL,
  `FileCreationTime` datetime NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of maintenanceorderattachments
-- ----------------------------

-- ----------------------------
-- Table structure for `maintenanceorderlineitems`
-- ----------------------------
DROP TABLE IF EXISTS `maintenanceorderlineitems`;
CREATE TABLE `maintenanceorderlineitems` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `OrderID` bigint(20) NOT NULL,
  `FulfillmentDate` datetime NOT NULL,
  `Notes` varchar(200) DEFAULT NULL,
  `RepairResult` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of maintenanceorderlineitems
-- ----------------------------
INSERT INTO `maintenanceorderlineitems` VALUES ('00000000000000000005', '4', '2020-03-20 15:35:02', '123', '维修失败');
INSERT INTO `maintenanceorderlineitems` VALUES ('00000000000000000006', '4', '2020-03-20 15:35:07', '123', '维修成功');

-- ----------------------------
-- Table structure for `maintenanceorders`
-- ----------------------------
DROP TABLE IF EXISTS `maintenanceorders`;
CREATE TABLE `maintenanceorders` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `InstrumentID` bigint(20) NOT NULL,
  `OrderRefID` varchar(20) NOT NULL,
  `OrderDate` datetime NOT NULL,
  `FaultTime` datetime NOT NULL,
  `WarrantyStatus` int(1) NOT NULL,
  `Cost` varchar(20) DEFAULT NULL,
  `FaultCode` varchar(20) DEFAULT NULL,
  `FaultDescription` varchar(20) DEFAULT NULL,
  `RepairResult` varchar(20) DEFAULT NULL,
  `RepairDescription` varchar(200) DEFAULT NULL,
  `UserComment` varchar(20) DEFAULT NULL,
  `CommentContent` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of maintenanceorders
-- ----------------------------

-- ----------------------------
-- Table structure for `objectoperationauthitems`
-- ----------------------------
DROP TABLE IF EXISTS `objectoperationauthitems`;
CREATE TABLE `objectoperationauthitems` (
  `Id` varchar(250) NOT NULL,
  `ObjectId` varchar(250) NOT NULL,
  `ActorId` varchar(250) NOT NULL,
  `OperationId` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FK_OperationObjectOperationAuthItem` (`OperationId`),
  CONSTRAINT `FK_OperationObjectOperationAuthItem` FOREIGN KEY (`OperationId`) REFERENCES `operations` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of objectoperationauthitems
-- ----------------------------

-- ----------------------------
-- Table structure for `operations`
-- ----------------------------
DROP TABLE IF EXISTS `operations`;
CREATE TABLE `operations` (
  `Id` varchar(250) NOT NULL,
  `Name` varchar(250) NOT NULL,
  `DataType` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of operations
-- ----------------------------
INSERT INTO `operations` VALUES ('OP-DEVICE-AUTH-DISPATCH', '指派设备权限', 'Device');
INSERT INTO `operations` VALUES ('OP-DEVICE-CREATE', '创建设备', 'Device');
INSERT INTO `operations` VALUES ('OP-DEVICE-DELETE', '删除设备', 'Device');
INSERT INTO `operations` VALUES ('OP-DEVICE-EXECUTE', '操作使用设备', 'Device');
INSERT INTO `operations` VALUES ('OP-DEVICE-READ', '读取设备信息', 'Device');
INSERT INTO `operations` VALUES ('OP-DEVICE-UPDATE', '更新设备信息', 'Device');
INSERT INTO `operations` VALUES ('OP-INSTRUMENT-OPR-INSTRUMENT-CONFIG', '仪器设置', 'Instrument');
INSERT INTO `operations` VALUES ('OP-INSTRUMENT-OPR-INSTRUMENT-MAINTENANCE', '仪器维护', 'Instrument');
INSERT INTO `operations` VALUES ('OP-INSTRUMENT-OPR-MEASUREMENT-RESULT', '测量结果', 'Instrument');
INSERT INTO `operations` VALUES ('OP-INSTRUMENT-OPR-MEASUREMENT-SETTING', '测量设置', 'Instrument');
INSERT INTO `operations` VALUES ('SYS-APP-PRINTING', '打印系统数据', 'System');
INSERT INTO `operations` VALUES ('SYS-APP-SETTING', '修改系统参数设定', 'System');
INSERT INTO `operations` VALUES ('SYS-LOGIN', '登录系统', 'System');
INSERT INTO `operations` VALUES ('SYS-ROLE-CREATE', '创建角色', 'System');
INSERT INTO `operations` VALUES ('SYS-ROLE-DELETE', '删除角色', 'System');
INSERT INTO `operations` VALUES ('SYS-ROLE-DISPATCH-OP', '为角色指派操作', 'System');
INSERT INTO `operations` VALUES ('SYS-ROLE-UPDATE', '更新角色', 'System');
INSERT INTO `operations` VALUES ('SYS-ROLE-VIEW', '查看角色', 'System');
INSERT INTO `operations` VALUES ('SYS-USR-CREATE', '创建用户', 'System');
INSERT INTO `operations` VALUES ('SYS-USR-DELETE', '删除用户', 'System');
INSERT INTO `operations` VALUES ('SYS-USR-DISPATCH-ROLE', '为用户指派角色', 'System');
INSERT INTO `operations` VALUES ('SYS-USR-PWD-RESET', '重置用户密码', 'System');
INSERT INTO `operations` VALUES ('SYS-USR-UPDATE', '更新用户', 'System');
INSERT INTO `operations` VALUES ('SYS-USR-VIEW', '查看用户', 'System');

-- ----------------------------
-- Table structure for `reportsettings`
-- ----------------------------
DROP TABLE IF EXISTS `reportsettings`;
CREATE TABLE `reportsettings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Title` varchar(20) DEFAULT NULL,
  `TemplateID` bigint(20) NOT NULL,
  `TemplateName` varchar(200) DEFAULT NULL,
  `AlarmMode` int(11) DEFAULT NULL,
  `Printer` varchar(200) DEFAULT NULL,
  `AutoPrintOnAlarm` int(11) NOT NULL,
  `AutoPrintOnMeasurement` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of reportsettings
-- ----------------------------

-- ----------------------------
-- Table structure for `roleactors`
-- ----------------------------
DROP TABLE IF EXISTS `roleactors`;
CREATE TABLE `roleactors` (
  `Id` varchar(250) NOT NULL,
  `RoleId` varchar(250) NOT NULL,
  `ActorId` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roleactors
-- ----------------------------
INSERT INTO `roleactors` VALUES ('RAT-SYS-BUILTIN-ADMIN', 'ROLE-SYS-ADMIN', 'USR-SYS-BUILTIN-ADMIN');

-- ----------------------------
-- Table structure for `roledatascopes`
-- ----------------------------
DROP TABLE IF EXISTS `roledatascopes`;
CREATE TABLE `roledatascopes` (
  `Id` varchar(250) NOT NULL,
  `RoleId` varchar(250) NOT NULL,
  `DataScopeId` varchar(250) NOT NULL,
  `ScopeValue` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FK_DataScopeRoleDataScope` (`DataScopeId`),
  CONSTRAINT `FK_DataScopeRoleDataScope` FOREIGN KEY (`DataScopeId`) REFERENCES `datascopes` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roledatascopes
-- ----------------------------

-- ----------------------------
-- Table structure for `roleoperations`
-- ----------------------------
DROP TABLE IF EXISTS `roleoperations`;
CREATE TABLE `roleoperations` (
  `Id` varchar(250) NOT NULL,
  `RoleId` varchar(250) NOT NULL,
  `OperationId` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_FK_OperationRoleOperation` (`OperationId`),
  CONSTRAINT `FK_OperationRoleOperation` FOREIGN KEY (`OperationId`) REFERENCES `operations` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roleoperations
-- ----------------------------
INSERT INTO `roleoperations` VALUES ('239876f2-52de-4635-be5a-d87bfcfc691d', 'ROLE-INSTRUMENT-OPR-INSTRUMENT-MAINTENANCE', 'OP-INSTRUMENT-OPR-INSTRUMENT-MAINTENANCE');
INSERT INTO `roleoperations` VALUES ('54b80d6c-4dc7-427e-bbd4-b98966cf45bf', 'ROLE-INSTRUMENT-OPR-MEASUREMENT-SETTING', 'OP-INSTRUMENT-OPR-MEASUREMENT-SETTING');
INSERT INTO `roleoperations` VALUES ('90fd8573-9d78-45d1-b263-f84acf11c99b', 'ROLE-INSTRUMENT-OPR-INSTRUMENT-CONFIG', 'OP-INSTRUMENT-OPR-INSTRUMENT-CONFIG');
INSERT INTO `roleoperations` VALUES ('96c8f89a-d7b3-4f2f-b04b-5c528402e21c', 'ROLE-INSTRUMENT-OPR-MEASUREMENT-RESULT', 'OP-INSTRUMENT-OPR-MEASUREMENT-RESULT');
INSERT INTO `roleoperations` VALUES ('ROP-OP-INSTRUMENT-OPR-INSTRUMENT-MAINTENANCE', 'ROLE-SYS-ADMIN', 'OP-INSTRUMENT-OPR-INSTRUMENT-MAINTENANCE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-APP-PRINTING', 'ROLE-SYS-ADMIN', 'SYS-APP-PRINTING');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-APP-SETTING', 'ROLE-SYS-ADMIN', 'SYS-APP-SETTING');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-LOGIN', 'ROLE-SYS-ADMIN', 'SYS-LOGIN');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-ROLE-CREATE', 'ROLE-SYS-ADMIN', 'SYS-ROLE-CREATE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-ROLE-DELETE', 'ROLE-SYS-ADMIN', 'SYS-ROLE-DELETE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-ROLE-DISPATCH-OP', 'ROLE-SYS-ADMIN', 'SYS-ROLE-DISPATCH-OP');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-ROLE-UPDATE', 'ROLE-SYS-ADMIN', 'SYS-ROLE-UPDATE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-ROLE-VIEW', 'ROLE-SYS-ADMIN', 'SYS-ROLE-VIEW');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-USR-CREATE', 'ROLE-SYS-ADMIN', 'SYS-USR-CREATE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-USR-DELETE', 'ROLE-SYS-ADMIN', 'SYS-USR-DELETE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-USR-DISPATCH-ROLE', 'ROLE-SYS-ADMIN', 'SYS-USR-DISPATCH-ROLE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-USR-PWD-RESET', 'ROLE-SYS-ADMIN', 'SYS-USR-PWD-RESET');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-USR-UPDATE', 'ROLE-SYS-ADMIN', 'SYS-USR-UPDATE');
INSERT INTO `roleoperations` VALUES ('ROP-SYS-USR-VIEW', 'ROLE-SYS-ADMIN', 'SYS-USR-VIEW');

-- ----------------------------
-- Table structure for `roles`
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `Id` varchar(250) NOT NULL,
  `Name` varchar(250) NOT NULL,
  `RoleType` varchar(250) NOT NULL,
  `Description` varchar(250) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roles
-- ----------------------------
INSERT INTO `roles` VALUES ('ROLE-APP-ADMIN', '应用程序管理员', '1', '应用程序管理员');
INSERT INTO `roles` VALUES ('ROLE-DATA-ADMIN', '数据管理员', '1', '数据管理员');
INSERT INTO `roles` VALUES ('ROLE-DEVICE-MTR', '设备保养人', '1', '设备保养人');
INSERT INTO `roles` VALUES ('ROLE-DEVICE-OPR', '设备使用人', '1', '设备使用人');
INSERT INTO `roles` VALUES ('ROLE-DEVICE-OWNER', '设备所有人', '1', '设备所有人');
INSERT INTO `roles` VALUES ('ROLE-DEVICE-RPR', '设备维修人', '1', '设备维修人');
INSERT INTO `roles` VALUES ('ROLE-INSTRUMENT-OPR-INSTRUMENT-CONFIG', '仪器设置操作角色', '1', '仪器设置操作角色');
INSERT INTO `roles` VALUES ('ROLE-INSTRUMENT-OPR-INSTRUMENT-MAINTENANCE', '仪器维护操作角色', '1', '仪器维护操作角色');
INSERT INTO `roles` VALUES ('ROLE-INSTRUMENT-OPR-MEASUREMENT-RESULT', '测量结果操作角色', '1', '测量结果操作角色');
INSERT INTO `roles` VALUES ('ROLE-INSTRUMENT-OPR-MEASUREMENT-SETTING', '测量设置操作角色', '1', '测量设置操作角色');
INSERT INTO `roles` VALUES ('ROLE-SEC-ADMIN', '安全管理员', '1', '安全管理员');
INSERT INTO `roles` VALUES ('ROLE-SYS-ADMIN', '系统管理员', '1', '系统管理员');

-- ----------------------------
-- Table structure for `users`
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `Id` varchar(128) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  `UserType` int(1) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `PasswordSalt` varchar(128) NOT NULL,
  `PasswordRev` int(11) NOT NULL,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(18) NOT NULL,
  `Description` longtext,
  `CreateDate` datetime DEFAULT NULL,
  `ConfirmationToken` varchar(128) DEFAULT NULL,
  `IsConfirmed` tinyint(1) NOT NULL,
  `LastPasswordFailureDate` datetime DEFAULT NULL,
  `PasswordFailuresSinceLastSuccess` int(11) DEFAULT NULL,
  `PasswordChangedDate` datetime DEFAULT NULL,
  `PasswordVerificationToken` varchar(128) DEFAULT NULL,
  `PasswordVerificationTokenExpirationDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES ('USR-SYS-BUILTIN-ADMIN', 'admin', '0', null, '0', '5ed469efeaca14ca9310581dcf853dda0ddb690b', 's7im', '1', 'BFEBFBFF000506E3', null, '0', '0', '2020-03-20 09:59:04', '0', '0', null, '2020-03-20 17:59:04', 'ac558ef13a6dcca0de0b38c24c40dd98dde4fbb7', '0', '2020-03-20 17:59:04', '0', '2020-03-20 18:00:54', 'd694b3a99f28f72f5de64228af8e283a', '2020-04-19 19:10:19');
INSERT INTO `users` VALUES ('USR-SYS-BUILTIN-SUPPER', 'root', '0', null, '0', '5c4ae1aa8339e49de74c8b4176c69acbad7d8e55', 'p3FM', '0', null, null, '0', '0', '2020-03-20 09:59:04', '0', '0', null, '2020-03-20 17:59:04', null, '0', '2020-03-20 17:59:04', '0', '2020-03-20 17:59:04', null, null);


-- ----------------------------
-- Table structure for `imagerawfiles`
-- ----------------------------
DROP TABLE IF EXISTS `rawfiles`;
CREATE TABLE `rawfiles` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `AlarmID` varchar(128) NOT NULL,
  `FilePath` varchar(1200) NOT NULL,
  `FileName` varchar(250) NOT NULL,
  `FileType` varchar(20) NOT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `FileCreationTime` datetime NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rawfiles
-- ----------------------------

DROP TABLE IF EXISTS `measurementdataitems`;
CREATE TABLE `measurementdataitems` (
  `ID` varchar(128) NOT NULL,
  `MeasurementTime` datetime DEFAULT NULL,
  `InstrumentID` bigint(20) NOT NULL,
  `DetectorType` varchar(20) DEFAULT NULL,
  `DetectorNo` varchar(20) DEFAULT NULL,
  `CameraNo` varchar(20) DEFAULT NULL,
  `VehicleType` varchar(20) DEFAULT NULL,
  `VehicleNo` varchar(20) DEFAULT NULL,
  `VehicleSpeed` float(255,8) DEFAULT NULL,
  `OperatorID` varchar(20) DEFAULT NULL,
  `BackgroundValue` float(255,8) DEFAULT NULL,
  `AlarmThresholdOne` float(255,8) DEFAULT NULL,
  `AlarmThresholdTwo` float(255,8) DEFAULT NULL,
  `MeasuredValue` float(255,8) DEFAULT NULL,
  `NuclideCategory` int DEFAULT NULL,
  `Nuclide1` varchar(20) DEFAULT NULL,
  `Credibility1`  float(255,8) DEFAULT NULL,
  `Nuclide2` varchar(20) DEFAULT NULL,
  `Credibility2`  float(255,8) DEFAULT NULL,
  `Nuclide3` varchar(20) DEFAULT NULL,
  `Credibility3`  float(255,8) DEFAULT NULL,
  `Result` varchar(20) DEFAULT NULL,
  `State` varchar(20) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `WindSpeed` float(255,8) DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `alarmdataitems`;
CREATE TABLE `alarmdataitems` (
  `ID` varchar(128) NOT NULL,
  `MeasurementTime` datetime DEFAULT NULL,
  `InstrumentID` bigint(20) NOT NULL,
  `DetectorType` varchar(20) DEFAULT NULL,
  `DetectorNo` varchar(20) DEFAULT NULL,
  `CameraNo` varchar(20) DEFAULT NULL,
  `VehicleType` varchar(20) DEFAULT NULL,
  `VehicleNo` varchar(20) DEFAULT NULL,
  `VehicleSpeed` float(255,8) DEFAULT NULL,
  `OperatorID` varchar(20) DEFAULT NULL,
  `BackgroundValue` float(255,8) DEFAULT NULL,
  `AlarmThresholdOne` float(255,8) DEFAULT NULL,
  `AlarmThresholdTwo` float(255,8) DEFAULT NULL,
  `MeasuredValue` float(255,8) DEFAULT NULL,
  `NuclideCategory` int DEFAULT NULL,
  `Nuclide1` varchar(20) DEFAULT NULL,
  `Credibility1`  float(255,8) DEFAULT NULL,
  `Nuclide2` varchar(20) DEFAULT NULL,
  `Credibility2`  float(255,8) DEFAULT NULL,
  `Nuclide3` varchar(20) DEFAULT NULL,
  `Credibility3`  float(255,8) DEFAULT NULL,
  `Result` varchar(20) DEFAULT NULL,
  `State` varchar(20) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `WindSpeed` float(255,8) DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
  `IsByMistake` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for nuclide
-- ----------------------------
DROP TABLE IF EXISTS `nuclide`;
CREATE TABLE `nuclide`  (
  `ID` int(10) NOT NULL,
  `No` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Name` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Symbol` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Type` int(10) NULL DEFAULT NULL,
  `Category` int(10) NULL DEFAULT NULL,
  `Energy1` float(255, 8) NULL DEFAULT NULL,
  `BranchingRatio1` float(255, 8) NULL DEFAULT NULL,
  `Channel1` float(255, 8) NULL DEFAULT NULL,
  `Energy2` float(255, 8) NULL DEFAULT NULL,
  `BranchingRatio2` float(255, 8) NULL DEFAULT NULL,
  `Channel2` float(255, 8) NULL DEFAULT NULL,
  `Energy3` float(255, 8) NULL DEFAULT NULL,
  `BranchingRatio3` float(255, 8) NULL DEFAULT NULL,
  `Channel3` float(255, 8) NULL DEFAULT NULL,
  `HalfLife` float(255, 8) NULL DEFAULT NULL,
  `HalfLifeUnit` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `HalfAddress` varchar(20) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci;

-- ----------------------------
-- Records of nuclide
-- ----------------------------
INSERT INTO `nuclide` VALUES (1, '1', 'Co-57', 'Co', 0, 0, 122.06140137, 85.59999847, 12.00000000, 136.47430420, 10.68000031, 12.00000000, 692.40997314, 0.14900000, 12.00000000, 271.79000854, 'd','');
INSERT INTO `nuclide` VALUES (2, '2', 'Co-60', 'Co', 0, 0, 11173.23730469, 99.97000122, 12.00000000, 1332.50097656, 99.98000336, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 5.57140017, 'y','');
INSERT INTO `nuclide` VALUES (3, '3', 'Ba-133', 'Ba', 0, 0, 80.99700165, 34.06000137, 12.00000000, 276.39999390, 7.16400003, 12.00000000, 302.85101318, 18.32999992, 12.00000000, 10.51000023, 'y','');
INSERT INTO `nuclide` VALUES (4, '4', 'CS-137', 'Cs', 0, 0, 661.65698242, 85.09999847, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 30.06999969, 'y','');
INSERT INTO `nuclide` VALUES (5, '5', 'Eu-152', 'Eu', 0, 0, 867.37298584, 5.90600014, 12.00000000, 964.07897949, 20.31999969, 12.00000000, 1085.86901855, 14.19999981, 12.00000000, 13.53699970, 'y','');
INSERT INTO `nuclide` VALUES (6, '6', 'Ra-226', 'Ra', 0, 0, 295.22399902, 19.29999924, 12.00000000, 351.93200684, 37.59999847, 12.00000000, 609.31201172, 46.09999847, 12.00000000, 1600.00000000, 'y','');
INSERT INTO `nuclide` VALUES (7, '7', 'K-40', 'K', 0, 0, 1460.82995605, 10.67000008, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 1.27699995, 'y','');

DROP TABLE IF EXISTS `energycalibration`;
CREATE TABLE `energycalibration` (
  `ID` varchar(128) NOT NULL,
  `Time` datetime DEFAULT NULL,
  `OperatorID` varchar(128) DEFAULT NULL,
  `InstrumentNo` varchar(20) DEFAULT NULL,
  `NuclideBoardNo` varchar(20) DEFAULT NULL,
  `Channel1` float(255,8) DEFAULT NULL,
  `Energy1` float(255,8) DEFAULT NULL,
  `Channel2` float(255,8) DEFAULT NULL,
  `Energy2` float(255,8) DEFAULT NULL,
  `Channel3` float(255,8) DEFAULT NULL,
  `Energy3` float(255,8) DEFAULT NULL,
  `Channel4` float(255,8) DEFAULT NULL,
  `Energy4` float(255,8) DEFAULT NULL,
  `Channel5` float(255,8) DEFAULT NULL,
  `Energy5` float(255,8) DEFAULT NULL,
  `CoefficientA` float(255,8) DEFAULT NULL,
  `CoefficientB` float(255,8) DEFAULT NULL,
  `CoefficientC` float(255,8) DEFAULT NULL,
  `EnergyResolution` float(255,8) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `circuitconfigs`;
CREATE TABLE `circuitconfigs` (
  `ID` varchar(128) NOT NULL,
  `CircuitName` varchar(128) DEFAULT NULL,
  `CircuitNo` varchar(128) DEFAULT NULL,
  `Address` varchar(128) DEFAULT NULL,
  `Port` int DEFAULT NULL,
  `Status` int DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `backgrounddataitems`;
CREATE TABLE `backgrounddataitems` (
  `ID` varchar(128) NOT NULL,
  `MeasurementTime` datetime DEFAULT NULL,
  `InstrumentID` bigint(20) NOT NULL,
  `DetectorType` varchar(20) DEFAULT NULL,
  `DetectorNo` varchar(20) DEFAULT NULL,
  `BackgroundValue` float(255,8) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `backgrounddailyaverages`;
CREATE TABLE `backgrounddailyaverages` (
  `ID` varchar(128) NOT NULL,
  `StatTime` datetime DEFAULT NULL,
  `InstrumentID` bigint(20) NOT NULL,
  `DetectorType` varchar(20) DEFAULT NULL,
  `DetectorNo` varchar(20) DEFAULT NULL,
  `BackgroundValue` float(255,8) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `backgroundmonthlyaverages`;
CREATE TABLE `backgroundmonthlyaverages` (
  `ID` varchar(128) NOT NULL,
  `StatTime` datetime DEFAULT NULL,
  `InstrumentID` bigint(20) NOT NULL,
  `DetectorType` varchar(20) DEFAULT NULL,
  `DetectorNo` varchar(20) DEFAULT NULL,
  `BackgroundValue` float(255,8) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

