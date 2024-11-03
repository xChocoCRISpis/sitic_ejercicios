--_______________________________________--
--			CREACIÓN DE LA DB
--_______________________________________--

IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'SITIC_GTA')
BEGIN
	CREATE DATABASE SITIC_GTA;
END;
GO;

USE SITIC_GTA;
GO;


--_____________________________________--
--			CREACIÓN DE SQUEMAS			--
--_____________________________________--

IF NOT EXISTS(SELECT 1 FROM sys.schemas WHERE name='priorities')
BEGIN
	EXEC('CREATE SCHEMA priorities');
END;
GO;



IF NOT EXISTS(SELECT 1 FROM sys.schemas WHERE name='states')
BEGIN
	EXEC('CREATE SCHEMA states');
END;
GO;


IF NOT EXISTS(SELECT 1 FROM sys.schemas WHERE name='tasks')
BEGIN
	EXEC('CREATE SCHEMA tasks');
END;
GO;



--	____________________________________________--
--			CREACIÓN DE LAS TABLAS				--
--______________________________________________--


IF NOT EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = OBJECT_ID (N'[dbo].[priorities]') AND OBJECTPROPERTY(id,N'isUserTable')=1)
BEGIN
	CREATE TABLE priorities(
		id INT IDENTITY(1,1) PRIMARY KEY,
		[priority] VARCHAR(150),
		code CHAR(2)
	);
END;
GO;


IF NOT EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = OBJECT_ID (N'[dbo].[states]') AND OBJECTPROPERTY(id,N'isUserTable')=1)
BEGIN
	CREATE TABLE states(
		id INT IDENTITY(1,1) PRIMARY KEY,
		[status] VARCHAR(150),
		code CHAR(2)
	);
END;
GO;



-- Validar si en la tabla con los objetos del sistema, existe la tb 'tasks' en el schema dbo
-- Y validar que el id obtenido tiene la propiedad 'isUserTable'
IF NOT EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = OBJECT_ID (N'[dbo].[tasks]') AND OBJECTPROPERTY(id,N'isUserTable')=1)
BEGIN
	CREATE TABLE tasks(
		id INT IDENTITY(1,1) PRIMARY KEY,
		[name] VARCHAR(500),
		[priority] INT NOT NULL,
		[state] INT NOT NULL,

		FOREIGN KEY ([priority]) REFERENCES priorities(id),
		FOREIGN KEY ([state]) REFERENCES states(id)
	);
END;
GO;
