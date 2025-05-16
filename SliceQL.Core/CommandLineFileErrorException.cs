namespace SliceQL.Core
{
    public class CommandLineFileErrorException : Exception
    {
        public CommandLineFileErrorException()
        {

        }
        public CommandLineFileErrorException(string message)
            : base(message)
        {

        }
    }
}
