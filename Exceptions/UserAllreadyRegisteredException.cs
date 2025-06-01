namespace userJwtApp.Exceptions;

public class UserAllreadyRegisteredException : System.Exception
{
    private const string EXCEPTION_MESSAGE = "User with username {0} allready registered";

    public UserAllreadyRegisteredException(string username) : base(string.Format(EXCEPTION_MESSAGE, username))
    { }
}