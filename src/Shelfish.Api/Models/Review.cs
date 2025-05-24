using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;

public class Review
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int? PatronAccountId { get; set; }
    public PatronAccount? PatronAccount { get; set; }

    public int LibraryId { get; set; }
    public Library Library { get; set; } = null!;

    public string? Name { get; set; } // For public reviews
    public int Rating { get; set; }
    public string? Body { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
