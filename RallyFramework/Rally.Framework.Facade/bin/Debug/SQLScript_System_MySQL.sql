use newford;

DROP TABLE IF EXISTS `application_settings`;

CREATE TABLE `application_settings` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `PlatformIpAddress` varchar(20) DEFAULT NULL,
  `PlatformPortNumber` int DEFAULT NULL,
  `AlarmMode` int DEFAULT NULL,
  `AlarmSound` varchar(20) DEFAULT NULL,
  `NotificationMode` int DEFAULT NULL,
  `NotificationSound` varchar(20) DEFAULT NULL,
  `Language` int DEFAULT NULL,
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;