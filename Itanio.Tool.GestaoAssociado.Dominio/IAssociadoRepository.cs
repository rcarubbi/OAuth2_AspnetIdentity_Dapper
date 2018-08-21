using System.Collections.Generic;
using Itanio.Tool.GestaoAssociado.Dominio.Entidades;

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