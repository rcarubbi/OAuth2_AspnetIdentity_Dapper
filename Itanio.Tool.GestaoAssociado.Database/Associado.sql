CREATE TABLE [dbo].[Associado]	
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [IdUsuario] UNIQUEIDENTIFIER NOT NULL, 
	[FormaTratamento] TINYINT NOT NULL,
    [Sexo] TINYINT NOT NULL, 
    [EstadoCivil] TINYINT NULL, 
	[Nome] VARCHAR(100) NOT NULL, 
    [DataNascimento] DATETIME NOT NULL, 
    [RG] VARCHAR(15) NOT NULL, 
    [CPF] VARCHAR(12) NOT NULL, 
    [ReceberEmails] BIT NOT NULL, 
    [ReceberSMS] BIT NULL, 
)
