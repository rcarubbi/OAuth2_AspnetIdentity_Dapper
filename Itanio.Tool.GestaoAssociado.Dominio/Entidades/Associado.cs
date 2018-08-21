using System;
using System.Collections.Generic;
using Itanio.Autenticacao.Entidades;

namespace Itanio.Tool.GestaoAssociado.Dominio.Entidades
{
    public class Associado
    {
        public int Id { get; set; }

        public Usuario Conta { get; set; }

        public string Email
        {
            get => Conta?.Email;
            set
            {
                if (Conta != null)
                    Conta.Email = value;
            }
        }

        public FormaTratamento FormaTratamento { get; set; }

        public Sexo Sexo { get; set; }

        public EstadoCivil? EstadoCivil { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string RG { get; set; }

        public string CPF { get; set; }

        public List<Telefone> Telefones { get; set; }

        public List<Endereco> Enderecos { get; set; }

        public List<Categoria> Categorias { get; set; }

        public bool ReceberEmail { get; set; }

        public bool ReceberSMS { get; set; }

        public string CRM { get; set; }

        public Regiao UFCRM { get; set; }

        public string Faculdade { get; set; }

        public int AnoFormatura { get; set; }

        public bool Especialista { get; set; }

        public int AnoTitulo { get; set; }
    }
}