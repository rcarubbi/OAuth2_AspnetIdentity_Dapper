using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Itanio.Autenticacao.Entidades;
using Microsoft.AspNet.Identity;

namespace Itanio.Autenticacao.Repositorios
{
    public class PermissaoRepository : IQueryableRoleStore<Permissao, int>
    {
        private const string SQL_ListarTodas = @"SELECT * FROM Permissao";

        private GerenciadorConexao _gerenciadorConexao;

        public PermissaoRepository()
            : this(new GerenciadorConexao())
        {
        }

        public PermissaoRepository(GerenciadorConexao gerenciadorConexao)
        {
            _gerenciadorConexao = gerenciadorConexao;
        }

        public IQueryable<Permissao> Roles => ListarTodas().AsQueryable();

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
            return Task.FromResult(ListarTodas().SingleOrDefault(p => p.Id == roleId));
        }

        public Task<Permissao> FindByNameAsync(string roleName)
        {
            return Task.FromResult(ListarTodas().SingleOrDefault(p => p.Nome == roleName));
        }

        public ICollection<Permissao> ListarTodas()
        {
            return _gerenciadorConexao.Conexao.Query<Permissao>(SQL_ListarTodas).ToList();
        }
    }
}