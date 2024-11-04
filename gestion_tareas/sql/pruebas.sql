USE SITIC_GTA;
GO

-- Pruebas para [priorities].[insert]
BEGIN TRANSACTION;
    PRINT 'Prueba de [priorities].[insert]';
    EXEC [priorities].[insert] 'MEDIUM PRIORITY', 'MP';
    EXEC [priorities].[insert] 'LOW PRIORITY', 'LP';
    EXEC [priorities].[insert] 'HIGH PRIORITY', 'HP';
    SELECT * FROM dbo.priorities;
ROLLBACK TRANSACTION;

-- Prueba para [priorities].[get]
BEGIN TRANSACTION;
	--sin filtros
    EXEC [priorities].[get] 0;
	--priority
    EXEC [priorities].[get] NULL, 'HIGH PRIORITY';
	--code
    EXEC [priorities].[get] NULL, NULL, 'HP';
ROLLBACK TRANSACTION;


--  [states].[insert]
BEGIN TRANSACTION;
    EXEC [states].[insert] 'TO DO', 'TD';
    EXEC [states].[insert] 'IN PROGRESS', 'IP';
    EXEC [states].[insert] 'COMPLETED', 'C';
    SELECT * FROM dbo.states;
ROLLBACK TRANSACTION;

-- Prueba para [states].[get]
BEGIN TRANSACTION;
	--sin filtros
    EXEC [states].[get] NULL;
	--por status
    EXEC [states].[get] NULL, 'TO DO'; 
	--por code
	EXEC [states].[get] NULL, NULL, 'C'; 

ROLLBACK TRANSACTION;

-- Prueba de inserción duplicada en [priorities].[insert]
BEGIN TRANSACTION;
--codigo duplicado
    EXEC [priorities].[insert] 'HIGH PRIORITY', 'HP';
ROLLBACK TRANSACTION;

-- Prueba de actualización en [priorities].[update]
BEGIN TRANSACTION;
    EXEC [priorities].[update] NULL, 'UPDATED PRIORITY', 'HP';
    SELECT * FROM dbo.priorities;
ROLLBACK TRANSACTION;


-- Prueba de eliminación en [priorities].[delete]
BEGIN TRANSACTION;
    EXEC [priorities].[delete] 3;
    SELECT * FROM dbo.priorities;
ROLLBACK TRANSACTION;


-- Insertar tareas en [tasks]
BEGIN TRANSACTION;
    -- Insertar Task 1
    EXEC [tasks].[insert] 'Task 1', NULL, 'MP', NULL, 'TD';
    -- Insertar Task 2
    EXEC [tasks].[insert] 'Task 2', NULL, 'LP', NULL, 'IP';
    -- Insertar Task 3
    EXEC [tasks].[insert] 'Task 3', NULL, 'HP', NULL, 'C';
    -- Ver tareas insertadas
    SELECT * FROM dbo.tasks;
ROLLBACK TRANSACTION;

-- Obtener tareas con filtros
BEGIN TRANSACTION;
    -- Obtener todas las tareas
    EXEC [tasks].[get] NULL;
    -- Obtener tareas con prioridad 'HP'
    EXEC [tasks].[get] NULL, NULL, 'HP';
    -- Obtener tareas con estado 'IP'
    EXEC [tasks].[get] NULL, NULL, NULL, NULL, 'IP';
ROLLBACK TRANSACTION;

-- Prueba de inserción duplicada
BEGIN TRANSACTION;
    -- Intentar insertar tarea duplicada con prioridad 'HP'
    EXEC [tasks].[insert] 'Task Duplicada', NULL, 'HP', NULL, 'TD';
ROLLBACK TRANSACTION;

-- Actualizar tarea en [tasks]
BEGIN TRANSACTION;
    -- Actualizar con id
    EXEC [tasks].[update]  1, 
        'Task Updated', NULL, 'LP', NULL, 'C';

    SELECT * FROM dbo.tasks;
ROLLBACK TRANSACTION;

-- Eliminar tarea en [tasks]
BEGIN TRANSACTION;
    -- Eliminar la tarea
    EXEC [tasks].[delete] 1

    SELECT * FROM dbo.tasks;
ROLLBACK TRANSACTION;