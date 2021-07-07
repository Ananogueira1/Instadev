using Instadev.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace Instadev.Controllers
{
    [Route("Feed")]
    public class FeedController : Controller
    {
        Post PostModel = new Post();
        Usuario UsuarioModel = new Usuario();
        [Route("Index")]
        public IActionResult Index()
        {
            // if (HttpContext.Session.GetString("Username") != null)
            // {
                Random aleatorio = new Random();
                bool condicao = false;
                List<Usuario> _usuarios = new List<Usuario>();
                _usuarios = UsuarioModel.ExibirInfo();
                _usuarios.RemoveAll(x => x.NomeDeUsuario == HttpContext.Session.GetString("Username"));
                do
                {
                    if (_usuarios.Count <= 7)
                    {
                        condicao = false;
                    }
                    else
                    {
                        _usuarios.Remove(_usuarios[aleatorio.Next(_usuarios.Count)]);
                        condicao = true;
                    }
                } while (condicao == true);
                ViewBag.UsuariosExpostos = _usuarios;
                ViewBag.Usuarios = UsuarioModel.ExibirInfo();
                ViewBag.Posts = PostModel.LerTodas();
                return View();
            // }
            // else
            // {
            //     return LocalRedirect("~/Cadastro/Index");
            // }
        }
        [Route("Postar")]
        public IActionResult Postar(IFormCollection Form)
        {
            List<Usuario> usuarios = new List<Usuario>();
            Post NovoPost = new Post();
            ViewBag.NomeUsuarioLogado = usuarios.Find(x => x.NomeDeUsuario == HttpContext.Session.GetString("Username")).Nome;
            ViewBag.NomeDeUsuarioUsuarioLogado = usuarios.Find(x => x.NomeDeUsuario == HttpContext.Session.GetString("Username"));
            ViewBag.ImagemDePerfilUsuarioLogado = usuarios.Find(x => x.NomeDeUsuario == HttpContext.Session.GetString("Username")).ImagemDePerfil;
            // PostModel.IDUsuario = usuarios.Find(x => x.NomeDeUsuario == HttpContext.Session.GetString("Username")).IdUsuario;
            NovoPost.Legenda = Form["Legenda"];
            NovoPost.IDPost = PostModel.GerarID("Database/post.csv");
            if (Form.Files.Count > 0)
            {
                var Arquivo = Form.Files[0];
                var Pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Feed/images/Posts");
                if (!Directory.Exists(Pasta))
                {
                    Directory.CreateDirectory(Pasta);
                }
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Feed/images/Posts", Arquivo.FileName);
                using (var Stream = new FileStream(caminho, FileMode.Create))
                {
                    Arquivo.CopyTo(Stream);
                }
                NovoPost.Imagem = Arquivo.FileName;
            }
            else
            {
                NovoPost.Imagem = "padrao.png";
            }
            PostModel.Criar(NovoPost);
            ViewBag.Posts = PostModel.LerTodas();
            return LocalRedirect("~/Feed/Index");
        }
    }
}