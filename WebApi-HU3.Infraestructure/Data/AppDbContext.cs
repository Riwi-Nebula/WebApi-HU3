using Microsoft.EntityFrameworkCore;
using WebApi_HU3.Domain.Entities;

namespace WebApi_HU3.Infraestructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    //Metodo protegido para definir pruebas adicionales o configuraciones
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Ejemplo:
        // modelBuilder.Entity<Student>()
        //     .HasIndex(s => s.Email)
        //     .IsUnique();W
        //Esto crea un indice unico sobre el campo de del email en la tabla de students
        base.OnModelCreating(modelBuilder);
    }
    
    //Aca agregan las entidades con las que se van a crear las tablas
    //Ejemplo: public DbSet<Course> Courses { get; set; }
}