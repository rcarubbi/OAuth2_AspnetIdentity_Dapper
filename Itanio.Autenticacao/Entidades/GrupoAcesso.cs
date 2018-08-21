using System.Collections.Generic;

namespace Itanio.Autenticacao.Entidades
{
    public class GrupoAcesso
    {
        public GrupoAcesso()
        {
        }

        public GrupoAcesso(int id, string nome)
        {
            Nome = nome;
            Id = id;
        }


        public bool Ativo { get; set; }

        public int Id { get; internal set; }

        public string Nome { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public List<Permissao> Permissoes { get; set; }
    }
}