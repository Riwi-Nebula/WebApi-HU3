using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_HU3.Domain.Entities;

public class User
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "varchar(100)")] public string Username { get; set; } = string.Empty;
    [Column(TypeName = "varchar(250)")] public string Email { get; set; } = string.Empty;
    [Column(TypeName = "varchar(100)")] public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;
}