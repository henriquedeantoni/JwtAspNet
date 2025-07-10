namespace userJwtApp.Models.ProductModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using userJwtApp.Models.UserModels;

[Table("Products")]
public class ProductModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("ID")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("ProductName")]
    public string ProductName { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Serial")]
    public string Serial { get; set; }

    [Required]
    [Column("Price")]
    public decimal Price { get; set; }

    [Required]
    [Column("Active")]
    public bool Active { get; set; }
    
    [Required]
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("Updated")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [ForeignKey("CreatedBy")]
    public Guid CreatedById { get; set; }
    public UserModel CreatedBy { get; set; } // navegação
}