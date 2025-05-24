namespace Shelfish.Api.Models;

public class ReadingLog
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int PatronAccountId { get; set; }
    public PatronAccount PatronAccount { get; set; } = null!;

    public DateTime ReadAt { get; set; } = DateTime.UtcNow;
}
