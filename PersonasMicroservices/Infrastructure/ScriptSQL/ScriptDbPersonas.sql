IF NOT EXISTS (
    SELECT name 
    FROM sys.databases 
    WHERE name = 'PersonasDb'
)
BEGIN
    CREATE DATABASE PersonasDb;
    PRINT 'La base de datos se creó correctamente.';
END
ELSE
BEGIN
    PRINT 'La base de datos ya existe.';
END;

USE PersonasDb
GO
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = 'TipoPersonas' AND TABLE_SCHEMA = 'dbo' -- Ajusta el esquema si es necesario
)
BEGIN
CREATE TABLE TipoPersonas (
    codigo INT  primary key ,
    descripcion VARCHAR(100) NOT NULL,
)
    PRINT 'La tabla TipoPersonas  se creó correctamente.';
END
ELSE
BEGIN
    PRINT 'La tabla TipoPersonas ya existe.';
END;
GO
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = 'Personas' AND TABLE_SCHEMA = 'dbo' -- Ajusta el esquema si es necesario
)
BEGIN

    PRINT 'La tabla Personas  se creó correctamente.';
	CREATE TABLE Personas (
    codigo INT PRIMARY KEY IDENTITY(1,1), 
    nombres VARCHAR(200) NOT NULL,  
	apellidos VARCHAR(200) NOT NULL,
	documento VARCHAR(200) NOT NULL,
	codigoTipoPersona INT  NOT NULL,
    fechaNacimiento DATE,                     
    genero CHAR(1),                                               
    direccion VARCHAR(255),                  
    telefono VARCHAR(15),                    
    correoElectronico VARCHAR(100),
	estado bit,
	CONSTRAINT FkTipoPersonaClaveForanea FOREIGN KEY (codigoTipoPersona)
        REFERENCES TipoPersonas(codigo)  -- Relación con tabla TipoPersonas
);

END
ELSE
BEGIN
    PRINT 'La tabla Personas ya existe.';
END;
GO
IF NOT EXISTS (SELECT  codigo FROM TipoPersonas where codigo=1)
BEGIN
    INSERT INTO TipoPersonas VALUES (1, 'Medico')

END
GO
IF NOT EXISTS (SELECT  codigo FROM TipoPersonas where codigo=2)
BEGIN
    INSERT INTO TipoPersonas VALUES (2, 'Paciente')

END