CREATE TABLE "application_settings" (
	"ID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"PlatformIpAddress"	varchar(20) DEFAULT NULL,
	"PlatformPortNumber"	int DEFAULT NULL,
	"AlarmMode"	int DEFAULT NULL,
	"AlarmSound"	varchar(20) DEFAULT NULL,
	"NotificationMode"	int DEFAULT NULL,
	"NotificationSound"	varchar(20) DEFAULT NULL,
	"Language"	int DEFAULT NULL
);