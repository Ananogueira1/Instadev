using System.Collections.Generic;
using Instadev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Instadev.Controllers
{
    [Route("Perfil")]
    public class PerfilController : Controller
    {
        Usuario Usuario = new Usuario();
        Post p = new Post();

        [Route("Listar")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                List<Usuario> Usuarios = Usuario.ExibirInfo();
                ViewBag.Perfil = Usuarios.Find(x => x.NomeDeUsuario == ViewBag.Username);
                ViewBag.post = p.LerTodas();
                return View();
            }
            else
            {
                return LocalRedirect("~/Cadastrar/Index");
            }
        }

        [Route("Sair")]
        public IActionResult Sair()
        {
            HttpContext.Session.Remove("_UserName");
            return LocalRedirect("~/");
        }
    }
}