using Aula3108.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula3108.Controllers
{
    public class CarrinhoController : Controller
    {
        public CarrinhoController() { }

        public ActionResult VisualizarAddProdutoCarrinho(int idproduto)
        {
            ViewBag.Title = "Adicionar produtos ao carrinho";

            var produto = Produtos.GetProduto(idproduto);
            //ViewBag.Message = "Adicionar produtos a loja";
            ViewBag.Produto = produto;
            return View();
        }

        [HttpGet]
        public void AumentarQuantidadeProduto(int idproduto)
        {
            //ViewBag.Title = "Adicionar produtos ao carrinho";
            var produto = Produtos.GetProduto(idproduto);

            Carrinho.AlterarQuantidadeProdutoCarrinho(idproduto, 1, Convert.ToDecimal(produto.VlrProduto));
            //ViewBag.Produto = produto;
            Response.Redirect("/Carrinho/ListaProdutosCarrinho");
        }

        [HttpGet]
        public void DiminuirQuantidadeProduto(int idproduto)
        {
            //ViewBag.Title = "Adicionar produtos ao carrinho";
            var produto = Produtos.GetProduto(idproduto);
            Carrinho.AlterarQuantidadeProdutoCarrinho(idproduto, -1, Convert.ToDecimal(produto.VlrProduto));
            //ViewBag.Produto = produto;
            Response.Redirect("/Carrinho/ListaProdutosCarrinho");
        }

        [HttpGet]
        public void RemoverProdutoCarrinho(int idproduto)
        {
            //ViewBag.Title = "Adicionar produtos ao carrinho";
            Carrinho.RemoverProdutoCarrinho(idproduto);
            //ViewBag.Produto = produto;
            Response.Redirect("/Carrinho/ListaProdutosCarrinho");
        }

        [HttpGet]
        public void EsvaziarCarrinho()
        {
            //ViewBag.Title = "Adicionar produtos ao carrinho";
            Carrinho.EsvaziarCarrinho();
            //ViewBag.Produto = produto;
            Response.Redirect("/Carrinho/ListaProdutosCarrinho");
        }

        [HttpPost]
        public void AddProdutoCarrinho()
        {
            int idProduto = Convert.ToInt32(Request["idproduto"]);
            decimal quantidade = Convert.ToDecimal(Request["quantidade"]);
            decimal valorUnitarioProduto = Convert.ToDecimal(Request["vlrproduto"]);
            Carrinho.AddProdutoCarrinho(idProduto, quantidade, valorUnitarioProduto);
            Response.Redirect("/Carrinho/ListaProdutosCarrinho");
        }

        public ActionResult ListaProdutosCarrinho()
        {
            ViewBag.Title = "Produtos do carrinho";
            //ViewBag.Message = "Relação de produtos";
            var carrinho = Carrinho.GetCarrinho();
            var produtosCarrinho = Carrinho.GetProdutosCarrinho(carrinho.IdCarrinho);

            var listaInfosProduto = Produtos.GetProdutos(produtosCarrinho.Select(p => p.IdProduto).ToList());

            decimal valorTotalCarrinho = 0;

            foreach (var produto in produtosCarrinho)
            {
                valorTotalCarrinho += produto.Quantidade * produto.VlrUnitarioProduto;
            }

            var representacaoCarrinho = new RepresentacaoCarrinho
            {
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

            ViewBag.Carrinho = representacaoCarrinho;
            return View();
        }

        public ActionResult Alterar_Prod(int idproduto)
        {
            ViewBag.Title = "Produtos";
            ViewBag.Message = "Alterar produtos " + idproduto;
            var produto = Produtos.GetProduto(idproduto);
            ViewBag.Produto = produto;
            return View();
        }

        public ActionResult Excluir_Prod(int idproduto)
        {
            ViewBag.Title = "Produtos";
            ViewBag.Message = "Excluir produtos " + idproduto;
            var produto = Produtos.GetProduto(idproduto);
            ViewBag.Produto = produto;
            return View();
        }

        [HttpPost]
        public void Salvar()
        {
            var produto = new Produtos
            {
                IdProduto = Convert.ToInt32("0" + Request["idproduto"]),
                NomeProduto = Request["nomeproduto"],
                QuantEstoq = Convert.ToInt16(Request["quantestoq"]),
                VlrProduto = Convert.ToDouble(Request["vlrproduto"]),
                Peso = Convert.ToDouble(Request["peso"]),
            };
            produto.Salvar();
            Response.Redirect("/Produtos/ListaProdutos");
        }

        [HttpPost]
        public void Excluir()
        {
            var idProdutoRequest = Request["idproduto"];

            int idProduto = Convert.ToInt32("0" + idProdutoRequest);
            Produtos.Excluir(idProduto);
            Response.Redirect("/Produtos/ListaProdutos");
        }
    }
}