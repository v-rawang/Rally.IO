DROP TABLE IF EXISTS accounts;
CREATE TABLE accounts (
  Id varchar(128) NOT NULL,
  Name varchar(252) NOT NULL,
  FirstName varchar(252) DEFAULT NULL,
  LastName varchar(252) DEFAULT NULL,
  NickName varchar(252) NOT NULL,
  Gender int DEFAULT NULL,
  BirthDate timestamp NULL,
  Title varchar(252) DEFAULT NULL,
  SID varchar(252) DEFAULT NULL,
  Alias varchar(252) DEFAULT NULL,
  Address varchar(252) DEFAULT NULL,
  ZipCode varchar(252) DEFAULT NULL,
  Email varchar(252) DEFAULT NULL,
  Mobile varchar(252) DEFAULT NULL,
  PhoneNumber varchar(252) DEFAULT NULL,
  BloodType varchar(4) DEFAULT NULL,
  Constellation varchar(16) DEFAULT NULL,
  Hobby varchar(100) DEFAULT NULL,
  PoliticsStatus varchar(152) DEFAULT NULL,
  Education varchar(152) DEFAULT NULL,
  Industry varchar(152) DEFAULT NULL,
  Organization varchar(252) DEFAULT NULL,
  Department varchar(252) DEFAULT NULL,
  WorkGroup varchar(252) DEFAULT NULL,
  Position varchar(252) DEFAULT NULL,
  Headline varchar(252) DEFAULT NULL,
  Biography text,
  Description varchar(500) DEFAULT NULL,
  HeadImageFileID varchar(252) DEFAULT NULL,
  ModifiedDate timestamp DEFAULT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE AccountProviders
(
  Id varchar(128) NOT NULL,
  Name varchar(252) NOT NULL,
  AppId varchar(252) NULL,
  AppSecret varchar(252) NULL,
  AdditionalInfo text DEFAULT NULL,
  ModifiedDate timestamp DEFAULT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE ExternalAccounts 
(
  AccountId varchar(128) NOT NULL,
  ProviderId varchar(128) NOT NULL,
  Identifier varchar(128) NOT NULL,
  AdditionalInfo text DEFAULT NULL,
  ModifiedDate timestamp DEFAULT NULL,
   --INDEX IX_FK_ExternalAccountAccounts (AccountId ASC),
   --CONSTRAINT FK_ExternalAccountAccounts
   -- FOREIGN KEY (AccountId)
   -- REFERENCES Accounts (Id)
   -- ON DELETE NO ACTION
   -- ON UPDATE NO ACTION,
   --INDEX IX_FK_ExternalAccountProviders (ProviderId ASC),
   --CONSTRAINT FK_ExternalAccountProviders
   -- FOREIGN KEY (ProviderId)
   -- REFERENCES AccountProviders (Id)
   -- ON DELETE NO ACTION
   -- ON UPDATE NO ACTION
);