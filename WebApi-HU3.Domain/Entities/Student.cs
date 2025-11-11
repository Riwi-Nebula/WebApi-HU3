using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_HU3.Domain.Entities;

public class Student
{
    [Key] public int Id { get; set; }
    [Column(TypeName = "varchar(100)")] public string FirstName { get; set; } = string.Empty;
    [Column(TypeName = "varchar(100)")] public string LastName { get; set; } = string.Empty;
    [Column(TypeName = "varchar(250)")] public string Email { get; set; } = string.Empty;
}