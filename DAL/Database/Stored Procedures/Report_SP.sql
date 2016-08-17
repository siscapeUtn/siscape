USE ProUTN;

CREATE PROCEDURE SP_REPORT_TEACHER
@PERIOD_ID NUMERIC
AS
BEGIN
	SELECT T.NAME + ' ' + T.SURNAME AS 'TEACHER_NAME', P.NAME
	FROM TEACHER_SCHEDULE TS, TEACHER T, PERIOD P
	WHERE TS.PERIOD_ID = @PERIOD_ID AND
	TS.TEACHER_ID = T.TEACHER_ID
END;
GO;

CREATE PROCEDURE SP_REPORT_ClASSROOM
@PERIOD_ID NUMERIC
AS
BEGIN
	SELECT C.NUM_ROOM
	FROM ACADEMIC_OFFER A, CLASSROOM C
	WHERE A.CLASSROOM_ID = C.CLASSROOM_ID
END;
GO;