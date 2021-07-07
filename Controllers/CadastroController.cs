using System;
using System.Collections.Generic;
using Instadev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instadev.Controllers
{
    [Route("Cadastro")]

    public class CadastroController : Controller
    {
        Usuario usuarioModel = new Usuario();

        [TempData]
        public string Mensagem { get; set; }

        public IActionResult Index()
        {
            // List<Usuario> UsuariosExistentes = usuarioModel.ExibirInfo();
            // List<string> Emails = new List<string>();
            // List<string> NomesDeUsuarios = new List<string>();
            // foreach (var item in UsuariosExistentes)
            // {
            //     Emails.Add(item.Email);
            //     NomesDeUsuarios.Add(item.NomeDeUsuario);
            // }
            // ViewBag.EmailsExistentes = Emails;
            // ViewBag.NomesDeUsuariosExistentes = NomesDeUsuarios;
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            bool EmailCerto = true;
            bool NomeDeUsuarioCerto = true;
            List<Usuario> usuarios = usuarioModel.ExibirInfo();
            Usuario novoUsuario = new Usuario(); 
            novoUsuario.ImagemDePerfil = "semfoto.png";
            novoUsuario.IdUsuario = usuarioModel.GerarID("Database/Usuario.csv"); 
            novoUsuario.Nome = form["Nome"];
            novoUsuario.NomeDeUsuario = form["Nome de Usuario"];
            novoUsuario.Email = form["Email"];
            novoUsuario.ModificarSenha(form["Senha"]);

            foreach (var item in usuarios)
            {
                if (item.Email == novoUsuario.Email)
                {
                    EmailCerto = false;
                }
                if (item.NomeDeUsuario == novoUsuario.NomeDeUsuario)
                {
                    NomeDeUsuarioCerto = false;
                }
            }
            if (EmailCerto == true && NomeDeUsuarioCerto == true)
            {
                usuarioModel.Cadastrar(novoUsuario);
                return LocalRedirect("~/Login/Index");
                
            }
            else if (EmailCerto == false && NomeDeUsuarioCerto == true)
            {
                Mensagem = "Conflito com banco de dados: Email já existente";
                return LocalRedirect("~/Home/Index");
            }
            else if (EmailCerto == true && NomeDeUsuarioCerto == false)
            {
                Mensagem = "Conflito com banco de dados: Nome de usuário já existente";
                return LocalRedirect("~/Home/Index");
            }
            else
            {
                Mensagem = "Conflito com banco de dados: Email e nome de usuário já existentes";
                return LocalRedirect("~/Home/Index");
            }

        }
    }
}