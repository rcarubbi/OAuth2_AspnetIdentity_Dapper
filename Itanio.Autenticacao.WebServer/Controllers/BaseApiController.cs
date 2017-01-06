using Itanio.Autenticacao.WebServer.ServicosDeAplicacao;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Itanio.Autenticacao.WebServer.Controllers
{
    public class BaseApiController : ApiController
    {
        public UsuarioService UsuarioService
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Get<UsuarioService>();
            }
        }
        public string UsuarioLogado
        {
            get
            {
                ClaimsIdentity signedInIdentity = Request.GetOwinContext().Request.User.Identity as ClaimsIdentity;
                if (signedInIdentity != null)
                {
                    return signedInIdentity.Claims.Single(c => c.Type == ClaimTypes.Name).Value;
                }
                else
                    return string.Empty;
            }
        }

        public string[] Permissoes
        {
            get {
                ClaimsIdentity signedInIdentity = Request.GetOwinContext().Request.User.Identity as ClaimsIdentity;
                if (signedInIdentity != null)
                {
                    return signedInIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
                }
                else
                    return new string[] { };
            }
        }
    }
}