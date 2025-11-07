using Microsoft.EntityFrameworkCore;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Infraestructure.Data.Configurations;

namespace WebApi_HU3.Infraestructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<User>  Users { get; set; }
    public DbSet<Student>   Students { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica las configuraciones Fluent API desde la carpeta Configurations
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
    }
    
}