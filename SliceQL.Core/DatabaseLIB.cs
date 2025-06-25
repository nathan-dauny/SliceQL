using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using CsvToDynamicObjectLib;
using Matrix = System.Collections.Generic.List<string?[]>;

namespace SliceQL.Core
{
    public class DatabaseLIB : IDisposable
    {
        /// <summary>
        /// String to define the database.
        ///Data Source: to specify the database name and the location of the SQLite database.
        ///Use the special data source filename :memory: to create an in-memory database. 
        ///using :memory:, each connection creates its own database.
        ///Mode: Defines the access mode for the database.
        ///Cache: Controls the caching behavior for the database connection. Shared: make the database shareable between multiple connections

        /// </summary>
        public const string BDD_CONNECTION_STRING = $"Data Source=:memory:;Cache=Shared;";
        /// <summary>
        /// Gets a a connection to communicate with the database
        /// </summary>
        private readonly SQLiteConnection connection;

        /// <summary>
        /// Write the Query to create the table and the queries to insert data
        /// <param name="data">Data in the file</param>
        /// /// <param name="tableName">name of the table provided from the name of the File input</param>
        public DatabaseLIB(StreamReader data, string tableName)
        {
            connection = new SQLiteConnection(BDD_CONNECTION_STRING);
            connection.Open();

            var (CSV, dicoType) = GetObjetCSV(data);
            IEnumerable<SQLiteCommand> queriesToExecute = BuildCommandQuery(CSV, dicoType, tableName);
            foreach (SQLiteCommand command in queriesToExecute)
            {
                command.Connection = connection;
                using (command)
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public (CsvFinalObject, Dictionary<string, Type>) GetObjetCSV(StreamReader data)
        {
            var reader = new CsvReaderConverter();
            var csvRead = reader.ReadCsv(data);
            //var headers = csvRead.First().Select(kvp => kvp.Key).ToList();

            var columnsTypeMULTITHREADING = new ColumnsTypeMULTITHREADING();
            var columnstypeDico = columnsTypeMULTITHREADING.GetAllColumnsTypesMULTITHREADING(csvRead);
            var csvTyped = new CsvTyped();
            var csvTypedFields = csvTyped.GetFieldsTyped(columnstypeDico, csvRead);
            return (new CsvFinalObject(csvTypedFields), columnstypeDico);
        }
        /// <summary>
        /// Call the dispose method of SQLite connection to release the resource. 
        /// Then Close the connection.
        void IDisposable.Dispose()
        {
            connection.Dispose();
        }
        /// <summary>
        /// Build the commands queries needed to provide the SQL table.
        /// CREATE TABLE
        /// DELETE 
        /// INSERT for each line
        /// <param name="data">Data in the file</param>
        static IEnumerable<SQLiteCommand> BuildCommandQuery(CsvFinalObject data,
            Dictionary<string, Type> columnstypeDico, string tableName)
        {
            const string DATA_TYPE = "TEXT";
            const string DATA_NULLABLE = "NOT NULL";

            //CsvParser csvParsed = new CsvParser(data);

            StringBuilder createTableQuery = new StringBuilder();
            createTableQuery.Append($"CREATE TABLE {tableName} (Id INTEGER PRIMARY KEY AUTOINCREMENT,");

            int index = 0;
            StringBuilder insertValueQuery = new StringBuilder();
            StringBuilder insertTitleQuery = new StringBuilder();
            var test = data.First();
            foreach (var kvp in test)
            {
                createTableQuery.Append($"{kvp.Key} {TypeMapper.GetSQLType(columnstypeDico[kvp.Key])} ,");
                insertTitleQuery.Append($"{kvp.Key},");
                insertValueQuery.Append($"@Column{index},");
                index++;
            }
            createTableQuery
                .Remove(createTableQuery.Length - 1, 1)
                .Append(')');
            insertTitleQuery.Remove(insertTitleQuery.Length - 1, 1);
            insertValueQuery.Remove(insertValueQuery.Length - 1, 1);
            string insertQuery = $"INSERT INTO {tableName} ( {insertTitleQuery} ) VALUES ( {insertValueQuery} )";

            yield return new SQLiteCommand(createTableQuery.ToString());

            yield return new SQLiteCommand($"DELETE FROM {tableName}");

            foreach (var csvLine in data)
            {
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery);
                int indexfe = 0;
                foreach (var kvp in csvLine)
                {
                    insertCommand.Parameters.AddWithValue("@Column" + indexfe, kvp.Value);
                    indexfe++;
                }
                yield return insertCommand;
            }
        }

        public void ExecuteSqlQuery(Query sqlQUery)
        {
            using (SQLiteCommand userCommand = new SQLiteCommand(sqlQUery.asString, connection))
            {
                using (SQLiteDataReader readerResultQuery = userCommand.ExecuteReader())
                {
                    Matrix matrix = MatrixAdapter.ToMatrix(readerResultQuery);
                    string result = MatrixPrinter.Print(matrix);
                    Console.WriteLine(result);
                }
            }
        }

        public class MatrixAdapter
        {
            public static Matrix ToMatrix(SQLiteDataReader sqlReader)
            {
                return SqlReaderToMatrixAdapter(sqlReader);
            }
            static Matrix SqlReaderToMatrixAdapter(SQLiteDataReader sqlReader)
            {
                Matrix matrix = new Matrix();
                string[] valueTitle = new string[sqlReader.FieldCount - 1];
                for (int j = 0; j < sqlReader.FieldCount - 1; j++)
                {
                    valueTitle[j] = sqlReader.GetName(j + 1).ToString();
                }
                matrix.Add(valueTitle);
                //sqlReader.Read();
                while (sqlReader.Read())
                {
                    string?[] valueField = new string[sqlReader.FieldCount - 1];
                    StringBuilder displayLine = new StringBuilder();

                    for (int j = 0; j < sqlReader.FieldCount - 1; j++)
                    {
                        if (sqlReader.IsDBNull(j + 1))
                        {
                            valueField[j] = "";
                        }
                        else
                        {
                            valueField[j] = sqlReader.GetValue(j + 1).ToString();
                        }
                    }
                    matrix.Add(valueField);
                }
                return matrix;
            }
        }
    }
}
