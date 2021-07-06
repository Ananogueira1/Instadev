using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Instadev.Models;

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
            return View();
>>>>>>> 7577a2544197bf248554d3be010fd90065e44c5f
        }

        public IActionResult Privacy()
        {
            return View();
        }
<<<<<<< HEAD

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
=======
    }

>>>>>>> 7577a2544197bf248554d3be010fd90065e44c5f
}
