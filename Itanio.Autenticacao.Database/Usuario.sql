CREATE TABLE [dbo].[Usuario]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Login] VARCHAR(50) NOT NULL, 
    [Senha] VARCHAR(3000) NOT NULL, 
    [Ativo] BIT NOT NULL, 
    [DataHoraCriacao] DATETIME NOT NULL DEFAULT getdate(), 
	[DataHoraUltimaAlteracao] DATETIME NULL,
    [DataHoraFimBloqueio] DATETIME NULL, 
    [QuantidadeFalhasConsecutivas] INT NOT NULL DEFAULT 0, 
    [BloqueioHabilitado] BIT NOT NULL DEFAULT 0, 
    [DuplaVerificacaoHabilitada] BIT NOT NULL DEFAULT 0
)
