CREATE TABLE users (
  Id varchar(128) NOT NULL,
  UserName varchar(256) NOT NULL,
  UserType smallint NULL,
  Email varchar(256) DEFAULT NULL,
  EmailConfirmed smallint NOT NULL,
  PasswordHash text,
  PasswordSalt varchar(128) NOT NULL,
  PasswordRev int NOT NULL,
  SecurityStamp text,
  PhoneNumber text,
  PhoneNumberConfirmed smallint NOT NULL,
  TwoFactorEnabled smallint NOT NULL,
  LockoutEndDateUtc timestamp DEFAULT NULL,
  LockoutEnabled smallint NOT NULL,
  AccessFailedCount int NOT NULL,  
  Description text NULL,
  CreateDate timestamp,
  ConfirmationToken varchar(128),
  IsConfirmed smallint NOT NULL,
  LastPasswordFailureDate timestamp,
  PasswordFailuresSinceLastSuccess int,
  PasswordChangedDate timestamp,
  PasswordVerificationToken varchar(128),
  PasswordVerificationTokenExpirationDate timestamp,
  PRIMARY KEY (Id)
);

CREATE TABLE userclaims (
  Id serial NOT NULL,
  UserId varchar(128) NOT NULL,
  ClaimType text,
  ClaimValue text,
  PRIMARY KEY (Id)
  --UNIQUE KEY Id (Id),
  --KEY UserId (UserId),
  --CONSTRAINT ApplicationUser_Claims FOREIGN KEY (UserId) REFERENCES users (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE userlogins (
  LoginProvider varchar(128) NOT NULL,
  ProviderKey varchar(128) NOT NULL,
  UserId varchar(128) NOT NULL,
  PRIMARY KEY (LoginProvider, ProviderKey, UserId)
  --KEY ApplicationUser_Logins (UserId),
  --CONSTRAINT ApplicationUser_Logins FOREIGN KEY (UserId) REFERENCES users (Id) ON DELETE CASCADE ON UPDATE NO ACTION
);