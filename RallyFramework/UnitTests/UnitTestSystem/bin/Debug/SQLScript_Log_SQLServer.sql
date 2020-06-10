use newford;

DROP TABLE IF EXISTS Logs;

CREATE TABLE Logs (

  ID int NOT NULL IDENTITY(1,1),
  
  TimeStamp datetime NOT NULL,
  
  Category nvarchar(25) NULL,

  LogLevel nvarchar(25) NOT NULL,

  CallSite nvarchar(max) DEFAULT NULL,

  Message text,

  StackTrace nvarchar(max) DEFAULT NULL,
  
  Exception nvarchar(max) DEFAULT NULL,
  
  MachineName nvarchar(30) DEFAULT NULL,

  "Identity" nvarchar(40) DEFAULT NULL,
   
  ProcessName nvarchar(40) DEFAULT NULL,
  
  ThreadName nvarchar(40) DEFAULT NULL,
  
  LoggerName nvarchar(40) DEFAULT NULL,
   
  PRIMARY KEY (ID)
);