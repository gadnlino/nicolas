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
                name:"ProdutosAdd_Loja",
                url:"Produtos/Add_Produto",
                new {controller = "Produtos", action = "Add_Produto" }
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
