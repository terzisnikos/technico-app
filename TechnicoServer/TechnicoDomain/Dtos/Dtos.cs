using System.ComponentModel.DataAnnotations;

namespace TechnicoDomain.Dtos;

public class ResponseAPI<T>
{
    public int StatusCode { get; set; }
    public string description { get; set; } = string.Empty;
    public T? Value { get; set; }
}

public class OwnerRequestDto
{
    public string VATNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public enum OwnerType;
}

public class OwnerResponseDto
{
    public long Id { get; set; }
    public string VATNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string ?Password { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public string Owner { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public enum Type;

    public enum OwnerType;
}

public class PropertyItemRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string PropertyIdentificationNumber { get; set; } = string.Empty;  
    public string Address { get; set; } = string.Empty;                       
    public int YearOfConstruction { get; set; }                               
    public string VatNumber { get; set; } = string.Empty;                     
}

public class PropertyItemResponseDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public long? OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public string PropertyIdentificationNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
    public string VatNumber { get; set; } = string.Empty;
}

public class RepairRequestDto
{
    [Required]
    public string Type { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    public long PropertyItemId { get; set; }

    [Required] 
    public string Address { get; set; } = string.Empty; 

    [Required] 
    public decimal Cost { get; set; } 

    public DateTime? ScheduledDate { get; set; } 

    [Required] 
    public string Status { get; set; } = "Pending";
}


public class RepairResponseDto
{
    public long? Id { get; set; }

    public string? Type { get; set; } = string.Empty;

    public long? OwnerId { get; set; }

    public string? OwnerName { get; set; } = string.Empty;

    public long? PropertyItemId { get; set; }

    public string? PropertyItemTitle { get; set; } = string.Empty;

    public string? Address { get; set; } = string.Empty;

    public decimal? Cost { get; set; } 

    public DateTime? ScheduledDate { get; set; } 

    public string? Status { get; set; } = "Pending"; 

    public DateTime? Created { get; set; } 

    public DateTime? Updated { get; set; } 
}
