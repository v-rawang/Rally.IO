use newford;

DROP TABLE IF EXISTS files;

CREATE TABLE files (
  ID bigint NOT NULL IDENTITY(1,1),
  FilePath nvarchar(1200) NOT NULL,
  FileName nvarchar(250) NOT NULL,
  FileType nvarchar(20) NOT NULL,
  FileSize nvarchar(20) DEFAULT NULL,
  Version nvarchar(20) DEFAULT NULL,
  FileCreationTime datetime NOT NULL,
  FileOwner nvarchar(128)  NOT NULL,
  PRIMARY KEY (Id)
);
