using Itanio.Autenticacao.Entidades;
using Itanio.Autenticacao.Repositorios;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
namespace Itanio.Autenticacao.WebServer.Controllers
{
    [Authorize(Roles = "Listar Grupos de Acesso")]
    public class GrupoAcessoController : BaseApiController
    {
        private GrupoAcessoRepository _repo;
        public GrupoAcessoRepository Repository {
            get
            {
                if (_repo == null)
                    _repo = new GrupoAcessoRepository();

                return _repo;
            }
        }

        public IEnumerable<GrupoAcesso> Get()
        {
            return Repository.ListarTodos();
        }

        
        public IHttpActionResult Get(int id)
        {
            return Ok(Repository.ObterPorId(id));
        }

        public IHttpActionResult Post(GrupoAcesso grupo)
        {
            try
            {
                Repository.Salvar(grupo);
                return CreatedAtRoute("DefaultApi", new { id = grupo.Id }, grupo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put(GrupoAcesso grupo)
        {
            try
            {
                Repository.Salvar(grupo);
                return Ok(grupo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                Repository.Excluir(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [ActionName("AdicionarUsuario")]
        [HttpPost]
        public IHttpActionResult AdicionarUsuario([FromBody]int idGrupoAcesso, [FromBody]Guid idUsuario)
        {
            var grupo = Repository.ObterPorId(idGrupoAcesso);
            if (!grupo.Usuarios.Any(u => u.Id == idUsuario))
            {
                Repository.AdicionarUsuario(idUsuario, idGrupoAcesso);
                return Ok();
            }
            else
                return BadRequest();
        }

        [ActionName("RemoverUsuario")]
        [HttpPost]
        public IHttpActionResult RemoverUsuario([FromBody]int idGrupoAcesso, [FromBody]Guid idUsuario)
        {
            var grupo = Repository.ObterPorId(idGrupoAcesso);
            if (grupo.Usuarios.Any(u => u.Id == idUsuario))
            {
                Repository.RemoverUsuario(idUsuario, idGrupoAcesso);
                return Ok();
            }
            else
                return BadRequest();
        }

        [ActionName("AdicionarPermissao")]
        [HttpPost]
        public IHttpActionResult AdicionarPermissao([FromBody]int idGrupoAcesso, [FromBody]int idPermissao)
        {
            var grupo = Repository.ObterPorId(idGrupoAcesso);
            if (!grupo.Permissoes.Any(p => p.Id == idPermissao))
            {
                Repository.AdicionarPermissao(idPermissao, idGrupoAcesso);
                return Ok();
            }
            else
                return BadRequest();
        }

        [ActionName("RemoverPermissao")]
        [HttpPost]
        public IHttpActionResult RemoverPermissao([FromBody]int idGrupoAcesso, [FromBody]int idPermissao)
        {
            var grupo = Repository.ObterPorId(idGrupoAcesso);
            if (grupo.Permissoes.Any(p => p.Id == idPermissao))
            {
                Repository.RemoverPermissao(idPermissao, idGrupoAcesso);
                return Ok();
            }
            else
                return BadRequest();
        }

    }
}
