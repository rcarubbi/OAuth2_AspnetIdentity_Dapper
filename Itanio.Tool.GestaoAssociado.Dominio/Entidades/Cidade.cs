namespace Itanio.Tool.GestaoAssociado.Dominio.Entidades
{
    public class Cidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Regiao Regiao { get; set; }
    }
}