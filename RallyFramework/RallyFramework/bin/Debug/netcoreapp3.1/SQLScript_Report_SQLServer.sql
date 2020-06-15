use newford;

DROP TABLE IF EXISTS report_settings;

CREATE TABLE report_settings (
  ID bigint NOT NULL IDENTITY(1,1),
  Title nvarchar(20) DEFAULT NULL,
  TemplateID bigint NOT NULL,
  TemplateName nvarchar(200),
  AlarmMode int DEFAULT NULL,
  Printer nvarchar(200) DEFAULT NULL,
  AutoPrintOnAlarm int NOT NULL,
  AutoPrintOnMeasurement int NOT NULL,
   PRIMARY KEY (Id)
);