USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[states].[get]') AND type IN(N'P',N'PC'))
BEGIN
	DROP PROCEDURE [states].[get];
END;
GO

CREATE PROCEDURE [states].[get]
	@id INT = NULL,
	@status VARCHAR(150) = NULL,
	@code  CHAR(2) = NULL
WITH ENCRYPTION
AS
BEGIN
	SELECT s.id,s.status,s.code FROM [dbo].states AS s
	WHERE (@id IS NULL OR id=@id) AND
	(@status IS NULL OR s.status=@status) AND
	(@code IS NULL OR s.code=@code);
END;