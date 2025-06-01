using ILogger = Serilog.ILogger;
using userJwtApp.Exceptions;
using userJwtApp.Services.Jwt;
using userJwtApp.Repositories;
using userJwtApp.Models.Controllers;
using userJwtApp.Validators.ProductValidators;
using userJwtApp.Models.ProductModel;
using userJwtApp.Models.UserModel;
using FluentValidation;
using FluentValidation.Results;

namespace userJwtApp.Controllers;

public class ProductController : IProductController
{
    private ProductRepository Repository { get; }
    private UserRepository UserRepository { get; }
    private ILogger Logger { get; }
    private IValidator<ProductRegisterRequestModel> ProductRegisterValidator { get; }
    private IValidator<ProductUpdateRequestModel> ProductUpdateValidator { get; }

    public ProductController(
        ProductRepository Repository,
        UserRepository userRepository,
        ILogger logger,
        IValidator<ProductRegisterRequestModel> productRegisterValidator,
        IValidator<ProductUpdateRequestModel> productUpdateValidator
    )
    {
        this.Repository = Repository;
        this.UserRepository = UserRepository;
        this.Logger = Logger;
        this.ProductRegisterValidator = ProductRegisterValidator;
        this.ProductUpdateValidator = ProductUpdateValidator;
    }

    public async Task<int> RegisterProduct(ProductRegisterRequestModel registerRequestModel, int userId)
    {
        #region Validation
        Logger.Information("Validating Product register request");
        ValidationResult validationResult = await ProductRegisterValidator.ValidateAsync(registerRequestModel);
        if (!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidRequestInfoException = new(validationResult.Errors.Select(e => e.ErrorMessage));
            Logger.Error("Invalid product register request: {InvalidInfoException}", invalidRequestInfoException.Message);
            throw invalidRequestInfoException;
        }
        UserModel? relatedUser = await UserRepository.GetUserById(userId);
        if (relatedUser is null)
        {
            UserNotFoundException userNotFoundException = new(userId.ToString());
            Logger.Error("User ID[{Id}] not found: {UserNotFoundException}", userId, userNotFoundException);
            throw userNotFoundException;
        }
        #endregion

        #region Register Product
        Logger.Information("Registering product...");
        ProductModel product = new()
        {
            ProductName = registerRequestModel.ProductName,
            CreatedBy = relatedUser
        };
        ProductModel newProduct = await Repository.RegisterProduct(product);
        await Repository.FlushChanges();
        #endregion

        Logger.Information("Product registered successfully");
        return newProduct.Id;
    }

    public async Task<IReadOnlyList<ProductReadModel>> GetUserProducts(int userId)
    {
        #region Validation

        Logger.Information("Validating user ID[{Id}]", userId);
        UserModel? user = await UserRepository.GetUserById(userId);
        if (user is null)
        {
            UserNotFoundException userNotFoundException = new(userId.ToString());
            Logger.Error("User ID[{Id}] not found: {UserNotFoundException}",
            userId, userNotFoundException.Message);
            throw userNotFoundException;
        }
        #endregion

        #region Get user clients
            Logger.Information("Getting user ID[{Id}] clients", userId);
            var userClients = await Repository.GetUserRelatedProducts(userId);
            var userClientRead = 
            userClients.Select(ProductReadModel.FromProductModel).ToList();
        #endregion
    
    return userClientRead;
    }

    public async Task<int> UpdateProduct(ProductUpdateRequestModel updateRequest, int productId)
    {
        throw new NotImplementedException();
    }
    public async Task<int> DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }
}
