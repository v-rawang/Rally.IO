CREATE TABLE IF NOT EXISTS `Roles` 
(
  `Id` VARCHAR(250) NOT NULL,
  `Name` VARCHAR(250) NOT NULL,
  `RoleType` VARCHAR(250) NOT NULL,
  `Description` VARCHAR(250) NOT NULL,
   PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `RoleActors` 
(
  `Id` VARCHAR(250) NOT NULL,
  `RoleId` VARCHAR(250) NOT NULL,
  `ActorId` VARCHAR(250) NOT NULL,
   PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `Operations` 
(
  `Id` VARCHAR(250) NOT NULL,
  `Name` VARCHAR(250) NOT NULL,
  `DataType` VARCHAR(250) NOT NULL,
   PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `DataScopes` (
  `Id` VARCHAR(250) NOT NULL,
  `ScopeName` VARCHAR(250) NOT NULL,
  `ScopeType` VARCHAR(250) NOT NULL,
  `DataType` VARCHAR(250) NOT NULL,
  `DataIdentifier` VARCHAR(250) NOT NULL,
   PRIMARY KEY (`Id`)
);

CREATE TABLE IF NOT EXISTS `ObjectOperationAuthItems` (
  `Id` VARCHAR(250) NOT NULL,
  `ObjectId` VARCHAR(250) NOT NULL,
  `ActorId` VARCHAR(250) NOT NULL,
  `OperationId` VARCHAR(250) NOT NULL,
  PRIMARY KEY (`Id`)--,
  --INDEX `IX_FK_OperationObjectOperationAuthItem` (`OperationId` ASC),
  --CONSTRAINT `FK_OperationObjectOperationAuthItem`
   --FOREIGN KEY (`OperationId`)
    --REFERENCES `Operations` (`Id`)
   --ON DELETE NO ACTION
    --ON UPDATE NO ACTION
);

CREATE TABLE IF NOT EXISTS `RoleOperations` (
  `Id` VARCHAR(250) NOT NULL,
  `RoleId` VARCHAR(250) NOT NULL,
  `OperationId` VARCHAR(250) NOT NULL,
  PRIMARY KEY (`Id`)--,
  --INDEX `IX_FK_OperationRoleOperation` (`OperationId` ASC),
  --CONSTRAINT `FK_OperationRoleOperation`
    --FOREIGN KEY (`OperationId`)
    --REFERENCES `Operations` (`Id`)
    --ON DELETE NO ACTION
    --ON UPDATE NO ACTION
);


CREATE TABLE IF NOT EXISTS `RoleDataScopes` (
  `Id` VARCHAR(250) NOT NULL,
  `RoleId` VARCHAR(250) NOT NULL,
  `DataScopeId` VARCHAR(250) NOT NULL,
  `ScopeValue` VARCHAR(250) NOT NULL,
  PRIMARY KEY (`Id`)--,
  --INDEX `IX_FK_DataScopeRoleDataScope` (`DataScopeId` ASC),
  --CONSTRAINT `FK_DataScopeRoleDataScope`
    --FOREIGN KEY (`DataScopeId`)
    --REFERENCES `DataScopes` (`Id`)
    --ON DELETE NO ACTION
    --ON UPDATE NO ACTION
);