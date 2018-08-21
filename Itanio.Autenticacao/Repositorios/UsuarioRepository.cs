using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Itanio.Autenticacao.Entidades;
using Microsoft.AspNet.Identity;

namespace Itanio.Autenticacao.Repositorios
{
    public class UsuarioRepository : IUserRoleStore<Usuario, Guid>,
        IUserPasswordStore<Usuario, Guid>,
        IQueryableUserStore<Usuario, Guid>,
        IUserStore<Usuario, Guid>,
        IUserLockoutStore<Usuario, Guid>,
        IUserTwoFactorStore<Usuario, Guid>
    {
        private GerenciadorConexao _gerenciadorConexao;

        public UsuarioRepository()
            : this(new GerenciadorConexao())
        {
        }

        public UsuarioRepository(GerenciadorConexao gerenciadorConexao)
        {
            _gerenciadorConexao = gerenciadorConexao;
        }

        public IQueryable<Usuario> Users => ListarTodos().AsQueryable();

        public Task<DateTimeOffset> GetLockoutEndDateAsync(Usuario user)
        {
            return
                Task.FromResult(user.DataHoraFimBloqueio.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.DataHoraFimBloqueio.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task SetLockoutEndDateAsync(Usuario user, DateTimeOffset lockoutEnd)
        {
            user.DataHoraFimBloqueio = lockoutEnd.UtcDateTime;
            Salvar(user);
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(Usuario user)
        {
            user.QuantidadeFalhasConsecutivas++;
            Salvar(user);
            return Task.FromResult(user.QuantidadeFalhasConsecutivas);
        }

        public Task ResetAccessFailedCountAsync(Usuario user)
        {
            user.QuantidadeFalhasConsecutivas = 0;
            Salvar(user);
            return Task.FromResult(user.QuantidadeFalhasConsecutivas);
        }

        public Task<int> GetAccessFailedCountAsync(Usuario user)
        {
            return Task.FromResult(user.QuantidadeFalhasConsecutivas);
        }

        public Task<bool> GetLockoutEnabledAsync(Usuario user)
        {
            return Task.FromResult(user.BloqueioHabilitado);
        }

        public Task SetLockoutEnabledAsync(Usuario user, bool enabled)
        {
            user.BloqueioHabilitado = enabled;
            Salvar(user);
            return Task.FromResult(0);
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash)
        {
            user.Senha = passwordHash;
            Salvar(user);
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(Usuario user)
        {
            return Task.FromResult(user.Senha);
        }

        public Task<bool> HasPasswordAsync(Usuario user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Senha));
        }

        public Task AddToRoleAsync(Usuario user, string roleName)
        {
            throw new NotSupportedException();
        }

        public Task RemoveFromRoleAsync(Usuario user, string roleName)
        {
            throw new NotSupportedException();
        }

        public Task<IList<string>> GetRolesAsync(Usuario user)
        {
            var nomesPermissoes = user.GruposAcesso
                .Where(g => g.Ativo)
                .SelectMany(g => g.Permissoes)
                .Select(p => p.Name).ToList();

            return Task.FromResult<IList<string>>(nomesPermissoes);
        }

        public Task<bool> IsInRoleAsync(Usuario user, string roleName)
        {
            return Task.FromResult(GetRolesAsync(user).Result.Contains(roleName));
        }

        public Task CreateAsync(Usuario user)
        {
            Salvar(user);
            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(Usuario user)
        {
            Salvar(user);
            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(Usuario user)
        {
            Excluir(user.Id);
            return Task.FromResult<object>(null);
        }

        public Task<Usuario> FindByIdAsync(Guid userId)
        {
            return Task.FromResult(ObterPorId(userId));
        }

        public Task<Usuario> FindByNameAsync(string userName)
        {
            return Task.FromResult(ObterPorEmail(userName));
        }

        public void Dispose()
        {
            if (_gerenciadorConexao != null)
            {
                _gerenciadorConexao.Dispose();
                _gerenciadorConexao = null;
            }
        }

        public Task SetTwoFactorEnabledAsync(Usuario user, bool enabled)
        {
            user.DuplaVerificacaoHabilitada = enabled;
            Salvar(user);
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(Usuario user)
        {
            return Task.FromResult(user.DuplaVerificacaoHabilitada);
        }

        #region queries

        public ICollection<Usuario> ListarTodos()
        {
            return _gerenciadorConexao.Conexao.Query<Usuario>(SQL_ListarTodos).ToList();
        }

        public Usuario ObterPorId(Guid id)
        {
            var resultados = _gerenciadorConexao.Conexao.QueryMultiple(SQL_ObterPorId
                                                                       + SQL_ListarGruposPorUsuario
                                                                       + SQL_ListarPermissoesPorUsuario, new {id});
            var usuario = resultados.Read<Usuario>().SingleOrDefault();

            if (usuario != null)
            {
                var grupos = resultados.Read<GrupoAcesso>().ToList();
                usuario.GruposAcesso.AddRange(grupos);
                var permissoes = resultados.Read<PermissaoPorGrupo>();
                var permissoesPorGrupo = permissoes.GroupBy(x => x.IdGrupoAcesso);
                foreach (var item in permissoesPorGrupo)
                    usuario.GruposAcesso.Single(x => x.Id == item.Key).Permissoes = item
                        .ToList()
                        .ConvertAll(
                            x => new Permissao(x.Id, x.Nome));
            }

            return usuario;
        }

        public void Salvar(Usuario user)
        {
            if (user.Id == Guid.Empty)
            {
                var guid = _gerenciadorConexao.Conexao.Query<Guid>(SQL_Inserir, user).Single();
                user.Id = guid;
            }
            else
            {
                _gerenciadorConexao.Conexao.Execute(SQL_Alterar, user);
            }
        }

        public void Excluir(Guid id)
        {
            _gerenciadorConexao.Conexao.Execute(SQL_Excluir, new {id});
        }

        public Usuario ObterPorEmail(string email)
        {
            var usuario = _gerenciadorConexao.Conexao.Query<Usuario>(SQL_ObterPorEmail, new {email}).SingleOrDefault();
            if (usuario != null) usuario = ObterPorId(usuario.Id);
            return usuario;
        }

        private const string SQL_ListarTodos = @"SELECT 
                                                    *
                                                FROM
                                                    Usuario";

        private const string SQL_ObterPorId = @"SELECT 
                                                *
                                           FROM
                                                Usuario
                                           WHERE 
                                                ID = @ID";

        private const string SQL_ObterPorEmail = @"SELECT 
                                                *
                                           FROM
                                                Usuario
                                           WHERE 
                                                Email = @Email";


        private const string SQL_ListarGruposPorUsuario = @"SELECT 
                                                            ga.*
                                                        FROM 
                                                            GrupoAcesso ga
                                                        inner join
                                                            GrupoAcessoUsuario gu on ga.id = gu.idGrupoAcesso
                                                        where
                                                            gu.idUsuario = @Id";

        private const string SQL_ListarPermissoesPorUsuario = @"SELECT 
                                                                    P.*,
                                                                    gp.idGrupoAcesso
                                                                FROM
                                                                    Persmissao p
                                                                inner join
                                                                    GrupoAcessoPermissao gp on p.id = gp.idPermissao
                                                                inner join
                                                                    GrupoAcessoUsuario gu on gu.idGrupoAcesso = gp.idGrupoAcesso
                                                                where gu.idUsuario = @id";

        private const string SQL_Inserir = @"INSERT INTO 
                                                Usuario
                                             (
                                                Email
                                                ,Senha
                                                ,Ativo
                                             )
                                             VALUES
                                             (
                                                @Email
                                                ,@Senha
                                                1
                                             ); 
                                             select @SCOPE_IDENTITY();";

        private const string SQL_Alterar = @"UPDATE
                                                Usuario
                                             SET
                                                 Senha = @Senha
                                                ,Ativo = @Ativo
                                                ,DataHoraFimBloqueio = @DataHoraFimBloqueio
                                                ,QuantidadeFalhasConsecutivas = @QuantidadeFalhasConsecutivas
                                                ,BloqueioHabilitado = @BloqueioHabilitado
                                                ,DuplaVerificacaoHabilitada = @DuplaVerificacaoHabilitada
                                                ,DataHoraUltimaAlteracao = GETDATE()
                                            WHERE
                                                ID = @ID";


        private const string SQL_Excluir = @"DELETE FROM Usuario WHERE Id = @Id";

        #endregion
    }
}