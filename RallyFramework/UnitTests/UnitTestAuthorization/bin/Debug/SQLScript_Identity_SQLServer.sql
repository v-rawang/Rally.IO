use newford;

CREATE TABLE users (
  Id varchar(128) NOT NULL,
  UserName varchar(256) NOT NULL,
  UserType int NULL,
  Email nvarchar(256) DEFAULT NULL,
  EmailConfirmed tinyint NOT NULL,
  PasswordHash nvarchar(max),
  PasswordSalt nvarchar(128) NOT NULL,
  PasswordRev int NOT NULL,
  SecurityStamp nvarchar(max),
  PhoneNumber nvarchar(max),
  PhoneNumberConfirmed tinyint NOT NULL,
  TwoFactorEnabled tinyint NOT NULL,
  LockoutEndDateUtc datetime DEFAULT NULL,
  LockoutEnabled tinyint NOT NULL,
  AccessFailedCount int NOT NULL,  
  Description nvarchar(max) NULL,
  CreateDate datetime,
  ConfirmationToken nvarchar(128),
  IsConfirmed tinyint NOT NULL,
  LastPasswordFailureDate datetime,
  PasswordFailuresSinceLastSuccess int,
  PasswordChangedDate datetime,
  PasswordVerificationToken nvarchar(128),
  PasswordVerificationTokenExpirationDate datetime,
  PRIMARY KEY (Id)
);

CREATE TABLE userclaims (
  Id int NOT NULL IDENTITY(1,1),
  UserId nvarchar(128) NOT NULL,
  ClaimType nvarchar(max),
  ClaimValue nvarchar(max),
  PRIMARY KEY (Id),
  --UNIQUE KEY Id (Id),
  --KEY UserId (UserId),
  --CONSTRAINT ApplicationUser_Claims FOREIGN KEY UserId REFERENCES users (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE userlogins (
  LoginProvider varchar(128) NOT NULL,
  ProviderKey varchar(128) NOT NULL,
  UserId varchar(128) NOT NULL,
  PRIMARY KEY (LoginProvider,ProviderKey,UserId),
  --KEY ApplicationUser_Logins (UserId),
  --CONSTRAINT ApplicationUser_Logins FOREIGN KEY (UserId) REFERENCES users (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);