using Aula3108.Cadastro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Aula3108.Models
{
    public class Controle
    {
        public bool tem;
        public String mensagem = "";
        public bool Acessar(String Login, String Senha)
        {
            LoginControle loginControle = new LoginControle();
            tem = loginControle.verificarLogin(Login, Senha);
            if (!loginControle.mensagem.Equals(""))
            {
                this.mensagem = loginControle.mensagem;
            }
            return tem;
        }
        public string Cadastrar(String Login, String Senha, String confSenha)
        {
            return mensagem;
        }
    }
}