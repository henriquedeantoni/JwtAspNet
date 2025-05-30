using userJwtApp.Models.ProductModel;

namespace userJwtApp.Repositories;
public interface IProductRepository
{
    /// <summary>
    /// Register a new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public Task<ProductModel> RegisterProduct(ProductModel product);
    /// <summary>
    /// Return a product by product Id
    /// </summary>
    /// <param name="client"></param>
    /// <param name=""></param>
    /// <returns></returns>
    public Task<ProductModel> GetProductById(int productId);
    /// <summary>
    /// Return all products registered by user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<IReadOnlyList<ProductModel>> GetUserRelatedProducts(int userId);
    /// <summary>
    /// Delete some client 
    /// </summary>
    /// <param name="productModel"></param>
    /// <returns></returns>
    public ProductModel DeleteProduct(ProductModel productModel);
    /// <summary>
    /// Apply all changes made to database
    /// </summary>
    /// <returns></returns>
    public Task FlushChanges();

}