using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
