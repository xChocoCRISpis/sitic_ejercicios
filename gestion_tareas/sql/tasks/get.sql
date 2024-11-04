USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[tasks].[get]'))
BEGIN
	DROP PROCEDURE [tasks].[get];
END;
GO

CREATE PROCEDURE [tasks].[get]
	@id INT = NULL,
	@priority_id INT = NULL,
	@priority_code CHAR(2) = NULL,
	@state_id INT = NULL,
	@state_code CHAR(2) = NULL 
WITH ENCRYPTION
AS
BEGIN
	SELECT 
		t.id AS 'task_id',
		t.[name] AS 'task_name',
		p.priority AS 'priority',
		p.code AS 'priority_code',
		s.status AS 'status',
		s.code AS 'state_code'
	FROM dbo.tasks AS t
	JOIN dbo.priorities AS p on t.[priority]=p.id
	JOIN dbo.states AS s ON s.id = t.[state]
	WHERE (@id IS NULL OR t.id = @id)
	AND (@priority_id IS NULL OR p.id=@priority_id)
	AND (@priority_code IS NULL OR p.code=@priority_code)
	AND (@state_id IS NULL OR s.id = @state_id)
	AND (@state_code IS NULL OR s.code = @state_code);
END;