using Instadev.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Instadev.Controllers
{
    [Route("Feed")]
    public class FeedController : Controller
    {
        Post PostModel = new Post();
        [Route("Index")]
        public IActionResult Index(){
            ViewBag.Posts = PostModel.LerTodas();
            return View();
        }
        [Route("Postar")]
        public IActionResult Postar(IFormCollection Form){
            Post NovoPost = new Post();
            // PostModel.IDUsuario = ViewBag.IDUsuario = HttpContext.Session.GetString("IDUsuario");
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