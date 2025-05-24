using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;

public class PatronAccount
{
    public int Id { get; set; }

    public int LibraryId { get; set; }
    public Library Library { get; set; } = null!;

    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string UserName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Password { get; set; } = null!;

    public ICollection<ReadingLog> ReadingLogs { get; set; } = new List<ReadingLog>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
}
