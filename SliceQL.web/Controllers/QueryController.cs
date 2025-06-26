using System.Text;
using Microsoft.AspNetCore.Mvc;
using SliceQL.Core;
using SliceQL.web.Models;

namespace SliceQL.web.Controllers
{
    public class QueryController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View(new QueryInputViewModel());

        [HttpPost]
        public async Task<IActionResult> Index(QueryInputViewModel model)
        {
            if (model.CsvFile == null || string.IsNullOrWhiteSpace(model.SqlQuery))
            {
                ModelState.AddModelError("", "Fichier CSV et requête SQL requis.");
                return View(model);
            }

            using var reader = new StreamReader(model.CsvFile.OpenReadStream(), Encoding.UTF8);
            var db = new DatabaseLIB(reader, "MyTable");

            var result = db.ExecuteSqlQuery(model.SqlQuery);
            model.Result = result;
            model.Columns = result.Count > 0 ? result[0].Keys.ToList() : new List<string>();

            return View(model);
        }
    }
}
