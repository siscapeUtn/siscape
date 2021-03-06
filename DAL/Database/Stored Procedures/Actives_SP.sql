USE ProUTN;
GO

/* ---- Store Procedures Actives ---- */
--to get last code
--drop procedure SP_GETNEXTCODEACTIVES go
CREATE PROCEDURE SP_GETNEXTCODEACTIVES
AS
BEGIN
	SELECT MAX(Actives_ID)
	FROM Actives;
END;
GO


--to check that a SP_EXISTACTIVES exists
--drop procedure SP_EXISTACTIVES go
CREATE PROCEDURE SP_EXISTACTIVES
@ID NUMERIC
AS
BEGIN
	SELECT COUNT(Actives_ID)
	FROM Actives
	WHERE Actives_ID = @ID;
END;
GO

--to check that a SP_EXISTACTIVESALPHANUMERIC exists
--drop procedure SP_EXISTACTIVESALPHANUMERIC go
CREATE PROCEDURE SP_EXISTACTIVESALPHANUMERIC
@ID VARCHAR(50)
AS
BEGIN
	SELECT COUNT(Actives_ID)
	FROM Actives
	WHERE CODEAPLHANUMERIC = @ID;
END;
GO

--to insert a Actives
--drop procedure SP_INSERTACTIVES 
CREATE PROCEDURE SP_INSERTACTIVES
@ID NUMERIC,
@STATEACTIVE NUMERIC,
@CODEALPHANUMERIC VARCHAR(50),
@DESCRIPTION VARCHAR(50),
@CLASSROOM_ID NUMERIC,
@PROGRAM_ID NUMERIC
AS
BEGIN

	INSERT INTO [dbo].[Actives]
           ([Actives_ID],[ActivesSatus_ID],[CODEAPLHANUMERIC],[DESCRIPTION],[CLASSROOM_ID],[PROGRAM_ID],[DELETED])
     VALUES(@ID,@STATEACTIVE,@CODEALPHANUMERIC,@DESCRIPTION,@CLASSROOM_ID,@PROGRAM_ID,1)

END;
GO

--to modify a Actives
--drop procedure SP_MODIFYACTIVES
CREATE PROCEDURE SP_MODIFYACTIVES
@ID NUMERIC,
@STATEACTIVE NUMERIC,
@DESCRIPTION VARCHAR(50),
@CLASSROOM_ID NUMERIC,
@PROGRAM_ID NUMERIC 
AS
BEGIN

UPDATE [dbo].[Actives]
   SET [ActivesSatus_ID] = @STATEACTIVE
      ,[DESCRIPTION] =  @DESCRIPTION
      ,[CLASSROOM_ID] = @CLASSROOM_ID
      ,[PROGRAM_ID] = @PROGRAM_ID
 WHERE [Actives_ID] = @ID

END;
GO 

-- to delete Actives
-- DROP PROCEDURE SP_DELETEACTIVES
CREATE PROCEDURE SP_DELETEACTIVES
@CODE NUMERIC
AS
BEGIN
	UPDATE Actives
	SET DELETED = 0
	WHERE Actives_ID = @CODE;
END;
GO


--to get all Actives
--drop procedure SP_GETALLACTIVES
CREATE PROCEDURE SP_GETALLACTIVES
AS
BEGIN

  SELECT A.Actives_ID,A.ActivesSatus_ID,ST.DESCRIPTION AS STATUS,A.CODEAPLHANUMERIC,A.DESCRIPTION,A.CLASSROOM_ID,C.NUM_ROOM,A.PROGRAM_ID,P.NAME
  FROM [dbo].[ACTIVES] A,PROGRAM P, CLASSROOM C, ActivesStatus ST
  WHERE A.DELETED = 1 AND A.PROGRAM_ID = P.PROGRAM_ID AND A.CLASSROOM_ID=C.CLASSROOM_ID AND A.ActivesSatus_ID=ST.ActivesSatus_ID
  order by A.PROGRAM_ID,A.CLASSROOM_ID;
  	
END;
GO

--to get all Actives by program
--drop procedure SP_GETALLACTIVESBYPROGRAM
CREATE PROCEDURE SP_GETALLACTIVESBYPROGRAM
@PROGRAM_ID NUMERIC
AS
BEGIN

  SELECT A.Actives_ID,A.ActivesSatus_ID,ST.DESCRIPTION AS STATUS,A.CODEAPLHANUMERIC,A.DESCRIPTION,A.CLASSROOM_ID,C.NUM_ROOM,A.PROGRAM_ID,P.NAME
  FROM [dbo].[ACTIVES] A,PROGRAM P, CLASSROOM C, ActivesStatus ST
  WHERE A.DELETED = 1 AND A.PROGRAM_ID = @PROGRAM_ID AND P.PROGRAM_ID=@PROGRAM_ID AND A.CLASSROOM_ID=C.CLASSROOM_ID AND A.ActivesSatus_ID=ST.ActivesSatus_ID
  order by A.PROGRAM_ID,A.CLASSROOM_ID;
  	
END;
GO

--to get all Actives by program
--drop procedure SP_GETACTIVE
CREATE PROCEDURE SP_GETACTIVE
@ID NUMERIC
AS
BEGIN

  SELECT A.Actives_ID,A.ActivesSatus_ID,ST.DESCRIPTION AS STATUS,A.CODEAPLHANUMERIC,A.DESCRIPTION,A.CLASSROOM_ID,C.NUM_ROOM,A.PROGRAM_ID,P.NAME
  FROM [dbo].[ACTIVES] A,PROGRAM P, CLASSROOM C, ActivesStatus ST
  WHERE A.DELETED = 1 AND A.PROGRAM_ID = P.PROGRAM_ID AND A.CLASSROOM_ID=C.CLASSROOM_ID AND A.ActivesSatus_ID=ST.ActivesSatus_ID
  AND A.Actives_ID=@ID
  order by A.PROGRAM_ID,A.CLASSROOM_ID;
  	
END;
GO

