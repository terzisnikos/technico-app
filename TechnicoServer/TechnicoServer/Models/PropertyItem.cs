namespace TechnicoDomain.Models;

public class PropertyItem
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual long? OwnerId { get; set; }
    public virtual Owner? Owner { get; set; }
    public virtual List<Repair> Repairs { get; set; } = [];
}
