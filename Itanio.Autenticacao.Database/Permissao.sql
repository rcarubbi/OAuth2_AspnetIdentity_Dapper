CREATE TABLE [dbo].[Permissao]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Nome] VARCHAR(50) NOT NULL, 
    [DataHoraCriacao] NCHAR(10) NOT NULL DEFAULT getdate()
)
