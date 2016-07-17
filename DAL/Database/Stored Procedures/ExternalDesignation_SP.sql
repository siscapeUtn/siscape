USE ProUTN;
GO
/* ---- Store Procedures EXTERNAL_DESIGNATION ---- */
--TO GET LAST CODE
CREATE PROCEDURE SP_GETNEXTCODEEXTERNAL_DESIGNATION
AS
BEGIN
	SELECT MAX(DESIGNATION_ID)
	FROM EXTERNAL_DESIGNATION;
END;
GO

--CHECK AN EXTERNAL_DESIGNATION EXISTS
CREATE PROCEDURE SP_EXISTEXTERNAL_DESIGNATION
@ID NUMERIC
AS
BEGIN
	SELECT COUNT(DESIGNATION_ID)
	FROM EXTERNAL_DESIGNATION
	WHERE DESIGNATION_ID = @ID;
END;
GO

--INSERT A EXTERNAL_DESIGNATION
CREATE PROCEDURE SP_INSERTEXTERNAL_DESIGNATION
@ID NUMERIC,
@TEACHER_ID NUMERIC,
@LOCATION	VARCHAR(50),
@POSITION	VARCHAR(20),
@HOURS		INT,
@INITIAL_DATE DATETIME,
@FINAL_DATE	 DATETIME 
AS
BEGIN

INSERT INTO [dbo].[EXTERNAL_DESIGNATION]
           ([DESIGNATION_ID],[TEACHER_ID],[LOCATION],[POSITION],[HOURS],[INITIAL_DATE],[FINAL_DATE])
     VALUES (@ID,@TEACHER_ID,@LOCATION,@POSITION,@HOURS,@INITIAL_DATE,@FINAL_DATE)
END;
GO

--MODIFY EXTERNALDESIGNATION
CREATE PROCEDURE SP_MODIFY_EXTERNAL_DESIGNATION
@ID NUMERIC,
@TEACHER_ID NUMERIC,
@LOCATION	VARCHAR(50),
@POSITION	VARCHAR(20),
@HOURS		INT,
@INITIAL_DATE DATETIME,
@FINAL_DATE	 DATETIME 
AS
BEGIN
UPDATE [dbo].[EXTERNAL_DESIGNATION]
   SET [TEACHER_ID] =@TEACHER_ID
      ,[LOCATION] = @LOCATION
      ,[POSITION] = @POSITION
      ,[HOURS] = @HOURS
      ,[INITIAL_DATE] = @INITIAL_DATE
      ,[FINAL_DATE] = @FINAL_DATE
 WHERE [DESIGNATION_ID] = @ID
END;
GO

--GET EXTERNAL DESIGNATION BY TEACHER
CREATE PROCEDURE SP_GETALLEXTERNAL_DESIGNATION
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
	  FROM EXTERNAL_DESIGNATION E, TEACHER T WHERE T.TEACHER_ID = E.TEACHER_ID
END;
GO


--GET EXTERNAL DESIGNATION BY TEACHER
CREATE PROCEDURE SP_EXTERNAL_DESIGNATION_BY_TEACHER
@ID NUMERIC 
AS
BEGIN
	SELECT E.DESIGNATION_ID
		  ,CONCAT(T.NAME,' ',T.SURNAME) AS NAME
		  ,E.LOCATION
		  ,E.POSITION
		  ,E.HOURS
		  ,E.INITIAL_DATE
		  ,E.FINAL_DATE
	  FROM EXTERNAL_DESIGNATION E, TEACHER T WHERE T.TEACHER_ID = E.TEACHER_ID AND E.TEACHER_ID = @ID ; 
END;
GO

--EXTERNAL BY CODE
create procedure SP_EXTERNAL_DESIGNATION_BY_CODE
@ID NUMERIC 
AS
BEGIN
SELECT [DESIGNATION_ID],[TEACHER_ID],[LOCATION]
      ,[POSITION],[HOURS],[INITIAL_DATE],[FINAL_DATE]
  FROM [dbo].[EXTERNAL_DESIGNATION]
  WHERE DESIGNATION_ID=@ID
END
GO

--DROP PROCEDURE SP_EXTERNAL_DESIGNATION_BY_CODE_JOURNEY

create procedure SP_EXTERNAL_DESIGNATION_BY_CODE_JOURNEY
@ID NUMERIC 
AS
BEGIN

SELECT J.JOURNEY_ID,J.DAY_ID,D.DESCRIPTION,J.START,J.FINISH
FROM EXTERNAL_DESIGNATION E, EXT_DESIG_JOURNEY EX, JOURNEY J, DAY D
WHERE EX.EXTERNAL_DESIGNATION_ID =@ID AND
EX.JOURNEY_ID = J.JOURNEY_ID AND
E.DESIGNATION_ID = EX.EXTERNAL_DESIGNATION_ID AND
j.DAY_ID=d.DAY_ID
END
GO


CREATE PROCEDURE SP_DELETEJOURNEYSBYID
@ID NUMERIC 
AS
BEGIN

DELETE FROM EXT_DESIG_JOURNEY
WHERE EXTERNAL_DESIGNATION_ID = @ID

END
GO

--DROP TRIGGER DELETEJOURNEY
CREATE TRIGGER DELETEJOURNEY
ON EXT_DESIG_JOURNEY
AFTER DELETE	
AS
BEGIN
  DELETE FROM JOURNEY
  WHERE JOURNEY_ID NOT IN 
   (
    SELECT JOURNEY_ID FROM EXT_DESIG_JOURNEY
   );
END
GO





