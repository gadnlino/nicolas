using Aula3108.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aula3108.Controllers
{
    public class VeiculosController : Controller
    {
        // GET: Veiculos
        public ActionResult Adicionar()
        {
            ViewBag.Title = "Veículos";
            ViewBag.Message = "Adicionar veículos";
            return View();
        }

        public ActionResult Alterar(int id)
        {
            ViewBag.Title = "Veículos";
            ViewBag.Message = "Alterar veículos " + id;
            var veiculo = new Veiculos();
            veiculo.GetVeiculo(id);
            ViewBag.Veiculo = veiculo;
            return View();
        }

        public ActionResult Excluir(int id)
        {
            ViewBag.Title = "Veículos";
            ViewBag.Message = "Excluir veículos " + id;
            var veiculo = new Veiculos();
            veiculo.GetVeiculo(id);
            ViewBag.Veiculo = veiculo;
            return View();
        }

        [HttpPost]
        public void Salvar()
        {
            var veiculo = new Veiculos();
            veiculo.Id = Convert.ToInt32("0" + Request["id"]);
            veiculo.Nome = Request["nome"];
            veiculo.Modelo = Request["modelo"];
            veiculo.Ano = Convert.ToInt16(Request["fabricacao"]);
            veiculo.Fabricacao = Convert.ToInt16(Request["fabricacao"]);
            veiculo.Cor = Request["cor"];
            veiculo.Combustivel = Request["combustivel"];
            veiculo.Automatico = Request["automatico"];
            veiculo.Valor = Convert.ToDecimal(Request["valor"]);
            veiculo.Ativo = Request["ativo"];
            veiculo.Salvar();
            Response.Redirect("/Home/Automóvel");
        }

        [HttpPost]
        public void Excluir()
        {
            var veiculo = new Veiculos();
            veiculo.Id = Convert.ToInt32("0" + Request["id"]);
            veiculo.Excluir();
            Response.Redirect("/Home/Automóvel");
        }
    }
}