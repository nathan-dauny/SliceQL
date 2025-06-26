using System.Data.SQLite;
using SliceQL.Core;


//SliceQL.Console --data-file "C:\Users\anous\Desktop\Applications dev\SLiceQL\inputs\tableName.txt" -s "SELECT * FROM tableName;"
class Program
{
    static Task Main(string[] args)
    {
        //IInputInterface input = new CommandLineInterface(args);
        string queryString = "SELECT * FROM tableName WHERE Age>25 ORDER BY Name DESC";
        try
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inputs", "tableName.csv");

            Query query = new Query(queryString);

            StreamReader reader = new StreamReader(path);
            using (DatabaseLIB database = new DatabaseLIB(reader, "tableName"))
            {
                database.ExecuteSqlQuery(query);
            }
        }
        //try
        //{
        //    Query query = new Query(input.querySql);
        //    using (DatabaseLIB database = new DatabaseLIB(input.data, input.tableName))
        //    {
        //        database.ExecuteSqlQuery(query);
        //    }
        //}
        catch (InvalidSqlQueryException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(queryString);
            Environment.Exit((int)ParsingError.codeError.InputError);
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite Error: {ex.Message} (Code: {ex.ErrorCode})");
            Environment.Exit((int)ParsingError.codeError.InputError);
        }
        return Task.CompletedTask;
    }
}
