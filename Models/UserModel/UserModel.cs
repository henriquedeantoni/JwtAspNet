using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace userJwtApp.Models.UserModel;

[Table("Users")]
public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    [Column("ID")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    [Column("FirstName")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("LastName")]
    public string LastName { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("Username")]
    public string Username { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Email")]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Password")]
    public string Password { get; set; }

    [Required]
    [Column("CreatedAt")]
    public DateTime createdAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Column("Updated")]
    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
}
