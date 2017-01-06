using Itanio.Autenticacao.Factories;
using Itanio.Autenticacao.WebServer.ServicosDeAplicacao;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Itanio.Autenticacao.WebServer
{
    public partial class Startup
    {


        public void ConfigureAuth(IAppBuilder app)
        {
         
            // Configure the db context, AppMember manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(SimpleFactory<GerenciadorConexao>.Criar);
            app.CreatePerOwinContext<UsuarioService>(UsuarioServiceFactory.Criar);
            app.CreatePerOwinContext<AutenticacaoService>(AutenticacaoServiceFactory.Criar);
            app.CreatePerOwinContext<PermissaoService>(PermissaoServiceFactory.Criar);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                Provider = new OAuthAuthorizationServerProvider()
                {
                    OnValidateClientAuthentication = async ctx =>
                    {
                        await Task.Run(() => ctx.Validated());
                    },
                    OnGrantResourceOwnerCredentials = async ctx =>
                    {
                        await Task.Run(() =>
                        {
                            var autenticacaoService = ctx.OwinContext.Get<AutenticacaoService>();
                            var status = autenticacaoService.PasswordSignIn(ctx.UserName, ctx.Password, false, false);
                            if (status != SignInStatus.Success)
                            {
                                ctx.Rejected();
                                return;
                            }

                            ClaimsIdentity identity = CarregarPermissoes(ctx);
                            ctx.Validated(identity);
                        });
                    }
                }
            });


            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            
            
        }

        private static ClaimsIdentity CarregarPermissoes(OAuthGrantResourceOwnerCredentialsContext ctx)
        {
            var usuarioService = ctx.OwinContext.Get<UsuarioService>();
            var usuario = usuarioService.FindByName(ctx.UserName);
            var permissoes = usuarioService.GetRoles(usuario.Id);
            var claims = new List<Claim> {
                                    new Claim(ClaimTypes.Name, ctx.UserName),
                            };

            foreach (var permissao in permissoes)
            {
                claims.Add(new Claim(ClaimTypes.Role, permissao));
            }

            var identity = new ClaimsIdentity(claims.ToArray(), ctx.Options.AuthenticationType);
            return identity;
        }
    }
}