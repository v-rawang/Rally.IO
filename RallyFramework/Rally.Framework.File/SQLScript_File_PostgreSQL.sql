use db_edge_nuclide;

DROP TABLE IF EXISTS files;

CREATE TABLE files (
  ID bigserial NOT NULL,
  FilePath varchar(1200) NOT NULL,
  FileName varchar(250) NOT NULL,
  FileType varchar(20) NOT NULL,
  FileSize varchar(20) DEFAULT NULL,
  Version varchar(20) DEFAULT NULL,
  FileCreationTime timestamp NOT NULL,
  FileOwner varchar(128)  NOT NULL,
  PRIMARY KEY (Id)
);
