using TechnicoDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace TechnicoDomain.Context;

public class TechnicoDbContext : DbContext
{
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<PropertyItem> ProperyItems { get; set; }
    public DbSet<Owner> Owners { get; set; }

    public TechnicoDbContext(DbContextOptions<TechnicoDbContext> options): base(options)
    {
    }
    public TechnicoDbContext() 
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Set up your
        // string and other configuration here
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TechnicoDB;Trusted_Connection=True;TrustServerCertificate=True;");

        // Enable sensitive data logging (only in development)
        optionsBuilder.EnableSensitiveDataLogging();

        // Additional configurations as needed
    }
}