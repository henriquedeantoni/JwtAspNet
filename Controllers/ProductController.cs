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
    private IValidator<DateTime> DateTimeValidator { get; }

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

    public async Task<Guid> RegisterProduct(ProductRegisterRequestModel registerRequestModel, Guid userId)
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

    public async Task<IReadOnlyList<ProductReadModel>> GetUserProducts(Guid userId)
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

        #region Get user products
            Logger.Information("Getting user ID[{Id}] products", userId);
            var userProducts = await Repository.GetUserRelatedProducts(userId);
            var userProductRead = 
            userProducts.Select(ProductReadModel.FromProductModel).ToList();
        #endregion
    
    return userProductRead;
    }

    public async Task<IReadOnlyList<ProductModel>> GetProductByDate(DateTime date)
    {
        #region Validation
        Logger.Information("Validation date request");
        ValidationResult validationResult = await DateTimeValidator.ValidateAsync(date);
        if (date.Kind != DateTimeKind.Utc)
        {
            InvalidRequestInfoException invalidRequestInfoException = new(validationResult.Errors
            .Select(e => e.ErrorMessage));
            Logger.Error("Invalid date format, date must be utc kind, {date}", date);
            throw invalidRequestInfoException;
        }
        #endregion

       #region Get product by date
        Logger.Information("Getting products by Month with {date}", date);
        var month = date.Month;
        var year = date.Year;

        IReadOnlyList<ProductModel> products = await Repository.GetProductsByMonth(month, year);

        if (products == null || !products.Any())
        {
            Logger.Warning("No products found for Month {Month} and Year {Year}", month, year);
            return new List<ProductModel>();
        }

        Logger.Information("Found {Count} products for Month {Month} and Year {Year}", products.Count, month, year);
        return products;
        #endregion
    }

    public async Task<Guid> UpdateProduct(ProductUpdateRequestModel updateRequest, Guid productId)
    {
        #region Validation

        Logger.Information("Validation product update request");
        ValidationResult validationResult = await ProductUpdateValidator.ValidateAsync(updateRequest);
        if (!validationResult.IsValid)
        {
            InvalidRequestInfoException invalidRequestInfoException = new(validationResult.Errors
                .Select(e => e.ErrorMessage));
            Logger.Error("Invalid product update request: {InvalidRequestInfoException}", invalidRequestInfoException.Message);
            throw invalidRequestInfoException;
        }
        ProductModel? productModel = await Repository.GetProductById(productId);
        if (productModel is null)
        {
            ProductNotFoundException productNotFoundException = new(productId.ToString());
            Logger.Error("Product ID[{Id}] not found: {InvalidRequestInfoException}", productId, productNotFoundException.Message
            );
            throw productNotFoundException;
        }
        #endregion

        #region Check for updates
        bool hasUpdates = false;
        Logger.Information("Checking for updates");
        if (productModel.ProductName != updateRequest.ProductName)
        {
            Logger.Information("Product Name changed from {OldProductName} to {NewProductName}",
            productModel.ProductName, updateRequest.ProductName);
            productModel.ProductName = updateRequest.ProductName;
            hasUpdates = true;
        }
        #endregion

        #region Update product
        if (!hasUpdates)
        {
            Logger.Warning("There are no updates to be made on ID[{Id}]", productModel.Id);
            return productModel.Id;
        }
        Logger.Information("Updating product ID{Id}", productModel.Id);
        await Repository.FlushChanges();
        #endregion

        Logger.Information("Product ID[{Id}] updated successfully", productModel.Id);
        return productModel.Id;
    }
    public async Task<Guid> DeleteProduct(Guid productId)
    {
        #region Validation
        Logger.Information("Validating product ID[{Id}]", productId);
        ProductModel? productModel = await Repository.GetProductById(productId);

        if (productModel is null)
        {
            ProductNotFoundException productNotFoundException = new(productId.ToString());
            Logger.Error("Product ID[{Id}] not found: {InvalidRequestInfoException}",
                productId, productNotFoundException.Message);
            throw productNotFoundException;
        }
        #endregion

        #region Delete product 
        Logger.Information("Deleting product ID[{Id}]", productModel.Id);
        ProductModel deletedProduct = Repository.DeleteProduct(productModel);
        await Repository.FlushChanges();
        #endregion

        Logger.Information("Product ID[{Id}] deleted successfully", deletedProduct.Id);
        return deletedProduct.Id;
    }
}
