using System.ComponentModel.DataAnnotations;

namespace TechnicoDomain.Models;

public class Owner
{
    public long Id { get; set; }
    [Key]
    public string ?VATNumber { get; set; } // Unique identifier (ΑΦΜ)

    [Required]
    public string ?Name { get; set; }

    [Required]
    public string ?Surname { get; set; }

    public string ?Address { get; set; }

    [Required]
    [Phone]
    public string ?PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string ?Email { get; set; } // Username

    [Required]
    public string ?Password { get; set; }

    public OwnerType OwnerType { get; set; }
    public virtual List<PropertyItem> PropertyItems { get; set; } = [];
}
