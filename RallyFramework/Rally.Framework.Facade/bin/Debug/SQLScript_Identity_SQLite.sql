CREATE TABLE `users` (
  `Id` varchar(128) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  `UserType` int(1) NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `PasswordSalt` varchar(128) NOT NULL,
  `PasswordRev` int(11) NOT NULL,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(18) NOT NULL,  
  `Description` longtext NULL,
  `CreateDate` datetime,
  `ConfirmationToken` varchar(128),
  `IsConfirmed` tinyint(1) NOT NULL,
  `LastPasswordFailureDate` datetime,
  `PasswordFailuresSinceLastSuccess` int,
  `PasswordChangedDate` datetime,
  `PasswordVerificationToken` varchar(128),
  `PasswordVerificationTokenExpirationDate` datetime,
  PRIMARY KEY (`Id`)
);

CREATE TABLE `userclaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`)
  --UNIQUE KEY `Id` (`Id`),
  --KEY `UserId` (`UserId`),
  --CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE `userlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`)--,
  --KEY `ApplicationUser_Logins` (`UserId`),
  --CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
);