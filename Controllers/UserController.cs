using ILogger = Serilog.ILogger;
using userJwtApp.Models.Controllers;
using FluentValidation;
namespace userJwtApp.Controllers;


public class UserController : IUserController
{
    private UserRepository Repository { get; }
    private JwtService JwtService { get; }
    private ILogger Logger { get; }
    private IValidator<UserLoginRequestModel> LoginValidator { get; }
    private IValidator<UserSignRequestModel> SignValidator { get; }

    public UserController(
        UserRepository repository, JwtService jwtService, ILogger logger,
        IValidator<UserLoginRequestModel> LoginValidator,
        IValidator<UserSignRequestModel> SignValidator)
    {

        this.Repository = Repository;
        this.JwtService = JwtService;
        this.Logger = Logger;
        this.LoginValidator = LoginValidator;
        this.SignValidator = SignValidator;
    }

    public async Task<string> SignUser(UserSignRequestModel signRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<string> LoginUser(UserLoginRequestModel loginRequest)
    {
        throw new NotImplementedException();
    }
}