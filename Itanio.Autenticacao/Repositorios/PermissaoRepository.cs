using Dapper;
using Itanio.Autenticacao.Entidades;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Autenticacao.Repositorios
{
    public class PermissaoRepository : IQueryableRoleStore<Permissao, int>
    {
        public PermissaoRepository()
            : this(new GerenciadorConexao())
        {

        }

        public PermissaoRepository(GerenciadorConexao gerenciadorConexao)
        {
            _gerenciadorConexao = gerenciadorConexao;
        }

        private GerenciadorConexao _gerenciadorConexao;

        public IQueryable<Permissao> Roles
        {
            get
            {
                return ListarTodas().AsQueryable();
            }
        }

        private const string SQL_ListarTodas = @"SELECT * FROM Permissao";

        public Task UpdateAsync(Permissao role)
        {
            throw new NotSupportedException();
        }

        public Task CreateAsync(Permissao role)
        {
            throw new NotSupportedException();
        }

        public Task DeleteAsync(Permissao role)
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            if (_gerenciadorConexao != null)
            {
                _gerenciadorConexao.Dispose();
                _gerenciadorConexao = null;
            }
        }

        public Task<Permissao> FindByIdAsync(int roleId)
        {
            return Task.FromResult<Permissao>(ListarTodas().SingleOrDefault(p => p.Id == roleId));
        }

        public Task<Permissao> FindByNameAsync(string roleName)
        {
            return Task.FromResult<Permissao>(ListarTodas().SingleOrDefault(p => p.Nome == roleName));
        }

        public ICollection<Permissao> ListarTodas()
        {
            return _gerenciadorConexao.Conexao.Query<Permissao>(SQL_ListarTodas).ToList();
        }
    }
}
