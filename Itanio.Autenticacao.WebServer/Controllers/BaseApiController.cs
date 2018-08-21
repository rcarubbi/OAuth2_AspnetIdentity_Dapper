using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Itanio.Autenticacao.WebServer.ServicosDeAplicacao;
using Microsoft.AspNet.Identity.Owin;

namespace Itanio.Autenticacao.WebServer.Controllers
{
    public class BaseApiController : ApiController
    {
        public UsuarioService UsuarioService => HttpContext.Current.GetOwinContext().Get<UsuarioService>();

        public string UsuarioLogado
        {
            get
            {
                var signedInIdentity = Request.GetOwinContext().Request.User.Identity as ClaimsIdentity;
                if (signedInIdentity != null)
                    return signedInIdentity.Claims.Single(c => c.Type == ClaimTypes.Name).Value;
                return string.Empty;
            }
        }

        public string[] Permissoes
        {
            get
            {
                var signedInIdentity = Request.GetOwinContext().Request.User.Identity as ClaimsIdentity;
                if (signedInIdentity != null)
                    return signedInIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
                return new string[] { };
            }
        }
    }
}