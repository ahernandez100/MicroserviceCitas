IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'RecetasDb'
)
BEGIN
    CREATE DATABASE RecetasDb;
    PRINT 'La base de datos se creó correctamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos ya existe.';
END;
GO
USE RecetasDb
GO
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = 'Recetas' AND TABLE_SCHEMA = 'dbo' -- Ajusta el esquema si es necesario
)
BEGIN
CREATE TABLE Recetas (
    codigo INT PRIMARY KEY IDENTITY(1,1),
	codigoPaciente INT NOT NULL,
	nombrePaciente VARCHAR(400) NOT NULL,
	codigoMedico  INT NOT NULL,
	nombreMedico VARCHAR(400) NOT NULL,
	fecha DATETIME NOT NULL,
    observacion VARCHAR(500) ,
    codigoCita INT NOT NULL,
    estado VARCHAR(20) NOT NULL,
)
    PRINT 'La tabla Recetas  se creó correctamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Recetas ya existe.';
END;
GO
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = 'DetalleRecetas' AND TABLE_SCHEMA = 'dbo' -- Ajusta el esquema si es necesario
)
BEGIN
CREATE TABLE DetalleRecetas (
    codigoReceta INT NOT NULL,
	numero INT NOT NULL,
    nombreMedicamento VARCHAR(200) NOT NULL,
	dosis VARCHAR(100) NOT NULL,
	frecuencia VARCHAR(100) NOT NULL,
	PRIMARY KEY (codigoReceta, numero), -- Llave primaria compuesta
    CONSTRAINT FkTipoRecetaClaveForanea FOREIGN KEY (codigoReceta)
        REFERENCES Recetas(codigo)  -- Relación con tabla Recetas
)
    PRINT 'La tabla DetalleRecetas  se creó correctamente.';
END
ELSE
BEGIN
    PRINT 'La tabla DetalleRecetas ya existe.';
END;
GO