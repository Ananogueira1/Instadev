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
            Usuario novoUsuario = new Usuario(); 
            novoUsuario.ImagemDePerfil = "semfoto.png";
            novoUsuario.IdUsuario = usuarioModel.GerarID("Database/Usuario.csv"); 
            novoUsuario.Nome = form["Nome"];
            novoUsuario.NomeDeUsuario = form["Nome de Usuario"];
            novoUsuario.Email = form["Email"];
            novoUsuario.ModificarSenha(form["Senha"]);

            usuarioModel.Cadastrar(novoUsuario);

            return LocalRedirect("~/Login/Index");
        }
    }
}