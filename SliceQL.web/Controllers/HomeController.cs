using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SliceQL.web.Models;
using SliceQL.Core;
using System.Data.SQLite;

namespace SliceQL.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    string[] args = new string[5];
        //    args[0] = "SliceQL.Console";
        //    args[1] = "--data-file";
        //    args[2] = "C:\\Users\\anous\\Desktop\\Applications dev\\SliceQL\\inputs\\tableName.txt";
        //    args[3] = "--sql";
        //    args[4] = "SELECT * FROM tableName;";

        //    IInputInterface input = new CommandLineInterface(args);
        //    try
        //    {
        //        Query query = new Query(args[4]);
        //        using (Database database = new Database(input.data, input.tableName))
        //        {
        //            database.ExecuteSqlQuery(query);
        //        }
        //    }
        //    catch (InvalidSqlQueryException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.WriteLine(input.querySql);
        //        Environment.Exit((int)ParsingError.codeError.InputError);
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        Console.WriteLine($"SQLite Error: {ex.Message} (Code: {ex.ErrorCode})");
        //        Environment.Exit((int)ParsingError.codeError.InputError);
        //    }
        //    return View();
        //}
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile dataFile, string sqlQuery)
        {
            string[] args = new string[5];
            args[0] = "SliceQL.Console";
            args[1] = "--data-file";
            args[2] = "C:\\Users\\anous\\Desktop\\Applications dev\\SliceQL\\inputs\\tableName.txt";
            args[3] = "--sql";
            args[4] = sqlQuery;
            //args[4] = "SELECT * FROM tableName WHERE Age='24'";

            IInputInterface input = new CommandLineInterface(args);
            try
            {
                Query query = new Query(args[4]);
                using (Database database = new Database(input.data, input.tableName))
                {
                    database.ExecuteSqlQuery(query);
                }
            }
            catch (InvalidSqlQueryException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(input.querySql);
                Environment.Exit((int)ParsingError.codeError.InputError);
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite Error: {ex.Message} (Code: {ex.ErrorCode})");
                Environment.Exit((int)ParsingError.codeError.InputError);
            }
            return View();

            //if (dataFile == null || string.IsNullOrWhiteSpace(sqlQuery))
            //{
            //    ViewBag.Error = "Fichier ou requête manquant.";
            //    return View();
            //}

            //using var reader = new StreamReader(dataFile.OpenReadStream());

            //// OPTION 1 : avec le nouveau constructeur
            //var cli = new CommandLineInterface(reader, sqlQuery, Path.GetFileNameWithoutExtension(dataFile.FileName));

            //// OU OPTION 2 : avec une méthode
            //// var cli = new CommandLineInterface();
            //// cli.OverrideData(reader, sqlQuery, Path.GetFileNameWithoutExtension(dataFile.FileName));

            //var preview = await cli.data.ReadToEndAsync();
            //ViewBag.Result = preview;

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
