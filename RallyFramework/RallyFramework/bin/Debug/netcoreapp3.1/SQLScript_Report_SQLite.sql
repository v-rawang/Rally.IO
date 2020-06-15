CREATE TABLE "report_settings" (
	"ID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"Title"	varchar(20) DEFAULT NULL,
	"TemplateID"	bigint(20) NOT NULL,
	`TemplateName` varchar(200),
	"AlarmMode"	int DEFAULT NULL,
	"Printer"	varchar(200) DEFAULT NULL,
	"AutoPrintOnAlarm"	int NOT NULL,
	"AutoPrintOnMeasurement"	int NOT NULL
);