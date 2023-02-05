using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aula3108
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //PRODUTOS
            routes.MapRoute(
                name: "ProdutosSalvar",
                url: "Produtos/Salvar",
                new { controller = "Produtos", action = "Salvar" }
                );

            routes.MapRoute(
                name: "ProdutosExcluir",
                url: "Produtos/Excluir_Prod/{idproduto}",
                new { controller = "Produtos", action = "Excluir_Prod", idproduto = 0 }
                );

            routes.MapRoute(
               name: "ProdutosAlterar",
               url: "Produtos/Alterar_Prod/{idproduto}",
               new { controller = "Produtos", action = "Alterar_Prod", idproduto = 0 }
               );

            routes.MapRoute(
                name: "ProdutosAdd_Loja",
                url: "Produtos/Add_Produto",
                new { controller = "Produtos", action = "Add_Produto" }
                );

            //CARRINHO
            routes.MapRoute(
               name: "CarrinhoVisualizarAddProdutoCarrinho",
               url: "Carrinho/VisualizarAddProdutoCarrinho/{idProduto}",
               new { controller = "Carrinho", action = "VisualizarAddProdutoCarrinho" }
               );

            routes.MapRoute(
               name: "CarrinhoAddProdutoCarrinho",
               url: "Carrinho/AddProdutoCarrinho",
               new { controller = "Carrinho", action = "AddProdutoCarrinho" }
               );

            routes.MapRoute(
               name: "CarrinhoAumentarQuantidadeProduto",
               url: "Carrinho/AumentarQuantidadeProduto/{idproduto}",
               new { controller = "Carrinho", action = "AumentarQuantidadeProduto" }
               );

            routes.MapRoute(
              name: "CarrinhoDiminuirQuantidadeProduto",
              url: "Carrinho/DiminuirQuantidadeProduto/{idproduto}",
              new { controller = "Carrinho", action = "DiminuirQuantidadeProduto" }
              );

            routes.MapRoute(
              name: "CarrinhoRemoverProdutoCarrinho",
              url: "Carrinho/RemoverProdutoCarrinho/{idproduto}",
              new { controller = "Carrinho", action = "RemoverProdutoCarrinho" }
              );

            routes.MapRoute(
              name: "CarrinhoEsvaziarCarrinho",
              url: "Carrinho/EsvaziarCarrinho",
              new { controller = "Carrinho", action = "EsvaziarCarrinho" }
              );

            //PEDIDO
            routes.MapRoute(
              name: "PedidoFinalizarPedido",
              url: "Pedido/FinalizarPedido/{idCarrinho}",
              new { controller = "Pedido", action = "FinalizarPedido", idCarrinho = 0 }
              );

            routes.MapRoute(
              name: "PedidoListaPedidos",
              url: "Pedido/ListaPedidos",
              new { controller = "Pedido", action = "ListaPedidos" }
              );

            routes.MapRoute(
             name: "PedidoVisualizarItensPedido",
             url: "Pedido/VisualizarItensPedido/{idpedido}",
             new { controller = "Pedido", action = "VisualizarItensPedido", idpedido = 0 }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
