using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_HU3.Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "varchar(100)")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "varchar(250)")]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
}