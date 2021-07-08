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
        Comentario ComentarioModel = new Comentario();
        [Route("Index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                Random aleatorio = new Random();
                bool condicao = false;
                string NomeDeUsuarioUsuarioLogado = HttpContext.Session.GetString("Username");
                List<Usuario> _usuarios = new List<Usuario>();
                _usuarios = UsuarioModel.ExibirInfo();
                _usuarios.RemoveAll(x => x.NomeDeUsuario == NomeDeUsuarioUsuarioLogado);
                ViewBag.FotoDePerfil = HttpContext.Session.GetString("FotoDePerfil");
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
                string UsuarioLogadoImagemDePerfil =  UsuarioModel.ExibirInfo().Find(x => x.NomeDeUsuario == NomeDeUsuarioUsuarioLogado).ImagemDePerfil;
                string UsuarioLogadoNome =  UsuarioModel.ExibirInfo().Find(x => x.NomeDeUsuario == NomeDeUsuarioUsuarioLogado).Nome;
                ViewBag.UsuarioLogadoImagemDePerfil = UsuarioLogadoImagemDePerfil;
                ViewBag.UsuarioLogadoNome = UsuarioLogadoNome;
                ViewBag.UsuarioLogadoNomeDeUsuario =  NomeDeUsuarioUsuarioLogado;
                ViewBag.UsuariosExpostos = _usuarios;
                ViewBag.Usuarios = UsuarioModel.ExibirInfo();
                ViewBag.Posts = PostModel.LerTodas();
                ViewBag.Comentarios = ComentarioModel.LerTodas();
                return View();
            }
            else
            {
                return LocalRedirect("~/Home/Index");
            }
        }
        [Route("Postar")]
        public IActionResult Postar(IFormCollection Form)
        {
            string NomeDeUsuarioUsuarioLogado = HttpContext.Session.GetString("Username");
            List<Usuario> usuarios = new List<Usuario>();
            usuarios = UsuarioModel.ExibirInfo();
            Post NovoPost = new Post();
            NovoPost.IDUsuario = usuarios.Find(x => x.NomeDeUsuario == NomeDeUsuarioUsuarioLogado).IdUsuario;
            NovoPost.Legenda = Form["Legenda"];
            NovoPost.IDPost = PostModel.GerarID("Database/post.csv");
            if (Form.Files.Count > 0)
            {
                var Arquivo = Form.Files[0];
                var Pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Posts");
                if (!Directory.Exists(Pasta))
                {
                    Directory.CreateDirectory(Pasta);
                }
                string[] arquivoSeparado = Arquivo.FileName.Split(".");
                var NomeDoArquivo = arquivoSeparado[0] + NovoPost.IDPost.ToString() + "." + arquivoSeparado[1];
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Posts", NomeDoArquivo);
                using (var Stream = new FileStream(caminho, FileMode.Create))
                {
                    Arquivo.CopyTo(Stream);
                }
                NovoPost.Imagem = NomeDoArquivo;
            }
            else
            {
                NovoPost.Imagem = "padrao.png";
            }
            PostModel.Criar(NovoPost);
            ViewBag.Posts = PostModel.LerTodas();
            return LocalRedirect("~/Feed/Index");
        }

        [Route("Comentar")]
        public IActionResult Comentar(IFormCollection Form){
            Comentario NovoComentario = new Comentario();
            NovoComentario.TextoComentario = Form["Comentario"];
            NovoComentario.IDUsuario = Int32.Parse(HttpContext.Session.GetString("IdUsuario"));
            NovoComentario.IDPost = Int32.Parse(Form["IDPost"]);
            NovoComentario.IDComentario = ComentarioModel.GerarID("Database/Comentario.csv");

            ComentarioModel.Criar(NovoComentario);

            return LocalRedirect("~/Feed/Index");
        }
    }
}