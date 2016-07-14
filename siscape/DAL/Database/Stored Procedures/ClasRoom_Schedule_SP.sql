USE ProUTN;
GO
/* ---- Store Procedures CLASSROOM_SCHEDULE ---- */
--to get last code
CREATE PROCEDURE SP_GETNEXTCODECLASSROOM_SCHEDULE
AS
BEGIN
	SELECT MAX(CLASSROOM_SCHEDULE_ID) 
	FROM CLASSROOM_SCHEDULE;
END;
GO

--to insert a CLASSROOM_SCHEDULE
-- drop procedure SP_INSERTCLASSROOM_SCHEDULE
CREATE PROCEDURE SP_INSERTCLASSROOM_SCHEDULE
@CLASSROOM_ID NUMERIC,
@CLASSROOM_SCHEDULE_ID NUMERIC,
@PERIOD_ID NUMERIC,
@Course_ID NUMERIC,
@DIA_ID NUMERIC,
@INITIAL_HOUR TIME,
@FINAL_HOUR TIME,
@ACADEMICOFFER_ID   NUMERIC
AS
BEGIN

INSERT INTO [dbo].[CLASSROOM_SCHEDULE]
           ([CLASSROOM_SCHEDULE_ID],[CLASSROOM_ID],[PERIOD_ID]
           ,[DAY_ID] ,[INITIAL_HOUR],[FINAL_HOUR],[STATE],[Course_ID],[ACADEMIC_OFFER_ID])
     VALUES
           (@CLASSROOM_SCHEDULE_ID,@CLASSROOM_ID,@PERIOD_ID,@DIA_ID,@INITIAL_HOUR,@FINAL_HOUR,1,@Course_ID,@ACADEMICOFFER_ID)

END;
GO

--select clasroom by schedule
--drop procedure SP_CLASSROOMBYSCHEDULE
create procedure SP_CLASSROOMBYSCHEDULE
@period numeric,
@program numeric,
@inicialHour time,
@finalhour time,
@day1 numeric,
@day2 numeric,
@day3 numeric,
@day4 numeric,
@day5 numeric,
@day6 numeric,
@day7 numeric
as
begin 
SELECT [CLASSROOM_ID],[NUM_ROOM],CT.DESCRIPTION,CL.[LOCATION_ID],LT.BUILDING, LT.MODULE,HQ.NAME,CL.PROGRAM_ID
FROM CLASSROOM CL, CLASSROOMSTYPE CT,LOCATION LT, HEADQUARTERS HQ
WHERE CL.CLASSROOMSTYPE_ID=CT.CLASSROOMSTYPE_ID AND CL.LOCATION_ID=LT.LOCATION_ID AND 
LT.HEADQUARTERS_ID=HQ.HEADQUARTERS_ID AND CL.STATE= 1 AND CL.PROGRAM_ID=@program AND CL.CLASSROOM_ID not in
 (SELECT c.CLASSROOM_ID FROM CLASSROOM C, CLASSROOM_SCHEDULE CS 
 WHERE C.CLASSROOM_ID=CS.CLASSROOM_ID AND CS.PERIOD_ID=@period and (CS.DAY_ID=@day1 or CS.DAY_ID=@day2 or CS.DAY_ID=@day3 or CS.DAY_ID=@day4 or CS.DAY_ID=@day5
  or CS.DAY_ID=@day6 or CS.DAY_ID=@day7) AND cs.INITIAL_HOUR>=@inicialHour and cs.FINAL_HOUR<=@finalhour or cs.FINAL_HOUR>=@inicialHour or cs.INITIAL_HOUR<= @inicialHour)
end;
GO


CREATE PROCEDURE SP_REPORTCLASSROOM
AS
BEGIN
	SELECT PD.NAME, CLM.NUM_ROOM,DAY.DESCRIPTION, CSC.INITIAL_HOUR,CSC.FINAL_HOUR
	FROM CLASSROOM CLM, CLASSROOM_SCHEDULE CSC, DAY DAY,  PERIOD PD
	WHERE CSC.CLASSROOM_ID = CLM.CLASSROOM_ID AND CSC.DAY_ID = DAY.DAY_ID AND PD.PERIOD_ID = CSC.PERIOD_ID
END;
GO

--drop procedure SP_REPORTSCHEDULE
CREATE PROCEDURE SP_REPORTSCHEDULE
AS
BEGIN
  SELECT  CONCAT(TC.NAME,' ',TC.SURNAME), day.DESCRIPTION,ts.INITIAL_HOUR,ts.FINAL_HOUR,SB.DESCRIPTION
  FROM SCHEDULE SC,COURSE SB,TEACHER TC,DAY day, TEACHER_SCHEDULE ts
  WHERE ts.TEACHER_ID = TC.TEACHER_ID AND ts.COURSE_ID = SB.COURSE_ID
END;
GO

  