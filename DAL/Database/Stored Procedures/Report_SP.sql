USE ProUTN;
GO

CREATE PROCEDURE SP_REPORT_TEACHER
@PERIOD_ID NUMERIC
AS
BEGIN
	
	SELECT T.NAME, T.SURNAME, C.DESCRIPTION, S.DESCRIPTION AS "DAYS", S.STARTTIME, S.ENDTIME, A.HOURS, T.STATE
	FROM TEACHER T, PERIOD P, COURSE C, ACADEMIC_OFFER A, SCHEDULE S
	WHERE A.TEACHER_ID = T.TEACHER_ID AND 
	A.SCHEDULE_ID = S.SCHEDULE_ID AND
	A.PERIOD_ID = P.PERIOD_ID AND
	A.SCHEDULE_ID = S.SCHEDULE_ID AND
	A.COURSE_ID = C.COURSE_ID AND
	P.PERIOD_ID = @PERIOD_ID AND
	A.DELETED = 0;

END;
GO

CREATE PROCEDURE SP_REPORT_CLASSROOM
@PERIOD_ID NUMERIC
AS
BEGIN
	SELECT C.CLASSROOM_ID,C.NUM_ROOM,C.SIZE,C.PROGRAM_ID, P.NAME,C.CLASSROOMSTYPE_ID, CT.DESCRIPTION,C.LOCATION_ID,l.HEADQUARTERS_ID,h.NAME,L.BUILDING,L.MODULE ,C.STATE 
	FROM CLASSROOM C INNER JOIN PROGRAM P
	ON C.PROGRAM_ID = P.PROGRAM_ID
	INNER JOIN CLASSROOMSTYPE CT
	ON C.CLASSROOMSTYPE_ID = CT.CLASSROOMSTYPE_ID
	INNER JOIN LOCATION l
	ON C.LOCATION_ID=L.LOCATION_ID
	INNER JOIN HEADQUARTERS h
	ON l.HEADQUARTERS_ID=h.HEADQUARTERS_ID
	INNER JOIN ACADEMIC_OFFER A
	ON C.CLASSROOM_ID = A.CLASSROOM_ID
	INNER JOIN PERIOD K
	ON A.PERIOD_ID = K.PERIOD_ID
	WHERE C.DELETED = 1 AND
	K.PERIOD_ID = @PERIOD_ID;
END;
GO

CREATE PROCEDURE SP_REPORT_ACADEMIC_OFFER
@PERIOD_ID NUMERIC
AS
BEGIN
	SELECT ACADEMIC_OFFER_ID, C.DESCRIPTION, T.NAME, T.SURNAME, SH.DESCRIPTION,
	SH.TYPESHEDULE, CL.NUM_ROOM, AO.PRICE, P.NAME, SH.STARTTIME, SH.ENDTIME
	FROM ACADEMIC_OFFER AO,COURSE C,TEACHER T,SCHEDULE SH,CLASSROOM CL, PERIOD P
	WHERE AO.COURSE_ID=C.COURSE_ID AND 
	AO.TEACHER_ID=T.TEACHER_ID 
	AND AO.SCHEDULE_ID=SH.SCHEDULE_ID AND 
	AO.CLASSROOM_ID=CL.CLASSROOM_ID AND 
	AO.PERIOD_ID = P.PERIOD_ID AND
	AO.DELETED = 0
	AND P.PERIOD_ID = @PERIOD_ID
	ORDER BY AO.PROGRAM_ID 
END;
GO

CREATE PROCEDURE SP_REPORT_WAITING_LIST
@PERIOD_ID NUMERIC,
@IS_CONTACT NUMERIC
AS
BEGIN
<<<<<<< HEAD
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
=======
	
	SELECT W.NAME, W.SURNAME, W.PHONE, W.CELLPHONE, W.MAIL, C.DESCRIPTION, W.DAY
	FROM WAITING_LIST W, PERIOD P, COURSE C
	WHERE W.COURSE_ID = C.COURSE_ID AND
	W.PERIOD_ID = P.PERIOD_ID AND
	P.PERIOD_ID = @PERIOD_ID AND
	W.CONTACTED = @IS_CONTACT

END;
GO
>>>>>>> e56360761499c06364ae2d1600759e934738ec03
