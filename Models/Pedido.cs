using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Data.Common;

namespace Aula3108.Models
{
    public class Pedido
    {
        private readonly static string _conn = DbConnectionString.GetDbConnectionString();

        public int IdPedido { get; set; }
        public int IdCarrinho { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal VlrPedido { get; set; }
        public int QuantidadeProdutos { get; set; }

        public Pedido() { }

        public static List<Pedido> GetPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();

            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();

                string sql = @"select idPedido, idCarrinho, dataPedido, vlrPedido, quantidadeProdutos from Pedido;";

                using (var cmd = new SqlCommand(sql, cn))
                {
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Pedido pedido = new Pedido
                                {
                                    DataPedido = Convert.ToDateTime(dr["dataPedido"]),
                                    IdPedido = Convert.ToInt32(dr["idPedido"]),
                                    QuantidadeProdutos = Convert.ToInt32(dr["quantidadeProdutos"]),
                                    VlrPedido = Convert.ToDecimal(dr["vlrPedido"]),
                                    IdCarrinho = Convert.ToInt32(dr["idCarrinho"])
                                };

                                pedidos.Add(pedido);
                            }                            
                        }
                    }
                }
            }

            return pedidos;
        }

        public static Pedido GetPedido(int idPedido)
        {
            List<Pedido> pedidos = new List<Pedido>();

            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();

                string sql = @"select idPedido, idCarrinho, dataPedido, vlrPedido, quantidadeProdutos 
                                from Pedido where idPedido = @idPedido;";

                using (var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@idPedido", idPedido);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Pedido pedido = new Pedido
                                {
                                    DataPedido = Convert.ToDateTime(dr["dataPedido"]),
                                    IdPedido = Convert.ToInt32(dr["idPedido"]),
                                    QuantidadeProdutos = Convert.ToInt32(dr["quantidadeProdutos"]),
                                    VlrPedido = Convert.ToDecimal(dr["vlrPedido"]),
                                    IdCarrinho = Convert.ToInt32(dr["idCarrinho"])
                                };

                                pedidos.Add(pedido);
                            }
                        }
                    }
                }
            }

            return pedidos.First();
        }

        public static void CriarPedido(
            int idCarrinho)
        {
            using (var cn = new SqlConnection(_conn))
            {
                cn.Open();

                var dbTransaction = cn.BeginTransaction();

                try
                {
                    var carrinho = Carrinho.GetCarrinho(cn, dbTransaction);
                    var repCarrinho = Carrinho.GetRepresentacaoCarrinho(carrinho.IdCarrinho);

                    decimal qtdProdutos = 0;

                    repCarrinho.ListaProdutos.ForEach(a =>
                    {
                        qtdProdutos += a.Quantidade;
                    });

                    string sql = @"insert into Pedido(idCarrinho, dataPedido, vlrPedido, quantidadeProdutos) 
                            values (@idCarrinho, @dataPedido, @vlrPedido, @qtdProdutos);";

                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Transaction = dbTransaction;

                        cmd.Parameters.AddWithValue("@idCarrinho", idCarrinho);
                        cmd.Parameters.AddWithValue("@dataPedido", DateTime.Now);
                        cmd.Parameters.AddWithValue("@vlrPedido", repCarrinho.ValorTotalCarrinho);
                        cmd.Parameters.AddWithValue("@qtdProdutos", qtdProdutos);

                        cmd.ExecuteNonQuery();
                    }

                    foreach(var p in repCarrinho.ListaProdutos)
                    {
                        Produtos.ReduzEstoqueProduto(p.IdProduto, (int)p.Quantidade, cn, dbTransaction);
                    }

                    Carrinho.FinalizarPedidoCarrinho(cn, dbTransaction);

                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}