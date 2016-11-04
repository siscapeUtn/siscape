USE ProUTN;
GO

CREATE PROCEDURE SP_GETALLACTIVESSTATUS
AS
BEGIN

	SELECT ActivesSatus_ID,DESCRIPTION
	FROM ActivesStatus;

END;
GO

INSERT INTO ActivesStatus (ActivesSatus_ID, DESCRIPTION) VALUES (1	, 'Funciona');
INSERT INTO ActivesStatus (ActivesSatus_ID, DESCRIPTION) VALUES (2	, 'No Funciona');
INSERT INTO ActivesStatus (ActivesSatus_ID, DESCRIPTION) VALUES (3	, 'Obsoleto');
