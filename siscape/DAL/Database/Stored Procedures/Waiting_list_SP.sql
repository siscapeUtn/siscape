USE ProUTN;
GO

--TO GET LAST CODE
CREATE PROCEDURE SP_GETNEXTCODEWAITINGLIST
AS
BEGIN
	SELECT MAX(WAITING_LIST_ID) 
	FROM WAITING_LIST;
END;
GO

--TO GET LAST CODE TENTATIVE_SHCEDULE
CREATE PROCEDURE SP_GET_NEXT_CODE_TENTATIVE_SHCEDULE
AS
BEGIN
	SELECT MAX(TENTATIVE_SHEDULE_ID) 
	FROM TENTATIVE_SCHEDULE;
END;
GO



--TO INSERT A CUSTOMER
CREATE PROCEDURE SP_INSERT_WAITINGLIST
	@WAITING_LIST_ID NUMERIC,
	@ID VARCHAR(15),
	@NAME VARCHAR(50),
	@SURNAME VARCHAR(50),
	@PHONE VARCHAR(15),
	@CELLPHONE VARCHAR(15),
	@MAIL VARCHAR(100),
	@PERIOD NUMERIC,
	@CONTACTED NUMERIC(1)
AS
BEGIN
	INSERT INTO WAITING_LIST(WAITING_LIST_ID, ID, NAME, SURNAME, PHONE, CELLPHONE, MAIL, PERIOD_ID, CONTACTED)
	VALUES( @WAITING_LIST_ID, @ID, @NAME, @SURNAME, @PHONE, @CELLPHONE, @MAIL, @PERIOD, @CONTACTED );
END;
GO


--TO INSERT A TENTATIVE SCHEDULE
--drop proc SP_INSERT_TENTATIVE_SHCEDULE
CREATE PROCEDURE SP_INSERT_TENTATIVE_SCHEDULE
	@TENTATIVE_SCHEDULE_ID NUMERIC,
	@DESCRIPTION          VARCHAR(15),
	@SCHEDULE		      VARCHAR(15),
	@COURSE_ID		      NUMERIC
AS
BEGIN
	INSERT INTO TENTATIVE_SCHEDULE(TENTATIVE_SHEDULE_ID,DESCRIPTION, SCHEDULE, COURSE_ID)
	VALUES(@TENTATIVE_SCHEDULE_ID, @DESCRIPTION, @SCHEDULE, @COURSE_ID);
END;
GO

--TO INSERT A WAITING_LIST_TENTATIVE_SCHEDULE
CREATE PROCEDURE SP_INSERT_WAITING_LIST_TENTATIVE_SCHEDULE
	@WAITING_LIST_ID NUMERIC, 
	@TENTATIVE_SHEDULE_ID NUMERIC
AS
BEGIN
	INSERT INTO WAITING_LIST_TENTATIVE_SCHEDULE(TENTATIVE_SHEDULE_ID,WAITING_LIST_ID) 
	VALUES (@TENTATIVE_SHEDULE_ID,@WAITING_LIST_ID);
END;
GO
 
 
--TO MODIFY WAITING_LIST_
CREATE PROCEDURE SP_MODIFY_WAITING_LIST
	@WAITING_LIST_ID NUMERIC,
	@CONTACTED NUMERIC(1)
AS 
BEGIN
	UPDATE WAITING_LIST 
	SET CONTACTED = @CONTACTED 
	WHERE WAITING_LIST_ID = @WAITING_LIST_ID
END;
GO

--TO MODIFY WAITING_LIST_ALL
/*CREATE PROCEDURE SP_MODIFY_WAITING_LIST_ALL
	@WAITING_LIST_ID NUMERIC,
	@CONTACTED NUMERIC(1),
	@ID varchar(15)
AS 
BEGIN
	UPDATE WAITING_LIST 
	SET CONTACTED = @CONTACTED 
	WHERE ID = @ID
END;
GO
*/

--TO GET ALL CUSTOMER FROM WAITING LIST UNCONTACTED
CREATE PROCEDURE SP_GET_ALL_CUSTOMER
AS
BEGIN
	SELECT W.WAITING_LIST_ID AS CODE,W.ID,CONCAT(W.NAME,' ',W.SURNAME) AS NAME, W.CELLPHONE,W.PHONE,W.MAIL, C.DESCRIPTION AS COURSE, T.DESCRIPTION,T.SCHEDULE, W.CONTACTED 
	FROM WAITING_LIST W , TENTATIVE_SCHEDULE T, WAITING_LIST_TENTATIVE_SCHEDULE WT, COURSE C
	WHERE W.WAITING_LIST_ID = WT.WAITING_LIST_ID AND WT.TENTATIVE_SHEDULE_ID = T.TENTATIVE_SHEDULE_ID AND T.COURSE_ID = C.COURSE_ID
	ORDER BY W.CONTACTED,ID ASC
END;
GO

--TO GET ALL CUSTOMER FROM WAITING LIST CONTACTED
CREATE PROCEDURE SP_GET_ALL_CUSTOMER_2
AS
BEGIN
	SELECT W.WAITING_LIST_ID AS CODE,W.ID,CONCAT(W.NAME,' ',W.SURNAME) AS NAME, W.CELLPHONE,W.PHONE,W.MAIL, C.DESCRIPTION AS COURSE, T.DESCRIPTION,T.SCHEDULE, W.CONTACTED 
	FROM WAITING_LIST W , TENTATIVE_SCHEDULE T, WAITING_LIST_TENTATIVE_SCHEDULE WT, COURSE C
	WHERE W.WAITING_LIST_ID = WT.WAITING_LIST_ID AND WT.TENTATIVE_SHEDULE_ID = T.TENTATIVE_SHEDULE_ID AND T.COURSE_ID = C.COURSE_ID
	ORDER BY W.CONTACTED desc,ID;
END;
GO
	
--TO GET ALL CUSTOMER FROM WAITING LIST BY COURSE
CREATE PROCEDURE SP_GET_ALL_CUSTOMER_BY_COURSE
AS
BEGIN
	SELECT  DISTINCT W.WAITING_LIST_ID AS CODE,W.ID,CONCAT(W.NAME,' ',W.SURNAME) AS NAME, W.CELLPHONE,W.PHONE,W.MAIL,C.COURSE_ID, C.DESCRIPTION AS COURSE, T.DESCRIPTION,T.SCHEDULE, W.CONTACTED 
	FROM WAITING_LIST W , TENTATIVE_SCHEDULE T, WAITING_LIST_TENTATIVE_SCHEDULE WT, COURSE C, PROGRAM P,PERIOD PE
	WHERE W.WAITING_LIST_ID = WT.WAITING_LIST_ID AND WT.TENTATIVE_SHEDULE_ID = T.TENTATIVE_SHEDULE_ID AND T.COURSE_ID = C.COURSE_ID 
	ORDER BY   C.COURSE_ID,ID ASC;
END;
GO

--TO GET ALL CUSTOMER FROM WAITING LIST BY PERIOD, PROGRAM
/*still not used*/
CREATE PROCEDURE SP_GET_ALL_CUSTOMER_BY_PERIOD
@PERDIOD NUMERIC,
@PROGRAM NUMERIC
AS
BEGIN
	SELECT W.WAITING_LIST_ID AS CODE,W.ID,CONCAT(W.NAME,' ',W.SURNAME) AS NAME, W.CELLPHONE,W.PHONE,W.MAIL, C.DESCRIPTION AS COURSE, T.DESCRIPTION,T.SCHEDULE, W.CONTACTED 
	FROM WAITING_LIST W , TENTATIVE_SCHEDULE T, WAITING_LIST_TENTATIVE_SCHEDULE WT, COURSE C, PROGRAM P,PERIOD PE
	WHERE W.WAITING_LIST_ID = WT.WAITING_LIST_ID AND WT.TENTATIVE_SHEDULE_ID = T.TENTATIVE_SHEDULE_ID AND T.COURSE_ID = C.COURSE_ID AND C.PROGRAM_ID = P.PROGRAM_ID AND P.PROGRAM_ID = @PROGRAM AND W.PERIOD_ID= PE.PERIOD_ID AND PE.PERIOD_ID = @PERDIOD
	ORDER BY W.CONTACTED DESC
END;
GO

