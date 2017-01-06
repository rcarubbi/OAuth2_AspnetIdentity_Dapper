CREATE TABLE [dbo].[GrupoAcesso]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Nome] VARCHAR(50) NOT NULL, 
    [Ativo] BIT NOT NULL DEFAULT 1, 
    [DataHoraCriacao] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DataHoraUltimaAlteracao] DATETIME NULL
)
