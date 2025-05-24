using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;
public class Category
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
}
