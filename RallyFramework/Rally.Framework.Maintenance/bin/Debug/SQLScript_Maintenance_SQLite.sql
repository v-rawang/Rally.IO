CREATE TABLE "maintenance_orders" (
	"ID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"InstrumentID"	bigint(20) NOT NULL,
	"OrderRefID"	varchar(20) NOT NULL,
	"OrderDate"	datetime NOT NULL,
	"FaultTime"	datetime NOT NULL,
	"WarrantyStatus"	int(1) NOT NULL,
	"Cost"	varchar(20) DEFAULT NULL,
	"FaultCode"	varchar(20) DEFAULT NULL,
	"FaultDescription"	varchar(20) DEFAULT NULL,
	"RepairResult"	varchar(20) DEFAULT NULL,
	"RepairDescription"	varchar(200) DEFAULT NULL,
	"UserComment"	varchar(20) DEFAULT NULL,
	"CommentContent"	varchar(200) DEFAULT NULL
);

CREATE TABLE "tb_mon_MaintenanceOrderLineitems" (
	"ID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"OrderID"	bigint(20) NOT NULL,
	"FulfillmentDate"	datetime NOT NULL,
	"Notes"	varchar(200) DEFAULT NULL,
	`RepairResult` varchar(200) DEFAULT NULL
);

CREATE TABLE "maintenance_order_attachments" (
	"ID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"OrderID"	bigint(20) NOT NULL,
	"FileID"	bigint(20) NOT NULL,
	"FileCreationTime"	datetime NOT NULL
);

CREATE TABLE "instrument_faults" (
	"ID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"InstrumentID"	bigint(20) NOT NULL,
	"FaultTime"	datetime NOT NULL,
	"FaultType"	int NOT NULL,
	"FaultCode"	varchar(20) DEFAULT NULL,
	"FaultMessage"	varchar(20) NOT NULL
);