using ILogger = Serilog.ILogger;
using userJwtApp.Exceptions;
using userJwtApp.Services.Jwt;
using userJwtApp.Repositories;
using userJwtApp.Models.Controllers;
using userJwtApp.Validators.UserValidators;
using userJwtApp.Models.UserModel;
using FluentValidation;
using FluentValidation.Results;
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
        #region Validation
        Logger.Information("Validated user signin request");

        ValidationResult validationResult = await SignValidator.ValidateAsync(signRequest);
        if (!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidRequestInfoException = new(validationResult.Errors
                .Select(e => e.ErrorMessage));
            Logger.Error("Invalid user signin request: {InvalidInfoException}", invalidRequestInfoException.Message);
            throw invalidRequestInfoException;
        }
        UserModel? allreadyRegisterUser = await Repository.GetUserByUserName(signRequest.Username);

        if (allreadyRegisterUser is not null)
        {
            UserAllreadyRegisteredException allreadyRegisteredException = new(signRequest.Username);
            Logger.Error("User Username [{username}] already registered:{AllreadyRegisteredException}",
                signRequest.Username, allreadyRegisteredException.Message);
            throw allreadyRegisteredException;
        }

        Logger.Information("User signin request is valid");
        #endregion

        #region Register User

        UserModel user = new()
        {
            Username = signRequest.Username,
            Email = signRequest.Email,
            Password = signRequest.Password
        };
        UserModel registeredUser = await Repository.RegisterUser(user);
        await Repository.FlushChanges();

        Logger.Information("User Username[{Username}] registered", user.Username);
        #endregion

        #region Create JWT
        Logger.Information("Creating JWT for user Username[{Username}]", user.Username);
        string newUserJwt = JwtService.GenerateToken(registeredUser.Id);
        #endregion

        return newUserJwt;
    }

    public async Task<string> LoginUser(UserLoginRequestModel loginRequest)
    {
        #region Validation
        Logger.Information("Validating user login request.");
        ValidationResult validationResult = await LoginValidator.ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidRequestInfoException = new(validationResult.Errors
                .Select(e => e.ErrorMessage));
            Logger.Error("Invalid user login request: {InvalidInfoException}", invalidRequestInfoException.Message);
            throw invalidRequestInfoException;
        }
        #endregion

        #region Login user
        Logger.Information("Loggin in user Username[{Username}]", loginRequest.Username);
        UserModel? user = await Repository.GetUserByUserName(loginRequest.Username);
        if (user is null)
        {
            UserNotFoundException userNotFoundException = new(loginRequest.Username);
            Logger.Error("User Username [{Username}] not found: {userNotFoundException}",
                loginRequest.Username, userNotFoundException.Message);
            throw userNotFoundException;
        }
        if (user.Password != loginRequest.Password)
        {
            WrongUserPasswordException wrongUserPasswordException = new(loginRequest.Username);
            Logger.Error("Invalid password for user Username[{Username}]:{InvalidPasswordException}",
                loginRequest.Username, wrongUserPasswordException.Message);
            throw wrongUserPasswordException;
        }
        Logger.Information("User logged in Username[{Username}]", loginRequest.Username);
        #endregion

        #region Create JWT
        Logger.Information("Creating JWT for user Username[{Username}]", loginRequest.Username);
        string userJwt = JwtService.GenerateToken(user.Id);
        #endregion

        return userJwt;
    }
}