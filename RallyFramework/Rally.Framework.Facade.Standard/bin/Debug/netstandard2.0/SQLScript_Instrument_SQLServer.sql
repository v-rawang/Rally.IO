use newford;

DROP TABLE IF EXISTS instruments;
DROP TABLE IF EXISTS instrument_commnunication_settings;
DROP TABLE IF EXISTS instrument_camera_settings;

CREATE TABLE instruments (
  ID bigint NOT NULL IDENTITY(1,1),
  Name nvarchar(20) NOT NULL,
  Alias nvarchar(20) DEFAULT NULL,
  Type int DEFAULT NULL,
  Model nvarchar(20) DEFAULT NULL,
  SerialNumber nvarchar(20) DEFAULT NULL,
  Manufacturer nvarchar(20) DEFAULT NULL,
  Brand nvarchar(20) DEFAULT NULL,
  SKU nvarchar(20) DEFAULT NULL,
  WarrantyPeriod int DEFAULT NULL,
  ShipmentDate datetime DEFAULT NULL,
  PurchaseDate datetime DEFAULT NULL,
  Latitude nvarchar(20) DEFAULT NULL,
  Longitude nvarchar(20) DEFAULT NULL,
  Location nvarchar(20) DEFAULT NULL,
  InstallationDate datetime DEFAULT NULL,
  AcceptanceDate datetime DEFAULT NULL,
  Organization varchar(20) DEFAULT NULL,
  Department varchar(20) DEFAULT NULL,
  WorkGroup varchar(20) DEFAULT NULL,
  Remarks varchar(200) DEFAULT NULL,
   PRIMARY KEY (Id)
);

CREATE TABLE instrument_commnunication_settings (
  ID bigint NOT NULL IDENTITY(1,1),
  InstrumentID bigint NOT NULL,
  Type int DEFAULT NULL,
  IpAddress nvarchar(20) DEFAULT NULL,
  PortNumber int DEFAULT NULL,
  SerialPortName nvarchar(20) DEFAULT NULL,
  SerialPortBaudRate int DEFAULT NULL,
  BluetoothDeviceName nvarchar(20) DEFAULT NULL,
  BluetoothAddress nvarchar(20) DEFAULT NULL,
  BluetoothKey nvarchar(20) DEFAULT NULL,
  Remarks nvarchar(200) DEFAULT NULL,
   PRIMARY KEY (Id)
);

CREATE TABLE instrument_camera_settings (
  ID bigint NOT NULL IDENTITY(1,1),
  InstrumentID bigint NOT NULL,
  CameraType int DEFAULT NULL,
  ConnectionType int DEFAULT NULL,
  Model nvarchar(20) DEFAULT NULL,
  SerialNumber nvarchar(20) DEFAULT NULL,
  Manufacturer nvarchar(20) DEFAULT NULL,
  Brand nvarchar(20) DEFAULT NULL,
  SKU nvarchar(20) DEFAULT NULL,
  IpAddress nvarchar(20) DEFAULT NULL,
  PortNumber int DEFAULT NULL,
  LoginName nvarchar(20) DEFAULT NULL,
  Password nvarchar(20) DEFAULT NULL,
  AssemblyName nvarchar(20) DEFAULT NULL,
  AssemblyPath nvarchar(20) DEFAULT NULL,
  ClassName nvarchar(20) DEFAULT NULL,
  Version nvarchar(20) DEFAULT NULL,
  Remarks nvarchar(200) DEFAULT NULL,
   PRIMARY KEY (Id)
);