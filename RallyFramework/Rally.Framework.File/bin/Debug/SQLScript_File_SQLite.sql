CREATE TABLE "files" (
	"ID"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	"FilePath"	varchar(1200) NOT NULL,
	"FileName"	varchar(250) NOT NULL,
	"FileType"	varchar(20) NOT NULL,
	"FileSize"	varchar(20) DEFAULT NULL,
	"Version"	varchar(20) DEFAULT NULL,
	"FileCreationTime"	datetime NOT NULL,
	"FileOwner"	varchar(128) NOT NULL
);