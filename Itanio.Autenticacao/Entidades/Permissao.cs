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

        public string Nome { get; set; }

        public int Id { get; internal set; }

        public string Name
        {
            get => Nome;
            set => Nome = value;
        }
    }
}