namespace Shelfish.Api.Models;
public class BookshelfBook
{
    public int BookshelfId { get; set; }
    public Bookshelf Bookshelf { get; set; } = null!;

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
