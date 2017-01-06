using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Itanio.Autenticacao.WebServer.ServicosDeAplicacao
{
    public static class AutenticacaoServiceFactory
    {
        public static AutenticacaoService Criar(IdentityFactoryOptions<AutenticacaoService> options, IOwinContext context)
        {
            return new AutenticacaoService(context.GetUserManager<UsuarioService>(), context.Authentication);
        }
    }
}