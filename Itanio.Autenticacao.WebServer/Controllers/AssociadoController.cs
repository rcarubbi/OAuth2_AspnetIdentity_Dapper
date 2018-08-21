using System.Collections.Generic;
using System.Web.Http;
using Itanio.Tool.GestaoAssociado.DataAccess;
using Itanio.Tool.GestaoAssociado.Dominio.Entidades;

namespace Itanio.Autenticacao.WebServer.Controllers
{
    [Authorize]
    public class AssociadoController : BaseApiController
    {
        private AssociadoRepository _repo;

        public AssociadoRepository Repository
        {
            get
            {
                if (_repo == null)
                    _repo = new AssociadoRepository();

                return _repo;
            }
        }

        [Authorize(Roles = "Listar Associados")]
        // GET api/Usuario
        public IEnumerable<Associado> Get()
        {
            return Repository.ListarTodos();
        }
    }
}