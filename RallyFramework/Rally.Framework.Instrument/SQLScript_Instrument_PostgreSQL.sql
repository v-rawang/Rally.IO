-- ----------------------------
-- Table structure for instrumentcamerasettings
-- ----------------------------
DROP TABLE IF EXISTS instrumentcamerasettings;
CREATE TABLE instrumentcamerasettings (
  ID bigserial NOT NULL,
  InstrumentID bigint NOT NULL,
  CameraType smallint DEFAULT NULL,
  ConnectionType smallint DEFAULT NULL,
  Model varchar(200) DEFAULT NULL,
  SerialNumber varchar(200) DEFAULT NULL,
  Manufacturer varchar(200) DEFAULT NULL,
  Brand varchar(200) DEFAULT NULL,
  SKU varchar(200) DEFAULT NULL,
  IpAddress varchar(200) DEFAULT NULL,
  PortNumber int DEFAULT NULL,
  LoginName varchar(200) DEFAULT NULL,
  Password varchar(200) DEFAULT NULL,
  AssemblyName varchar(200) DEFAULT NULL,
  AssemblyPath varchar(200) DEFAULT NULL,
  ClassName varchar(200) DEFAULT NULL,
  Version varchar(200) DEFAULT NULL,
  Remarks varchar(200) DEFAULT NULL,
  Index int DEFAULT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of instrumentcamerasettings
-- ----------------------------
INSERT INTO instrumentcamerasettings VALUES ('00000000000000000009', '8', '1', '2', '请选择', null, null, null, null, '', '0', '', '', null, null, null, null, null, '1');
INSERT INTO instrumentcamerasettings VALUES ('00000000000000000010', '8', '1', '2', '', null, null, null, null, '', '0', '', '', null, null, null, null, null, '2');

-- ----------------------------
-- Table structure for instrumentcommnunicationsettings
-- ----------------------------
DROP TABLE IF EXISTS instrumentcommnunicationsettings;
CREATE TABLE instrumentcommnunicationsettings (
  ID bigserial NOT NULL,
  InstrumentID bigint NOT NULL,
  Type smallint DEFAULT NULL,
  IpAddress varchar(200) DEFAULT NULL,
  PortNumber int DEFAULT NULL,
  SerialPortName varchar(200) DEFAULT NULL,
  SerialPortBaudRate int DEFAULT NULL,
  BluetoothDeviceName varchar(200) DEFAULT NULL,
  BluetoothAddress varchar(200) DEFAULT NULL,
  BluetoothKey varchar(200) DEFAULT NULL,
  Remarks varchar(200) DEFAULT NULL,
  Protocol varchar(200) DEFAULT NULL,
  AssemblyName varchar(200) DEFAULT NULL,
  AssemblyPath varchar(200) DEFAULT NULL,
  ClassName varchar(200) DEFAULT NULL,
  Version varchar(200) DEFAULT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of instrumentcommnunicationsettings
-- ----------------------------

-- ----------------------------
-- Table structure for instrumentfaults
-- ----------------------------
DROP TABLE IF EXISTS instrumentfaults;
CREATE TABLE instrumentfaults (
  ID bigserial NOT NULL,
  InstrumentID bigint NOT NULL,
  FaultTime timestamp NOT NULL,
  FaultType int NOT NULL,
  FaultCode varchar(20) DEFAULT NULL,
  FaultMessage varchar(20) NOT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of instrumentfaults
-- ----------------------------

-- ----------------------------
-- Table structure for instruments
-- ----------------------------
DROP TABLE IF EXISTS instruments;
CREATE TABLE instruments (
  ID bigserial NOT NULL,
  Name varchar(20) NOT NULL,
  Alias varchar(20) DEFAULT NULL,
  Type smallint DEFAULT NULL,
  Model varchar(20) DEFAULT NULL,
  SerialNumber varchar(20) DEFAULT NULL,
  Manufacturer varchar(20) DEFAULT NULL,
  Brand varchar(20) DEFAULT NULL,
  SKU varchar(20) DEFAULT NULL,
  WarrantyPeriod int DEFAULT NULL,
  ShipmentDate timestamp DEFAULT NULL,
  PurchaseDate timestamp DEFAULT NULL,
  Latitude varchar(20) DEFAULT NULL,
  Longitude varchar(20) DEFAULT NULL,
  Location varchar(20) DEFAULT NULL,
  InstallationDate timestamp DEFAULT NULL,
  AcceptanceDate timestamp DEFAULT NULL,
  Organization varchar(20) DEFAULT NULL,
  Department varchar(20) DEFAULT NULL,
  WorkGroup varchar(20) DEFAULT NULL,
  Index int DEFAULT NULL,
  Remarks varchar(200) DEFAULT NULL,
  PRIMARY KEY (ID)
);