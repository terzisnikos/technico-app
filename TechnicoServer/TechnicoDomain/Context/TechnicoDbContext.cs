using TechnicoDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace TechnicoDomain.Context;

public class TechnicoDbContext : DbContext
{
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<PropertyItem> PropertyItems { get; set; }
    public DbSet<Owner> Owners { get; set; }

    public TechnicoDbContext(DbContextOptions<TechnicoDbContext> options): base(options)
    {
    }
    public TechnicoDbContext() 
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TechnicoDB;Trusted_Connection=True;TrustServerCertificate=True;");
        optionsBuilder.EnableSensitiveDataLogging();
    }
}