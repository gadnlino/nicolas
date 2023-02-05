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
    public class CarrinhoProduto
    {
        public CarrinhoProduto() { }

        public int IdCarrinho { get; set; }

        public int IdProduto { get; set; }

        public decimal Quantidade { get; set; }

        public decimal VlrUnitarioProduto { get; set; }
    }

    public class RepresentacaoProdutoCarrinho
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotalProduto { get; set; }
    }

    public class RepresentacaoCarrinho
    {
        public decimal ValorTotalCarrinho { get; set; }
        public List<RepresentacaoProdutoCarrinho> ListaProdutos { get; set; }
    }

    public class Carrinho
    {
        private readonly static string _conn = DbConnectionString.GetDbConnectionString();

        public int IdCarrinho { get; set; }
        public DateTime DataCriacao { get; set; }

        public Carrinho()
        {
            DataCriacao = DateTime.Now;
        }

        public static Carrinho GetCarrinho(SqlConnection connection = null, SqlTransaction transaction = null)
        {
            bool createdNewConnection = false;

            if (connection == null)
            {
                connection = new SqlConnection(_conn);
                connection.Open();

                createdNewConnection = true;
            }

            Carrinho carrinho = null;

            string sql = @"select top 1 idCarrinho, dataCriacao from Carrinho where pedidoEfetuado = 0 order by idCarrinho asc;";

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Transaction = transaction;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows && dr.Read())
                    {
                        carrinho = new Carrinho
                        {
                            IdCarrinho = Convert.ToInt32(dr["idCarrinho"]),
                            DataCriacao = Convert.ToDateTime(dr["dataCriacao"])
                        };
                    }
                }
            }

            if (createdNewConnection)
            {
                connection.Close();
            }

            return carrinho;
        }

        public static void CriarCarrinho(SqlConnection connection, SqlTransaction transaction)
        {
            DateTime dataCriacao = DateTime.Now;

            string sql = @"insert into Carrinho(dataCriacao, pedidoEfetuado) values (@dataCriacao, @pedidoEfetuado);";

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@dataCriacao", dataCriacao);
                cmd.Parameters.AddWithValue("@pedidoEfetuado", 0);

                cmd.ExecuteNonQuery();
            }
        }

        public static CarrinhoProduto GetProdutoCarrinho(
            int idCarrinho, int idProduto, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            bool createdNewConnection = false;

            if (connection == null)
            {
                connection = new SqlConnection(_conn);
                connection.Open();

                createdNewConnection = true;
            }

            CarrinhoProduto carrinhoProduto = null;

            string sql = @"select idCarrinho, idProduto, quantidade, vlrUnitarioProduto
                            from CarrinhoProduto where idCarrinho = @idCarrinho and idProduto = @idProduto;";

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("idCarrinho", idCarrinho);
                cmd.Parameters.AddWithValue("idProduto", idProduto);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows && dr.Read())
                    {
                        carrinhoProduto = new CarrinhoProduto
                        {
                            IdCarrinho = Convert.ToInt32(dr["idCarrinho"]),
                            IdProduto = Convert.ToInt32(dr["idCarrinho"]),
                            Quantidade = Convert.ToInt32(dr["quantidade"]),
                            VlrUnitarioProduto = Convert.ToInt32(dr["vlrUnitarioProduto"])
                        };
                    }
                }
            }

            if (createdNewConnection)
            {
                connection.Close();
            }

            return carrinhoProduto;
        }

        public static void CriarCarrinhoProduto(
            int idCarrinho, int idProduto, decimal quantidade, decimal vlrUnitarioProduto,
            SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"insert into CarrinhoProduto(idCarrinho, idProduto, quantidade, vlrUnitarioProduto) 
                            values (@idCarrinho, @idProduto, @quantidade, @vlrUnitarioProduto);";

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@idCarrinho", idCarrinho);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.Parameters.AddWithValue("@quantidade", quantidade);
                cmd.Parameters.AddWithValue("@vlrUnitarioProduto", vlrUnitarioProduto);

                cmd.ExecuteNonQuery();
            }
        }

        public static void AlterarCarrinhoProduto(
            int idCarrinho, int idProduto, decimal quantidade, decimal vlrUnitarioProduto,
            SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"update CarrinhoProduto set quantidade = @quantidade, vlrUnitarioProduto = @vlrUnitarioProduto
                            where idCarrinho = @idCarrinho and idProduto = @idProduto;";

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@idCarrinho", idCarrinho);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.Parameters.AddWithValue("@quantidade", quantidade);
                cmd.Parameters.AddWithValue("@vlrUnitarioProduto", vlrUnitarioProduto);

                cmd.ExecuteNonQuery();
            }
        }

        public static void AddProdutoCarrinho(int idProduto, decimal quantidade, decimal vlrUnitarioProduto)
        {
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();

                var dbTransaction = cn.BeginTransaction();

                try
                {
                    Carrinho c = GetCarrinho(cn, dbTransaction);

                    if (c == null)
                    {
                        CriarCarrinho(cn, dbTransaction);
                        c = GetCarrinho(cn, dbTransaction);
                    }

                    CarrinhoProduto carrinhoProduto = GetProdutoCarrinho(c.IdCarrinho, idProduto, cn, dbTransaction);

                    if (carrinhoProduto == null)
                    {
                        CriarCarrinhoProduto(c.IdCarrinho, idProduto, quantidade, vlrUnitarioProduto, cn, dbTransaction);
                        carrinhoProduto = GetProdutoCarrinho(c.IdCarrinho, idProduto, cn, dbTransaction);
                    }
                    else
                    {
                        var novaQuantidade = carrinhoProduto.Quantidade + quantidade;

                        AlterarCarrinhoProduto(c.IdCarrinho, idProduto, novaQuantidade, vlrUnitarioProduto, cn, dbTransaction);
                    }

                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        public static void AlterarQuantidadeProdutoCarrinho(int idProduto, decimal aumentoDiminuicaoQuantidade, decimal vlrUnitarioProduto)
        {
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();

                var dbTransaction = cn.BeginTransaction();

                try
                {
                    Carrinho c = GetCarrinho(cn, dbTransaction);

                    CarrinhoProduto carrinhoProduto = GetProdutoCarrinho(c.IdCarrinho, idProduto, cn, dbTransaction);

                    var novaQuantidade = carrinhoProduto.Quantidade + aumentoDiminuicaoQuantidade;

                    AlterarCarrinhoProduto(c.IdCarrinho, idProduto, novaQuantidade, vlrUnitarioProduto, cn, dbTransaction);

                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        public static List<CarrinhoProduto> GetProdutosCarrinho(int idCarrinho)
        {
            var rSQL = @"SELECT idCarrinho, idProduto, quantidade, vlrUnitarioProduto
                        FROM CarrinhoProduto where idCarrinho = @idCarrinho;";

            List<CarrinhoProduto> response = new List<CarrinhoProduto>();

            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();
                using (var cmd = new SqlCommand(rSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@idCarrinho", idCarrinho);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                            while (dr.Read())
                            {
                                response.Add(new CarrinhoProduto
                                {
                                    IdCarrinho = Convert.ToInt32(dr["idCarrinho"]),
                                    IdProduto = Convert.ToInt32(dr["idProduto"]),
                                    Quantidade = Convert.ToInt32(dr["quantidade"]),
                                    VlrUnitarioProduto = Convert.ToDecimal(dr["vlrUnitarioProduto"])
                                });
                            }
                    }
                }
            }

            return response;
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

        //public void Salvar()
        //{
        //    string sql;

        //    if (IdProduto == 0)
        //    {
        //        sql = "INSERT INTO Produto (nomeproduto, quantestoq, vlrproduto, peso)" +
        //        "VALUES(@nomeproduto, @quantestoq, @vlrproduto, @peso)";
        //    }
        //    else
        //    {
        //        sql = "UPDATE Produto set nomeproduto=@nomeproduto, quantestoq=@quantestoq, vlrproduto=@vlrproduto, peso=@peso " +
        //             "WHERE idProduto = " + IdProduto;
        //    }
        //    try
        //    {
        //        using (var cn = new SqlConnection(_conn))
        //        {
        //            cn.Open();
        //            using (var cmd = new SqlCommand(sql, cn))
        //            {
        //                cmd.Parameters.AddWithValue("@nomeproduto", NomeProduto);
        //                cmd.Parameters.AddWithValue("@quantestoq", QuantEstoq);
        //                cmd.Parameters.AddWithValue("@vlrproduto", VlrProduto);
        //                cmd.Parameters.AddWithValue("@peso", Peso);
        //                //cmd.Parameters.AddWithValue("@unidade", Unidade);
        //                //cmd.Parameters.AddWithValue("@loja", Loja);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Falha: " + ex.Message);
        //        throw;
        //    }
        //}

        //public static void Excluir(int idproduto)
        //{
        //    var sql = "DELETE FROM Produto WHERE idProduto = " + idproduto;
        //    try
        //    {
        //        using (var cn = new SqlConnection(_conn))
        //        {
        //            cn.Open();
        //            using (var cmd = new SqlCommand(sql, cn))
        //            {
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Falha: " + ex.Message);
        //        throw;
        //    }
        //}

        //public static Produtos GetProduto(int idproduto)
        //{
        //    var sql = "SELECT * FROM Produto WHERE idProduto = " + idproduto;

        //    Produtos returnValue = null;

        //    try
        //    {
        //        using (var cn = new SqlConnection(_conn))
        //        {
        //            cn.Open();
        //            using (var cmd = new SqlCommand(sql, cn))
        //            {
        //                using (var dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.HasRows)
        //                    {
        //                        if (dr.Read())
        //                        {
        //                            returnValue = new Produtos
        //                            {
        //                                IdProduto = idproduto,
        //                                NomeProduto = dr["nomeProduto"].ToString(),
        //                                QuantEstoq = Convert.ToInt16(dr["quantEstoq"]),
        //                                VlrProduto = Convert.ToDouble(dr["vlrProduto"]),
        //                                Peso = Convert.ToInt16(dr["Peso"])
        //                            };
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Falha: " + ex.Message);
        //        throw;
        //    }

        //    return returnValue;
        //}
    }
}