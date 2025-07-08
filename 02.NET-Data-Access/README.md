USE [master];

DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('Curso')

EXEC(@kill);

DROP DATABASE [Curso]

USE [Curso]

DROP TABLE [Aluno]
CREATE TABLE [Aluno] (
    [Id] INT NOT NULL, 
    [Nome] NVARCHAR(80) NOT NULL,
    [Email] NVARCHAR(180) NOT NULL,
    [Nascimento] DATETIME NULL,
    [Active] BIT NOT NULL DEFAULT(0),

    CONSTRAINT [PK_Aluno] PRIMARY KEY([Id]),
    CONSTRAINT [UQ_Aluno_Email] UNIQUE([Email]),
)
GO

CREATE INDEX [IX_Aluno_Email] ON [Aluno]([Email])
DROP INDEX [IX_Aluno_Email] ON [Aluno]

SELECT NEWID()

DROP TABLE [Curso]
CREATE TABLE [Curso] (
    [Id] INT NOT NULL IDENTITY(1, 1), 
    [Nome] NVARCHAR(80) NOT NULL,
    [CategoriaId] INT NOT NULL,

    CONSTRAINT [PK_Curso] PRIMARY KEY([Id]),
    CONSTRAINT [FK_Curso_Categoria] FOREIGN KEY([CategoriaId])
        REFERENCES [Categoria]([Id])
)
GO

CREATE TABLE [Categoria] (
    [Id] INT NOT NULL, 
    [Nome] NVARCHAR(80) NOT NULL,

    CONSTRAINT [PK_Categoria] PRIMARY KEY([Id])
)
GO

DROP TABLE [ProgressoCurso]
CREATE TABLE [ProgressoCurso] (
    [AlunoId] INT NOT NULL, 
    [CursoId] INT NOT NULL, 
    [Progresso] INT NOT NULL,
    [UltimaAtualizacao] DATETIME NOT NULL DEFAULT(GETDATE()),
    
    CONSTRAINT PK_ProgressoCurso PRIMARY KEY([AlunoId], [CursoId]),
)
GO

INSERT INTO [Curso]([Nome], [CategoriaId]) VALUES('Fundamentos de C#', 1)
INSERT INTO [Curso]([Nome], [CategoriaId]) VALUES('Fundamentos de OOP', 1)
INSERT INTO [Curso]([Nome], [CategoriaId]) VALUES('Angular', 2)
INSERT INTO [Curso]([Nome], [CategoriaId]) VALUES('Flutter', 3)

SELECT TOP 100
    [Id], [Nome], [CategoriaId]
FROM 
    [Curso]
-- WHERE
    -- [CategoriaId] = 1 
ORDER BY 
    [Nome] 

SELECT TOP 100
    *
FROM 
    [Curso]
WHERE
    [Nome] LIKE '% de %'

SELECT TOP 100
    *
FROM 
    [Curso]
WHERE
    [Id] BETWEEN 2 AND 3

SELECT TOP 100
    *
FROM 
    [Curso]
    INNER JOIN [Categoria] 
        ON [Curso].[CategoriaId] = [Categoria].[Id]