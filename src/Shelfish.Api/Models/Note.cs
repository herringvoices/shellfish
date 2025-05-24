using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;

public class Note
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public int? UserId { get; set; }
    public User? User { get; set; }

    public int? PatronAccountId { get; set; }
    public PatronAccount? PatronAccount { get; set; }

    [Required]
    public string Body { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
