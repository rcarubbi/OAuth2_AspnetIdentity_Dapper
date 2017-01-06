namespace Itanio.Tool.GestaoAssociado.Dominio.Entidades
{
    public class Endereco
    {
        public int Id { get; set; }

        public string Logradouro { get; set; }

        public int? Numero { get; set; }

        public string Bairro { get; set; }

        public Cidade Cidade { get; set; }

        public Regiao Regiao { get; set; }

        public Pais Pais { get; set; }

        public string Complemento { get; set; }

        public string CEP { get; set; }

        public TipoEndereco Tipo { get; set; }

        public bool ReceberCorrespondencia { get; set; }
    }
}