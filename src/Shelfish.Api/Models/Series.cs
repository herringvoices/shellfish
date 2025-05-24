using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;
public class Series
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Title { get; set; } = null!;
}
