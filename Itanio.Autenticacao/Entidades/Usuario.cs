using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Itanio.Autenticacao.Entidades
{
    public class Usuario : IUser<Guid>
    {
        public string Email { get; set; }

        public bool Ativo { get; set; }

        public List<GrupoAcesso> GruposAcesso { get; set; }

        public DateTime? DataHoraFimBloqueio { get; set; }

        public int QuantidadeFalhasConsecutivas { get; set; }

        public bool BloqueioHabilitado { get; set; }

        public string Senha { get; set; }

        public bool DuplaVerificacaoHabilitada { get; set; }
        public Guid Id { get; internal set; }

        public string UserName
        {
            get => Email;
            set => Email = value;
        }
    }
}