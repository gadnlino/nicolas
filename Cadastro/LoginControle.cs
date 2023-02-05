using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Aula3108.Cadastro
{
    class LoginControle
    {
        public bool tem = false;
        public String mensagem = "";
		readonly SqlCommand cmd = new SqlCommand();
		readonly Conexao con = new Conexao();
        SqlDataReader dr;
        public bool VerificarLogin(String Login, String Senha)
        {
            cmd.CommandText="SELECT * from Usuário where Login = @Login and Senha = @Senha";
            cmd.Parameters.AddWithValue("@Login", Login);
            cmd.Parameters.AddWithValue("@Senha", Senha);
            try
            {
                cmd.Connection=con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) 
                {
                    tem=true;
                }

            }
            catch(SqlException)
            {
                this.mensagem="Erro com banco de dados!";
            }

            return tem;
        }

		internal bool verificarLogin(string login, string senha)
		{
			throw new NotImplementedException();
		}

		public string Cadastrar(String Login, String Senha, String confSenha)
        {

            return mensagem;
        }
    }
}