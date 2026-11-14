CREATE DATABASE GimnasioDB;
GO

USE GimnasioDB;
GO

CREATE TABLE dbo.Socios (
Id INT IDENTITY (1,1) PRIMARY KEY,
Nombre NVARCHAR(100) NOT NULL,
Membresia NVARCHAR(50) NOT NULL,
Cuota DECIMAL(10,2) NOT NULL,
);
INSERT INTO dbo.Socios (Nombre, Membresia, Cuota)
VALUES ('Karla Soto', 'Mensual', 180),
	   ('Diego Paz', 'Trimetral', 500)

SELECT * FROM dbo.Socios