using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TechnicoDomain.Models;

/// <summary>
/// Repair model ver.2.1
/// </summary>
public class Repair  
{
    [Key]
    public long Id { get; set; }
 
    [Required]
    public DateTime ScheduledDate { get; set; }

    [Required]
    public string ?Type { get; set; } // E.g., Painting, Plumbing, etc.

    public string ?Description { get; set; } // Free-text field

    [Required]
    public string ?Address { get; set; } // Repair address

    [Required]
    public string Status { get; set; } = "Pending"; // Default: Pending

    [Required]
    public decimal Cost { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; }
    public virtual long? PropertyItemId { get; set; }
    public virtual PropertyItem? PropertyItem { get; set; }
    public virtual long? PropertyItemOwner => PropertyItem?.Owner?.Id;
}
