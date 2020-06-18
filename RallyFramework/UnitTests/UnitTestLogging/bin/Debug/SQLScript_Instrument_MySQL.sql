use db_edge_nuclide;

DROP TABLE IF EXISTS `instruments`;
DROP TABLE IF EXISTS `instrument_commnunication_settings`;
DROP TABLE IF EXISTS `instrument_camera_settings`;

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
  `Remarks` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `instrument_commnunication_settings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `InstrumentID` bigint(20) NOT NULL,
  `Type` int(1) DEFAULT NULL,
  `IpAddress` varchar(20) DEFAULT NULL,
  `PortNumber` int DEFAULT NULL,
  `SerialPortName` varchar(20) DEFAULT NULL,
  `SerialPortBaudRate` int DEFAULT NULL,
  `BluetoothDeviceName` varchar(20) DEFAULT NULL,
  `BluetoothAddress` varchar(20) DEFAULT NULL,
  `BluetoothKey` varchar(20) DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `instrument_camera_settings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `InstrumentID` bigint(20) NOT NULL,
  `CameraType` int(1) DEFAULT NULL,
  `ConnectionType` int(1) DEFAULT NULL,
  `Model` varchar(20) DEFAULT NULL,
  `SerialNumber` varchar(20) DEFAULT NULL,
  `Manufacturer` varchar(20) DEFAULT NULL,
  `Brand` varchar(20) DEFAULT NULL,
  `SKU` varchar(20) DEFAULT NULL,
  `IpAddress` varchar(20) DEFAULT NULL,
  `PortNumber` int DEFAULT NULL,
  `LoginName` varchar(20) DEFAULT NULL,
  `Password` varchar(20) DEFAULT NULL,
  `AssemblyName` varchar(20) DEFAULT NULL,
  `AssemblyPath` varchar(20) DEFAULT NULL,
  `ClassName` varchar(20) DEFAULT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `Remarks` varchar(200) DEFAULT NULL,
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;