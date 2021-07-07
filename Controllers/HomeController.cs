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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
<<<<<<< HEAD
            return LocalRedirect("~/Cadastro");
=======
            // ViewBag.Username = HttpContext.Session.GetString("Username");

            return View();
>>>>>>> cb324e578f62c58c78fe10dc903b93e4ec581f52
        }

        public IActionResult Privacy()
        {
            return View();
        }
<<<<<<< HEAD

=======
>>>>>>> cb324e578f62c58c78fe10dc903b93e4ec581f52
    }
}

