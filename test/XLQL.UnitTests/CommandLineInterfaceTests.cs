using System.Runtime.Serialization;

namespace XLQL.UnitTests
{
    public class CommandLineInterfaceTests
    {
        [Fact]
        public void GoodArgsCommandLineInterface()
        {
            //Arrange
            string pathFile = Path.Combine(Path.GetTempPath(), "temporaryFile.txt");
            File.WriteAllText(pathFile, "content test");
            string expectedFile = "content test";
            string optionFile = "--data-file";
            string optionFileAlias = "-f";
            string expectedSql = "SELECT*";
            string optionSql = "--sql";
            string optionSqlAlias = "-s";
            string[] args = { optionFile, pathFile, optionSql, expectedSql };
            string[] argsAlias = { optionFileAlias, pathFile, optionSqlAlias, expectedSql };

            //Act
            IInputInterface input = new CommandLineInterface(args);
            IInputInterface inputAlias = new CommandLineInterface(argsAlias);
            string extractedFile = input.data.ReadToEnd();
            string extractedFileAlias = inputAlias.data.ReadToEnd();
            string extractedSql = input.querySql;
            string extractedSqlAlias = inputAlias.querySql;

            //Assert
            Assert.Equal(expectedFile, extractedFile);
            Assert.Equal(expectedFile, extractedFileAlias);
            Assert.Equal(expectedSql, extractedSql);
            Assert.Equal(expectedSql, extractedSqlAlias);
        }
    }
}
