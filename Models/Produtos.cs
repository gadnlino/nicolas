using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Aula3108.Models
{
    public class Produtos
    {
        //private readonly static string _conn = @"Data Source=(localdb)\SQLEXPRESS;
        //    Initial Catalog=BD;
        //    Integrated Security=True;
        //    Connect Timeout=30;
        //    Encrypt=False;
        //    TrustServerCertificate=False;
        //    ApplicationIntent=ReadWrite;
        //    MultiSubnetFailover=False";

        private readonly static string _conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\guiav\Downloads\Aula3108 (0402)_3\Aula3108\App_Data\Database1.mdf"";Integrated Security=True";

        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório", AllowEmptyStrings = false)]
        public string NomeProduto { get; set; }
        public int QuantEstoq { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto", AllowEmptyStrings = false)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double VlrProduto { get; set; }
        public int Unidade { get; set; }
        public double Peso { get; set; }

        public Produtos() { }

        public Produtos(int idproduto, string nomeproduto, int quantestoq, double vlrproduto, int unidade, double peso)
        {
            IdProduto = idproduto;
            NomeProduto = nomeproduto;
            QuantEstoq = quantestoq;
            VlrProduto = vlrproduto;
            Unidade = unidade;
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
                                        Convert.ToInt16(dr["Unidade"]),
                                        Convert.ToInt16(dr["Peso"])));
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
            return listaProdutos;
        }

        public void Salvar()
        {
            string sql;
            if (IdProduto == 0)
            {
                sql = "INSERT INTO Produto (nomeproduto, quantestoq, vlrproduto, peso, unidade)" +
                "VALUES(@nomeproduto, @quantestoq, @vlrproduto, @peso, @unidade)";
            }
            else
            {
                sql = "UPDATE Produto set nomeproduto=@nomeproduto, quantestoq=@quantestoq, vlrproduto=@vlrproduto, peso=@peso, unidade=@unidade" +
                     "WHERE idproduto = " + IdProduto;
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
                        cmd.Parameters.AddWithValue("@unidade", Unidade);
                        cmd.Parameters.AddWithValue("@peso", Peso);
                        //cmd.Parameters.AddWithValue("@loja", Loja);
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
            var sql = "DELETE FROM Produto WHERE idproduto = " + IdProduto;
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

        public static Produtos GetProduto(int idproduto)
        {
            var sql = "SELECT * FROM Produto WHERE idproduto = " + idproduto;

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
                                        Unidade = Convert.ToInt16(dr["Unidade"]),
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
            }

            return returnValue;
        }
    }
}