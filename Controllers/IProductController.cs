using userJwtApp.Models.ProductModels;

namespace userJwtApp.Models.Controllers;

public interface IProductController
{
    /// <summary>
    /// product register
    /// </summary>
    /// <param name="registerRequestModel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<Guid> RegisterProduct(ProductRegisterRequestModel registerRequestModel, Guid userId);

    /// <summary>
    /// Products registered by User
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<IReadOnlyList<ProductReadModel>> GetUserProducts(Guid Id);

    /// <summary>
    /// Product Update 
    /// </summary>
    /// <param name="productUpdateRequestModel"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Task<Guid> UpdateProduct(ProductUpdateRequestModel productUpdateRequestModel, Guid productId);

    /// <summary>
    /// Product List By date
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public Task<IReadOnlyList<ProductModel>> GetProductByDate(DateTime date);

    /// <summary>
    /// Product Delete
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Task<Guid> DeleteProduct(Guid productId);
}