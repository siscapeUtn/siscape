
USE ProUTN;
GO

--to get last code
CREATE PROCEDURE SP_GETNEXTCODEUSERSYSTEM
AS
BEGIN
	SELECT MAX(USERSYSTEM_ID) 
	FROM USERSYSTEM;
END;
GO

--To verify if exist the user
CREATE PROCEDURE SP_EXISTUSERSYSTEM
@ID NUMERIC
AS
BEGIN
	SELECT COUNT(USERSYSTEM_ID) 
	FROM USERSYSTEM 
	WHERE USERSYSTEM_ID =@ID;
END;
GO

--insert the usersystem
--drop procedure SP_INSERTUSERSYSTEM
CREATE PROCEDURE SP_INSERTUSERSYSTEM
	@USERSYSTEM_ID NUMERIC,
	@ID VARCHAR(15),
	@NAME VARCHAR(50),
	@SURNAME VARCHAR(50),
	@PHONE VARCHAR(15),
	@CELLPHONE VARCHAR(15),
	@MAIL VARCHAR(100),
	@PASSWORD VARCHAR(100),
	@ROLE_ID NUMERIC,
	@PROGRAM_ID NUMERIC=0,
	@STATE NUMERIC(1)
AS 
BEGIN
	if(@PROGRAM_ID=0)
	set @PROGRAM_ID=null
	
		INSERT INTO USERSYSTEM(USERSYSTEM_ID, ID, NAME, SURNAME, PHONE, CELLPHONE, MAIL,PASSWORD,Set_Password,PROGRAM_ID,ROLE_ID, STATE, DELETED)
	VALUES( @USERSYSTEM_ID, @ID, @NAME, @SURNAME, @PHONE, @CELLPHONE, @MAIL, ENCRYPTBYPASSPHRASE('password', @PASSWORD),0,@PROGRAM_ID,@ROLE_ID,@STATE, 1 );
END;
GO

--modify user
--drop procedure SP_MODIFYUSERSYSTEM
CREATE PROCEDURE SP_MODIFYUSERSYSTEM
	@USERSYSTEM_ID NUMERIC,
	@ID VARCHAR(15),
	@NAME VARCHAR(50),
	@SURNAME VARCHAR(50),
	@PHONE VARCHAR(15),
	@CELLPHONE VARCHAR(15),
	@MAIL VARCHAR(100),
	@PASSWORD VARCHAR(100),
	@ROLE_ID NUMERIC,
	@PROGRAM_ID NUMERIC=null,
	@STATE NUMERIC(1)
AS 
BEGIN
	UPDATE USERSYSTEM
	SET ID = @ID,
	NAME = @NAME,
	SURNAME = @SURNAME,
	PHONE = @PHONE,
	CELLPHONE = @CELLPHONE,
	MAIL = @MAIL,
	PASSWORD= ENCRYPTBYPASSPHRASE('password', @PASSWORD),
	ROLE_ID=@ROLE_ID,
	PROGRAM_ID=@PROGRAM_ID,
	STATE = @STATE
	WHERE USERSYSTEM_ID = @USERSYSTEM_ID; 
END;
GO

CREATE PROCEDURE SP_DELETEUSER
@USERSYSTEM_ID NUMERIC
AS
BEGIN
	UPDATE USERSYSTEM
	SET DELETED = 0
	WHERE USERSYSTEM_ID = @USERSYSTEM_ID; 
END;
GO


CREATE PROCEDURE SP_GETALLUSERSYTEM
AS
BEGIN


SELECT US.USERSYSTEM_ID,US.ID,US.NAME,US.SURNAME,US.PROGRAM_ID,P.NAME AS PROGRAM,US.PHONE
      ,US.CELLPHONE,US.MAIL,US.ROLE_ID,R.DESCRIPTION,US.STATE
       FROM USERSYSTEM US, PROGRAM P,ROLE R
	   WHERE US.DELETED=1 AND US.PROGRAM_ID=P.PROGRAM_ID AND US.ROLE_ID=R.ROLE_ID

END;
GO
--drop procedure SP_GETUSERSYSTEM
CREATE PROCEDURE SP_GETUSERSYSTEM
@ID NUMERIC
AS
BEGIN			
	   SELECT US.USERSYSTEM_ID,US.ID,US.NAME,US.SURNAME,US.PROGRAM_ID,P.NAME AS PROGRAM,US.PHONE
      ,US.CELLPHONE,US.MAIL,US.ROLE_ID,R.DESCRIPTION,US.STATE
       FROM USERSYSTEM US, PROGRAM P,ROLE R
	   WHERE US.DELETED=1 AND US.PROGRAM_ID=P.PROGRAM_ID AND US.ROLE_ID=R.ROLE_ID AND US.USERSYSTEM_ID=@ID
END;
GO

