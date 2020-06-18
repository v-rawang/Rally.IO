use db_edge_nuclide;

DROP TABLE IF EXISTS `report_settings`;

CREATE TABLE `report_settings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `Title` varchar(20) DEFAULT NULL,
  `TemplateID` bigint(20) NOT NULL,
  `TemplateName` varchar(200),
  `AlarmMode` int DEFAULT NULL,
  `Printer` varchar(200) DEFAULT NULL,
  `AutoPrintOnAlarm` int NOT NULL,
  `AutoPrintOnMeasurement` int NOT NULL,
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;