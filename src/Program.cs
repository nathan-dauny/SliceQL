using System.Data.SQLite;
using XLQL;
class Program
{
    static Task Main(string[] args)
    {
        IInputInterface input = new CommandLineInterface(args);
        try
        {
            Query query= new Query(input.querySql);
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
        return Task.CompletedTask;
    }
}
