USE ProUTN;
GO
/* ---- Store Procedures Program ---- */
--to get last code
CREATE PROCEDURE SP_GETNEXTCODE_SLIDER
AS
BEGIN
	SELECT MAX(SLIDER_ID) 
	FROM SLIDER;
END;
GO

--to check that a program exists
--CREATE PROCEDURE SP_EXISTSLIDER
--@ID NUMERIC
--AS
--BEGIN
--	SELECT COUNT(SLIDER_ID) 
--	FROM SLIDER 
--	WHERE SLIDER_ID =@ID;
--END;
--GO

--to insert a program
CREATE PROCEDURE SP_INSERT_SLIDER
@ID NUMERIC,
@NAME VARCHAR(50),
@IMAGE TEXT
AS
BEGIN
	INSERT INTO SLIDER (SLIDER_ID, DESCRIPTION, IMAGE) 
	VALUES (@ID,@NAME,@IMAGE);
END;
GO

--to modify program
--CREATE PROCEDURE SP_MODIFYSLIDER
--@ID NUMERIC,
--@NAME VARCHAR(50),
--@IMAGE TEXT, 
--AS 
--BEGIN
--	UPDATE SLIDER
--	SET DESCRIPTION = @NAME,
--	IMAGE = @IMAGE,
--	WHERE SLIDER_ID = @ID; 
--END;
--GO

-- to delete program
CREATE PROCEDURE SP_DELETESLIDER
@ID NUMERIC
AS
BEGIN
	DELETE SLIDER
	WHERE SLIDER_ID = @ID;
END;
GO

--to get all program
CREATE PROCEDURE SP_GETALLACTIVE_SLIDER
AS
BEGIN
	SELECT SLIDER_ID, DESCRIPTION, IMAGE
	FROM SLIDER
END;
GO

--to get all program
CREATE PROCEDURE SP_GETALL_SLIDER
AS
BEGIN
	SELECT SLIDER_ID,DESCRIPTION, IMAGE
	FROM SLIDER
END;
GO

-- to get specific slider
CREATE PROCEDURE SP_GET_SLIDER
@ID NUMERIC
AS
BEGIN
	SELECT SLIDER_ID,DESCRIPTION, IMAGE
	FROM SLIDER
	WHERE SLIDER_ID = @ID;
END;
GO