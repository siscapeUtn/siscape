USE ProUTN;
<<<<<<< HEAD
GO
=======
go
>>>>>>> fcd3008a6ffed83068e47a52fc9af0733a248776

CREATE PROCEDURE SP_REPORT_TEACHER
@PERIOD_ID NUMERIC
AS
BEGIN
	SELECT CONCAT(T.NAME,' ',T.SURNAME) AS 'TEACHER_NAME', P.NAME
	FROM TEACHER_SCHEDULE TS, TEACHER T, PERIOD P
	WHERE TS.PERIOD_ID = @PERIOD_ID AND
	TS.TEACHER_ID = T.TEACHER_ID
END;
GO

CREATE PROCEDURE SP_REPORT_CLASSROOM
@PERIOD_ID NUMERIC
AS
BEGIN
	SELECT DISTINCT(C.NUM_ROOM) 
	FROM ACADEMIC_OFFER A, CLASSROOM C
	WHERE A.CLASSROOM_ID = C.CLASSROOM_ID AND
	A.PERIOD_ID	= @PERIOD_ID
END;
<<<<<<< HEAD
GO;

CREATE PROCEDURE SP_REPORT_ACADEMIC_OFFER
@PERIOD_ID NUMERIC
AS
BEGIN
	SELECT ACADEMIC_OFFER_ID,C.DESCRIPTION,T.NAME,T.SURNAME,SH.DESCRIPTION,
	SH.TYPESHEDULE,CL.NUM_ROOM,AO.PRICE
	FROM ACADEMIC_OFFER AO,COURSE C,TEACHER T,SCHEDULE SH,CLASSROOM CL, PERIOD P
	WHERE AO.COURSE_ID=C.COURSE_ID AND AO.TEACHER_ID=T.TEACHER_ID 
	AND AO.SCHEDULE_ID=SH.SCHEDULE_ID AND AO.CLASSROOM_ID=CL.CLASSROOM_ID AND AO.DELETED=0
	AND P.PERIOD_ID = @PERIOD_ID
	ORDER BY AO.PROGRAM_ID 
END;
GO;

CREATE PROCEDURE SP_REPORT_EXTERNAL_DESIGNATION
AS
BEGIN
	SELECT E.DESIGNATION_ID
		  ,CONCAT(T.NAME,' ',T.SURNAME) AS NAME
		  ,T.ID
		  ,E.POSITION
		  ,E.LOCATION
		  ,E.HOURS
		  ,E.INITIAL_DATE
		  ,E.FINAL_DATE
	  FROM EXTERNAL_DESIGNATION E, TEACHER T 
	  WHERE T.TEACHER_ID = E.TEACHER_ID
END;
GO

CREATE PROCEDURE SP_REPORT_WAITING_LIST
@PERIOD_ID NUMERIC
AS
BEGIN
SELECT WAITING_LIST_ID
      ,ID
      ,NAME
      ,SURNAME
      ,PHONE
      ,CELLPHONE
      ,MAIL
      ,PERIOD_ID
      ,CONTACTED
  FROM WAITING_LIST
  WHERE PERIOD_ID = @PERIOD_ID
END;
GO
=======
GO
>>>>>>> fcd3008a6ffed83068e47a52fc9af0733a248776
