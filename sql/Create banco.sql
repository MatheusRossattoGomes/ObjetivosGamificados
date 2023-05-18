

CREATE TABLE Usuario (
  Id BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Email VARCHAR(300) NOT NULL,
  Nome VARCHAR(45) NOT NULL);

CREATE UNIQUE INDEX Id_UNIQUE
ON Usuario (Id);
CREATE UNIQUE INDEX Email_UNIQUE
ON Usuario (Email);
CREATE UNIQUE INDEX Nome_UNIQUE
ON Usuario (Nome);

INSERT INTO [dbo].[Usuario]
           ([Email]
           ,[Nome])
     VALUES
           ('Teste@teste.com'
           ,'Teste')

CREATE TABLE TiposObjetivos (
  Id BIGINT NOT NULL,
  Descricao VARCHAR(45) NOT NULL,
  PRIMARY KEY (Id));

CREATE UNIQUE INDEX Id_UNIQUE
ON TiposObjetivos (Id);
CREATE UNIQUE INDEX Descricao_UNIQUE
ON TiposObjetivos (Descricao);


INSERT INTO TiposObjetivos (Id, Descricao) VALUES (-1, 'Video'), (-2, 'Texto'), (-3, 'Questionário');



CREATE TABLE Objetivos (
  Id BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  Objetivo VARCHAR(MAX) NOT NULL,
  Descricao VARCHAR(100) NOT NULL,
  DataCriacao DATETIME NOT NULL,
  DataEntrega DATE NOT NULL,
  Quantidade INT NOT NULL,
  IdTipoObjetivo BIGINT NOT NULL,
  IdUsuario BIGINT NOT NULL,
  CONSTRAINT IdUsuario
    FOREIGN KEY (IdUsuario)
    REFERENCES Usuario (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT IdTipoObjetivo
    FOREIGN KEY (IdTipoObjetivo)
    REFERENCES TiposObjetivos (Id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE UNIQUE INDEX Id_UNIQUE
ON Objetivos (Id);
CREATE UNIQUE INDEX Descricao_UNIQUE
ON Objetivos (Descricao);
CREATE INDEX IdTipoObjetivo_idx
ON Objetivos (IdTipoObjetivo);
CREATE INDEX IdUsuario_idx
ON Objetivos (IdUsuario);

