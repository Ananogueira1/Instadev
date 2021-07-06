using System.Collections.Generic;
using Instadev.Models;
using Microsoft.AspNetCore.Mvc;

namespace Instadev.Controllers
{
    [Route("Perfil")]
    public class PerfilController : Controller
    {
        Usuario Usuario = new Usuario();

        [Route("Listar")]
        public IActionResult Index(){
            List<Usuario> Usuarios = Usuario.ExibirInfo();
            ViewBag.Perfil = Usuarios.Find( x => x.NomeDeUsuario == ViewBag.Username);
            return View();
        }

        [Route("Sair")]
        public IActionResult Sair(){
            HttpContext.Session.Remove("_UserName");
            return LocalRedirect("~/");
        }
    }
}