USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[tasks].[delete]'))
BEGIN
	DROP PROCEDURE [tasks].[delete];
END;
GO

CREATE PROCEDURE [tasks].[delete]
	@task_id INT
WITH ENCRYPTION
AS
BEGIN
	DELETE FROM dbo.tasks WHERE id=@task_id;
END;