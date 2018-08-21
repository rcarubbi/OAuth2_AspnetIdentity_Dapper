namespace Itanio.Autenticacao.Factories
{
    public static class SimpleFactory<T> where T : new()
    {
        public static T Criar()
        {
            return new T();
        }
    }
}