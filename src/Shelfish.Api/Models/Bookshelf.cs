using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;

public class Bookshelf
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;

    public int LibraryId { get; set; }
    public Library Library { get; set; } = null!;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
