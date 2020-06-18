-- ----------------------------
-- Table structure for nuclide
-- ----------------------------
DROP TABLE IF EXISTS nuclide;
CREATE TABLE nuclide  (
  ID int NOT NULL,
  No varchar(128),
  Name varchar(128),
  Symbol varchar(20),
  Type int NULL DEFAULT NULL,
  Category int NULL DEFAULT NULL,
  Energy1 numeric(255, 8) NULL DEFAULT NULL,
  BranchingRatio1 numeric(255, 8) NULL DEFAULT NULL,
  Channel1 numeric(255, 8) NULL DEFAULT NULL,
  Energy2 numeric(255, 8) NULL DEFAULT NULL,
  BranchingRatio2 numeric(255, 8) NULL DEFAULT NULL,
  Channel2 numeric(255, 8) NULL DEFAULT NULL,
  Energy3 numeric(255, 8) NULL DEFAULT NULL,
  BranchingRatio3 numeric(255, 8) NULL DEFAULT NULL,
  Channel3 numeric(255, 8) NULL DEFAULT NULL,
  HalfLife numeric(255, 8) NULL DEFAULT NULL,
  HalfLifeUnit varchar(20) DEFAULT NULL,
  HalfAddress varchar(20) NULL DEFAULT NULL,
  PRIMARY KEY (ID)
);

-- ----------------------------
-- Records of nuclide
-- ----------------------------
INSERT INTO nuclide VALUES (1, '1', 'Co-57', 'Co', 0, 0, 122.06140137, 85.59999847, 12.00000000, 136.47430420, 10.68000031, 12.00000000, 692.40997314, 0.14900000, 12.00000000, 271.79000854, 'd','');
INSERT INTO nuclide VALUES (2, '2', 'Co-60', 'Co', 0, 0, 11173.23730469, 99.97000122, 12.00000000, 1332.50097656, 99.98000336, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 5.57140017, 'y','');
INSERT INTO nuclide VALUES (3, '3', 'Ba-133', 'Ba', 0, 0, 80.99700165, 34.06000137, 12.00000000, 276.39999390, 7.16400003, 12.00000000, 302.85101318, 18.32999992, 12.00000000, 10.51000023, 'y','');
INSERT INTO nuclide VALUES (4, '4', 'CS-137', 'Cs', 0, 0, 661.65698242, 85.09999847, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 30.06999969, 'y','');
INSERT INTO nuclide VALUES (5, '5', 'Eu-152', 'Eu', 0, 0, 867.37298584, 5.90600014, 12.00000000, 964.07897949, 20.31999969, 12.00000000, 1085.86901855, 14.19999981, 12.00000000, 13.53699970, 'y','');
INSERT INTO nuclide VALUES (6, '6', 'Ra-226', 'Ra', 0, 0, 295.22399902, 19.29999924, 12.00000000, 351.93200684, 37.59999847, 12.00000000, 609.31201172, 46.09999847, 12.00000000, 1600.00000000, 'y','');
INSERT INTO nuclide VALUES (7, '7', 'K-40', 'K', 0, 0, 1460.82995605, 10.67000008, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 0.00000000, 0.00000000, 12.00000000, 1.27699995, 'y','');

DROP TABLE IF EXISTS energycalibration;
CREATE TABLE energycalibration (
  ID varchar(128) NOT NULL,
  Time timestamp DEFAULT NULL,
  OperatorID varchar(128) DEFAULT NULL,
  InstrumentNo varchar(20) DEFAULT NULL,
  NuclideBoardNo varchar(20) DEFAULT NULL,
  Channel1 numeric(255,8) DEFAULT NULL,
  Energy1 numeric(255,8) DEFAULT NULL,
  Channel2 numeric(255,8) DEFAULT NULL,
  Energy2 numeric(255,8) DEFAULT NULL,
  Channel3 numeric(255,8) DEFAULT NULL,
  Energy3 numeric(255,8) DEFAULT NULL,
  Channel4 numeric(255,8) DEFAULT NULL,
  Energy4 numeric(255,8) DEFAULT NULL,
  Channel5 numeric(255,8) DEFAULT NULL,
  Energy5 numeric(255,8) DEFAULT NULL,
  CoefficientA numeric(255,8) DEFAULT NULL,
  CoefficientB numeric(255,8) DEFAULT NULL,
  CoefficientC numeric(255,8) DEFAULT NULL,
  EnergyResolution numeric(255,8) DEFAULT NULL,
  PRIMARY KEY (ID)
);

DROP TABLE IF EXISTS backgrounddataitems;
CREATE TABLE backgrounddataitems (
  ID varchar(128) NOT NULL,
  MeasurementTime timestamp DEFAULT NULL,
  InstrumentID bigint NOT NULL,
  DetectorType varchar(20) DEFAULT NULL,
  DetectorNo varchar(20) DEFAULT NULL,
  BackgroundValue numeric(255,8) DEFAULT NULL,
  PRIMARY KEY (ID)
);

DROP TABLE IF EXISTS backgrounddailyaverages;
CREATE TABLE backgrounddailyaverages (
  ID varchar(128) NOT NULL,
  StatTime timestamp DEFAULT NULL,
  InstrumentID bigint NOT NULL,
  DetectorType varchar DEFAULT NULL,
  DetectorNo varchar(20) DEFAULT NULL,
  BackgroundValue numeric(255,8) DEFAULT NULL,
  PRIMARY KEY (ID)
);

DROP TABLE IF EXISTS backgroundmonthlyaverages;
CREATE TABLE backgroundmonthlyaverages (
  ID varchar(128) NOT NULL,
  StatTime timestamp DEFAULT NULL,
  InstrumentID bigint NOT NULL,
  DetectorType varchar(20) DEFAULT NULL,
  DetectorNo varchar(20) DEFAULT NULL,
  BackgroundValue numeric(255,8) DEFAULT NULL,
  PRIMARY KEY (ID)
);

