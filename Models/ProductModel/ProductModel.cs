using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace userJwtApp.Models.ProductModel;
using userJwtApp.Models.UserModel;

public class ProductModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("ID")]
    public int Id { get; set; }

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
    [ForeignKey("CreatedBy")]
    public int CreatedById { get; set; }
    public UserModel CreatedBy { get; set; } // navegação
}