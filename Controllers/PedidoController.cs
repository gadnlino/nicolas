using Aula3108.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula3108.Controllers
{
    public class PedidoController : Controller
    {
        public PedidoController() { }

        [HttpGet]
        public void FinalizarPedido(int idCarrinho)
        {
            //ViewBag.Title = "Adicionar produtos ao carrinho";
            //ViewBag.Produto = produto;

            Pedido.CriarPedido(idCarrinho);
            
            Response.Redirect("/Pedido/ListaPedidos");
        }

        public ActionResult ListaPedidos()
        {
            ViewBag.Title = "Pedidos efetuados";
            //ViewBag.Message = "Relação de produtos";

            ViewBag.Pedidos = Pedido.GetPedidos();
            return View();
        }

        public ActionResult VisualizarItensPedido(int idpedido)
        {
            ViewBag.Title = "Itens do pedido";

            var pedido = Pedido.GetPedido(idpedido);

            var repCarrinho = Carrinho.GetRepresentacaoCarrinho(pedido.IdCarrinho);

            ViewBag.Carrinho = repCarrinho;

            return View();
        }
    }
}