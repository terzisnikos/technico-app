namespace TechnicoDomain.Models;

public class PropertyItem
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PropertyIdentificationNumber { get; set; }
    = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int YearOfConstruction { get; set; }
    public virtual long? OwnerId { get; set; }
    public virtual Owner? Owner { get; set; }
    public virtual List<Repair> Repairs { get; set; } = [];
}
