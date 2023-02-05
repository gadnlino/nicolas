using Aula3108.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula3108.Controllers
{
    public class ProdutosController : Controller
    {
        readonly ProdutosDbContext db;
        public ProdutosController()
        {
            db = new ProdutosDbContext();
        }

        public ActionResult Index()
        {
            var produtos = db.Produtos.ToList();
            return View(produtos);
        }

        public ActionResult Add_Loja()
        {
            ViewBag.Title = "Produtos";
            ViewBag.Message = "Adicionar produtos a loja";
            return View();
        }

        public ActionResult ListaProdutos()
        {
            ViewBag.Title = "Vende-se";
            ViewBag.Message = "Relação de produtos";
            var lista = Produtos.GetProdutos();
            ViewBag.Lista = lista;
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
                //Loja = Request["loja"]
            };
            produto.Salvar();
            //Response.Redirect("/Home/Produto");
            Response.Redirect("/Produtos/ListaProdutos");
        }

        [HttpPost]
        public void Excluir()
        {
            //var produto = new Produtos
            //{
            //	IdProduto = Convert.ToInt32("0" + Request["idproduto"])
            //};

            var idProdutoRequest = Request["idproduto"];

            int idProduto = Convert.ToInt32("0" + idProdutoRequest);
            Produtos.Excluir(idProduto);
            //Response.Redirect("/Home/Produto");
            Response.Redirect("/Produtos/ListaProdutos");
        }
    }
}