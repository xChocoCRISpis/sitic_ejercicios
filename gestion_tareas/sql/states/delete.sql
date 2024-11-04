USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[states].[delete]') AND type IN(N'P',N'PC'))
BEGIN
	DROP PROCEDURE [states].[delete];
END;
GO

CREATE PROCEDURE [states].[delete]
	@id INT 
WITH ENCRYPTION
AS
BEGIN
	DELETE FROM dbo.tasks WHERE [state] = @id;
	DELETE FROM dbo.states WHERE id = @id;
END;