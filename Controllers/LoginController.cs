using System;
using Instadev.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instadev.Controllers
{

    [Route("Login")]
    public class LoginController : Controller
    {
        Usuario UsuarioModel = new Usuario();

        [TempData]
        public string Mensagem { get; set; }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection Form)
        {
            List<string> UsuariosCSV = UsuarioModel.LerTodasLinhasCSV("Database/Usuario.csv");
            var Logado = UsuariosCSV.Find(
                x =>
                x.Split(";")[4] == Form["Email"] &&
                x.Split(";")[5] == Form["Senha"]
            );

            if (Logado != null)
            {
                HttpContext.Session.SetString("IdUsuario", Logado.Split(";")[0]);
                HttpContext.Session.SetString("Username", Logado.Split(";")[3]);
                HttpContext.Session.SetString("FotoDePerfil", Logado.Split(";")[1]);
                HttpContext.Session.SetString("Email", Logado.Split(";")[4]);
                HttpContext.Session.SetString("Senha", Logado.Split(";")[5]);
                HttpContext.Session.SetString("Nome", Logado.Split(";")[2]);

                return LocalRedirect("~/Feed/Index");
            }
            else
            {
                Mensagem = "Dados incorretos, tente novamente...";
                return LocalRedirect("~/Login/Index");
            }
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return LocalRedirect("~/");
        }
    }
}


