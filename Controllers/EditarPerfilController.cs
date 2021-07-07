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
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewBag.Usuarios = UsuarioModel.ExibirInfo();
                ViewBag.UserName = HttpContext.Session.GetString("Username");
                ViewBag.Nome = HttpContext.Session.GetString("Nome");
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.Senha = HttpContext.Session.GetString("Senha");
                ViewBag.FotoDePerfil = HttpContext.Session.GetString("FotoDePerfil");
                return View();
            }
            else
            {
                return LocalRedirect("~/Cadastrar/Index");
            }
        }
        [Route("AlterarUsuario")]
        public IActionResult AlterarUsuario(IFormCollection Form)
        {
            List<Usuario> Usuarios = UsuarioModel.ExibirInfo();
            Usuario NovoUsuario = Usuarios.Find(x => x.NomeDeUsuario == HttpContext.Session.GetString("Username"));
            NovoUsuario.ModificarSenha(HttpContext.Session.GetString("Senha"));
            NovoUsuario.Nome = Form["Nome"];
            NovoUsuario.NomeDeUsuario = Form["NomeDeUsuario"];
            NovoUsuario.Email = Form["Email"];
            if (NovoUsuario.Email.Length < 1)
            {
                NovoUsuario.Email = HttpContext.Session.GetString("Email");
            }
            if (NovoUsuario.Nome.Length < 1)
            {
                NovoUsuario.Nome = HttpContext.Session.GetString("Nome");
            }
            if (NovoUsuario.NomeDeUsuario.Length < 1)
            {
                NovoUsuario.NomeDeUsuario = HttpContext.Session.GetString("Username");
            }
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
            else
            {
                NovoUsuario.ImagemDePerfil = HttpContext.Session.GetString("FotoDePerfil");
            }
            UsuarioModel.EditarPerfil(NovoUsuario);

            return LocalRedirect("~/Login/Logout");
        }
        public IActionResult DeletarUsuario()
        {
            UsuarioModel.DeletarPerfil(UsuarioModel.ExibirInfo().Find(x => x.NomeDeUsuario == HttpContext.Session.GetString("Username")).IdUsuario);
            HttpContext.Session.Remove("Username");
            return LocalRedirect("~/Home/Index");
        }
    }
}