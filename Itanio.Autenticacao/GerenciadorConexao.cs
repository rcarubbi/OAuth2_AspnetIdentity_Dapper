using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Itanio.Autenticacao
{
    public class GerenciadorConexao : IDisposable
    {
        private IDbConnection _conexao { get; set; }

       
        public IDbConnection Conexao
        {
            get
            {
                if (_conexao.State == ConnectionState.Closed)
                    _conexao.Open();

                return _conexao;
            }
        }

        public GerenciadorConexao()
        {
            _conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Itanio.Autenticacao.Server"].ConnectionString);
        }

        
        public void Dispose()
        {
            if (_conexao != null)
            {
                if (_conexao.State == ConnectionState.Open)
                {
                    _conexao.Close();
                    _conexao.Dispose();
                }
                _conexao = null;
            }
        }
    }
}
