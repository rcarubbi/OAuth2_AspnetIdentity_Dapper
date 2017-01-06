using Itanio.Autenticacao.Repositorios;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Itanio.Autenticacao.WebServer.ServicosDeAplicacao
{
    public class PermissaoServiceFactory
    {
        public static PermissaoService Criar(IdentityFactoryOptions<PermissaoService> options, IOwinContext context)
        {
            var manager = new PermissaoService(new PermissaoRepository(context.Get<GerenciadorConexao>()));
            return manager;
        }
    }
}