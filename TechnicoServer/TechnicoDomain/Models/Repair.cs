using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TechnicoDomain.Models;

public class Repair  
{
    [Key]
    public long Id { get; set; }
 
    public DateTime? ScheduledDate { get; set; }

    [Required]
    public string? Type { get; set; } 

    public string? Description { get; set; } 

    [Required]
    public string? Address { get; set; } 

    [Required]
    public string Status { get; set; } = "Pending"; 

    [Required]
    public decimal Cost { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;
    public DateTime? Updated { get; set; }
    public virtual long? PropertyItemId { get; set; }
    public virtual PropertyItem? PropertyItem { get; set; }
    public virtual long? PropertyItemOwner => PropertyItem?.Owner?.Id;
}
