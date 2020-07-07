CREATE DATABASE Entrevista
go

USE Entrevista
go

CREATE TABLE TipoUsuario(
IdTipoUsuario INT PRIMARY KEY IDENTITY,
Titulo VARCHAR (500)
)
go

INSERT INTO TipoUsuario(Titulo)
VALUES ('Pessoa jurídica'),('Pessoa física');
go

SELECT * FROM TipoUsuario
go
DROP TABLE Usuario
CREATE TABLE Usuario (
IdUsuario INT PRIMARY KEY IDENTITY,
IdTipoUsuario INT FOREIGN KEY REFERENCES TipoUsuario (IdTipoUsuario),
NomeUsuario VARCHAR(500),
NumeroDocumento DECIMAL UNIQUE,
Telefone INT
)
go

INSERT INTO Usuario (IdTipoUsuario,NomeUsuario,NumeroDocumento,Telefone)
VALUES (1,'Murilo',123,111)
go

INSERT INTO Usuario (IdTipoUsuario,NomeUsuario,NumeroDocumento,Telefone)
VALUES (2,'Luiz',52584781816,111)
go

SELECT * FROM Usuario
SELECT * FROM Usuario
SELECT * FROM Usuario WHERE NumeroDocumento = 123

INSERT INTO Usuario (NomeUsuario, IdTipoUsuario, NumeroDocumento, Telefone)
VALUES('Carlos',2, 111,32111)

UPDATE Usuario SET NomeUsuario = 'pedro', IdTipoUsuario = 1, NumeroDocumento = 222, Telefone = 123333
where NumeroDocumento = 111

DELETE FROM Usuario WHERE NumeroDocumento = 222

SELECT * FROM Usuario WHERE NomeUsuario LIKE '%' +'Luiz' + '%'