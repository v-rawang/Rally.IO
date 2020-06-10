use newford;

CREATE TABLE `Accounts` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(252) NOT NULL,
  `FirstName` varchar(252) NULL,
  `LastName` varchar(252) NULL,
  `NickName` varchar(252) NOT NULL,
  `Gender` tinyint(1) DEFAULT NULL,
  `BirthDate` datetime DEFAULT NULL,
  `Title` varchar(252) NULL,
  `SID` varchar(252) NULL,
  `Alias` varchar(252) NULL,
  `Address` varchar(252) NULL,
  `ZipCode` varchar(252) NULL,
  `Email` varchar(252) DEFAULT NULL,
  `Mobile` varchar(252) DEFAULT NULL,
  `PhoneNumber` varchar(252) DEFAULT NULL,
  `BloodType` varchar(4) DEFAULT NULL,
  `Constellation` varchar(16) DEFAULT NULL,
  `Hobby` varchar(100) DEFAULT NULL,
  `PoliticsStatus` varchar(152) DEFAULT NULL,
  `Education` varchar(152) DEFAULT NULL,
  `Industry` varchar(152) DEFAULT NULL,
  `Organization` varchar(252) DEFAULT NULL,
  `Department` varchar(252) DEFAULT NULL,
  `WorkGroup` varchar(252) DEFAULT NULL,
  `Position` varchar(252) DEFAULT NULL,
  `Headline` varchar(252) DEFAULT NULL,
  `Biography` longtext DEFAULT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `HeadImageFileID` varchar(252) NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
)ENGINE=InnoDB DEFAULT CHARSET=gb2312;

CREATE TABLE `AccountProviders` 
(
  `Id` varchar(128) NOT NULL,
  `Name` varchar(252) NOT NULL,
  `AppId` varchar(252) NULL,
  `AppSecret` varchar(252) NULL,
  `AdditionalInfo` longtext DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
)ENGINE=InnoDB DEFAULT CHARSET=gb2312;

CREATE TABLE `ExternalAccounts` 
(
  `AccountId` varchar(128) NOT NULL,
  `ProviderId` varchar(128) NOT NULL,
  `Identifier` varchar(128) NOT NULL,
  `AdditionalInfo` longtext DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
   INDEX `IX_FK_ExternalAccountAccounts` (`AccountId` ASC),
   CONSTRAINT `FK_ExternalAccountAccounts`
    FOREIGN KEY (`AccountId`)
    REFERENCES `Accounts` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
   INDEX `IX_FK_ExternalAccountProviders` (`ProviderId` ASC),
   CONSTRAINT `FK_ExternalAccountProviders`
    FOREIGN KEY (`ProviderId`)
    REFERENCES `AccountProviders` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
)ENGINE=InnoDB DEFAULT CHARSET=gb2312;