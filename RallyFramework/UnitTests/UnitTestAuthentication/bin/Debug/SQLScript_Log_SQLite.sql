CREATE TABLE `Logs` (

  `ID` int(11) NOT NULL,
  
  `TimeStamp` datetime NOT NULL,
  
  `Category` varchar(25) NULL,

  `LogLevel` varchar(25) NOT NULL,

  `CallSite` varchar(5000) DEFAULT NULL,

  `Message` longtext,

  `StackTrace` varchar(5000) DEFAULT NULL,
  
  `Exception` varchar(5000) DEFAULT NULL,
  
  `MachineName` varchar(30) DEFAULT NULL,

  `Identity` varchar(40) DEFAULT NULL,
   
  `ProcessName` varchar(40) DEFAULT NULL,
  
  `ThreadName` varchar(40) DEFAULT NULL,
  
  `LoggerName` varchar(40) DEFAULT NULL,
   
  PRIMARY KEY (`ID`)
);