USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[states].[insert]') AND type IN(N'P',N'PC'))
BEGIN
	DROP PROCEDURE [states].[insert];
END;
GO

CREATE PROCEDURE [states].[insert]
	@status VARCHAR(150),
	@code  CHAR(2)
WITH ENCRYPTION
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [dbo].states WHERE code = @code)
	BEGIN
		PRINT '"code" ya existe';
		RETURN;
	END;

	 INSERT INTO [dbo].states ([status], code) VALUES (UPPER(@status), UPPER(@code));
END;