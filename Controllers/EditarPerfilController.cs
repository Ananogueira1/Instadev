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
            List<Usuario> Usuarios = UsuarioModel.ExibirInfo();
            Usuario NovoUsuario = Usuarios.Find(x => x.NomeDeUsuario == ViewBag.Username);
            NovoUsuario.Nome = Form["Nome"];
            NovoUsuario.NomeDeUsuario = Form["NomeDeUsuario"];
            NovoUsuario.Email = Form["Email"];
            if (NovoUsuario.Email == null)
            {
                NovoUsuario.Email = Usuarios.Find(x => x.NomeDeUsuario == ViewBag.Username).Email;
            }
            if (NovoUsuario.Nome == null)
            {
                NovoUsuario.Nome = Usuarios.Find(x => x.NomeDeUsuario == ViewBag.Username).Nome;
            }
            if (NovoUsuario.NomeDeUsuario == null)
            {
                NovoUsuario.NomeDeUsuario = Usuarios.Find(x => x.NomeDeUsuario == ViewBag.Username).NomeDeUsuario;
            }
            return LocalRedirect("~/EditarPerfil/Index");
        }
    }
}