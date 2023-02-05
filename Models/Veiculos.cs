using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aula3108.Models
{
    public class Veiculos
    {
        private readonly static string _conn = @"Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=AgenciaAuto;
            Integrated Security=True;
            Connect Timeout=30;
            Encrypt=False;
            TrustServerCertificate=False;
            ApplicationIntent=ReadWrite;
            MultiSubnetFailover=False";


        public int Id { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public short Ano { get; set; }
        public short Fabricacao { get; set; }
        public string Cor { get; set; }
        public string Combustivel { get; set; }
        public string Automatico { get; set; }
        public decimal Valor { get; set; }
        public string Ativo { get; set; }

        public Veiculos() { }

        public Veiculos(int id, string nome, string modelo, short ano, short fabricacao, string cor,
            string combustivel, string automatico, decimal valor, string ativo)
        {
            Id = id;
            Nome = nome;
            Modelo = modelo;
            Ano = ano;
            Fabricacao = fabricacao;
            Cor = cor;
            Combustivel = combustivel;
            Automatico = automatico;
            Valor = valor;
            Ativo = ativo;
        }

        public static List<Veiculos> GetCarros()
        {
            var listaCarros = new List<Veiculos>();
            var rSQL = "SELECT * FROM tb_Veiculos";
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
                                    listaCarros.Add(new Veiculos(
                                        Convert.ToInt32(dr["Id"]),
                                        dr["Nome"].ToString(),
                                        dr["Modelo"].ToString(),
                                        Convert.ToInt16(dr["Ano"]),
                                        Convert.ToInt16(dr["Fabricacao"]),
                                        dr["Cor"].ToString(),
                                        dr["Combustivel"].ToString(),
                                        dr["Automatico"].ToString(),
                                        Convert.ToDecimal(dr["Valor"]),
                                        dr["Ativo"].ToString()
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
            return listaCarros;
        }

        public void Salvar()
        {
            var sql = "";
            if (Id == 0)
            {
                sql = "INSERT INTO tb_Veiculos (nome, modelo, ano, fabricacao, cor, combustivel, automatico, valor, ativo) " +
                "VALUES(@nome, @modelo, @ano, @fabricacao, @cor, @combustivel, @automatico, @valor, @ativo)";
            }
            else
            {
                sql = "UPDATE tb_Veiculos set nome=@nome, modelo=@modelo, ano=@ano, fabricacao=@fabricacao, " +
                    "cor=@cor, combustivel=@combustivel, automatico=@automatico, valor=@valor, " +
                    "ativo=@ativo WHERE id = " + Id;
            }
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@modelo", Modelo);
                        cmd.Parameters.AddWithValue("@ano", Ano);
                        cmd.Parameters.AddWithValue("@fabricacao", Fabricacao);
                        cmd.Parameters.AddWithValue("@cor", Cor);
                        cmd.Parameters.AddWithValue("@combustivel", Combustivel);
                        cmd.Parameters.AddWithValue("@automatico", Automatico);
                        cmd.Parameters.AddWithValue("@valor", Valor);
                        cmd.Parameters.AddWithValue("@ativo", Ativo);
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
            var sql = "DELETE FROM tb_Veiculos WHERE id = " + Id;
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
            var sql = "SELECT * FROM tb_Veiculos WHERE id = " + id;
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
                                    Id = id;
                                    Nome = dr["Nome"].ToString();
                                    Modelo = dr["Modelo"].ToString();
                                    Ano = Convert.ToInt16(dr["Ano"]);
                                    Fabricacao = Convert.ToInt16(dr["Fabricacao"]);
                                    Cor = dr["Cor"].ToString();
                                    Combustivel = dr["Combustivel"].ToString();
                                    Automatico = dr["Automatico"].ToString();
                                    Valor = Convert.ToDecimal(dr["Valor"]);
                                    Ativo = dr["Ativo"].ToString();
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