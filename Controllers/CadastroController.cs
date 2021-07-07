using System;
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
            ViewBag.Usuarios = usuarioModel.ExibirInfo();

            return LocalRedirect("~/Login/Index");
        }
    }
}