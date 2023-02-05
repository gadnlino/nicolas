using Aula3108.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula3108.Controllers
{
	public class UsuariosController : Controller
    {
        public ActionResult Cadastrar()
        {
            ViewBag.Title = "Usuários";
            ViewBag.Message = "Gerenciar usuários";
            return View();
        }

        public ActionResult Alterar_Info(int CPF_CNPJ)
        {
            ViewBag.Title = "Alterar";
            ViewBag.Message = "Alterar informações do perfil " + CPF_CNPJ;
            var cadastro = new Aula3108.Models.Usuario();
            Aula3108.Models.Usuario.GetCadastro(CPF_CNPJ);
            ViewBag.Cadastro = cadastro;
            return View();
        }

        public ActionResult Excluir_Prod(int idproduto)
        {
            ViewBag.Title = "Excluir";
            ViewBag.Message = "Excluir produto " + idproduto;
            var produto = Produtos.GetProduto(idproduto);
            ViewBag.Produto = produto;
            return View();
        }

        [HttpPost]
        public void Salvar()
        {
            var cadastro = new Aula3108.Models.Usuario();
            cadastro.IdProduto = Convert.ToInt32("0" + Request["id.produto"]);
            cadastro.nomeProduto = Request["nomeproduto"];
            cadastro.quantEstoq = Convert.ToInt16(Request["quantestoq"]);
            cadastro.vlrProduto = Convert.ToDouble(Request["vlrproduto"]);
            cadastro.Unidade = Convert.ToInt16(Request["unidade"]);
            cadastro.Peso = Convert.ToDouble(Request["peso"]);
            cadastro.Loja = Request["loja"];
            cadastro.Salvar();
            Response.Redirect("/Home/Produto");
        }

        [HttpPost]
        public void Excluir()
        {
			var produto = new Produtos
			{
				IdProduto = Convert.ToInt32("0" + Request["idproduto"])
			};
			produto.Excluir();
            Response.Redirect("/Home/Produto");
        }
    }
}