namespace Itanio.Tool.GestaoAssociado.Dominio.Entidades
{
    public class Telefone
    {
        public int Id { get; set; }

        public int Numero { get; set; }

        public int DDD { get; set; }

        public int? DDI { get; set; }

        public int? Ramal { get; set; }

        public TipoTelefone Tipo { get; set; }
    }
}