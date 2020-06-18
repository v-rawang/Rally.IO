-- ----------------------------
-- Table structure for applicationsettings
-- ----------------------------
DROP TABLE IF EXISTS applicationsettings;
CREATE TABLE applicationsettings (
  ID bigserial NOT NULL,
  PlatformIpAddress varchar(20) DEFAULT NULL,
  PlatformPortNumber int DEFAULT NULL,
  AlarmMode int DEFAULT NULL,
  AlarmSound varchar(20) DEFAULT NULL,
  NotificationMode int DEFAULT NULL,
  NotificationSound varchar(20) DEFAULT NULL,
  Language int DEFAULT NULL,
  Protocol varchar(30) DEFAULT NULL,
  name2 varchar(30) DEFAULT NULL,
  PlatformIpAddress2 varchar(30) DEFAULT NULL,
  PlatformPortNumber2 varchar(30) DEFAULT NULL,
  Protocol2 varchar(30) DEFAULT NULL,
  name3 varchar(30) DEFAULT NULL,
  PlatformIpAddress3 varchar(30) DEFAULT NULL,
  PlatformPortNumber3 varchar(30) DEFAULT NULL,
  Protocol3 varchar(30) DEFAULT NULL,
  name4 varchar(30) DEFAULT NULL,
  PlatformIpAddress4 varchar(30) DEFAULT NULL,
  PlatformPortNumber4 varchar(30) DEFAULT NULL,
  Protocol4 varchar(30) DEFAULT NULL,
  theme varchar(30) DEFAULT NULL,
  time varchar(100) DEFAULT NULL,
  type varchar(100) DEFAULT NULL,
  Duration2 varchar(30) DEFAULT NULL,
  Duration1 varchar(30) DEFAULT NULL,
  name varchar(30) DEFAULT NULL,
  BgValArchFrequency int DEFAULT NULL, 
  ImageCapturingMode int DEFAULT NULL,
  ImageCapturingCount int DEFAULT NULL,
  VideoCapturingMode int DEFAULT NULL,
  VideoCapturingLength int DEFAULT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of applicationsettings
-- ----------------------------
-- INSERT INTO applicationsettings VALUES ('00000000000000000002', null, '0', '1', 'ALARM2.WAV', '1', 'ALARM2.WAV', '2', null, null, null, '0', null, null, null, '0', null, null, null, '0', null, '默认', '每月|1|24|0|0', null, '0', '0', null);

INSERT INTO applicationsettings VALUES ('00000000000000000002', null, '0', '1', 'ALARM2.WAV', '1', 'ALARM2.WAV', '2', null, null, null, '0', null, null, null, '0', null, null, null, '0', null, '默认', '每月|1|24|0|0', null, '0', '0', null, 60, 0,3,0,5);

