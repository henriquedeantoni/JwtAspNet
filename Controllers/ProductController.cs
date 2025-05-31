using ILogger = Serilog.ILogger;
using userJwtApp.Exceptions;
using userJwtApp.Services.Jwt;
using userJwtApp.Repositories;
using userJwtApp.Models.Controllers;
using userJwtApp.Validators.ProductValidators;
using userJwtApp.Models.ProductModel;
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
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<ProductReadModel>> GetUserProducts(int userId)
    {
        throw new NotImplementedException();
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
