using System.Collections.Generic;
using System.IO;
using Instadev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instadev.Controllers
{

    [Route("EditarPerfil")]
    public class EditarPerfilController : Controller
    {
        Usuario UsuarioModel = new Usuario();
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("AlterarFoto")]
        public IActionResult AlterarFoto(IFormCollection Form)
        {
            List<Usuario> Usuarios = UsuarioModel.ExibirInfo();
            Usuario NovoUsuario = Usuarios.Find(x => x.NomeDeUsuario == ViewBag.Username);

            if (Form.Files.Count > 0)
            {
                var Arquivo = Form.Files[0];
                var Pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Usuarios");
                if (!Directory.Exists(Pasta))
                {
                    Directory.CreateDirectory(Pasta);
                }
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Usuarios", Arquivo.FileName);
                using (var Stream = new FileStream(caminho, FileMode.Create))
                {
                    Arquivo.CopyTo(Stream);
                }
                NovoUsuario.ImagemDePerfil = Arquivo.FileName;
            }
            UsuarioModel.EditarPerfil(NovoUsuario);
            return LocalRedirect("~/EditarPerfil/Index");
        }
        [Route("AlterarUsuario")]
        public IActionResult AlterarUsuario(IFormCollection Form)
        {
            Usuario NovoUsuario = new Usuario();

            return LocalRedirect("~/EditarPerfil/Index");
        }
    }
}