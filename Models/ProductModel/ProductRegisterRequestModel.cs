public class ProductRegisterRequestModel
{
    public string ProductName { get; set; }
    public string Serial { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}