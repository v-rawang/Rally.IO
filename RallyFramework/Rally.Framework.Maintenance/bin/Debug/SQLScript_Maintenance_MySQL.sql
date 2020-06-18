use db_edge_nuclide;

DROP TABLE IF EXISTS `maintenance_orders`;
DROP TABLE IF EXISTS `tb_mon_MaintenanceOrderLineitems`;
DROP TABLE IF EXISTS `maintenance_order_attachments`;
DROP TABLE IF EXISTS `instrument_faults`;

CREATE TABLE `instrument_faults` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `InstrumentID` bigint(20) NOT NULL,
  `FaultTime` datetime NOT NULL,
  `FaultType` int NOT NULL,
  `FaultCode` varchar(20) DEFAULT NULL,
  `FaultMessage` varchar(20) NOT NULL,
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `maintenance_orders` (
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
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `tb_mon_MaintenanceOrderLineitems` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `OrderID` bigint(20) NOT NULL,
  `FulfillmentDate` datetime NOT NULL,
  `Notes` varchar(200) DEFAULT NULL,
  `RepairResult` varchar(200) DEFAULT NULL
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `maintenance_order_attachments` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `OrderID` bigint(20) NOT NULL,
  `FileID` bigint(20) NOT NULL,
  `FileCreationTime` datetime NOT NULL,
   PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;