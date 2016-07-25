USE MASTER;
GO
DROP DATABASE ProUTN;
GO

CREATE DATABASE ProUTN;
GO
USE ProUTN;
GO

-------------------------------------
-- Table structure for Role
-------------------------------------- 
IF OBJECT_ID('ROLE','U')IS NOT NULL
	DROP TABLE ROLE;
GO
CREATE TABLE ROLE(
ROLE_ID		        NUMERIC NOT NULL,
DESCRIPTION			VARCHAR(30)NOT NULL,
STATE				NUMERIC(1)NOT NULL,
DELETED				NUMERIC(1)NOT NULL,
PRIMARY KEY(ROLE_ID));
GO

-------------------------------------
-- Table structure for System Modules
-------------------------------------- 
IF OBJECT_ID('SYSTEMMODULE','U')IS NOT NULL
	DROP TABLE SYSTEMMODULE;
GO
CREATE TABLE SYSTEMMODULE(
SYSTEMMODULES_ID		NUMERIC NOT NULL,
DESCRIPTION			VARCHAR(30)NOT NULL,
STATE				NUMERIC(1)NOT NULL,
DELETED				NUMERIC(1)NOT NULL,
PRIMARY KEY(SYSTEMMODULES_ID));
GO

-------------------------------------
-- Table structure for RoleModule
-------------------------------------- 
IF OBJECT_ID('ROLEMODULE','U')IS NOT NULL
	DROP TABLE ROLEMODULE;
GO
CREATE TABLE ROLEMODULE(
ROLE_ID		        NUMERIC NOT NULL,
SYSTEMMODULES_ID    NUMERIC NOT NULL,
PRIMARY KEY(ROLE_ID,SYSTEMMODULES_ID),
FOREIGN KEY(SYSTEMMODULES_ID) REFERENCES SYSTEMMODULE(SYSTEMMODULES_ID),
FOREIGN KEY(ROLE_ID) REFERENCES ROLE(ROLE_ID));
GO

-------------------------------------
-- Table structure for program
-------------------------------------- 
IF OBJECT_ID('PROGRAM','U') IS NOT NULL
	DROP TABLE PROGRAM;
GO
CREATE TABLE PROGRAM
(PROGRAM_ID			NUMERIC NOT NULL,
 NAME				VARCHAR(50) NOT NULL,
 STATE				NUMERIC(1) NOT NULL,
 UNIT				BIGINT NOT NULL,
 DELETED			NUMERIC(1)NOT NULL,
 PRIMARY KEY(PROGRAM_ID));
 GO

-------------------------------------
-- Table structure for USERSYSTEM 
--------------------------------------
IF OBJECT_ID('USERSYSTEM','U') IS NOT NULL
	DROP TABLE USERSYSTEM;
GO
CREATE TABLE USERSYSTEM
(USERSYSTEM_ID  NUMERIC NOT NULL,
ID				VARCHAR(15) NOT NULL,
NAME			VARCHAR(50) NOT NULL,
SURNAME			VARCHAR(50) NOT NULL,
PROGRAM_ID		NUMERIC,
PHONE			VARCHAR(15),
CELLPHONE		VARCHAR(15) NOT NULL,
MAIL			VARCHAR(100)NOT NULL,
PASSWORD		VARCHAR(100)NOT NULL,
Set_Password    NUMERIC(1) NOT NULL,
ROLE_ID		    NUMERIC NOT NULL,
STATE			NUMERIC(1)NOT NULL,
DELETED			NUMERIC(1) NOT NULL,
PRIMARY KEY(USERSYSTEM_ID),
FOREIGN KEY(ROLE_ID)REFERENCES ROLE(ROLE_ID),
FOREIGN KEY(PROGRAM_ID)REFERENCES PROGRAM(PROGRAM_ID));
GO

-------------------------------------
-- Table structure for period type
-------------------------------------- 
IF OBJECT_ID('PERIODTYPE','U')IS NOT NULL
	DROP TABLE PERIODTYPE;
GO
CREATE TABLE PERIODTYPE(
PERIODTYPE_ID		NUMERIC NOT NULL,
DESCRIPTION			VARCHAR(30)NOT NULL,
STATE				NUMERIC(1)NOT NULL,
DELETED				NUMERIC(1)NOT NULL,
PRIMARY KEY(PERIODTYPE_ID));
GO

-------------------------------------
-- Table structure for period
--------------------------------------
 IF OBJECT_ID('PERIOD','U') IS NOT NULL
	DROP TABLE PERIOD;
GO
 CREATE TABLE PERIOD(
 PERIOD_ID			NUMERIC NOT NULL,
 NAME				VARCHAR(50) NOT NULL,
 STARTDATE			DATETIME NOT NULL,
 FINALDATE			DATETIME NOT NULL,
 MODALITY			NUMERIC NOT NULL,
 STATE				NUMERIC(1) NOT NULL,
 DELETED			NUMERIC(1)NOT NULL,
 PRIMARY KEY(PERIOD_ID),
 FOREIGN KEY(MODALITY)REFERENCES PERIODTYPE(PERIODTYPE_ID));
 GO

-------------------------------------
-- Table structure for headquarters
--------------------------------------
IF OBJECT_ID('HEADQUARTERS','U') IS NOT NULL
	DROP TABLE HEADQUARTERS;
GO
CREATE TABLE HEADQUARTERS
(HEADQUARTERS_ID	NUMERIC NOT NULL,
 NAME				VARCHAR(50) NOT NULL,
 STATE				NUMERIC(1) NOT NULL,
 DELETED			NUMERIC(1)NOT NULL,
 PRIMARY KEY(HEADQUARTERS_ID));
 GO

-------------------------------------
-- Table structure for location
--------------------------------------
IF OBJECT_ID('LOCATION','U') IS NOT NULL
	DROP TABLE LOCATION;
GO
CREATE TABLE LOCATION
(LOCATION_ID		NUMERIC NOT NULL,
 BUILDING			VARCHAR(100) NOT NULL,
 MODULE				VARCHAR(50) NOT NULL,
 HEADQUARTERS_ID	NUMERIC NOT NULL,
 STATE				NUMERIC(1) NOT NULL,
 DELETED			NUMERIC(1)NOT NULL,
 PRIMARY KEY(LOCATION_ID),
 FOREIGN KEY(HEADQUARTERS_ID) REFERENCES HEADQUARTERS(HEADQUARTERS_ID));
GO

-------------------------------------
-- Table structure for classroom type 
--------------------------------------
IF OBJECT_ID('CLASSROOMSTYPE','U') IS NOT NULL
	DROP TABLE CLASSROOMSTYPE;
GO
CREATE TABLE CLASSROOMSTYPE
(CLASSROOMSTYPE_ID	NUMERIC NOT NULL,
 DESCRIPTION		VARCHAR(70) NOT NULL,
 STATE				NUMERIC(1) NOT NULL,
 DELETED			NUMERIC(1)NOT NULL,
 PRIMARY KEY(CLASSROOMSTYPE_ID));
GO

-------------------------------------
-- Table structure for classroom 
--------------------------------------
IF OBJECT_ID('CLASSROOM','U') IS NOT NULL
	DROP TABLE CLASSROOM;
GO
CREATE TABLE CLASSROOM
(CLASSROOM_ID		NUMERIC NOT NULL,
 NUM_ROOM			VARCHAR(10) NOT NULL,
 SIZE				NUMERIC NOT NULL,
 PROGRAM_ID			NUMERIC NOT NULL,
 CLASSROOMSTYPE_ID	NUMERIC NOT NULL,
 LOCATION_ID		NUMERIC NOT NULL,
 DELETED			NUMERIC(1) NOT NULL,
 STATE				NUMERIC(1) NOT NULL,
 PRIMARY KEY(CLASSROOM_ID),
 FOREIGN KEY(PROGRAM_ID) REFERENCES PROGRAM(PROGRAM_ID),
 FOREIGN KEY(LOCATION_ID) REFERENCES LOCATION(LOCATION_ID),
 FOREIGN KEY(CLASSROOMSTYPE_ID) REFERENCES CLASSROOMSTYPE(CLASSROOMSTYPE_ID));
GO

-------------------------------------
-- Table structure for internal designation 
--------------------------------------
IF OBJECT_ID('INTERNAL_DESIGNATION','U') IS NOT NULL
	DROP TABLE INTERNAL_DESIGNATION;
GO
CREATE TABLE INTERNAL_DESIGNATION 
(DESIGNATION_ID		NUMERIC NOT NULL,
 DESCRIPTION		VARCHAR(100) NOT NULL,
 DELETED			NUMERIC(1) NOT NULL,
 STATE				NUMERIC(1) NOT NULL,
 BASE_SALARY		MONEY NOT NULL,
 ANNUITY			FLOAT NOT NULL,
 PRIMARY KEY(DESIGNATION_ID));
GO  

-------------------------------------
-- Table structure for teacher 
--------------------------------------
IF OBJECT_ID('TEACHER','U') IS NOT NULL
	DROP TABLE TEACHER;
GO
CREATE TABLE TEACHER
(TEACHER_ID		NUMERIC NOT NULL,
ID				VARCHAR(15) NOT NULL,				
NAME			VARCHAR(50) NOT NULL,
SURNAME			VARCHAR(50) NOT NULL,
PHONE			VARCHAR(15)NOT NULL,
CELEPHONE		VARCHAR(15)NOT NULL,
MAIL			VARCHAR(100)NOT NULL,
POSITION_ID		NUMERIC NOT NULL,
STATE			NUMERIC(1) NOT NULL,
DELETED			NUMERIC(1) NOT NULL,
PRIMARY KEY(TEACHER_ID),
FOREIGN KEY(POSITION_ID) REFERENCES INTERNAL_DESIGNATION(DESIGNATION_ID));
GO

-------------------------------------
-- Table structure for functionary 
--------------------------------------
IF OBJECT_ID('FUNCTIONARY','U') IS NOT NULL
	DROP TABLE FUNCTIONARY;
GO
CREATE TABLE FUNCTIONARY
(FUNCTIONARY_ID NUMERIC NOT NULL,
ID				VARCHAR(15) NOT NULL,
NAME			VARCHAR(50) NOT NULL,
SURNAME			VARCHAR(50) NOT NULL,
PROGRAM_ID		NUMERIC NOT NULL,
PHONE			VARCHAR(15),
CELLPHONE		VARCHAR(15) NOT NULL,
MAIL			VARCHAR(100)NOT NULL,
STATE			NUMERIC(1)NOT NULL,
DELETED			NUMERIC(1) NOT NULL,
PRIMARY KEY(FUNCTIONARY_ID),
FOREIGN KEY(PROGRAM_ID)REFERENCES PROGRAM(PROGRAM_ID));
GO

-------------------------------------
-- Table structure for Schedule 
--------------------------------------
IF OBJECT_ID('SCHEDULE','U') IS NOT NULL
	DROP TABLE SCHEDULE;
GO
CREATE TABLE SCHEDULE
(SCHEDULE_ID	NUMERIC NOT NULL,
PROGRAM_ID		NUMERIC NOT NULL,
DESCRIPTION		VARCHAR(50) NOT NULL,
CODDESCRIPTION	VARCHAR(10) NOT NULL,
TYPESHEDULE		VARCHAR(50) NOT NULL,
STARTTIME		TIME NOT NULL,
ENDTIME			TIME NOT NULL,
STATE			NUMERIC(1) NOT NULL,
DELETED			NUMERIC(1) NOT NULL,
PRIMARY KEY(SCHEDULE_ID),
FOREIGN KEY(PROGRAM_ID)REFERENCES PROGRAM(PROGRAM_ID));
GO 

-------------------------------------
-- Table structure for Course
--------------------------------------
IF OBJECT_ID('COURSE','U') IS NOT NULL
	DROP TABLE COURSE;
GO
CREATE TABLE COURSE
(COURSE_ID		NUMERIC NOT NULL,
 DESCRIPTION	VARCHAR(100) NOT NULL,
 STATE			NUMERIC(1) NOT NULL,
 PROGRAM_ID		NUMERIC NOT NULL,
 DELETED			NUMERIC(1) NOT NULL,
 PRIMARY KEY(COURSE_ID),
 FOREIGN KEY(PROGRAM_ID) REFERENCES PROGRAM(PROGRAM_ID));
 go

-------------------------------------
-- Table structure for Days
--------------------------------------
IF OBJECT_ID('DAY','U') IS NOT NULL
DROP TABLE DAY;
GO
CREATE TABLE DAY
(
DAY_ID NUMERIC NOT NULL,
DESCRIPTION VARCHAR(50) NOT NULL,
PRIMARY KEY(DAY_ID)
);
GO


-------------------------------------
-- Table structure for CLASSROOM_SCHEDULE
--------------------------------------
IF OBJECT_ID('CLASSROOM_SCHEDULE','U') IS NOT NULL
DROP TABLE CLASSROOM_SCHEDULE;
GO
CREATE TABLE CLASSROOM_SCHEDULE(
CLASSROOM_SCHEDULE_ID NUMERIC NOT NULL,
CLASSROOM_ID          NUMERIC NOT NULL,
PERIOD_ID             NUMERIC NOT NULL,
Course_ID			  NUMERIC NOT NULL,
DAY_ID                NUMERIC NOT NULL,
INITIAL_HOUR		  TIME NOT NULL,
FINAL_HOUR            TIME NOT NULL,
STATE                 NUMERIC(1) NOT  NULL,
ACADEMIC_OFFER_ID   NUMERIC	 NOT NULL,
PRIMARY KEY(CLASSROOM_SCHEDULE_ID,CLASSROOM_ID,PERIOD_ID),
FOREIGN KEY(CLASSROOM_ID)REFERENCES CLASSROOM(CLASSROOM_ID),
FOREIGN KEY(Course_ID) REFERENCES Course(Course_ID),
FOREIGN KEY(PERIOD_ID) REFERENCES PERIOD(PERIOD_ID),
FOREIGN KEY(DAY_ID) REFERENCES DAY (DAY_ID)
);
GO

-------------------------------------
-- Table structure for TEACHER_SCHEDULE
--------------------------------------
IF OBJECT_ID('TEACHER_SCHEDULE','U') IS NOT NULL
DROP TABLE TEACHER_SCHEDULE;
GO
CREATE TABLE TEACHER_SCHEDULE(
TEACHER_SCHEDULE_ID NUMERIC NOT NULL,
TEACHER_ID          NUMERIC NOT NULL,
PERIOD_ID           NUMERIC NOT NULL,
DAY_ID              NUMERIC NOT NULL,
Course_ID			NUMERIC NOT NULL,
INITIAL_HOUR		TIME NOT NULL,
FINAL_HOUR          TIME NOT NULL,
STATE               NUMERIC(1) NOT  NULL,
ACADEMIC_OFFER_ID   NUMERIC	 NOT NULL,
PRIMARY KEY(TEACHER_SCHEDULE_ID,TEACHER_ID,PERIOD_ID),
FOREIGN KEY(TEACHER_ID)REFERENCES TEACHER(TEACHER_ID),
FOREIGN KEY(PERIOD_ID) REFERENCES PERIOD(PERIOD_ID),
FOREIGN KEY(Course_ID) REFERENCES Course(Course_ID),
FOREIGN KEY(DAY_ID) REFERENCES DAY (DAY_ID)
);
GO

-------------------------------------
-- Table structure for ACADEMIC_OFFER
--------------------------------------
IF OBJECT_ID('ACADEMIC_OFFER','U') IS NOT NULL
DROP TABLE ACADEMIC_OFFER;
GO
CREATE TABLE ACADEMIC_OFFER
(
ACADEMIC_OFFER_ID   NUMERIC	 NOT NULL,
PERIOD_ID	        NUMERIC NOT NULL,
PROGRAM_ID			NUMERIC NOT NULL,
COURSE_ID			NUMERIC NOT NULL,
PRICE				MONEY NOT NULL,
CLASSROOM_ID		NUMERIC NOT NULL,
SCHEDULE_ID			NUMERIC NOT NULL,
TEACHER_ID			NUMERIC NOT NULL,
HOURS			    INT NOT NULL,
DELETED			    NUMERIC(1) NOT NULL,
PRIMARY KEY(ACADEMIC_OFFER_ID),
FOREIGN KEY(PERIOD_ID)REFERENCES PERIOD(PERIOD_ID),
FOREIGN KEY(PROGRAM_ID)REFERENCES PROGRAM(PROGRAM_ID),
FOREIGN KEY (COURSE_ID) REFERENCES COURSE(COURSE_ID),
FOREIGN KEY(CLASSROOM_ID)REFERENCES CLASSROOM(CLASSROOM_ID),
FOREIGN KEY(SCHEDULE_ID)REFERENCES SCHEDULE(SCHEDULE_ID),
FOREIGN KEY (TEACHER_ID) REFERENCES TEACHER(TEACHER_ID),
);
GO

-------------------------------------
-- Table structure for Journey
--------------------------------------
IF OBJECT_ID('JOURNEY','U') IS NOT NULL
DROP TABLE JOURNEY;
GO
CREATE TABLE JOURNEY
(
JOURNEY_ID NUMERIC NOT NULL,
DAY_ID  NUMERIC NOT NULL,
START	VARCHAR(12) NOT NULL,
FINISH  VARCHAR(12) NOT NULL,
PRIMARY KEY (JOURNEY_ID),
FOREIGN KEY(DAY_ID)REFERENCES DAY(DAY_ID));
GO

-------------------------------------
-- Table structure for External Designation
--------------------------------------
IF OBJECT_ID('EXTERNAL_DESIGNATION','U') IS NOT NULL
DROP TABLE EXTERNAL_DESIGNATION;
go
CREATE TABLE EXTERNAL_DESIGNATION
(DESIGNATION_ID NUMERIC NOT NULL,
TEACHER_ID		NUMERIC NOT NULL,
LOCATION		VARCHAR(50) NOT NULL,
POSITION		VARCHAR(20) NOT NULL,
HOURS			INT NOT NULL,
INITIAL_DATE    DATETIME NOT NULL,
FINAL_DATE		DATETIME NOT NULL,
PRIMARY KEY(DESIGNATION_ID),
FOREIGN KEY(TEACHER_ID)REFERENCES TEACHER(TEACHER_ID));
GO
-------------------------------------
-- Table union External Designation with journey
--------------------------------------
IF OBJECT_ID('EXT_DESIG_JOURNEY','U') IS NOT NULL
DROP TABLE EXT_DESIG_JOURNEY;
GO
CREATE TABLE EXT_DESIG_JOURNEY
(
EXTERNAL_DESIGNATION_ID NUMERIC	 NOT NULL,
JOURNEY_ID		        NUMERIC NOT NULL,
PRIMARY KEY(EXTERNAL_DESIGNATION_ID,JOURNEY_ID),
FOREIGN KEY(EXTERNAL_DESIGNATION_ID)REFERENCES EXTERNAL_DESIGNATION(DESIGNATION_ID),
FOREIGN KEY(JOURNEY_ID)REFERENCES JOURNEY(JOURNEY_ID)
);
GO

-------------------------------------
-- Table structure for Waiting list 
-------------------------------------- 
IF OBJECT_ID('WAITING_LIST','U')IS NOT NULL
	DROP TABLE WAITING_LIST;
GO
CREATE TABLE WAITING_LIST (
WAITING_LIST_ID  NUMERIC NOT NULL,
ID                VARCHAR(15) NOT NULL,
NAME			  VARCHAR(50) NOT NULL,
SURNAME			  VARCHAR(50) NOT NULL,
PHONE			  VARCHAR(15),
CELLPHONE		  VARCHAR(15) NOT NULL,
MAIL			  VARCHAR(100)NOT NULL,
PERIOD_ID         NUMERIC NOT NULL,
CONTACTED         BIT NOT NULL,
PRIMARY KEY(WAITING_LIST_ID),
FOREIGN KEY(PERIOD_ID)REFERENCES PERIOD(PERIOD_ID));
GO

-------------------------------------
-- Table structure for Tentative_Schedule 
-------------------------------------- 
IF OBJECT_ID('TENTATIVE_SCHEDULE','U')IS NOT NULL
	DROP TABLE TENTATIVE_SCHEDULE;
GO
CREATE TABLE TENTATIVE_SCHEDULE(
TENTATIVE_SHEDULE_ID NUMERIC NOT NULL,
DESCRIPTION          VARCHAR(15) NOT NULL,
SCHEDULE		     VARCHAR(15) NOT NULL,
COURSE_ID		     NUMERIC NOT NULL,
PRIMARY KEY(TENTATIVE_SHEDULE_ID));
GO

-------------------------------------
-- Table structure for Waiting_list_TENTATIVE_SCHEDULE
-------------------------------------- 
IF OBJECT_ID('WAITING_LIST_TENTATIVE_SCHEDULE','U')IS NOT NULL
	DROP TABLE WAITING_LIST_TENTATIVE_SCHEDULE;
GO
CREATE TABLE WAITING_LIST_TENTATIVE_SCHEDULE(
TENTATIVE_SHEDULE_ID NUMERIC NOT NULL,
WAITING_LIST_ID  NUMERIC NOT NULL,
PRIMARY KEY(TENTATIVE_SHEDULE_ID,WAITING_LIST_ID));
GO

-------------------------------------
-- Table structure for Slider
-------------------------------------- 
IF OBJECT_ID('SLIDER','U')IS NOT NULL
	DROP TABLE SLIDER;
GO
CREATE TABLE SLIDER(
SLIDER_ID			NUMERIC NOT NULL,
DESCRIPTION			VARCHAR(100)NOT NULL,
IMAGE				TEXT NOT NULL,
DELETED				NUMERIC(1)NOT NULL,
STATE				NUMERIC(1)NOT NULL,
PRIMARY KEY(SLIDER_ID));
GO

