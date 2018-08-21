using System.Collections.Generic;
using Itanio.Tool.GestaoAssociado.Dominio;
using Itanio.Tool.GestaoAssociado.Dominio.Entidades;

namespace Itanio.Tool.GestaoAssociado.DataAccess
{
    public class AssociadoRepository : IAssociadoRepository
    {
        public void Salvar(Associado associado)
        {
        }

        public Associado ObterPorId(int id)
        {
            return new Associado();
        }

        public ICollection<Associado> ListarTodos()
        {
            return new List<Associado>();
        }


        public void Excluir(int id)
        {
        }
    }
}