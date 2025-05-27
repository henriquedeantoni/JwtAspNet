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
    public int Id { get; set; }
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
    [Column("UserName")]
    public string UserName { get; set; }
    [Required]
    [MaxLength(100)]
    [Column("Email")]
    public string Email { get; set; }
    [Required]
    [MaxLength(100)]
    [Column("Password")]
    public string Password { get; set; }
}
