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

        [Route("Informacoes")]

        public IActionResult Index()
        {
            ViewBag.Usuarios = usuarioModel.ExibirInfo();
            return View();
        }
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Usuario novoUsuario = new Usuario();

            novoUsuario.ImagemDePerfil = form["Imagem de perfil"];
            novoUsuario.IdUsuario = Int32.Parse(form["IdUsuario"]);
            novoUsuario.Nome = form["Nome"];
            novoUsuario.NomeDeUsuario = form["Nome de Usuário"];
            novoUsuario.Email = form["Email"];
            novoUsuario.ModificarSenha(form["Senha"]);

            usuarioModel.Cadastrar(novoUsuario);
            ViewBag.Usuarioes = usuarioModel.ExibirInfo();

            return LocalRedirect("~/Cadastro/Listar");
        }
    }
}