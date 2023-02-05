using Aula3108.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Aula3108.Cadastro
{
    public class Conexao
    {
		readonly SqlConnection con = new SqlConnection();
        
        public Conexao()
        {
			//con.ConnectionString = @"Data Source=PC\SQLEXPRESS;
   //         Initial Catalog=BD;
   //         Integrated Security=True;
   //         Connect Timeout=30;
   //         Encrypt=False;
   //         TrustServerCertificate=False;
   //         ApplicationIntent=ReadWrite;
   //         MultiSubnetFailover=False";

            con.ConnectionString = DbConnectionString.GetDbConnectionString();
        }

        public SqlConnection Conectar() 
        {
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
        public void Desconectar()
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}