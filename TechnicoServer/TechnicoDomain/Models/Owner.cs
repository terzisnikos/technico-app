using System.ComponentModel.DataAnnotations;

namespace TechnicoDomain.Models;

public class Owner
{
    [Key]
    public long Id { get; set; } 
   
    public string ?VATNumber { get; set; } 

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
    public string ?Email { get; set; } 

    [Required]
    public string ?Password { get; set; }

    public OwnerType OwnerType { get; set; }
    public virtual List<PropertyItem> PropertyItems { get; set; } = [];
}
