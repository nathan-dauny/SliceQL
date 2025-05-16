using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SliceQL.web.Models;
using SliceQL.Core;

namespace SliceQL.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile dataFile, string sqlQuery)
        {
            if (dataFile == null || string.IsNullOrWhiteSpace(sqlQuery))
            {
                ViewBag.Error = "Fichier ou requête manquant.";
                return View();
            }

            using var reader = new StreamReader(dataFile.OpenReadStream());

            // OPTION 1 : avec le nouveau constructeur
            var cli = new CommandLineInterface(reader, sqlQuery, Path.GetFileNameWithoutExtension(dataFile.FileName));

            // OU OPTION 2 : avec une méthode
            // var cli = new CommandLineInterface();
            // cli.OverrideData(reader, sqlQuery, Path.GetFileNameWithoutExtension(dataFile.FileName));

            var preview = await cli.data.ReadToEndAsync();
            ViewBag.Result = preview;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
