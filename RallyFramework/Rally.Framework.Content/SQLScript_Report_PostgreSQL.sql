DROP TABLE IF EXISTS report_settings;

CREATE TABLE report_settings (
  ID bigserial NOT NULL,
  Title varchar(20) DEFAULT NULL,
  TemplateID bigint NOT NULL,
  TemplateName varchar(200),
  AlarmMode int DEFAULT NULL,
  Printer varchar(200) DEFAULT NULL,
  AutoPrintOnAlarm int NOT NULL,
  AutoPrintOnMeasurement int NOT NULL,
   PRIMARY KEY (Id)
);