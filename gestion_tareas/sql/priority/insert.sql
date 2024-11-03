USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[priorities].[insert]') AND type IN(N'P',N'PC'))
BEGIN
	DROP PROCEDURE [priorities].[insert];
END;
GO

CREATE PROCEDURE [priorities].[insert]
	@description VARCHAR(150),
	@code CHAR(2)
WITH ENCRYPTION
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM priorities WHERE code=@code)
		BEGIN
			INSERT INTO priorities ([priority],code) VALUES (@description,@code);
		END;
	ELSE
		BEGIN
			PRINT 'código de priorities ya existe, proporcionar uno distinto';
			RETURN; --Para saltar procesamiento de las siguientes instrucciones
		END;
END;