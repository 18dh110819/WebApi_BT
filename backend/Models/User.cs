using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
namespace backend.Models;

[Table("Users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
    [Display(Name = "Name")]
    [Column(TypeName = "nvarchar(255)")]
    public string Name { get; set; }

    [AllowNull]
    public string? Alias { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [StringLength(500)]
    [Column(TypeName = "nvarchar(500)")]
    public string? Description { get; set; }

    [Range(0, 99)]
    public int Age { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    [DefaultValue(false)]
    public bool Deleted { get; set; } = false;

    public string? AvatarUrl { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
}
