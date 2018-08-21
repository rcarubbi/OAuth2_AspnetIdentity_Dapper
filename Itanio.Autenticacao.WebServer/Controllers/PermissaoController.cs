using System.Collections.Generic;
using System.Web.Http;
using Itanio.Autenticacao.Entidades;
using Itanio.Autenticacao.Repositorios;

namespace Itanio.Autenticacao.WebServer.Controllers
{
    [Authorize]
    public class PermissaoController : BaseApiController
    {
        private PermissaoRepository _repo;

        public PermissaoRepository Repository
        {
            get
            {
                if (_repo == null)
                    _repo = new PermissaoRepository();

                return _repo;
            }
        }

        [Authorize(Roles = "Listar Permissões")]
        // GET api/Usuario
        public IEnumerable<Permissao> Get()
        {
            return Repository.Roles;
        }
    }
}