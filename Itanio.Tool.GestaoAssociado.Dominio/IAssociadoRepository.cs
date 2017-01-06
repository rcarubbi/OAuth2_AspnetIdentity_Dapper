using Itanio.Tool.GestaoAssociado.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Tool.GestaoAssociado.Dominio
{
    public interface IAssociadoRepository
    {
        void Salvar(Associado associado);

        Associado ObterPorId(int id);

        ICollection<Associado> ListarTodos();

        void Excluir(int id);


    }
}
