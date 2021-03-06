USE ProUTN;
GO
--to insert a periodType
CREATE PROCEDURE SP_INSERTPERIODTYPE
	@CODE NUMERIC,
	@DESCRIPTION VARCHAR(30),
	@STATE NUMERIC(1)
AS 
BEGIN
	INSERT INTO PERIODTYPE(PERIODTYPE_ID,DESCRIPTION,STATE, DELETED)
	VALUES(@CODE,@DESCRIPTION, @STATE, 1)
END;
GO

--to get last code
CREATE PROCEDURE SP_GETNEXTCODEPERIODTYPE
AS
BEGIN
	SELECT MAX(PERIODTYPE_ID) FROM PERIODTYPE;
END;
GO

--to modify periodType
CREATE PROCEDURE SP_MODIFYPERIODTYPE
@CODE NUMERIC,
@DESCRIPTION VARCHAR(30),
@STATE NUMERIC(1)
AS 
BEGIN
	UPDATE PERIODTYPE
	SET DESCRIPTION = @DESCRIPTION,
	STATE = @STATE
	WHERE PERIODTYPE_ID = @CODE; 
END;
GO

CREATE PROCEDURE SP_DELETEPERIODTYPE
@ID NUMERIC
AS
BEGIN
	UPDATE PERIODTYPE
	SET DELETED = 0
	WHERE PERIODTYPE_ID = @ID;
END;
GO

--to get all periodType
CREATE PROCEDURE SP_GETALLPERIODTYPE
AS
BEGIN
	SELECT PERIODTYPE_ID,DESCRIPTION,STATE 
	FROM PERIODTYPE
	WHERE DELETED = 1;
END;
GO

--to get all periodType by state is active
CREATE PROCEDURE SP_GETALLACTIVEPERIODTYPE
AS
BEGIN
	SELECT PERIODTYPE_ID,DESCRIPTION,STATE 
	FROM PERIODTYPE
	WHERE STATE = 1 AND
	DELETED = 1;
END;
GO

--to get a periodType by code
CREATE PROCEDURE SP_GETPERIODTYPE
@CODE NUMERIC
AS
BEGIN
	SELECT PERIODTYPE_ID ,DESCRIPTION,STATE 
	FROM PERIODTYPE WHERE PERIODTYPE_ID = @CODE
	AND DELETED = 1;
END;
GO

--to check that a periodType exists
CREATE PROCEDURE SP_EXISTSPERIODTYPE
@CODE NUMERIC
AS
BEGIN
	SELECT COUNT(PERIODTYPE_ID) FROM PERIODTYPE 
	WHERE PERIODTYPE_ID =@CODE;
END;
go
--end periodType procedures