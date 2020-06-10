use newford;

DROP TABLE IF EXISTS application_settings;

CREATE TABLE application_settings (
  ID bigint NOT NULL IDENTITY(1,1),
  PlatformIpAddress nvarchar(20) DEFAULT NULL,
  PlatformPortNumber int DEFAULT NULL,
  AlarmMode int DEFAULT NULL,
  AlarmSound nvarchar(20) DEFAULT NULL,
  NotificationMode int DEFAULT NULL,
  NotificationSound nvarchar(20) DEFAULT NULL,
  Language int DEFAULT NULL,
  PRIMARY KEY (Id)
);