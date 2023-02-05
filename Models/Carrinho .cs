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
        public int IdCarrinho { get; set; }
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

        public static void FinalizarPedidoCarrinho(SqlConnection connection, SqlTransaction transaction)
        {
            var carrinho = GetCarrinho();

            string sql = @"update Carrinho set pedidoEfetuado = @pedidoEfetuado
                            where idCarrinho = @idCarrinho;";

            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Transaction = transaction;

                cmd.Parameters.AddWithValue("@idCarrinho", carrinho.IdCarrinho);
                cmd.Parameters.AddWithValue("@pedidoEfetuado", 1);

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

        public static void RemoverProdutoCarrinho(int idProduto)
        {
            var carrinho = GetCarrinho();

            var rSQL = @"DELETE FROM CarrinhoProduto where idCarrinho = @idCarrinho and idProduto = @idProduto;";

            List<CarrinhoProduto> response = new List<CarrinhoProduto>();

            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand(rSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@idCarrinho", carrinho.IdCarrinho);
                    cmd.Parameters.AddWithValue("@idProduto", idProduto);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void EsvaziarCarrinho()
        {
            var carrinho = GetCarrinho();

            var rSQL = @"DELETE FROM CarrinhoProduto where idCarrinho = @idCarrinho;";

            List<CarrinhoProduto> response = new List<CarrinhoProduto>();

            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();

                using (var cmd = new SqlCommand(rSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@idCarrinho", carrinho.IdCarrinho);

                    cmd.ExecuteNonQuery();
                }
            }
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

        public static RepresentacaoCarrinho GetRepresentacaoCarrinho(int? idCarrinho)
        {
            if (!idCarrinho.HasValue)
            {
                return new RepresentacaoCarrinho
                {
                    ListaProdutos = new List<RepresentacaoProdutoCarrinho>(),
                    ValorTotalCarrinho = 0,
                };
            }

            var produtosCarrinho = GetProdutosCarrinho(idCarrinho.Value);

            var listaInfosProduto = Produtos.GetProdutos(produtosCarrinho.Select(p => p.IdProduto).ToList());

            decimal valorTotalCarrinho = 0;

            foreach (var produto in produtosCarrinho)
            {
                valorTotalCarrinho += produto.Quantidade * produto.VlrUnitarioProduto;
            }

            var representacaoCarrinho = new RepresentacaoCarrinho
            {
                IdCarrinho = idCarrinho.Value,
                ValorTotalCarrinho = valorTotalCarrinho,
                ListaProdutos = produtosCarrinho.Select(p =>
                {
                    string nomeProduto = listaInfosProduto.Where(pp => pp.IdProduto == p.IdProduto).Select(pp => pp.NomeProduto).First();

                    decimal valorTotalProduto = p.Quantidade * p.VlrUnitarioProduto;

                    return new RepresentacaoProdutoCarrinho
                    {
                        IdProduto = p.IdProduto,
                        NomeProduto = nomeProduto,
                        Quantidade = p.Quantidade,
                        ValorUnitario = p.VlrUnitarioProduto,
                        ValorTotalProduto = valorTotalProduto
                    };
                }).ToList()
            };

            return representacaoCarrinho;
        }
    }
}