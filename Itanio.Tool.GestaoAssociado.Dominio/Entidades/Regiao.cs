namespace Itanio.Tool.GestaoAssociado.Dominio.Entidades
{
    public class Regiao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Pais Pais { get; set; }

    }
}