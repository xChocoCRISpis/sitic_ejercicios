USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[priorities].[delete]') AND type IN(N'P',N'PC'))
BEGIN
	DROP PROCEDURE [priorities].[delete];
END;
GO

CREATE PROCEDURE [priorities].[delete]
	@id INT
WITH ENCRYPTION
AS
BEGIN
	--Esto borra todas las tareas que tuvieran ese id y luego elimina el id;
	DELETE FROM [dbo].tasks WHERE tasks.priority=@id;
	DELETE FROM [dbo].priorities WHERE id=@id;
END;