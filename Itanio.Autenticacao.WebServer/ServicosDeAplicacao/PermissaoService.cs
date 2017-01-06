using Itanio.Autenticacao.Entidades;
using Itanio.Autenticacao.Repositorios;
using Microsoft.AspNet.Identity;

namespace Itanio.Autenticacao.WebServer.ServicosDeAplicacao
{
    public class PermissaoService : RoleManager<Permissao, int>
    {
        public PermissaoService(PermissaoRepository repo)
            : base(repo)
        {
        }
    }
}