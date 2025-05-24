namespace userJwtApp.Models.ProductModel;
using userJwtApp.Models.UserModel;

public class ProductModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Serial { get; set; }
    public decimal Price { get; set; }
    public UserModel CreatedBy { get; set; }
}