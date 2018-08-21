using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Itanio.Autenticacao.Entidades;

namespace Itanio.Autenticacao.WebServer.Controllers
{
    [Authorize]
    public class UsuarioController : BaseApiController
    {
        [Authorize(Roles = "Listar Usuários")]
        // GET api/Usuario
        public IEnumerable<Usuario> Get()
        {
            return UsuarioService.Users;
        }


        // GET api/Usuario/5
        public IHttpActionResult Get(Guid id)
        {
            var usuario = UsuarioService.FindByIdAsync(id).Result;
            if (UsuarioLogado == usuario.Email || Permissoes.Contains("Listar Usuários"))
                return Ok(usuario);
            return Unauthorized();
        }

        // POST api/Usuario
        [AllowAnonymous]
        public IHttpActionResult Post(Usuario usuario)
        {
            usuario.Senha = UsuarioService.PasswordHasher.HashPassword(usuario.Senha);
            var resultado = UsuarioService.CreateAsync(usuario).Result;
            if (resultado.Succeeded)
                return CreatedAtRoute("DefaultApi", new {id = usuario.Id}, usuario);
            return BadRequest(string.Join(";", resultado.Errors));
        }

        [ActionName("AlterarSenha")]
        public IHttpActionResult AlterarSenha(Guid id, [FromBody] string senhaAtual, [FromBody] string novaSenha)
        {
            var usuario = UsuarioService.FindByIdAsync(id).Result;
            if (UsuarioLogado == usuario.Email)
            {
                var resultado = UsuarioService.ChangePasswordAsync(id,
                    UsuarioService.PasswordHasher.HashPassword(senhaAtual),
                    UsuarioService.PasswordHasher.HashPassword(novaSenha)).Result;

                if (resultado.Succeeded)
                    return Ok();
                return BadRequest(string.Join(";", resultado.Errors));
            }

            return Unauthorized();
        }

        // PUT api/Usuario/5
        public IHttpActionResult Put(Guid id, Usuario usuarioAlterado)
        {
            var usuario = UsuarioService.FindByIdAsync(id).Result;
            if (UsuarioLogado == usuario.Email || Permissoes.Contains("Alterar Usuários"))
            {
                var resultado = UsuarioService.UpdateAsync(usuarioAlterado).Result;
                if (resultado.Succeeded)
                    return Ok();
                return BadRequest(string.Join(";", resultado.Errors));
            }

            return Unauthorized();
        }


        [Authorize(Roles = "Alterar Usuários")]
        // DELETE api/Usuario/5
        public IHttpActionResult Delete(Guid id)
        {
            var resultado = UsuarioService.FindByIdAsync(id).ContinueWith(
                task => UsuarioService.DeleteAsync(task.Result)).Result.Result;

            if (resultado.Succeeded)
                return Ok();
            return BadRequest(string.Join(";", resultado.Errors));
        }
    }
}