USE SITIC_GTA;
GO

-- Validar si existe GET en el schema priorities y sea o un T-SQL o un CLR(sp ensamblado desde C#)
-- La 'N' antes de una string es para usar NVACHAR
IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[priorities].[get]') AND TYPE IN(N'P',N'PC'))
	BEGIN
			DROP PROCEDURE [priorities].[get];
	END;
GO -- HACE QUE LA PRIMERA LÍNEA DEL BATCH SEA LA SIGUIENTE DE ESTA CLAUSULA;
	
CREATE PROCEDURE [priorities].[get]
	@id INT = 0,
	@priority VARCHAR(150) = NULL,
	@code CHAR(2) = NULL
WITH ENCRYPTION
AS
BEGIN
	SELECT id,[priority],code FROM [dbo].[priorities] AS p
	WHERE (@id = 0 OR p.id = @id) 
      AND (@priority IS NULL OR p.[priority] = @priority)
      AND (@code IS NULL OR p.code = @code);
END;
		