using Microsoft.AspNet.Identity;

namespace Itanio.Autenticacao.Entidades
{
    public class Permissao : IRole<int>
    {
        public Permissao(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id
        {
            get;
            internal set;
        }

        public string Nome { get; set; }

        public string Name
        {
            get
            {
                return Nome;
            }
            set
            {
                Nome = value;
            }
        }
    }
}
