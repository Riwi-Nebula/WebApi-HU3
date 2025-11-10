using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi_HU3.Domain.Entities;

namespace WebApi_HU3.Infraestructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired();
        
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        
        builder.Property(u => u.Role)
            .IsRequired()
            .HasMaxLength(20);
        
        //Seed de usuario admin
        builder.HasData(new User
        {
            Id = 1,
            Username = "admin",
            Email = "admin@gmail.com",
            PasswordHash = "admin123", // se debe encriptar
            Role = UserRole.Admin
        });
    }
}