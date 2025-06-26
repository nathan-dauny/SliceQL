namespace SliceQL.web.Models
{
    public class QueryInputViewModel
    {
        public IFormFile CsvFile { get; set; }
        public string SqlQuery { get; set; }
        public List<Dictionary<string, object>> Result { get; set; }
        public List<string> Columns { get; set; }
    }
}
