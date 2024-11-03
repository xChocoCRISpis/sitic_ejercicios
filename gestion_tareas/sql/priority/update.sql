USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[priorities].[update]'))
BEGIN
	DROP PROCEDURE [priorities].[update];
END;
GO

CREATE PROCEDURE [priorities].[update]
	@id INT = NULL,
	@description VARCHAR(150),
	@code CHAR(2) = NULL
WITH ENCRYPTION
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [dbo].priorities WHERE code = @code)
		BEGIN
			PRINT '"code" ya existe en la base de datos'
			RETURN;
		END;
	ELSE IF(@code IS NOT NULL)
		BEGIN
			UPDATE [dbo].priorities SET [priority]=@description, code=@code
			WHERE (@id IS NOT NULL AND id=@id) OR (@code IS NOT NULL AND code=@code)
		END;
	ELSE BEGIN
			UPDATE [dbo].priorities SET [priority]=@description
			WHERE (@id IS NOT NULL AND id=@id) OR (@code IS NOT NULL AND code=@code)
		END;
END;