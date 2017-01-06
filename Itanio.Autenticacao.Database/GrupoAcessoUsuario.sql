CREATE TABLE [dbo].[GrupoAcessoUsuario]
(
	[IdUsuario] UNIQUEIDENTIFIER NOT NULL , 
    [IdGrupoAcesso] INT NOT NULL, 
    [DataHoraCriacao] DATETIME NOT NULL DEFAULT getdate(), 
    PRIMARY KEY ([IdUsuario], [IdGrupoAcesso]), 
    CONSTRAINT [FK_GrupoAcessoUsuario_GrupoAcesso] FOREIGN KEY (IdGrupoAcesso) REFERENCES GrupoAcesso(Id), 
    CONSTRAINT [FK_GrupoAcessoUsuario_Usuario] FOREIGN KEY (IdUsuario) REFERENCES Usuario(Id)
)
