namespace Itanio.Autenticacao.Entidades
{
    internal class PermissaoPorGrupo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int IdGrupoAcesso { get; set; }
    }
}