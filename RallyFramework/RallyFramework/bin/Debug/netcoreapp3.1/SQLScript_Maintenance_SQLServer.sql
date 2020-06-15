use newford;

DROP TABLE IF EXISTS maintenance_orders;
DROP TABLE IF EXISTS tb_mon_MaintenanceOrderLineitems;
DROP TABLE IF EXISTS maintenance_order_attachments;
DROP TABLE IF EXISTS instrument_faults;

CREATE TABLE maintenance_orders (
  ID bigint NOT NULL IDENTITY(1,1),
  InstrumentID bigint NOT NULL,
  OrderRefID Nvarchar(20) NOT NULL,
  OrderDate datetime NOT NULL,
  FaultTime datetime NOT NULL,
  WarrantyStatus int NOT NULL,
  Cost nvarchar(20) DEFAULT NULL,
  FaultCode nvarchar(20) DEFAULT NULL,
  FaultDescription nvarchar(20) DEFAULT NULL,
  RepairResult nvarchar(20) DEFAULT NULL,
  RepairDescription nvarchar(200) DEFAULT NULL,
  UserComment nvarchar(20) DEFAULT NULL,
  CommentContent nvarchar(200) DEFAULT NULL,
   PRIMARY KEY (Id)
);

CREATE TABLE tb_mon_MaintenanceOrderLineitems (
  ID bigint NOT NULL IDENTITY(1,1),
  OrderID bigint NOT NULL,
  FulfillmentDate datetime NOT NULL,
  Notes nvarchar(200) DEFAULT NULL,
  RepairResult nvarchar(200) DEFAULT NULL,
   PRIMARY KEY (Id)
);

CREATE TABLE maintenance_order_attachments (
  ID bigint NOT NULL IDENTITY(1,1),
  OrderID bigint NOT NULL,
  FileID bigint NOT NULL,
  FileCreationTime datetime NOT NULL,
   PRIMARY KEY (ID)
);

CREATE TABLE instrument_faults (
  ID bigint NOT NULL IDENTITY(1,1),
  InstrumentID bigint NOT NULL,
  FaultTime datetime NOT NULL,
  FaultType int NOT NULL,
  FaultCode nvarchar(20) DEFAULT NULL,
  FaultMessage nvarchar(20) NOT NULL,
  PRIMARY KEY (Id)
);