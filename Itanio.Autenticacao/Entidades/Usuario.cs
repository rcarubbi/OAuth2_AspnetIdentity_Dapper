using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace Itanio.Autenticacao.Entidades
{
    public class Usuario : IUser<Guid>
    {
        public Guid Id { get; internal set; }

        public string Email { get; set; }

        public bool Ativo { get; set; }

        public List<GrupoAcesso> GruposAcesso { get; set; }

        public DateTime? DataHoraFimBloqueio { get; set; }

        public int QuantidadeFalhasConsecutivas { get; set; }

        public bool BloqueioHabilitado { get; set; }

        public string Senha { get; set; }

        public bool DuplaVerificacaoHabilitada { get; set; }

        public string UserName
        {
            get
            {
                return Email;
            }
            set
            {
                Email = value;
            }
        }
    }
}
