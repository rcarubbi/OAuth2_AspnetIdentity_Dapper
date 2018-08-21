using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Itanio.Autenticacao.Entidades;

namespace Itanio.Autenticacao.Repositorios
{
    public class GrupoAcessoRepository
    {
        private const string SQL_Inserir = @"INSERT INTO GrupoAcesso VALUES (@nome, 1); select @Scope_Identity();";

        private const string SQL_Alterar =
            @"UPDATE GrupoAcesso Nome = @Nome, Ativo = @Ativo, DataHoraUltimaAlteracao = GETDATE() WHERE Id = @Id;";

        private const string SQL_Excluir_Usuarios = @"DELETE FROM GrupoAcessoUsuario Where IdGrupoAcesso = @id";
        private const string SQL_Excluir_Permissoes = @"DELETE FROM GrupoAcessoPermissao Where IdGrupoAcesso = @id";
        private const string SQL_Excluir = @"DELETE FROM GrupoAcesso Where id = @id";

        private const string SQL_ListarTodos = @"SELECT * FROM GrupoAcesso";
        private const string SQL_ObterPorId = @"SELECT * FROM GrupoAcesso where Id = @id";

        private const string SQL_ListarUsuariosPorGrupo =
            @"SELECT u.* FROM Usuario u inner join GrupoAcessoUsuario gu on gu.idusuario = u.id where IdGrupoAcesso = @id";

        private const string SQL_ListarPermissoesPorGrupo =
            @"SELECT p.* FROM Permissao p inner join GrupoAcessoPermissao gp on gp.idPermissao = p.id where Id = @id";

        private const string SQL_AdicionarPermissao =
            @"INSERT INTO GrupoAcessoPermissao (idPermissao, idGrupoAcesso) values(@idPermissao, @idGrupoAcesso)";

        private const string SQL_RemoverPermissao =
            @"DELETE FROM GrupoAcessoPermissao where idPermissao = @idPermissao and idGrupoAcesso = @idGrupoAcesso";

        private const string SQL_AdicionarUsuario =
            @"INSERT INTO GrupoAcessoUsuario (idUsuario, idGrupoAcesso) values(@idUsuario, @idGrupoAcesso)";

        private const string SQL_RemoverUsuario =
            @"DELETE FROM GrupoAcessoUsuario where idUsuario = @idUsuario and idGrupoAcesso = @idGrupoAcesso";

        private readonly GerenciadorConexao _gerenciadorConexao;

        public GrupoAcessoRepository()
            : this(new GerenciadorConexao())
        {
        }

        public GrupoAcessoRepository(GerenciadorConexao gerenciadorConexao)
        {
            _gerenciadorConexao = gerenciadorConexao;
        }

        public void Salvar(GrupoAcesso grupoAcesso)
        {
            if (grupoAcesso.Id == default(int))
            {
                var id = _gerenciadorConexao.Conexao.Query<int>(SQL_Inserir, grupoAcesso).Single();
                grupoAcesso.Id = id;
            }
            else
            {
                _gerenciadorConexao.Conexao.Execute(SQL_Alterar, grupoAcesso);
            }
        }

        public GrupoAcesso ObterPorId(int id)
        {
            var resultados =
                _gerenciadorConexao.Conexao.QueryMultiple(
                    SQL_ObterPorId + SQL_ListarUsuariosPorGrupo + SQL_ListarPermissoesPorGrupo, new {id});
            var grupo = resultados.Read<GrupoAcesso>().SingleOrDefault();
            if (grupo != null)
            {
                grupo.Usuarios.AddRange(resultados.Read<Usuario>().ToList());
                grupo.Permissoes.AddRange(resultados.Read<Permissao>().ToList());
            }

            return grupo;
        }

        public void Excluir(int id)
        {
            _gerenciadorConexao.Conexao.Execute(SQL_Excluir_Usuarios + SQL_Excluir_Permissoes + SQL_Excluir, new {id});
        }

        public ICollection<GrupoAcesso> ListarTodos()
        {
            return _gerenciadorConexao.Conexao.Query<GrupoAcesso>(SQL_ListarTodos).ToList();
        }

        public void AdicionarPermissao(int idPermissao, int idGrupoAcesso)
        {
            _gerenciadorConexao.Conexao
                .Execute(SQL_AdicionarPermissao,
                    new
                    {
                        idPermissao,
                        idGrupoAcesso
                    });
        }

        public void RemoverPermissao(int idPermissao, int idGrupoAcesso)
        {
            _gerenciadorConexao.Conexao
                .Execute(SQL_RemoverPermissao,
                    new
                    {
                        idPermissao,
                        idGrupoAcesso
                    });
        }

        public void AdicionarUsuario(Guid idUsuario, int idGrupoAcesso)
        {
            _gerenciadorConexao.Conexao
                .Execute(SQL_AdicionarUsuario,
                    new
                    {
                        idUsuario,
                        idGrupoAcesso
                    });
        }

        public void RemoverUsuario(Guid idUsuario, int idGrupoAcesso)
        {
            _gerenciadorConexao.Conexao
                .Execute(SQL_RemoverUsuario,
                    new
                    {
                        idUsuario,
                        idGrupoAcesso
                    });
        }
    }
}