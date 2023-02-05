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
            var carrinho = Carrinho.GetCarrinho();

            ViewBag.Carrinho = Carrinho.GetRepresentacaoCarrinho(carrinho?.IdCarrinho);
            return View();
        }
    }
}