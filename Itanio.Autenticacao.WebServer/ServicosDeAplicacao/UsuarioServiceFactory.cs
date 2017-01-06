using Itanio.Autenticacao.Entidades;
using Itanio.Autenticacao.Repositorios;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace Itanio.Autenticacao.WebServer.ServicosDeAplicacao
{
    public static class UsuarioServiceFactory
    {
        public static UsuarioService Criar(IdentityFactoryOptions<UsuarioService> options, IOwinContext context)
        {
            var service = new UsuarioService(new UsuarioRepository(context.Get<GerenciadorConexao>()));

            // Configure validation logic for usernames
            service.UserValidator = new UserValidator<Usuario, Guid>(service)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            service.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure AppMember lockout defaults
            service.UserLockoutEnabledByDefault = false;
            service.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            service.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the AppMember
            // You can write your own provider and plug it in here.
            service.RegisterTwoFactorProvider("Codigo por E-mail", new EmailTokenProvider<Usuario, Guid>
            {
                Subject = "Código de acesso",
                BodyFormat = "Seu código de acesso é {0}"
            });
            service.EmailService = new NotificacaoService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                service.UserTokenProvider = new DataProtectorTokenProvider<Usuario, Guid>(dataProtectionProvider.Create("Autenticação Itanio"));
            }
            return service;
        }
    }
}