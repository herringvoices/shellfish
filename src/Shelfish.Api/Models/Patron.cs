using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;
public class Patron
{
    public int Id { get; set; }

    public int LibraryId { get; set; }
    public Library Library { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Username { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
