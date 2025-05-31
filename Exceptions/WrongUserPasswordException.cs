namespace userJwtApp.Exceptions;

public class WrongUserPasswordException : Exception
{
    private const string MESSAGE_EXCEPTION = "Wrong password for user with Username[{0}]";

    public WrongUserPasswordException(string username) :
        base(string.Format(MESSAGE_EXCEPTION, username))
    {}
}