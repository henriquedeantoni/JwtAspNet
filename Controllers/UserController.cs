using ILogger = Serilog.ILogger;
using userJwtApp.Repositories;
using userJwtApp.Models.Controllers;
using userJwtApp.Validators.UserValidators;
using userJwtApp.Models.UserModel;
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
        #region 
        logger.Information("Validated user signin request");

        ValidationResult validationResult = await signinValidator.ValidateAsync(signinRequest);
        if (!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidRequestInfoException = new(validationResult.Errors)
                .Select(e => e.ErrorMessage);
            logger.Error("Invalid user signin request: {InvalidInfoException}", invalidRequestInfoException.Message);
            throw invalidInfoException;
        }
        UserModel? allreadyRegisterUser = await repository.GetUserByUsername(signinRequest.Username);

        #endregion
    }

    public async Task<string> LoginUser(UserLoginRequestModel loginRequest)
    {
        #region 

        #endregion
    }
}