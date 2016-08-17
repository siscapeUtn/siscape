USE ProUTN;
GO
/* Store Procedures SCHEDULE */
--to get last code
CREATE PROCEDURE SP_GETNEXTCODESCHEDULE
AS
BEGIN
	SELECT MAX(SCHEDULE_ID) 
	FROM SCHEDULE;
END;
GO

--to check that a exchedule exists
CREATE PROCEDURE SP_EXISTSCHEDULE
@ID NUMERIC
AS
BEGIN
	SELECT COUNT(SCHEDULE_ID) 
	FROM SCHEDULE 
	WHERE SCHEDULE_ID =@ID;
END;
GO

--to insert a schedule
--drop procedure SP_INSERTSCHEDULE
CREATE PROCEDURE SP_INSERTSCHEDULE
@ID NUMERIC,
@PROGRAM_ID NUMERIC,
@DESCRIPTION varchar(50),
@CODDESCRIPTION varchar(10),
@TYPESHEDULE varchar(50),
@STARTTIME datetime,
@ENDTIME datetime,
@STATE NUMERIC(1)  
AS
BEGIN


INSERT INTO [dbo].[SCHEDULE]([SCHEDULE_ID],[PROGRAM_ID],[DESCRIPTION],[CODDESCRIPTION],[TYPESHEDULE],[STARTTIME]
           ,[ENDTIME],[STATE],[DELETED])
     VALUES
           (@ID,@PROGRAM_ID,@DESCRIPTION,@CODDESCRIPTION,@TYPESHEDULE,@STARTTIME,@ENDTIME,@STATE,1);
   
END;
GO

--to modify a schedule
--drop procedure SP_MODIFYSCHEDULE
CREATE PROCEDURE SP_MODIFYSCHEDULE
@ID NUMERIC,
@PROGRAM_ID NUMERIC,
@DESCRIPTION varchar(50),
@CODDESCRIPTION varchar(10),
@TYPESHEDULE varchar(50),
@STARTTIME datetime,
@ENDTIME datetime,
@STATE NUMERIC(1)  
AS
BEGIN

UPDATE [dbo].[SCHEDULE]
   SET [DESCRIPTION] = @DESCRIPTION
	  ,[PROGRAM_ID]=@PROGRAM_ID
      ,[TYPESHEDULE] = @TYPESHEDULE
      ,[STARTTIME] = @STARTTIME
      ,[ENDTIME] = @ENDTIME
	  ,[CODDESCRIPTION]= @CODDESCRIPTION
      ,[STATE] = @STATE
 WHERE [SCHEDULE_ID] = @ID
END;
GO 

-- to delete schedule
CREATE PROCEDURE SP_DELETESCHEDULE
@ID NUMERIC
AS
BEGIN
	UPDATE SCHEDULE
	SET DELETED = 0
	WHERE SCHEDULE_ID = @ID;
END;
GO

--to get all schedule
--drop procedure SP_GETALLSCHEDULE
CREATE PROCEDURE SP_GETALLSCHEDULE
AS
BEGIN

  SELECT S.[SCHEDULE_ID],S.[PROGRAM_ID],S.[DESCRIPTION],S.[CODDESCRIPTION],S.[TYPESHEDULE],S.[STARTTIME],S.[ENDTIME],S.[STATE],P.NAME
  FROM [dbo].[SCHEDULE] S,PROGRAM P
  WHERE S.DELETED = 1 AND S.PROGRAM_ID = P.PROGRAM_ID
  order by S.PROGRAM_ID;
  	
END;
GO
--to get schudele by code
--drop procedure SP_GETSCHEDULE
CREATE PROCEDURE SP_GETSCHEDULE
@ID NUMERIC
AS
BEGIN
	SELECT [SCHEDULE_ID],[PROGRAM_ID],[DESCRIPTION],[CODDESCRIPTION],[TYPESHEDULE],[STARTTIME],[ENDTIME],[STATE]
    FROM [dbo].[SCHEDULE]
	WHERE [SCHEDULE_ID] = @ID AND
	DELETED = 1; 
END;
GO

--to get all SCHEDULE active
--drop procedure SP_GETALLSCHEDULEACTIVE
CREATE PROCEDURE SP_GETALLSCHEDULEACTIVE
AS
BEGIN
	SELECT [SCHEDULE_ID],[PROGRAM_ID],[DESCRIPTION],[CODDESCRIPTION],[TYPESHEDULE],[STARTTIME],[ENDTIME],[STATE]
    FROM [dbo].[SCHEDULE]	
    WHERE STATE = 1 AND
	DELETED = 1; 
END;
GO

--to get all schedule desactive
CREATE PROCEDURE SP_GETALLSCHEDULEDESACTIVE
AS
BEGIN
	SELECT [SCHEDULE_ID],[PROGRAM_ID],[DESCRIPTION],[CODDESCRIPTION],[TYPESHEDULE],[STARTTIME],[ENDTIME],[STATE]
    FROM [dbo].[SCHEDULE]	
    WHERE STATE = 0 AND
	DELETED = 1; 
END;
GO

--to get the schedule by program
--drop procedure SP_GETALLSCHEDULEACTIVEBYPROGRAM
CREATE PROCEDURE SP_GETALLSCHEDULEACTIVEBYPROGRAM
@PROGRAM_ID NUMERIC
AS
BEGIN
	SELECT [SCHEDULE_ID],[PROGRAM_ID],[DESCRIPTION],[CODDESCRIPTION],[TYPESHEDULE],[STARTTIME],[ENDTIME],[STATE]
    FROM [dbo].[SCHEDULE]	
    WHERE STATE = 1 AND [PROGRAM_ID]=@PROGRAM_ID AND
	DELETED = 1
	order by STATE; 
END;
GO

--drop procedure SP_GETALLSCHEDULEBYPROGRAM
CREATE PROCEDURE SP_GETALLSCHEDULEBYPROGRAM
@PROGRAM_ID NUMERIC
AS
BEGIN
SELECT S.[SCHEDULE_ID],S.[PROGRAM_ID],S.[DESCRIPTION],S.[CODDESCRIPTION],S.[TYPESHEDULE],S.[STARTTIME],S.[ENDTIME],S.[STATE],P.NAME
  FROM [dbo].[SCHEDULE] S,PROGRAM P	
    WHERE  S.[PROGRAM_ID]=@PROGRAM_ID AND S.PROGRAM_ID=p.PROGRAM_ID AND
	S.DELETED = 1
	order by STATE; 
END;
GO

--to get schedule active by id
CREATE PROCEDURE SP_GETALLSCHEDULEACTIVEBYPID
@SCHEDULE_ID NUMERIC
AS
BEGIN
	SELECT [SCHEDULE_ID],[PROGRAM_ID],[DESCRIPTION],[CODDESCRIPTION],[TYPESHEDULE],[STARTTIME],[ENDTIME],[STATE],[CODDESCRIPTION]
    FROM [dbo].[SCHEDULE]	
    WHERE STATE = 1 AND [SCHEDULE_ID]=@SCHEDULE_ID AND
	DELETED = 1; 
END;
GO

--report
create PROCEDURE SP_REPORTSCHEDULE
AS
BEGIN
  SELECT distinct CONCAT(TC.NAME,' ',TC.SURNAME),day.DESCRIPTION,ts.INITIAL_HOUR,ts.FINAL_HOUR,SB.DESCRIPTION
  FROM SCHEDULE SC,COURSE SB,TEACHER TC,DAY day, TEACHER_SCHEDULE ts
  WHERE ts.TEACHER_ID = TC.TEACHER_ID AND ts.Course_ID = SB.Course_ID and ts.DAY_ID=day.DAY_ID
END;

--drop procedure SP_REPORTSCHEDULE
--CREATE PROCEDURE SP_REPORTSCHEDULE
--AS
--BEGIN
--  SELECT  CONCAT(TC.NAME,' ',TC.SURNAME), day.DESCRIPTION,ts.INITIAL_HOUR,ts.FINAL_HOUR,SB.DESCRIPTION
--  FROM SCHEDULE SC,COURSE SB,TEACHER TC,DAY day, TEACHER_SCHEDULE ts
--  WHERE ts.TEACHER_ID = TC.TEACHER_ID AND ts.COURSE_ID = SB.COURSE_ID
--END;
--GO