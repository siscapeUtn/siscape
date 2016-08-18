USE ProUTN;
GO

/* ---- Store Procedures ACADEMIC_OFFER ---- */
--to get last code
--drop procedure SP_GETNEXTCODEACADEMIC_OFFER go
CREATE PROCEDURE SP_GETNEXTCODEACADEMIC_OFFER
AS
BEGIN
	SELECT MAX(ACADEMIC_OFFER_ID)
	FROM ACADEMIC_OFFER;
END;
GO

--to check that a SP_EXISTACADEMIC_OFFER exists
--drop procedure SP_EXISTACADEMIC_OFFER go
CREATE PROCEDURE SP_EXISTACADEMIC_OFFER
@ID NUMERIC
AS
BEGIN
	SELECT COUNT(ACADEMIC_OFFER_ID)
	FROM ACADEMIC_OFFER
	WHERE ACADEMIC_OFFER_ID = @ID;
END;
GO

--to insert a ACADEMIC_OFFER
CREATE PROCEDURE SP_INSERTACADEMIC_OFFER
@ID NUMERIC,
@TEACHER_ID NUMERIC,
@PERIOD_ID NUMERIC,
@PROGRAM_ID NUMERIC,
@COURSE_ID NUMERIC,
@PRICE MONEY,
@CLASSROOM_ID NUMERIC,
@SCHEDULE_ID NUMERIC,
@HOURS INT
AS
BEGIN

INSERT INTO [dbo].[ACADEMIC_OFFER]
           ([ACADEMIC_OFFER_ID],[PERIOD_ID],[PROGRAM_ID] ,[COURSE_ID]
           ,[PRICE],[CLASSROOM_ID],[SCHEDULE_ID],[TEACHER_ID],[HOURS],[DELETED])
     VALUES
           (@ID,@PERIOD_ID,@PROGRAM_ID,@COURSE_ID,@PRICE,@CLASSROOM_ID,@SCHEDULE_ID,@TEACHER_ID,@HOURS,0)

END;
GO


--THE GRID VIEW IN ACADEMICOFFEER
--DROP PROCEDURE SP_SELECTALLGRIDVIEW
CREATE PROCEDURE SP_SELECTALLGRIDVIEW
AS
BEGIN
SELECT [ACADEMIC_OFFER_ID],C.DESCRIPTION,T.[NAME],T.SURNAME,SH.DESCRIPTION,
SH.TYPESHEDULE,CL.NUM_ROOM,AO.PRICE,AO.PROGRAM_ID,P.NAME,AO.PERIOD_ID,PE.NAME,AO.HOURS
FROM [dbo].[ACADEMIC_OFFER] AO,COURSE C,TEACHER T,SCHEDULE SH,CLASSROOM CL,PROGRAM P, PERIOD PE
WHERE AO.COURSE_ID=C.COURSE_ID AND AO.TEACHER_ID=T.TEACHER_ID AND AO.PROGRAM_ID= P.PROGRAM_ID AND AO.PERIOD_ID=PE.PERIOD_ID
AND AO.SCHEDULE_ID=SH.SCHEDULE_ID AND AO.CLASSROOM_ID=CL.CLASSROOM_ID AND AO.DELETED=0
ORDER BY AO.PERIOD_ID, AO.PROGRAM_ID
END;
GO

--THE GRID VIEW IN ACADEMICOFFEER BY PROGRAM
--DROP PROCEDURE SP_SELECTPROGRAMGRIDVIEW
CREATE PROCEDURE SP_SELECTPROGRAMGRIDVIEW
@ID NUMERIC,
@PERIOD NUMERIC
AS
BEGIN

SELECT [ACADEMIC_OFFER_ID],C.DESCRIPTION,T.[NAME],T.SURNAME,SH.DESCRIPTION,
SH.TYPESHEDULE,CL.NUM_ROOM,AO.PRICE,AO.PROGRAM_ID,P.NAME,AO.PERIOD_ID,PE.NAME
FROM [dbo].[ACADEMIC_OFFER] AO,COURSE C,TEACHER T,SCHEDULE SH,CLASSROOM CL,PROGRAM P, PERIOD PE
WHERE AO.COURSE_ID=C.COURSE_ID AND AO.TEACHER_ID=T.TEACHER_ID  AND AO.PROGRAM_ID= P.PROGRAM_ID AND AO.PERIOD_ID=PE.PERIOD_ID
AND AO.SCHEDULE_ID=SH.SCHEDULE_ID AND AO.CLASSROOM_ID=CL.CLASSROOM_ID AND AO.DELETED=0 AND AO.PROGRAM_ID= @ID AND AO.PERIOD_ID=@PERIOD

END;
GO

--drop procedure SP_DELETEAcademicOffer
--delete the academic offer
CREATE PROCEDURE SP_DELETEAcademicOffer
@ID NUMERIC
AS
BEGIN
update ACADEMIC_OFFER
set deleted=1
where ACADEMIC_OFFER_ID=@ID;

delete from CLASSROOM_SCHEDULE
where ACADEMIC_OFFER_ID=@ID;

delete from TEACHER_SCHEDULE
where ACADEMIC_OFFER_ID=@ID;


END;
GO