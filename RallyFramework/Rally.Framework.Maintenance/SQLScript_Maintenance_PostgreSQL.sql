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
-- Table structure for maintenanceorderattachments
-- ----------------------------
DROP TABLE IF EXISTS maintenanceorderattachments;
CREATE TABLE maintenanceorderattachments (
  ID bigserial NOT NULL,
  OrderID bigint NOT NULL,
  FileID bigint NOT NULL,
  FileCreationTime timestamp NOT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of maintenanceorderattachments
-- ----------------------------

-- ----------------------------
-- Table structure for maintenanceorderlineitems
-- ----------------------------
DROP TABLE IF EXISTS maintenanceorderlineitems;
CREATE TABLE maintenanceorderlineitems (
  ID bigserial NOT NULL,
  OrderID bigint NOT NULL,
  FulfillmentDate timestamp NOT NULL,
  Notes varchar(200) DEFAULT NULL,
  RepairResult varchar(200) DEFAULT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of maintenanceorderlineitems
-- ----------------------------
INSERT INTO maintenanceorderlineitems VALUES ('00000000000000000005', '4', '2020-03-20 15:35:02', '123', '维修失败');
INSERT INTO maintenanceorderlineitems VALUES ('00000000000000000006', '4', '2020-03-20 15:35:07', '123', '维修成功');

-- ----------------------------
-- Table structure for maintenanceorders
-- ----------------------------
DROP TABLE IF EXISTS maintenanceorders;
CREATE TABLE maintenanceorders (
  ID bigserial NOT NULL,
  InstrumentID bigint NOT NULL,
  OrderRefID varchar(20) NOT NULL,
  OrderDate timestamp NOT NULL,
  FaultTime timestamp NOT NULL,
  WarrantyStatus smallint NOT NULL,
  Cost varchar(20) DEFAULT NULL,
  FaultCode varchar(20) DEFAULT NULL,
  FaultDescription varchar(20) DEFAULT NULL,
  RepairResult varchar(20) DEFAULT NULL,
  RepairDescription varchar(200) DEFAULT NULL,
  UserComment varchar(20) DEFAULT NULL,
  CommentContent varchar(200) DEFAULT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of maintenanceorders