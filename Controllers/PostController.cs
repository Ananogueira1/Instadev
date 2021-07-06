using System;
using System.IO;
using Instadev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instadev.Controllers
{
    [Route("Post")]
    public class PostController : Controller
    {
        Post PostModel = new Post();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.post = PostModel.LerTodas();
            return View();
        }
        [Route("Postar")]
        public IActionResult Postar(IFormCollection form)
        {
            Post NovoPost = new Post();
            Usuario usuarioModel = new Usuario();

            NovoPost.Legenda = form["Legenda"];
            // NovoPost.IDUsuario = usuarioModel.IdUsuario;

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Post"); //INCOMPLETO

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img/Post", folder, file.FileName); //INCOMPLETO

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                NovoPost.Imagem = file.FileName;
            }
            else
            {
                NovoPost.Imagem = "SemFoto";
            }

            PostModel.Criar(NovoPost);
            ViewBag.post = PostModel.LerTodas();

            return LocalRedirect("~/Post/Listar");
        }
        [Route("Excluir/{IDPost}")]
        public IActionResult Deletar(int IDPost)
        {
            PostModel.Deletar(IDPost);
            ViewBag.post = PostModel.LerTodas();
            return LocalRedirect("~/Post/Listar");
        }
    }
}