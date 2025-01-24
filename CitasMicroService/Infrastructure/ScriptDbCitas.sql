IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'CitasDb'
)
BEGIN
    CREATE DATABASE CitasDb;
    PRINT 'La base de datos se creó correctamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos ya existe.';
END;
GO
USE [CitasDb]
GO
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = 'Citas' AND TABLE_SCHEMA = 'dbo' -- Ajusta el esquema si es necesario
)
BEGIN
CREATE TABLE Citas (
    codigo INT PRIMARY KEY IDENTITY(1,1), 
    codigoPaciente INT NOT NULL,
	nombrePaciente VARCHAR(400) NOT NULL,
	codigoMedico  INT NOT NULL,
	nombreMedico VARCHAR(400) NOT NULL,
	fecha DATETIME NOT NULL,
	lugar VARCHAR(200) NOT NULL,
	estado VARCHAR(50) NOT NULL,


)
    PRINT 'La tabla Citas  se creó correctamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Citas ya existe.';
END;
