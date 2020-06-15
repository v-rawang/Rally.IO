use newford;

CREATE TABLE Accounts (
  Id nvarchar(128) NOT NULL,
  Name nvarchar(252) NOT NULL,
  FirstName nvarchar(252) NULL,
  LastName nvarchar(252) NULL,
  NickName nvarchar(252) NOT NULL,
  Gender tinyint DEFAULT NULL,
  BirthDate datetime DEFAULT NULL,
  Title nvarchar(252) NULL,
  SID nvarchar(252) NULL,
  Alias nvarchar(252) NULL,
  Address nvarchar(252) NULL,
  ZipCode nvarchar(252) NULL,
  Email nvarchar(252) DEFAULT NULL,
  Mobile nvarchar(252) DEFAULT NULL,
  PhoneNumber varchar(252) DEFAULT NULL,
  BloodType nvarchar(4) DEFAULT NULL,
  Constellation nvarchar(16) DEFAULT NULL,
  Hobby nvarchar(100) DEFAULT NULL,
  PoliticsStatus varchar(152) DEFAULT NULL,
  Education nvarchar(152) DEFAULT NULL,
  Industry nvarchar(152) DEFAULT NULL,
  Organization nvarchar(252) DEFAULT NULL,
  Department nvarchar(252) DEFAULT NULL,
  WorkGroup nvarchar(252) DEFAULT NULL,
  Position nvarchar(252) DEFAULT NULL,
  Headline nvarchar(252) DEFAULT NULL,
  Biography nvarchar(max) DEFAULT NULL,
  Description nvarchar(500) DEFAULT NULL,
  HeadImageFileID nvarchar(252) NULL,
  ModifiedDate datetime DEFAULT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE AccountProviders 
(
  Id nvarchar(128) NOT NULL,
  Name nvarchar(252) NOT NULL,
  AppId nvarchar(252) NULL,
  AppSecret nvarchar(252) NULL,
  AdditionalInfo nvarchar(max) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE ExternalAccounts 
(
  AccountId nvarchar(128) NOT NULL,
  ProviderId nvarchar(128) NOT NULL,
  Identifier nvarchar(128) NOT NULL,
  AdditionalInfo nvarchar(max) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
   INDEX IX_FK_ExternalAccountAccounts (AccountId ASC),
   CONSTRAINT FK_ExternalAccountAccounts
    FOREIGN KEY (AccountId)
    REFERENCES Accounts (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
   INDEX IX_FK_ExternalAccountProviders (ProviderId ASC),
   CONSTRAINT FK_ExternalAccountProviders
    FOREIGN KEY (ProviderId)
    REFERENCES AccountProviders (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);