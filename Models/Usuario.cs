using Aula3108.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aula3108.Models
{
    public class Usuario
    {
        //private readonly static string _conn = @"Data Source=(localdb)\SQLSEXPRESS;
        //    Initial Catalog=BD;
        //    Integrated Security=True;
        //    Connect Timeout=30;
        //    Encrypt=False;
        //    TrustServerCertificate=False;
        //    ApplicationIntent=ReadWrite;
        //    MultiSubnetFailover=False";

        private readonly static string _conn = DbConnectionString.GetDbConnectionString();

        internal string nomeProduto;
		internal short quantEstoq;
		internal double vlrProduto;

		public string Nome { get; set; }
        public int CPFCNPJ { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
        public int Idade { get; set; }   
        public string Login { get; set; }
        private string Senha { get; set; }
		public int IdProduto { get; internal set; }
		public short Unidade { get; internal set; }
		public double Peso { get; internal set; }
		public string Loja { get; internal set; }

		public Usuario() { }

        public Usuario( string nome, int cpfcnpj, string email, int telefone, int idade,
            string login, string senha)
        {
            Nome = nome;
            CPFCNPJ = cpfcnpj;
            Email = email;
            Telefone = telefone;
            Idade = idade;
            Login = login;
            Senha = senha;
        }

        public static List<Usuario> GetCadastro(int cPF_CNPJ)
        {
            var listaCadastro = new List<Usuario>();
            var rSQL = "SELECT * FROM Usuário";
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(rSQL, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                                while (dr.Read())
                                {
                                    listaCadastro.Add(new Usuario (
                                        dr["Nome"].ToString(),
                                        Convert.ToInt16(dr["CPFCNPJ"]),
                                        dr["Email"].ToString(),
                                        Convert.ToInt16(dr["Telefone"]),
                                        Convert.ToInt16(dr["Idade"]),
                                        dr["Login"].ToString(),
                                        dr["Senha"].ToString()
                                        ));
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
            return listaCadastro;
        }

        public void Salvar()
        {
            var sql = "";
            if (CPFCNPJ == 0)
            {
                sql = "INSERT INTO tb_Usuário (nome, cpfcnpj, email, telefone, idade, login, senha) " +
                "VALUES(@nome, @cpfcnpj, @email, @telefone, @idade, @login, @senha)";
            }
            else
            {
                sql = "UPDATE tb_Usuário set nome=@nome, email=@email, telefone=@telefone, " +
                    "idade=@idade, login=@login, senha=@senha, " +
                    "WHERE cpfcnpj = " + CPFCNPJ;
            }
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@telefone", Telefone);
                        cmd.Parameters.AddWithValue("@idade", Idade);
                        cmd.Parameters.AddWithValue("@login", Login);
                        cmd.Parameters.AddWithValue("@senha", Senha);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
        }

        public void Excluir()
        {
            var sql = "DELETE FROM tb_Usuário WHERE cpfcnpf = " + CPFCNPJ;
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
        }

            public void GetVeiculo(int id)
        {
            var sql = "SELECT * FROM tb_Usuário WHERE cpfcnpj = " + CPFCNPJ;
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    CPFCNPJ = Convert.ToInt16(dr["CPFCNPJ"]);
                                    Nome = dr["Nome"].ToString();
                                    Email = dr["Email"].ToString();
                                    Telefone = Convert.ToInt16(dr["Telefone"]);
                                    Idade = Convert.ToInt16(dr["Idade"]);
                                    Login = dr["Login"].ToString();
                                    Senha = dr["Senha"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
        }
    }
}