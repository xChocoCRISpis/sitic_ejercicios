USE SITIC_GTA;
GO

IF EXISTS(SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[tasks].[insert]'))
BEGIN
	DROP PROCEDURE [tasks].[insert];
END;
GO

CREATE PROCEDURE [tasks].[insert]
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

	 if @pid IS NULL
        BEGIN
            PRINT 'Es obligatorio proporcionar un ID o código válido de "priority".';
            RETURN;
        END

	


	 SET @sid = COALESCE(
		@state_id,
		(SELECT id FROM dbo.states WHERE code = @state_code)
	 );

	 if @sid IS NULL
        BEGIN
            PRINT 'Es obligatorio proporcionar un ID o código válido de "state".';
            RETURN;
        END
	

	
	INSERT INTO dbo.tasks ([name], priority, [state])
        VALUES (@name, @pid, @sid);

END;