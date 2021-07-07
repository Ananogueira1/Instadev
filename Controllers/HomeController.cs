using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Instadev.Models;
using Microsoft.AspNetCore.Http;

namespace Instadev.Controllers
{
    public class HomeController : Controller
    {
        Usuario UsuarioModel = new Usuario();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                ViewBag.FotoDePerfil = HttpContext.Session.GetString("FotoDePerfil");
            }
            return LocalRedirect("~/Cadastro");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
