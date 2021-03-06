USE ProUTN;
GO
--DROP PROCEDURE SP_OPENINGJUSTIFCATION
CREATE PROCEDURE SP_OPENINGJUSTIFCATION
@ID NUMERIC
AS
BEGIN

SELECT [ACADEMIC_OFFER_ID],T.[NAME],T.SURNAME,ID.DESCRIPTION,ID.BASE_SALARY,ID.ANNUITY,
AO.PRICE,AO.HOURS,C.DESCRIPTION,ID.DESIGNATION_ID
FROM [dbo].[ACADEMIC_OFFER] AO,TEACHER T, COURSE C, INTERNAL_DESIGNATION ID
WHERE   AO.TEACHER_ID=T.TEACHER_ID AND AO.COURSE_ID=C.COURSE_ID AND T.POSITION_ID=ID.DESIGNATION_ID 
AND AO.DELETED=0 AND AO.ACADEMIC_OFFER_ID=@ID

END;
GO