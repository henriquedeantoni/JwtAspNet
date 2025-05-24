using userJwtApp.Models.ProductModel;

namespace userJwtApp.Models.Controllers;

public interface IProductController
{
    /// <summary>
    /// product register
    /// </summary>
    /// <param name="registerRequestModel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<int> RegisterProduct(ProductRegisterRequestModel registerRequestModel, int userId);

    /// <summary>
    /// Products registered by User
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public Task<IReadOnlyList<ProductReadModel>> GetUserProducts(int Id);

    /// <summary>
    /// Product Update 
    /// </summary>
    /// <param name="productUpdateRequestModel"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Task<int> UpdateProduct(ProductUpdateRequestModel productUpdateRequestModel, int productId);

    /// <summary>
    /// Product Delete
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Task<int> DeleteProduct(int productId);
}