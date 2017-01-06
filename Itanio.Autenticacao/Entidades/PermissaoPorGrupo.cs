using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Autenticacao.Entidades
{
    internal class PermissaoPorGrupo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdGrupoAcesso { get; set; }

    }
}
