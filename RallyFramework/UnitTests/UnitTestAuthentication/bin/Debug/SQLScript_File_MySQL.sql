use newford;

DROP TABLE IF EXISTS `files`;

CREATE TABLE `files` (
  `ID` bigint(20) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `FilePath` varchar(1200) NOT NULL,
  `FileName` varchar(250) NOT NULL,
  `FileType` varchar(20) NOT NULL,
  `FileSize` varchar(20) DEFAULT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `FileCreationTime` datetime NOT NULL,
  `FileOwner` varchar(128)  NOT NULL,
   PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
