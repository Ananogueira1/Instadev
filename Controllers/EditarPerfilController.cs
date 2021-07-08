using System;
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
        Post PostModel = new Post();
        Comentario ComentarioModel = new Comentario();

        [TempData]
        public string Mensagem { get; set; }

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
            bool EmailCerto = true;
            bool NomeDeUsuarioCerto = true;
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
                string[] arquivoSeparado = Arquivo.FileName.Split(".");
                var NomeDoArquivo = arquivoSeparado[0] + NovoUsuario.IdUsuario.ToString() + "." + arquivoSeparado[1];
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Usuarios", NomeDoArquivo);
                using (var Stream = new FileStream(caminho, FileMode.Create))
                {
                    Arquivo.CopyTo(Stream);
                }
                NovoUsuario.ImagemDePerfil = NomeDoArquivo;
            }
            else
            {
                NovoUsuario.ImagemDePerfil = HttpContext.Session.GetString("FotoDePerfil");
            }
            foreach (var item in Usuarios)
            {
                if (item.Email == NovoUsuario.Email)
                {
                    if (item.IdUsuario != NovoUsuario.IdUsuario)
                    {
                        EmailCerto = false;
                    }
                }
                if (item.NomeDeUsuario == NovoUsuario.NomeDeUsuario)
                {
                    if (item.IdUsuario != NovoUsuario.IdUsuario)
                    {
                        NomeDeUsuarioCerto = false;
                    }
                }
            }
            if (EmailCerto == true && NomeDeUsuarioCerto == true)
            {
                UsuarioModel.EditarPerfil(NovoUsuario);
                return LocalRedirect("~/Login/Logout");
            }
            else if (EmailCerto == false && NomeDeUsuarioCerto == true)
            {
                Mensagem = "Conflito com banco de dados: Email já existente";
                return LocalRedirect("~/EditarPerfil/Index");
            }
            else if (EmailCerto == true && NomeDeUsuarioCerto == false)
            {
                Mensagem = "Conflito com banco de dados: Nome de usuário já existente";
                return LocalRedirect("~/EditarPerfil/Index");
            }
            else
            {
                Mensagem = "Conflito com banco de dados: Email e nome de usuário já existentes";
                return LocalRedirect("~/EditarPerfil/Index");
            }
        }
        [Route("DeletarUsuario")]
        public IActionResult DeletarUsuario()
        {
            List<Comentario> comentarios = ComentarioModel.LerTodas();
            List<Post> posts = PostModel.LerTodas();
            foreach (Post item in posts)
            {
                foreach (Comentario item2 in comentarios)
                {
                    if (item2.IDUsuario == int.Parse(HttpContext.Session.GetString("IdUsuario")))
                    {
                        ComentarioModel.Deletar(item2.IDComentario);
                    }
                    else if (item.IDUsuario == int.Parse(HttpContext.Session.GetString("IdUsuario")))
                    {
                        if (item2.IDPost.ToString() == item.IDPost.ToString())
                        {
                            ComentarioModel.Deletar(item2.IDComentario);
                        }
                    }
                }
            }
            UsuarioModel.DeletarPerfil(int.Parse(HttpContext.Session.GetString("IdUsuario")));
            PostModel.Deletar(int.Parse(HttpContext.Session.GetString("IdUsuario")));
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("IdUsuario");
            HttpContext.Session.Remove("Nome");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Senha");
            HttpContext.Session.Remove("FotoDePerfil");
            return LocalRedirect("~/Home/Index");
        }
    }
}