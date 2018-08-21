using System;
using Itanio.Autenticacao.Entidades;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Itanio.Autenticacao.WebServer.ServicosDeAplicacao
{
    public class AutenticacaoService : SignInManager<Usuario, Guid>
    {
        public AutenticacaoService(UsuarioService usuarioService, IAuthenticationManager authenticationManager)
            : base(usuarioService, authenticationManager)
        {
        }
    }
}