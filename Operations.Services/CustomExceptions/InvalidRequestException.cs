namespace Operations.Services.CustomExceptions
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string message = "Invalid Request") : base(message) { }
    }
}
