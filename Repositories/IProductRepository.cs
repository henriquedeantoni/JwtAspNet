using userJwtApp.Models.ProductModels;

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
    /// <param name="product"></param>
    /// <param name=""></param>
    /// <returns></returns>
    public Task<ProductModel> GetProductById(Guid productId);

    /// <summary>
    /// Return all products registered by year integer and by month integer 1-12
    /// </summary>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public Task<IReadOnlyList<ProductModel>> GetProductByMonth(int month, int year);

    /// <summary>
    /// Return all products registered by year integer
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    public Task<IReadOnlyList<ProductModel>> GetProductByYear(int year);

    /// <summary>
    /// Return all products registered by user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// 
    public Task<IReadOnlyList<ProductModel>> GetUserRelatedProducts(Guid userId);
    
    /// <summary>
    /// Delete some product 
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