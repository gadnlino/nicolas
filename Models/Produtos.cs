using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace Aula3108.Models
{
    public class Produtos
    {
        private readonly static string _conn = DbConnectionString.GetDbConnectionString();

        public Produtos() { }

        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório", AllowEmptyStrings = false)]
        public string NomeProduto { get; set; }
        public int QuantEstoq { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double VlrProduto { get; set; }
        public double Peso { get; set; }

        public Produtos(int idproduto, string nomeproduto, int quantestoq, double vlrproduto, double peso)
        {
            IdProduto = idproduto;
            NomeProduto = nomeproduto;
            QuantEstoq = quantestoq;
            VlrProduto = vlrproduto;
            Peso = peso;
        }

        public static List<Produtos> GetProdutos()
        {
            var listaProdutos = new List<Produtos>();

            var rSQL = "SELECT * FROM Produto";

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
                                    listaProdutos.Add(new Produtos(
                                        Convert.ToInt32(dr["idProduto"]),
                                        dr["nomeProduto"].ToString(),
                                        Convert.ToInt16(dr["quantEstoq"]),
                                        Convert.ToDouble(dr["vlrProduto"]),
                                        Convert.ToInt16(dr["Peso"])));
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                throw;
            }
            return listaProdutos;
        }

        public static List<Produtos> GetProdutos(List<int> idProdutos)
        {
            var listaProdutos = new List<Produtos>();

            if(idProdutos.Count == 0)
            {
                return listaProdutos;
            }

            string rSQL = " SELECT idProduto, nomeProduto, quantEstoq,vlrProduto, Peso FROM Produto where ";

            var parameters = new List<SqlParameter>();

            for (int i = 0; i < idProdutos.Count; i++)
            {
                rSQL += $" idProduto = @idProduto{i} ";

                if (i == idProdutos.Count - 1)
                {
                    rSQL += " ; ";
                }
                else
                {
                    rSQL += " or ";
                }

                parameters.Add(new SqlParameter($"@idProduto{i}", idProdutos.ElementAt(i)));
            }

            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();

                    using (var cmd = new SqlCommand(rSQL, cn))
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());

                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                                while (dr.Read())
                                {
                                    listaProdutos.Add(new Produtos(
                                        Convert.ToInt32(dr["idProduto"]),
                                        dr["nomeProduto"].ToString(),
                                        Convert.ToInt16(dr["quantEstoq"]),
                                        Convert.ToDouble(dr["vlrProduto"]),
                                        Convert.ToInt16(dr["Peso"])));
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                throw;
            }

            return listaProdutos;
        }

        public void Salvar()
        {
            string sql;
            if (IdProduto == 0)
            {
                sql = "INSERT INTO Produto (nomeproduto, quantestoq, vlrproduto, peso)" +
                "VALUES(@nomeproduto, @quantestoq, @vlrproduto, @peso)";
            }
            else
            {
                sql = "UPDATE Produto set nomeproduto=@nomeproduto, quantestoq=@quantestoq, vlrproduto=@vlrproduto, peso=@peso " +
                     "WHERE idProduto = " + IdProduto;
            }
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@nomeproduto", NomeProduto);
                        cmd.Parameters.AddWithValue("@quantestoq", QuantEstoq);
                        cmd.Parameters.AddWithValue("@vlrproduto", VlrProduto);
                        cmd.Parameters.AddWithValue("@peso", Peso);
                        //cmd.Parameters.AddWithValue("@unidade", Unidade);
                        //cmd.Parameters.AddWithValue("@loja", Loja);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                throw;
            }
        }

        public static void Excluir(int idproduto)
        {
            var sql = "DELETE FROM Produto WHERE idProduto = " + idproduto;
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
                throw;
            }
        }

        public static Produtos GetProduto(int idproduto)
        {
            var sql = "SELECT * FROM Produto WHERE idProduto = " + idproduto;

            Produtos returnValue = null;

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
                                    returnValue = new Produtos
                                    {
                                        IdProduto = idproduto,
                                        NomeProduto = dr["nomeProduto"].ToString(),
                                        QuantEstoq = Convert.ToInt16(dr["quantEstoq"]),
                                        VlrProduto = Convert.ToDouble(dr["vlrProduto"]),
                                        Peso = Convert.ToInt16(dr["Peso"])
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                throw;
            }

            return returnValue;
        }

        public static void ReduzEstoqueProduto(int idproduto, int quantidadeUtilizada, SqlConnection connection, SqlTransaction transaction)
        {
            var sql = "update Produto set quantEstoq = (quantEstoq - @quantidadeUtilizada) where idProduto = @idProduto;";

            try
            {
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Transaction = transaction;

                    cmd.Parameters.AddWithValue("quantidadeUtilizada", quantidadeUtilizada);
                    cmd.Parameters.AddWithValue("idProduto", idproduto);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
                throw;
            }
        }
    }
}