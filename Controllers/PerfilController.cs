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
            ViewBag.Perfil = Usuario.ExibirInfo();
            return View();
        }

        [Route("Sair")]
        public IActionResult Sair(){
            HttpContext.Session.Remove("_UserName");
            return LocalRedirect("~/");
        }
    }
}