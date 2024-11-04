USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[tasks].[update]'))
BEGIN
	DROP PROCEDURE [tasks].[update];
END;
GO

CREATE PROCEDURE [tasks].[update]
	@task_id INT,
	@name VARCHAR(150),
	@priority_id INT = NULL,
	@priority_code CHAR(2) = NULL,
	@state_id INT = NULL,
	@state_code CHAR(2) = NULL 
WITH ENCRYPTION
AS
BEGIN
	DECLARE @pid INT;
	DECLARE @sid INT;

	-- setear la variable con la primera condicion no nula
	 SET @pid = COALESCE(
		 @priority_id,
		 (SELECT id FROM dbo.priorities WHERE code = @priority_code));


	 SET @sid = COALESCE(
		@state_id,
		(SELECT id FROM dbo.states WHERE code = @state_code)
	 );

	 if NOT EXISTS(SELECT 1 FROM dbo.tasks WHERE id=@task_id)
	 BEGIN
		PRINT 'el "id" de la tarea no existe';
		RETURN;
	 END;


	UPDATE dbo.tasks
    SET 
        name = @name,
		--priority y state se setean por el primer valor no nulo
        [priority] = COALESCE(@pid, [priority]),
        [state] = COALESCE(@sid, [state])
    WHERE id = @task_id;
END;