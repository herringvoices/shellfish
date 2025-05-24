using System.ComponentModel.DataAnnotations;

namespace Shelfish.Api.Models;

public class Library
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;

    public bool IsClassroom { get; set; } // True = classroom/private, False = public

    public int LibrarianId { get; set; }
    public User Librarian { get; set; } = null!;

    [Required, MaxLength(7)]
    public string LibraryCode { get; set; } = null!;

    public ICollection<PatronAccount> PatronAccounts { get; set; } = new List<PatronAccount>();
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Bookshelf> Bookshelves { get; set; } = new List<Bookshelf>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
