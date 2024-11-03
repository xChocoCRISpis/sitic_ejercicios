USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[states].[update]') AND type IN(N'P',N'PC'))
BEGIN
	DROP PROCEDURE [states].[update];
END;
GO

CREATE PROCEDURE [states].[update]
	@id INT = NULL,
	@status VARCHAR(150),
	@code  CHAR(2) = NULL
WITH ENCRYPTION
AS
BEGIN
	IF(@code IS NOT NULL)
	BEGIN
		IF EXISTS(SELECT 1 FROM dbo.states WHERE code=UPPER(@code))
		BEGIN
			PRINT '"code" ya existe';
			RETURN;
		END;

		UPDATE dbo.states SET code=UPPER(@code), [status]=UPPER(@status)
		WHERE (@id IS NOT NULL AND id=@id) OR
		(@code IS NOT NULL AND code=@code);
		RETURN;
	END;

	UPDATE dbo.states SET [status]=UPPER(@status)
		WHERE (@id IS NOT NULL AND id=@id) OR
		(@code IS NOT NULL AND code=@code);
END;