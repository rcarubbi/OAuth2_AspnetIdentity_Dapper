using Itanio.Autenticacao.Entidades;
using Itanio.Autenticacao.Repositorios;
using Microsoft.AspNet.Identity;
using System;

namespace Itanio.Autenticacao.WebServer.ServicosDeAplicacao
{
    public class UsuarioService : UserManager<Usuario, Guid>
    {
        public UsuarioService(UsuarioRepository repo)
            : base(repo)
        {


        }

        public UsuarioService()
            : base(new UsuarioRepository())
        {

        }
    }
}