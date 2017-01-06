using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Itanio.Autenticacao.WebServer.ServicosDeAplicacao
{
    public class NotificacaoService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
           // TODO: Referenciar componente
           // IMailSender sender = new SmtpSender();
            var email = new System.Net.Mail.MailMessage
            {
                Body = message.Body,
                Subject = message.Subject,

            };

            email.To.Add(message.Destination);

            // sender.Send(email);

            return Task.FromResult<object>(null);
        }
    }
}