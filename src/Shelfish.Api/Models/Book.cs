using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;

public class Book
{
    public int Id { get; set; }

    public int LibraryId { get; set; }
    public Library Library { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Title { get; set; } = "Unknown Title";

    [Required, MaxLength(255)]
    public string Author { get; set; } = "Unknown Author";

    public bool IsSeries { get; set; }
    public string? SeriesName { get; set; }
    public int? Volume { get; set; }

    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }

    public string? GoogleBooksId { get; set; }

    public int? BookshelfId { get; set; }
    public Bookshelf? Bookshelf { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<ReadingLog> ReadingLogs { get; set; } = new List<ReadingLog>();
    public ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
}
