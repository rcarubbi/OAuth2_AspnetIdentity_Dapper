CREATE TABLE [dbo].[GrupoAcessoPermissao]
(
	[IdPermissao] INT NOT NULL , 
    [IdGrupoAcesso] INT NOT NULL, 
    [DataHoraCriacao] DATETIME NOT NULL DEFAULT getdate(), 
    PRIMARY KEY ([IdGrupoAcesso], [IdPermissao]), 
    CONSTRAINT [FK_GrupoAcessoPermissao_Permissao] FOREIGN KEY (IdPermissao) REFERENCES Permissao(Id), 
    CONSTRAINT [FK_GrupoAcessoPermissao_GrupoAcesso] FOREIGN KEY (IdGrupoAcesso) REFERENCES GrupoAcesso(Id)
)
