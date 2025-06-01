namespace userJwtApp.Exception;

public class ProductNotFoundException : System.Exception
{
    private const string MESSAGE_EXCEPTION = "Client with ID[{0}] was not found";

    public ProductNotFoundException(string productId) :
        base(string.Format(MESSAGE_EXCEPTION, productId))
    { }
} 