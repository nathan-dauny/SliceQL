using System.Data.SQLite;
using SliceQL.Core;


//SliceQL.Console --data-file "C:\Users\anous\Desktop\Applications dev\SLiceQL\inputs\tableName.txt" -s "SELECT * FROM tableName;"
class Program
{
    static Task Main(string[] args)
    {
        //IInputInterface input = new CommandLineInterface(args);
        string queryString = "SELECT \r\n    E.Name,\r\n    E.Age,\r\n    E.Salary,\r\n    D.DepartmentName\r\nFROM Employees E\r\nJOIN Departments D ON E.DepartmentId = D.DepartmentId;";
        try
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inputs", "Departments.csv");
            var path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inputs", "Employees.csv");

            Query query = new Query(queryString);

            StreamReader reader1 = new StreamReader(path);
            string tablename1=Path.GetFileNameWithoutExtension(path);
            StreamReader reader2 = new StreamReader(path2);
            string tablename2 = Path.GetFileNameWithoutExtension(path2);
            var csvInputs = new Dictionary<StreamReader,string>();
            csvInputs[reader1] = tablename1;
            csvInputs[reader2] = tablename2;

            using (DatabaseMULTI database = new DatabaseMULTI(csvInputs))
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
