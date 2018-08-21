using Itanio.Tool.GestaoAssociado.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itanio.Tool.GestaoAssociado.Dominio.Entidades;

namespace Itanio.Tool.GestalAssociado.DataAccess
{
    public class AssociadoRepository : IAssociadoRepository
    {
        public void Salvar(Associado associado)
        {
        }

        public Associado ObterPorId(int id)
        {
            return null;
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
