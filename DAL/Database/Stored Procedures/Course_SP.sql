USE ProUTN;
GO

-- To get last code 
CREATE PROCEDURE SP_GETNEXTCODECOURSE
AS
BEGIN
	SELECT MAX(COURSE_ID) FROM COURSE;
END;
GO

-- to check if a COURSE exist
CREATE PROCEDURE SP_EXISTCOURSE
@ID NUMERIC
AS
BEGIN
	SELECT COUNT(COURSE_ID) 
	FROM COURSE 
	WHERE COURSE_ID =@ID;
END;
GO

-- to insert a  COURSE
CREATE PROCEDURE SP_INSERTCOURSE
@ID NUMERIC,
@DESCRIPTION VARCHAR(50),
@STATE NUMERIC(1),
@PROGRAM_ID NUMERIC
AS
BEGIN
	INSERT INTO COURSE (COURSE_ID,DESCRIPTION,STATE,PROGRAM_ID, DELETED) 
	VALUES (@ID,@DESCRIPTION,@STATE, @PROGRAM_ID,1);
END;
GO

--to modify COURSE
CREATE PROCEDURE SP_MODIFYCOURSE
@ID NUMERIC,
@DESCRIPTION VARCHAR(50),
@STATE NUMERIC(1),
@PROGRAM_ID NUMERIC
AS 
BEGIN
	UPDATE COURSE
	SET DESCRIPTION = @DESCRIPTION,
	STATE = @STATE,
	PROGRAM_ID = @PROGRAM_ID
	WHERE COURSE_ID = @ID; 
END;
GO

--to delete de course
CREATE PROCEDURE SP_DELETECOURSE
@ID NUMERIC
AS
BEGIN
	UPDATE COURSE
	SET DELETED = 0
	WHERE COURSE_ID = @ID;
END;
GO

--to get all course
CREATE PROCEDURE SP_GETALLCOURSE
AS
BEGIN
    SELECT COURSE_ID,DESCRIPTION,STATE,PROGRAM_ID
    FROM COURSE
	WHERE DELETED = 1;
END;
GO

--to get all periodType by state is active
CREATE PROCEDURE SP_GETALLACTIVECOURSE
AS
BEGIN
    SELECT COURSE_ID,DESCRIPTION,STATE,PROGRAM_ID
    FROM COURSE
	WHERE STATE = 1 AND
	DELETED = 1;
END;
GO

--to get a periodType by code
CREATE PROCEDURE SP_GETCOURSE
@CODE NUMERIC
AS
BEGIN
    SELECT COURSE_ID,DESCRIPTION,STATE,PROGRAM_ID
    FROM COURSE
    WHERE COURSE_ID = @CODE
	AND DELETED = 1;
END;
GO

--to get all COURSE by program  and the state is active
CREATE PROCEDURE SP_GETALLACTIVECOURSEPROGRAM
@PROGRAM_ID NUMERIC
AS
BEGIN
    SELECT COURSE_ID,DESCRIPTION,STATE,PROGRAM_ID
    FROM COURSE
	WHERE STATE = 1 AND PROGRAM_ID=@PROGRAM_ID AND
	DELETED = 1;
END;
GO

-- TO GET ALL ACTIVE SUBJECT BY PROGRAM
CREATE PROCEDURE SP_GET_ALL_ACTIVE_COURSE_BY_PROGRAM
@PROGRAM_ID NUMERIC
AS
BEGIN
	SELECT S.COURSE_ID, S.DESCRIPTION, S.STATE, S.PROGRAM_ID, P.NAME 
	FROM COURSE S INNER JOIN PROGRAM P
	ON S.PROGRAM_ID = P.PROGRAM_ID
	WHERE S.STATE = 1 AND S.PROGRAM_ID=@PROGRAM_ID;
END;
GO
