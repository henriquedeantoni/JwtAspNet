namespace userJwtApp.Exceptions;

public class InvalidRequestInfoException : Exception
{
    const string MESSAGE_EXCEPTION = "Invalid request parameters: {0}";

    public InvalidRequestInfoException(IEnumerable<string> invalidParameters) :
        base(string.Format(MESSAGE_EXCEPTION, string.Join(" ", invalidParameters)))
        {}
}