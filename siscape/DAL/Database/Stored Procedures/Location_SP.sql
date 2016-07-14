USE ProUTN;
GO

CREATE PROCEDURE SP_GETNEXTCODELOCATION
AS
BEGIN
	SELECT MAX(LOCATION_ID) FROM LOCATION;
END;
GO

CREATE PROCEDURE SP_EXISTPLOCATION
@ID NUMERIC
AS
BEGIN
	SELECT COUNT(LOCATION_ID) FROM LOCATION WHERE LOCATION_ID =@ID;
END;
GO

CREATE PROCEDURE SP_INSERTLOCATION
@ID NUMERIC,
@BUILDING VARCHAR(100),
@MODULE VARCHAR(100),
@IDHEADQUARTERS NUMERIC,
@STATE NUMERIC(1)  
AS
BEGIN

INSERT INTO [dbo].[LOCATION]([LOCATION_ID],[BUILDING],[MODULE],[HEADQUARTERS_ID],[STATE], DELETED)
     VALUES (@ID,@BUILDING,@MODULE,@IDHEADQUARTERS,@STATE, 1)
END;
GO

CREATE PROCEDURE SP_MODIFYLOCATION
@ID NUMERIC,
@BUILDING VARCHAR(100),
@MODULE VARCHAR(100),
@IDHEADQUARTERS NUMERIC,
@STATE NUMERIC(1)  
AS 
BEGIN

UPDATE [dbo].[LOCATION]
   SET [BUILDING] = @BUILDING
      ,[MODULE] = @MODULE
      ,[HEADQUARTERS_ID] = @IDHEADQUARTERS
      ,[STATE] = @STATE
 WHERE [LOCATION_ID] = @ID

END;
GO

CREATE PROCEDURE SP_DELETELOCATION
@ID NUMERIC
AS BEGIN
	UPDATE LOCATION
	SET DELETED = 0
	WHERE LOCATION_ID = @ID
END;
GO

CREATE PROCEDURE SP_GETALLLOCATION
AS
BEGIN
SELECT [LOCATION_ID],[BUILDING],[MODULE],L.[HEADQUARTERS_ID],H.NAME,L.[STATE]
  FROM [dbo].[LOCATION] L INNER JOIN HEADQUARTERS H
  ON L.HEADQUARTERS_ID=H.HEADQUARTERS_ID
  WHERE L.DELETED = 1;
END;
GO

CREATE PROCEDURE SP_GETPLOCATION
@ID NUMERIC
AS
BEGIN
SELECT [LOCATION_ID],[BUILDING],[MODULE],L.[HEADQUARTERS_ID],H.NAME,L.[STATE]
  FROM [dbo].[LOCATION] L INNER JOIN HEADQUARTERS H
  ON L.HEADQUARTERS_ID=H.HEADQUARTERS_ID
  WHERE LOCATION_ID = @ID AND
  L.DELETED = 1;
END;
GO

CREATE PROCEDURE SP_GETALLLOCATIONACTIVE
AS
BEGIN
SELECT [LOCATION_ID],[BUILDING],[MODULE],L.[HEADQUARTERS_ID],H.NAME,L.[STATE]
  FROM [dbo].[LOCATION] L INNER JOIN HEADQUARTERS H
  ON L.HEADQUARTERS_ID=H.HEADQUARTERS_ID
  WHERE L.STATE = 1 AND
  L.DELETED = 1;
END;
GO


